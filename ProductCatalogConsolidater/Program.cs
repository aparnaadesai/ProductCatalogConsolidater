using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductCatalogConsolidater.Domain;
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
                GetRequiredService<ProductCatalogConsolidater>().
                Run();

            DisposeServices();
        }

        private static void RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddScoped<ProductCatalogConsolidater>();
            services.AddScoped<IInputService, CSVInputService>();
            services.AddScoped<IOutputService, CSVOutputService>();
            services.AddScoped<IProductCatalogMergerService, ProductCatlalogMergerService>();
            services.AddScoped<IProductCatalogMerger, ProductCatalogMerger>();

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
