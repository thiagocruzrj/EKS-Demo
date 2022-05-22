using System.Collections.Generic;

namespace Atturra.TaxCalculator.Options.Deductions
{
    public class BudgetRepairLevyDeduction : DeductionBase
    {
        public BudgetRepairLevyDeduction(List<ExcessOption> options, decimal taxableIncome) : base(options, taxableIncome) { }
    }
}