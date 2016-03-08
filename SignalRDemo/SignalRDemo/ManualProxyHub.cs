using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace SignalRDemo
{
  public class ManualProxyHub : Hub
  {
    public override Task OnConnected()
    {
      var qs = Context.QueryString["manualProxyUser"];

      ManualProxySend("ManualProxyHub", qs);

      return base.OnConnected();
    }

    public void ManualProxySend(string name, string message)
    {
      Clients.All.addManualMessage(name, "Manual Proxy Hub: " +  message);
    }

    public void ManualProxyPush(string name, string message)
    {
      Clients.All.addManualMessage(name, "Manual Proxy Hub: " + message);
    }
  }
}
