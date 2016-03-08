using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace SignalRDemo
{
  public class GeneratedProxyHub : Hub
  {
    // http://stackoverflow.com/questions/31169509/signalr-how-to-truly-call-a-hubs-method-from-the-server-c-sharp
    private static IHubContext context = GlobalHost.ConnectionManager.GetHubContext<GeneratedProxyHub>();

    public override Task OnConnected()
    {
      var qs = Context.QueryString["user"];

      Send("GeneratedProxyHub", qs);
      
      return base.OnConnected();
    }

    public void Send(string name, string message)
    {
      Clients.All.addMessage(name, "Generated Proxy Hub: " + message);
    }

    public static void Push(string name, string message)
    {
      context.Clients.All.addMessage(name, "Generated Proxy Hub: " + message);
    }
  }
}
