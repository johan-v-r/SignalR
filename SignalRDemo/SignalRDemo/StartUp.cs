using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace SignalRDemo
{

  public class Startup
  {
    public void Configuration(IAppBuilder app)
    {
      app.UseCors(CorsOptions.AllowAll);
      var httpConfiguration = SetupHttpConfiguration();

      app.UseWebApi(httpConfiguration);

      app.Map("/signalr", map =>
      {
        // Setup the cors middleware to run before SignalR.
        // By default this will allow all origins. You can 
        // configure the set of origins and/or http verbs by
        // providing a cors options with a different policy.
        map.UseCors(CorsOptions.AllowAll);

        var hubConfiguration = new HubConfiguration
        {
          // You can enable JSONP by uncommenting line below.
          // JSONP requests are insecure but some older browsers (and some
          // versions of IE) require JSONP to work cross domain
          // EnableJSONP = true
        };

        // Run the SignalR pipeline. We're not using MapSignalR
        // since this branch is already runs under the "/signalr"
        // path.
        map.RunSignalR(hubConfiguration);
      });
    }
    
    internal HttpConfiguration SetupHttpConfiguration()
    {
      var httpConfiguration = new HttpConfiguration();
      
      // Configure Web API Routes:
      // - Enable Attribute Mapping
      // - Enable Default routes at /api.
      httpConfiguration.MapHttpAttributeRoutes();
      httpConfiguration.Routes.MapHttpRoute(
          name: "DefaultApi",
          routeTemplate: "api/{controller}/{id}",
          defaults: new { id = RouteParameter.Optional }
          );

      // Only allow json output format -- prevent XML serialization errors
      // http://codeglee.com/2014/07/20/serializationexception-when-using-entityframework-pocos-in-webapi/

      httpConfiguration.Formatters.Clear();
      httpConfiguration.Formatters.Add(new JsonMediaTypeFormatter());
      httpConfiguration.Formatters.JsonFormatter.SerializerSettings =
          new JsonSerializerSettings
          {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            DateFormatString = "yyyy/MM/dd"
          };

      return httpConfiguration;
    }
  }
}
