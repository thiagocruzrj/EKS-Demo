using Atturra.TaxCalculator.Options.Deductions;

namespace Atturra.TaxCalculator.Entities
{
    public class Deduction
    {
        public MedicareLevyDeduction MedicareLevy { get; set; }
        public BudgetRepairLevyDeduction BudgetRepairLevy { get; set; }
        public IncomeTaxDeduction IncomeTax { get; set; }

        public Deduction(MedicareLevyDeduction medicareLevy, BudgetRepairLevyDeduction budgetRepairLevy, IncomeTaxDeduction incomeTax)
        {
            MedicareLevy = medicareLevy;
            BudgetRepairLevy = budgetRepairLevy;
            IncomeTax = incomeTax;
        }

        public int TotalDeduction => MedicareLevy.Value + BudgetRepairLevy.Value + IncomeTax.Value;
    }
}