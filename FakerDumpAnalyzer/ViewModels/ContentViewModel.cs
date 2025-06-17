using FakerDumpAnalyzer.Helpers;
using FakerDumpAnalyzer.Services;
using System.Text.Json;

namespace FakerDumpAnalyzer.ViewModels
{
    public class ContentViewModel : BaseViewModel
    {
        private readonly DumpProvider _dumpProvider;
        private string content;

        public string Content
        {
            get => content;
            set
            {
                content = value;
                OnPropertyChanged(nameof(Content));
            }
        }

        public ContentViewModel(DumpProvider dumpProvider)
        {
            _dumpProvider = dumpProvider;
            _dumpProvider.OnUpdated += _dumpProvider_OnUpdated;
        }

        private void _dumpProvider_OnUpdated(object? sender, EventArgs e)
        {
            Content = JsonSerializer.Serialize(_dumpProvider.Dump, DefaultSerializerOptions.GetOptions());
        }
    }
}
