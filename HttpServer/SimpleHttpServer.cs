// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using System.Net;
using System.Text;

namespace HttpServer;

public class SimpleHttpServer : IDisposable
{
    private readonly HttpListener _listener = new();

    public SimpleHttpServer()
    {
        ListenAsync();
    }

    public void Dispose()
    {
        _listener.Close();
    }

    private async Task ListenAsync()
    {
        _listener.Prefixes.Add("http://localhost:51111/MyApp/"); // Listen on
        _listener.Start(); // port 51111
// Await a client request:
        var context = await _listener.GetContextAsync().ConfigureAwait(false);
// Respond to the request:
        var msg = "You asked for: " + context.Request.RawUrl;
        context.Response.ContentLength64 = Encoding.UTF8.GetByteCount(msg);
        context.Response.StatusCode = (int) HttpStatusCode.OK;
        using var s = context.Response.OutputStream;
        using var writer = new StreamWriter(s);
        await writer.WriteAsync(msg).ConfigureAwait(false);
    }
}
