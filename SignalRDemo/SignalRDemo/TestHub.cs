using Microsoft.AspNet.SignalR;

namespace SignalRDemo
{
  public class TestHub : Hub
  {
    // http://stackoverflow.com/questions/31169509/signalr-how-to-truly-call-a-hubs-method-from-the-server-c-sharp
    private static IHubContext context = GlobalHost.ConnectionManager.GetHubContext<TestHub>();

    public void Send(string name, string message)
    {
      Clients.All.addMessage(name, message);
    }

    public static void Push(string name, string message)
    {
      context.Clients.All.addMessage(name, message);
    }
  }
}
