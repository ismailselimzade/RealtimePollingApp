namespace RealtimePollingApp.Server.Services
{
    public class IdGenerator
    {
        private long _counter;
        public string GenerateId()
        {
            return Interlocked.Increment(ref _counter).ToString();
        }
    }
}
