using Atturra.TaxCalculator.Configuration;
using Atturra.TaxCalculator.Intefaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Atturra.TaxCalculator
{
    public class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = DependencyConfig.ConfigureServices();
            var salaryCalculateService = serviceProvider.GetService<ISalaryCalculateService>();
            var reportService = serviceProvider.GetService<ISalaryReportService>();

            Console.Write("Enter your salary package amount: ");
            var grossPackage = Console.ReadLine();

            Console.Write("Enter your pay frequency (W for weekly, F for fortnightly, M for monthly): ");
            var payFrequency = Console.ReadLine().ToLower();

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
    }
}