using Atturra.TaxCalculator.Entities.Enums;
using Atturra.TaxCalculator.Options;
using Atturra.TaxCalculator.Services;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using Xunit;

namespace Atturra.TaxCalculatorTests.UseCase
{
    public class SalaryCalculateServiceTests
    {
        private readonly SalaryCalculateService _sut;
        private readonly SalaryReportService _report;
        private readonly IOptions<DeductionOptions> _options;

        public SalaryCalculateServiceTests()
        {
            _options = Options.Create(new DeductionOptions
            {
                SuperRate = 0.095M,
                MedicareExcess = new List<ExcessOption>
                {
                    new ExcessOption
                    {
                        ExcessThreshold = 21335,
                        ExcessRate = 0.1M
                    },
                    new ExcessOption
                    {
                        ExcessThreshold = 26668,
                        ExcessRate = 0.02M
                    }
                },
                BudgetRepairExcess = new List<ExcessOption>
                {
                    new ExcessOption
                    {
                        ExcessThreshold = 180000,
                        ExcessRate = 0.02M
                    }
                },
                IncomeTaxExcess = new List<ExcessOption>
                {
                    new ExcessOption
                    {
                        ExcessThreshold = 18200,
                        ExcessRate = 0.19M
                    },
                    new ExcessOption
                    {
                        ExcessThreshold = 37000,
                        ExcessRate = 0.325M
                    },
                    new ExcessOption
                    {
                        ExcessThreshold = 87000,
                        ExcessRate = 0.37M
                    },
                    new ExcessOption
                    {
                        ExcessThreshold = 180000,
                        ExcessRate = 0.47M
                    }
                }
            });
            _sut = new SalaryCalculateService(_options);
            _report = new SalaryReportService();
        }

        [Theory]
        [InlineData("", "M")]
        [InlineData("NotDecimal", "M")]
        public void SalaryReport_ThrowsError_WhenNotNumericInformed(string grossPackage, string payFrequency)
        {
            // Act
            void act() => _sut.CalculateSalaryTaxes(grossPackage, payFrequency);

            // Assert
            var ex = Assert.Throws<ArgumentException>(act);
            Assert.Equal("The gross package value is not numeric.", ex.Message);
        }

        [Theory]
        [InlineData("-0.1", "M")]
        [InlineData("-180000", "M")]
        [InlineData("0", "M")]
        public void SalaryReport_ThrowsError_WhenGrossIsLowerOrEqualZero(string grossPackage, string payFrequency)
        {
            // Act
            void act() => _sut.CalculateSalaryTaxes(grossPackage, payFrequency);

            // Assert
            var ex = Assert.Throws<ArgumentException>(act);
            Assert.Equal("The gross package value must to be greater than 1.", ex.Message);
        }

        [Theory]
        [InlineData("1", "")]
        [InlineData("1", "A")]
        public void SalaryReport_ThrowsError_WhenPayFrequencyIsInvalid(string grossPackage, string payFrequency)
        {
            // Act
            void act() => _sut.CalculateSalaryTaxes(grossPackage, payFrequency);

            // Assert
            var ex = Assert.Throws<ArgumentException>(act);
            Assert.Equal("The pay frequency value is invalid.", ex.Message);
        }

        [Theory]
        [InlineData("27375", "M", 367)]
        [InlineData("43800", "M", 800)]
        public void SalaryReport_MedicareLevy_ResultShouldBeCorrect(string grossPackage, string payFrequency, int expectedAmount)
        {
            // Act
            var result = _sut.CalculateSalaryTaxes(grossPackage, payFrequency);

            // Assert
            Assert.Equal(expectedAmount, result.Deduction.MedicareLevy.Value);
        }

        [Theory]
        [InlineData("219000", "M", 400)]
        public void SalaryReport_BudgetRepairLevy_ResultShouldBeCorrect(string grossPackage, string payFrequency, int expectedAmount)
        {
            // Act
            var result = _sut.CalculateSalaryTaxes(grossPackage, payFrequency);

            // Assert
            Assert.Equal(expectedAmount, result.Deduction.BudgetRepairLevy.Value);
        }

        [Theory]
        [InlineData("27375", "M", 1292)]
        [InlineData("49275", "M", 6172)]
        [InlineData("104025", "M", 22782)]
        [InlineData("197100", "M", 54232)]
        [InlineData("219000", "M", 63632)]
        public void SalaryReport_IncomeTax_ShouldBeCorrect(string grossPackage, string payFrequency, int expectedAmount)
        {
            // Act
            var result = _sut.CalculateSalaryTaxes(grossPackage, payFrequency);

            // Assert
            Assert.Equal(expectedAmount, result.Deduction.IncomeTax.Value);
        }

        [Fact]
        public void SalaryReport_SalaryInformation_ShouldBeCorrect()
        {
            // Arrange
            var grossPackage = "65000";
            var payFrequency = PayFrequency.M.ToString();

            // Act
            var result = _sut.CalculateSalaryTaxes(grossPackage, payFrequency);

            // Assert
            Assert.Equal(65000, result.GrossPackage);
            Assert.Equal(5639.27M, result.Superannuation);
            Assert.Equal(59360.73M, result.TaxableIncome);
            Assert.Equal(1188, result.Deduction.MedicareLevy.Value);
            Assert.Equal(0, result.Deduction.BudgetRepairLevy.Value);
            Assert.Equal(10839, result.Deduction.IncomeTax.Value);
            Assert.Equal(47333.73M, result.NetIncome);
            Assert.Equal(3944.48M, result.SalaryPackage);
        }

        [Fact]
        public void SalaryReport_SalaryReport_ShouldBeCorrect()
        {
            // Arrange
            var grossPackage = "65000";
            var payFrequency = PayFrequency.M.ToString();

            // Act
            var report = _report.SalaryReportString(_sut.CalculateSalaryTaxes(grossPackage, payFrequency));

            var finalReport = $@"
                    Calculating salary details...

                    Gross package: 65000
                    Superannuation: 5639.27

                    Taxable income: 59360.73

                    Deductions:
                    Medicare Levy: 1188
                    Budget Repair Levy: 0
                    Income Tax: 10839

                    Net income: 47333.73
                    Pay packet: 3944.48 per M";

            // Assert
            Assert.Equal(report, finalReport);
        }
    }
}