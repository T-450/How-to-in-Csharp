using HttpServer;

// Fires an HttpServer
var uri = new Uri("http://localhost:51111/MyApp/Request.txt");
using var httpServer = new SimpleHttpServer();
// Make a client request:
var res = await new HttpClient().GetStringAsync(uri).ConfigureAwait(true);
Console.WriteLine(res);

// Fires an WebPageServer
// Listen on port 51111, serving files in d:\webroot:
var webServer = new WebServer("http://localhost:51111/", @"d:\webroot");
try
{
    webServer.Start();
    Console.WriteLine("Server running... press Enter to stop");
    Console.ReadLine();
}
finally
{
    webServer.Stop();
}
