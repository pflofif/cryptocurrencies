using System;
using System.IO;
using System.Windows;
using Cryptocurrencies.MVVM.ViewModel;
using Cryptocurrencies.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cryptocurrencies
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider ServiceProvider { get; set; }
        private IConfiguration Configuration { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var root = Directory.GetCurrentDirectory();
            var builder = new ConfigurationBuilder()
                .SetBasePath(root)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            Current.DispatcherUnhandledException += App_DispatcherUnhandledException;

            ServiceProvider = serviceCollection.BuildServiceProvider();
            var mainViewModel = ServiceProvider.GetRequiredService<MainViewModel>();
            var mainWindow = new MainWindow(mainViewModel);
            mainWindow.Show();
        }

        private void App_DispatcherUnhandledException(object sender,
            System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
            => e.Handled = true;


        private void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddScoped<ICryptoApiService, CryptoApiService>();
            services.AddScoped<HomeViewModel>();
            services.AddScoped<InfoViewModel>();
            services.AddTransient<MainViewModel>();
        }
    }
}