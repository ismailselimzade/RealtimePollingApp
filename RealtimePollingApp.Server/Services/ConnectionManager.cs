using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace RealtimePollingApp.Server.Services
{
    public class ConnectionManager
    {
        private readonly ConcurrentDictionary<string, WebSocket> _connections = new();

        public bool Add(string id, WebSocket webSocket)
        {
            return _connections.TryAdd(id, webSocket);
        }
        public bool Remove(string id)
        {
            return _connections.TryRemove(id, out _);
        }
        public IEnumerable<WebSocket> GetAll()
        {
            return _connections.Values;
        }
    }
}
