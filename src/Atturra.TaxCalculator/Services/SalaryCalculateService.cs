using Atturra.TaxCalculator.Entities;
using Atturra.TaxCalculator.Entities.Enums;
using Atturra.TaxCalculator.Intefaces;
using Atturra.TaxCalculator.Options;
using Microsoft.Extensions.Options;
using System;

namespace Atturra.TaxCalculator.Services
{
    public class SalaryCalculateService : ISalaryCalculateService
    {
        private readonly DeductionOptions _options;

        public SalaryCalculateService(IOptions<DeductionOptions> options)
        {
            _options = options.Value;
        }

        public SalaryDetails CalculateSalaryTaxes(string grossPackage, string payFrequency)
        {
            if (!decimal.TryParse(grossPackage, out var grossPackageValue))
                throw new ArgumentException("A not numeric value was entered.");

            if (!Enum.TryParse<PayFrequency>(payFrequency, out var payFrequencyValue))
                throw new ArgumentException("A not payment frequency value was entered.");

            return new SalaryDetails(grossPackageValue, payFrequencyValue, _options);
        }
    }
}