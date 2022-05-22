using Atturra.TaxCalculator.Options;
using Atturra.TaxCalculator.Options.Deductions;

namespace Atturra.TaxCalculator.Entities
{
    public class Deduction
    {
        public MedicareLevyDeduction MedicareLevy { get; set; }
        public BudgetRepairLevyDeduction BudgetRepairLevy { get; set; }
        public IncomeTaxDeduction IncomeTax { get; set; }

        public Deduction(decimal incomeTax, DeductionOptions options)
        {
            MedicareLevy = new MedicareLevyDeduction(options.MedicareExcess, incomeTax);
            BudgetRepairLevy = new BudgetRepairLevyDeduction(options.BudgetRepairExcess, incomeTax);
            IncomeTax = new IncomeTaxDeduction(options.IncomeTaxExcess, incomeTax);
        }

        public int TotalDeduction => MedicareLevy.Value + BudgetRepairLevy.Value + IncomeTax.Value;
    }
}