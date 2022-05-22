using System;
using System.Collections.Generic;
using System.Linq;

namespace Atturra.TaxCalculator.Options.Deductions
{
    public class DeductionBase
    {
        public virtual int Value { get; set; }
        public virtual List<ExcessOption> ExcessOptions { get; set; }

        public DeductionBase(List<ExcessOption> excessOptions, decimal taxableIncome)
        {
            ExcessOptions = excessOptions;
            Value = CalculateExcess(taxableIncome);
        }

        internal virtual int CalculateExcess(decimal taxableIncome)
        {
            var total = 0M;
            var list = ExcessOptions.OrderBy(e => e.ExcessThreshold).ToList();

            for (int i = 0; i < list.Count; i++)
            {
                var current = list[i];

                if (i + 1 < list.Count && taxableIncome > list[i + 1].ExcessThreshold)
                {
                    total += Math.Floor((decimal)(list[i + 1].ExcessThreshold - current.ExcessThreshold)) * current.ExcessRate;
                } else
                {
                    total += Math.Floor((taxableIncome - current.ExcessThreshold)) * current.ExcessRate;
                    break;
                }
            }

            return total > 0 ? (int)Math.Ceiling(total) : 0;
        }
    }
}