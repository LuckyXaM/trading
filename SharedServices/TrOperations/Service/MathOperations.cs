using System;

namespace TrOperations.Service
{
    /// <summary>
    /// Математические операции
    /// </summary>
    public static class MathOperations
    {
        /// <summary>
        /// Делит с округлением
        /// </summary>
        /// <returns></returns>
        public static decimal RoudDivision(decimal numerator, decimal denominator)
        {
            var result = Math.Round(numerator / denominator, 8);

            return result;
        }

        /// <summary>
        /// Умножает с округлением
        /// </summary>
        /// <returns></returns>
        public static decimal RoundMultiplication(decimal multOne, decimal multTwo)
        {
            var result = Math.Round(multOne * multTwo, 8);

            return result;
        }
    }
}
