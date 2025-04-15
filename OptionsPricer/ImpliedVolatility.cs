using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionsPricer
{
    public static class ImpliedVolatilityCalculator // Class that will use a Newton Raphson method to calculate implied volatility given a market price
    {

        public static double NewtonRaphson(
            BSMOption option, 
            double MarketPrice,
            double Tolerance = 1e-6,
            double MaxIterations = 100
            )
        {
            double sigma = InitialGuessCalculator(option.S, option.K, option.R, option.T); // Initial guess
            int iterations = 0;

            while (iterations < MaxIterations) // Loop through max iterations until convergence within tolerance level
            {
                option.Sigma = sigma;

                option.CalculateValue();
                double OptionPrice = option.V;

                double vega = option.Vega;

                if (Math.Abs(OptionPrice - MarketPrice) < Tolerance)
                {
                    return sigma; // Exit function if convergence is reached early
                }

                sigma = sigma - (OptionPrice - MarketPrice) / vega;

                iterations++;

            }

            throw new InvalidOperationException($"No connvergence after{MaxIterations} iterations"); // Throw error when convergence not reached
        }

        private static double InitialGuessCalculator(double S, double K, double R, double T) // Function to calculate an initial guess to help with convergence
        {
            double Moneyness = S / (K * Math.Exp(-R * T)); // Calculate moneyness of option (how far in/out of the money)

            double InitialGuess =  Math.Sqrt(2 * Math.Abs(Math.Log(Moneyness)) / T); // Guess at inflection point of price/volatility curve

            return Math.Max(0.01, Math.Min(InitialGuess, 5.0)); // Clamp intitial guess bet 1-500% for numerical stability
        }
    }
}
