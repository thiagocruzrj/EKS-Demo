using Atturra.TaxCalculator.Entities.Enums;

namespace Atturra.TaxCalculator.Entities
{
    public class SalaryDetails
    {
        public decimal GrossPackage { get; set; }
        public decimal Superannuation { get; set; }
        public PayFrequency PayFrequency { get; set; }

        public decimal Deduction { get; set; }

        public decimal TaxableIncome => GrossPackage - Superannuation;
        public decimal NetIncome => TaxableIncome - Deduction;
    }
}