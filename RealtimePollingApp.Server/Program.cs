using System.Net.WebSockets;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseWebSockets();

app.MapGet("/", () => "Hello World!");

app.Map("/ws", async  (HttpContext context) =>
{
    if (context.WebSockets.IsWebSocketRequest)
    {
        var webSocket = await context.WebSockets.AcceptWebSocketAsync();

        byte[] buffer = new byte[1024];

        while (webSocket.State == WebSocketState.Open)
        {
            var receiveResult = await webSocket.ReceiveAsync(buffer, CancellationToken.None);

            if (receiveResult.MessageType == WebSocketMessageType.Close)
            {
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Connection closed", CancellationToken.None);
                break;
            }

            var responseMessage = $"Server ?: {Encoding.UTF8.GetString(buffer, 0, receiveResult.Count)}";
            
            await webSocket.SendAsync(Encoding.UTF8.GetBytes(responseMessage), WebSocketMessageType.Text, true, CancellationToken.None);
        }
    }
    else
    {
        context.Response.StatusCode = 400;
    }
});

app.Run();
