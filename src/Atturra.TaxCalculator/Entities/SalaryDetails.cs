using Atturra.TaxCalculator.Entities.Enums;
using Atturra.TaxCalculator.Options;
using System;
using System.ComponentModel.DataAnnotations;

namespace Atturra.TaxCalculator.Entities
{
    public class SalaryDetails
    {
        [Range(1.0, double.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public decimal GrossPackage { get; set; }
        public decimal Superannuation { get; set; }
        public PayFrequency PayFrequency { get; set; }
        public Deduction Deduction { get; set; }

        public decimal TaxableIncome => GrossPackage - Superannuation;

        public SalaryDetails(decimal grossPackage, PayFrequency payFrequency, DeductionOptions deductionOptions)
        {
            GrossPackage = grossPackage;
            PayFrequency = payFrequency;
            Superannuation = Math.Round(GrossPackage * deductionOptions.SuperRate / (1 + deductionOptions.SuperRate), 2);
            Deduction = new Deduction(TaxableIncome, deductionOptions);
        }

        public decimal NetIncome => TaxableIncome - Deduction.TotalDeduction;

        public decimal SalaryPackage =>
            PayFrequency.Equals(PayFrequency.W)
            ? Math.Round(NetIncome / 52, 2)
            : PayFrequency.Equals(PayFrequency.F)
            ? Math.Round(NetIncome / 26, 2)
            : Math.Round(NetIncome / 12, 2);
    }
}