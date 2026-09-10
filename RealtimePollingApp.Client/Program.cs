using System.Net.WebSockets;
using System.Text;

var client = new ClientWebSocket();

await client.ConnectAsync(new Uri("wss://localhost:7062/ws"), CancellationToken.None);

static async Task ReceiveLoopAsync(ClientWebSocket client)
{
    byte[] buffer = new byte[1024];

    while (client.State == WebSocketState.Open)
    {
        var receiveResult = await client.ReceiveAsync(buffer, CancellationToken.None);
        Console.WriteLine(Encoding.UTF8.GetString(buffer, 0, receiveResult.Count));
    }
}

static async Task SendLoopAsync(ClientWebSocket client)
{
    while (client.State == WebSocketState.Open)
    {
        string? message = Console.ReadLine();

        if (string.IsNullOrEmpty(message))
            continue;

        await client.SendAsync(Encoding.UTF8.GetBytes(message), WebSocketMessageType.Text, true, CancellationToken.None);
    }
}

await Task.WhenAll(ReceiveLoopAsync(client), SendLoopAsync(client));