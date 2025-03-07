using System.Globalization;
using UnityEngine;
namespace QuickTools.Scripts.Utilities
{
    public static class MoneyFormat
    {
        public static string Default(int amount)
        {
            return DefaultWithoutIcon(amount) + "<sprite=0>";
        }
        
        public static string DefaultWithoutIcon(int amount)
        {
            if (amount >= int.MaxValue)
                return "INF";

            var digitCount = Mathf.FloorToInt(Mathf.Log10(amount) + 1);
            var formatted = digitCount switch
            {
                <= 4 => amount.ToString(NumberFormatInfo.InvariantInfo),
                <= 6 => $"{amount / 1000f:F1}K",
                <= 9 => $"{amount / 1000000f:F1}M",
                _ => $"{amount / 1000000000f:F1}B"
            };

            return formatted;
        }

    }
}