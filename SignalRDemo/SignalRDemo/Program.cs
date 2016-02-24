using Microsoft.Owin.Hosting;
using System;

namespace SignalRDemo
{
  class Program
  {
    static void Main(string[] args)
    {
      var options = WebAppOptions();

      Console.WriteLine("RTTSWebAPI starting");
      // Start OWIN host 
      using (WebApp.Start<Startup>(options))
      {
        Console.WriteLine("Web Server and API started");
        Console.WriteLine("Press [Enter] to exit ...");
        Console.ReadLine();
      }
    }

    private static StartOptions WebAppOptions()
    {
      var options = new StartOptions();
      options.Urls.Add("http://*:9000");
      return options;
    }
  }
}
