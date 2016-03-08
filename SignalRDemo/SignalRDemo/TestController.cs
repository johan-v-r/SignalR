using System.Web.Http;

namespace SignalRDemo
{
  public class TestController : ApiController
  {
    public string Get()
    {
      GeneratedProxyHub.Push("WebApi", "Hello World!");
      return "Result sent to PayGate and Web";
    }
  }
}
