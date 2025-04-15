using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using ScottPlot.Interactivity.UserActionResponses;

namespace OptionsPricer
{
    public abstract class BSMOption //Black-Scholes-Merton Option class containing properties for BSM call/put options
    {
        public double S { get; set; } //Security Price
        public double K { get; set; } //Strike price
        public double T { get; set; } //Time to maturity
        public double R { get; set; } //Short Rate
        public double Sigma { get; set; } //Volatility
        public double V { get; set; } //Option Value

        //Computed Properties
        public double D1 => ((Math.Log(S / K) + (R + Math.Pow(Sigma, 2) / 2) * T) / (Sigma * Math.Sqrt(T)));
        public double D2 => D1 - (Sigma * Math.Sqrt(T));

        //Common Greeks
        public double Vega => S * Math.Sqrt(T) * HelperFunctions.NormalPDF(D1); // Partial Derivative wrt Volatility
        public double Gamma => HelperFunctions.NormalPDF(D1) / (S * Sigma * Math.Sqrt(T)); // Second Partial Derivative wrt Security Price

        public BSMOption(double s, double k, double t, double r, double sigma, double v)
        {

            if (s < 0 || k < 0 || t < 0 || sigma < 0)
                throw new ArgumentException("Negative values for S, K, T & Sigma not accepted");

            S = s;
            K = k;
            T = t;
            R = r;
            Sigma = sigma;
            V = v;
        }

        public abstract double CalculateValue(); // Method to be inherited by Call/Put subclasses

        public virtual double CalculateImpliedVolatility(double Tolerance = 1e-6, int MaxIterations = 100)
        {
            double MarketPrice = V;
            double IV = ImpliedVolatilityCalculator.NewtonRaphson(this, MarketPrice, Tolerance, MaxIterations);
            Sigma = IV;
            return IV;
        }

    }

}
