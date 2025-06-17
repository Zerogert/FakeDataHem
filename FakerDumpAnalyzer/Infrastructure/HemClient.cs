using FakerDumpAnalyzer.Helpers;
using FakerDumpAnalyzer.Models;
using FakerDumpAnalyzer.Services;
using System.Net.Http;
using System.Text.Json;

namespace FakerDumpAnalyzer.Infrastructure
{
    public class HemClient
    {
        private readonly AppSettings _settingsProvider;
        private readonly HttpClient _httpClient;

        public HemClient(AppSettings settingsProvider)
        {
            _httpClient = new HttpClient();
            _settingsProvider = settingsProvider;
        }

        public async Task<DumpModel?> GetDumpModelAsync()
        {
            const string query = @"/debug/dump";
            var url = new Uri($"{_settingsProvider.Host}{query}");
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var response = await _httpClient.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<DumpModel>(result, DefaultSerializerOptions.GetOptions());
        }
    }
}
