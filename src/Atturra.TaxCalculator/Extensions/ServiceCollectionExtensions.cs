using Atturra.TaxCalculator.Intefaces;
using Atturra.TaxCalculator.Options;
using Atturra.TaxCalculator.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Atturra.TaxCalculator.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddSalaryCalculateServices(configuration)
                .AddReportServices();

        private static IServiceCollection AddSalaryCalculateServices(this IServiceCollection services, IConfiguration configuration) =>
            services.Configure<DeductionOptions>(options =>
                    configuration.GetSection(nameof(DeductionOptions)).Bind(options))
            .AddScoped<ISalaryCalculateService, SalaryCalculateService>();

        private static IServiceCollection AddReportServices(this IServiceCollection services) =>
            services.AddScoped<ISalaryReportService, SalaryReportService>();
    }
}