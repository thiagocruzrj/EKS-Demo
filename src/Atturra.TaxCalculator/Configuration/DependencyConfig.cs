using Atturra.TaxCalculator.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Atturra.TaxCalculator.Configuration
{
    [ExcludeFromCodeCoverage]
    public static class DependencyConfig
    {
        internal static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            var configuration = BuildConfiguration();
            services.AddApplicationServices(configuration);
            return services.BuildServiceProvider();
        }

        internal static IConfiguration BuildConfiguration()
        {
            var embeddedProvider = new EmbeddedFileProvider(Assembly.GetExecutingAssembly());
            var configBuilder = new ConfigurationBuilder();
            configBuilder
                .AddJsonFile(embeddedProvider, "appsettings.json", optional: false, reloadOnChange: false);

            return configBuilder.Build();
        }
    }
}
