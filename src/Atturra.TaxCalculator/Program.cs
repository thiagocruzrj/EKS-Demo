using Atturra.TaxCalculator.Extensions;
using Atturra.TaxCalculator.Intefaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;
using System.Reflection;

namespace Atturra.TaxCalculator
{
    public class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = ConfigureServices();
            var salaryCalculateService = serviceProvider.GetService<ISalaryCalculateService>();
            var reportService = serviceProvider.GetService<ISalaryReportService>();

            Console.Write("Enter your salary package amount: ");
            var grossPackage = Console.ReadLine();

            Console.Write("Enter your pay frequency (W for weekly, F for fortnightly, M for monthly): ");
            var payFrequency = Console.ReadLine();

            try
            {
                var salary = salaryCalculateService.CalculateSalaryTaxes(grossPackage, payFrequency);
                reportService.SalaryReport(salary);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            var configuration = BuildConfiguration();
            services.AddApplicationServices(configuration);
            return services.BuildServiceProvider();
        }

        private static IConfiguration BuildConfiguration()
        {
            var embeddedProvider = new EmbeddedFileProvider(Assembly.GetExecutingAssembly());
            var configBuilder = new ConfigurationBuilder();
            configBuilder
                .AddJsonFile(embeddedProvider, "appsettings.json", optional: false, reloadOnChange: false);

            return configBuilder.Build();
        }
    }
}