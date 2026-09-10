using RealtimePollingApp.Server.Services;
using System.Net.WebSockets;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ConnectionManager>();
builder.Services.AddSingleton<IdGenerator>();
var app = builder.Build();

app.UseWebSockets();


app.Map("/ws", async (HttpContext context, ConnectionManager connectionManager, IdGenerator idGenerator) =>
{
    if (context.WebSockets.IsWebSocketRequest)
    {
        var webSocket = await context.WebSockets.AcceptWebSocketAsync();
        string id = idGenerator.GenerateId();

        connectionManager.Add(id, webSocket);
        Console.WriteLine($"Client connected: {id}");
        byte[] buffer = new byte[1024];

        try
        {
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
        catch (WebSocketException ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            connectionManager.Remove(id);
            Console.WriteLine($"Client disconnected: {id}");

        }
    }
    else
    {
        context.Response.StatusCode = 400;
    }
});

app.Run();
