using FakerDumpAnalyzer.Services;
using FakerHEM.Helpers;
using System.Windows.Input;

namespace FakerDumpAnalyzer.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly DumpProvider _dumperProvider;
        private readonly AppSettings _settings;
        private string host = "http://127.0.0.1:5000";

        public string Host
        {
            get => _settings.Host;
            set
            {
                _settings.Host = host;
                host = value;
            }
        }
        public ICommand RefreshData { get; private set; }
        public ContentViewModel ContentViewModel { get; private set; }
        public ExporterDesmosViewModel ExporterDesmosViewModel { get; private set; }
        
        public MainViewModel(
            AppSettings settingsProvider,
            DumpProvider dumpProvider,
            ContentViewModel contentViewModel,
            ExporterDesmosViewModel exporterDesmosViewModel)
        {
            _dumperProvider = dumpProvider;
            _settings = settingsProvider;
            RefreshData = new RelayCommand(RefreshDump);
            ContentViewModel = contentViewModel;
            ExporterDesmosViewModel = exporterDesmosViewModel;
        }

        private void RefreshDump()
        {
            _ = _dumperProvider.RefreshAsync();
        }
    }
}
