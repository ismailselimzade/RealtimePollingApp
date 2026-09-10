using System.Net.WebSockets;
using System.Text;

namespace RealtimePollingApp.Server.Services
{
    public class WebSocketHandler
    {
        public async Task HandleAsync(WebSocket webSocket, ConnectionManager connectionManager, IdGenerator idGenerator)
        {
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

                    var responseMessage = Encoding.UTF8.GetBytes($"Client {id} message: {Encoding.UTF8.GetString(buffer, 0, receiveResult.Count)}");

                    var AllWebSockets = connectionManager.GetAllExcept(id);

                    foreach (var ws in AllWebSockets)
                    {
                        try
                        {
                            await ws.SendAsync(responseMessage, WebSocketMessageType.Text, true, CancellationToken.None);
                        }
                        catch (WebSocketException ex)
                        {
                            Console.WriteLine($"Failed to send to a client: {ex.Message}");
                        }
                    }
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
    }
}
