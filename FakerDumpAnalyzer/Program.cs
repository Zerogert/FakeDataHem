using FakerDumpAnalyzer.Infrastructure;
using FakerDumpAnalyzer.Services;
using FakerDumpAnalyzer.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FakerDumpAnalyzer
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            // создаем хост приложения
            var host = Host.CreateDefaultBuilder()
                // внедряем сервисы
                .ConfigureServices(services =>
                {
                    services.AddSingleton<App>();
                    services.AddSingleton<MainWindow>();
                    services.AddSingleton<MainViewModel>();
                    services.AddSingleton<ContentViewModel>();
                    services.AddSingleton<ExporterDesmosViewModel>();
                    services.AddSingleton<JsonExporterDesmosViewModel>();
                    services.AddSingleton<DumpProvider>();

                    services.AddSingleton<HemClient>();
                    services.AddSingleton<AppSettings>();
                })
                .Build();
            // получаем сервис - объект класса App
            var app = host.Services.GetService<App>();
            // запускаем приложения
            app?.Run();
        }
    }
}
