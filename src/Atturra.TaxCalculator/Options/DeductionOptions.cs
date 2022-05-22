using System.Collections.Generic;

namespace Atturra.TaxCalculator.Options
{
    public class DeductionOptions
    {
        public List<ExcessOption> MedicareExcess { get; set; }
        public List<ExcessOption> BudgetRepairExcess { get; set; }
        public List<ExcessOption> IncomeTaxExcess { get; set; }
        public decimal SuperRate { get; set; }
    }
}