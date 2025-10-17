using FakerDumpAnalyzer.Infrastructure;
using FakerDumpAnalyzer.Models;

namespace FakerDumpAnalyzer.Services
{
    public class DumpProvider
    {
        private readonly HemClient _client;
        public DumpModel? Dump { get; private set; }

        public event EventHandler OnUpdated = delegate { };

        public DumpProvider(HemClient hemClient)
        {
            _client = hemClient;
        }

        public async Task RefreshAsync()
        {
            var dump = await _client.GetDumpModelAsync();
            Dump = dump;
            OnUpdated.Invoke(this, EventArgs.Empty);
        }
    }
}
