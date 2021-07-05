using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace ProductCatalogConsolidater
{
    class Program
    {
        private static ServiceProvider _serviceProvider;
        private static IConfigurationRoot _configuration;

        static void Main(string[] args)
        {
            RegisterServices();
            IServiceScope scope = _serviceProvider.CreateScope();
            scope.
                ServiceProvider.
                GetRequiredService<ConsoleApplication>().
                Run();

            DisposeServices();
        }

        private static void RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddScoped<ConsoleApplication>();
            services.AddScoped<IInputService, CSVReader>();
            services.AddScoped<IOutputService, CSVWriter>();
            services.AddScoped<IProductCatalogMerger, ProductCatlalogMerger>();

            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

            services.AddSingleton<IConfigurationRoot>(_configuration);

            _serviceProvider = services.BuildServiceProvider(true);
        }


        private static void DisposeServices()
        {
            if (_serviceProvider == null)
            {
                return;
            }
            if (_serviceProvider is IDisposable)
            {
                ((IDisposable)_serviceProvider).Dispose();
            }
        }
    }
}
