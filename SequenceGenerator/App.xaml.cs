using Microsoft.Extensions.DependencyInjection;
using SequenceGenerator.Services;
using SequenceGenerator.Services.Interfaces;
using SequenceGenerator.ViewModels;
using SequenceGenerator.Views;
using System.Windows;

namespace SequenceGenerator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;
        public App()
        {
            ServiceCollection services = new();
            ConfigureServices(services);
   
            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _serviceProvider.GetRequiredService<HomeWindow>().Show();
        }

        private static void ConfigureServices(ServiceCollection services)
        {

            services.AddSingleton<GenerateSequenceViewModel>();
            services.AddSingleton<HomeWindow>();

            services.AddSingleton<IGenerateRandomString, GenerateRandomString>();
            services.AddSingleton<ISequenceFinder, SequenceFinder>();
        }
    }
}
