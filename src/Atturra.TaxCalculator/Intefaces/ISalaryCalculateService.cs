using Atturra.TaxCalculator.Entities;

namespace Atturra.TaxCalculator.Intefaces
{
    public interface ISalaryCalculateService
    {
        public SalaryDetails CalculateSalaryTaxes(string grossPackage, string payFrequency);
    }
}