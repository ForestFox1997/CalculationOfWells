using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using CalculationOfWells.Services;
using CalculationOfWells.Services.Interfaces;
using CalculationOfWells.ViewModels;
using CalculationOfWells.Views;

namespace CalculationOfWells
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _provider;

        public App()
        {
            var services = new ServiceCollection();
            services.AddSingleton<ICsvReader, CsvReader>();
            services.AddSingleton<IWellImportService, WellImportService>();
            services.AddSingleton<IFileDialogService, FileDialogService>();
            services.AddSingleton<IValidationService, ValidationService>();
            services.AddSingleton<IAggregationService, AggregationService>();
            services.AddSingleton<MainViewModel>();
            _provider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var viewModel = _provider.GetRequiredService<MainViewModel>();
            var mainWindow = new MainWindow { DataContext = viewModel };
            mainWindow.Show();
            base.OnStartup(e);
        }
    }
}
