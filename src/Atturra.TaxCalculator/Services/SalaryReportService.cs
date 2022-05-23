using Atturra.TaxCalculator.Entities;
using Atturra.TaxCalculator.Intefaces;
using System;

namespace Atturra.TaxCalculator.Services
{
    public class SalaryReportService : ISalaryReportService
    {
        public void SalaryReport(SalaryDetails salary)
        {
            Console.WriteLine(SalaryReportString(salary));
        }

        public string SalaryReportString(SalaryDetails salary)
        {
            return $@"
                    Calculating salary details...

                    Gross package: {salary.GrossPackage}
                    Superannuation: {salary.Superannuation}

                    Taxable income: {salary.TaxableIncome}

                    Deductions:
                    Medicare Levy: {salary.Deduction.MedicareLevy.Value}
                    Budget Repair Levy: {salary.Deduction.BudgetRepairLevy.Value}
                    Income Tax: {salary.Deduction.IncomeTax.Value}

                    Net income: {salary.NetIncome}
                    Pay packet: {salary.SalaryPackage} per {salary.PayFrequency}";
        }
    }
}