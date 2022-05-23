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
                throw new ArgumentException("The gross package value is not numeric.");

            if (grossPackageValue <= 0)
                throw new ArgumentException("The gross package value must to be greater than 1.");

            if (!Enum.TryParse<PayFrequency>(payFrequency, out var payFrequencyValue))
                throw new ArgumentException("The pay frequency value is invalid.");

            return new SalaryDetails(grossPackageValue, payFrequencyValue, _options);
        }
    }
}