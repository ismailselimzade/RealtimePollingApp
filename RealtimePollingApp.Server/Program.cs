using RealtimePollingApp.Server.Services;
using System.Net.WebSockets;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ConnectionManager>();
builder.Services.AddSingleton<IdGenerator>();
builder.Services.AddSingleton<WebSocketHandler>();
var app = builder.Build();

app.UseWebSockets();


app.Map("/ws", async (HttpContext context, WebSocketHandler webSocketHandler, ConnectionManager connectionManager, IdGenerator idGenerator) =>
{
    if (context.WebSockets.IsWebSocketRequest)
    {
        var webSocket = await context.WebSockets.AcceptWebSocketAsync();
        await webSocketHandler.HandleAsync(webSocket, connectionManager, idGenerator);
    }
    else
    {
        context.Response.StatusCode = 400;
    }
});

app.Run();
