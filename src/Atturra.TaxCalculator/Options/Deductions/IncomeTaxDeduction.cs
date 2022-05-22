using System.Collections.Generic;

namespace Atturra.TaxCalculator.Options.Deductions
{
    public class IncomeTaxDeduction : DeductionBase
    {
        public IncomeTaxDeduction(List<ExcessOption> options, decimal taxableIncome) : base(options, taxableIncome) {}
    }
}