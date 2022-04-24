// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using System.Net;
using System.Text;

namespace HttpServer;

public class WebServer
{
    private readonly string _baseFolder; // Your web page folder.
    private readonly HttpListener _listener;

    public WebServer(string uriPrefix, string baseFolder)
    {
        _listener = new HttpListener();
        _listener.Prefixes.Add(uriPrefix);
        _baseFolder = baseFolder;
    }

    public async void Start()
    {
        _listener.Start();
        while (true)
        {
            try
            {
                var context = await _listener.GetContextAsync().ConfigureAwait(false);
                await Task.Run(() => ProcessRequestAsync(context)).ConfigureAwait(false);
            }
            catch (HttpListenerException)
            {
                break;
            } // Listener stopped.
            catch (InvalidOperationException)
            {
                break;
            } // Listener stopped.
        }
    }

    public void Stop()
    {
        _listener.Stop();
    }

    private async void ProcessRequestAsync(HttpListenerContext context)
    {
        try
        {
            var filename = Path.GetFileName(context.Request.RawUrl);
            if (filename == null)
            {
                return;
            }

            var path = Path.Combine(_baseFolder, filename);
            byte[] msg;
            if (!File.Exists(path))
            {
                Console.WriteLine("Resource not found: " + path);
                context.Response.StatusCode = (int) HttpStatusCode.NotFound;
                msg = Encoding.UTF8.GetBytes("Sorry, that page does not exist");
            }
            else
            {
                context.Response.StatusCode = (int) HttpStatusCode.OK;
                msg = await File.ReadAllBytesAsync(path).ConfigureAwait(false);
            }

            context.Response.ContentLength64 = msg.Length;
            using var s = context.Response.OutputStream;
            await s.WriteAsync(msg).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Request error: " + ex);
        }
    }
}
