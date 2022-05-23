using Atturra.TaxCalculator.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Atturra.TaxCalculator.Extensions
{
    public static class PaymentFrequencyExtension
    {
        public static string GetDisplayName(this PayFrequency payFrequency)
        {
            return payFrequency.GetType()
                .GetMember(payFrequency.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>()
                .GetName();
        }
    }
}