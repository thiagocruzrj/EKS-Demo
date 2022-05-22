using System;
using System.Collections.Generic;
using System.Linq;

namespace Atturra.TaxCalculator.Options.Deductions
{
    public class MedicareLevyDeduction : DeductionBase
    {
        public MedicareLevyDeduction(List<ExcessOption> options, decimal taxableIncome) : base(options, taxableIncome) { }

        internal override int CalculateExcess(decimal taxableIncome)
        {
            var topExcess = ExcessOptions.OrderByDescending(e => e.ExcessThreshold).FirstOrDefault();
            return taxableIncome > topExcess.ExcessThreshold
                ? (int)Math.Ceiling(taxableIncome * topExcess.ExcessRate)
                : base.CalculateExcess(taxableIncome);
        }
    }
}