using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Cryptocurrencies.Core;
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
            try
            {
                var root = Directory.GetCurrentDirectory();
                var builder = new ConfigurationBuilder()
                    .SetBasePath(root)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

                Configuration = builder.Build();

                var serviceCollection = new ServiceCollection();
                ConfigureServices(serviceCollection);
                App.Current.DispatcherUnhandledException += App_DispatcherUnhandledException;
                ServiceProvider = serviceCollection.BuildServiceProvider();
                var mainViewModel = ServiceProvider.GetRequiredService<MainViewModel>();
                var mainWindow = new MainWindow(mainViewModel);
                mainWindow.Show();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred during startup: {ex.Message}");
                Environment.Exit(1); // Exit the application with an error code
            }
        }
        
        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            // Handle the unhandled exception here
            Console.WriteLine($"An unhandled exception occurred: {e.Exception.Message}");
            e.Handled = true; // Mark the exception as handled to prevent the application from crashing
        }

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