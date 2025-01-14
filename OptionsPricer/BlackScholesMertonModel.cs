using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionsPricer
{
    internal abstract class BlackScholesMertonModel //Base class containing properties for BSM call/put options
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

        protected BlackScholesMertonModel(double s, double k, double t, double r, double sigma, double v)
        {
            S = s;
            K = k;
            T = t;
            R = r;
            Sigma = sigma;
            V = v;
        }

        public abstract double CalculateValue();

    }

    internal class CallOptionBSM : BlackScholesMertonModel
    {
        internal CallOptionBSM(double s, double k, double t, double r, double sigma, double v) :
            base(s, k, t, r, sigma, v)
        {
        }
        //Call Option Greeks
        public double Delta => HelperFunctions.NormalCDF(D1); // Partial Derivative wrt Security Price
        public double Rho => K * T * Math.Exp(-R * T) * HelperFunctions.NormalCDF(D2); // Partial Derivative wrt Short Rate
        public double Theta => -(S * HelperFunctions.NormalPDF(D1) * Sigma) / (2 * Math.Sqrt(T)) - R * Math.Exp(-R * T) * K * HelperFunctions.NormalCDF(D2); // Partial Derivative wrt Time

        public override double CalculateValue()
        {
            return S * HelperFunctions.NormalCDF(D1) - K * Math.Exp(-R * T) * HelperFunctions.NormalCDF(D2);
        }
    }

    internal class PutOptionBSM : BlackScholesMertonModel
    {
        internal PutOptionBSM(double s, double k, double t, double r, double sigma, double v) :
            base(s, k, t, r, sigma, v)

        {
        }
        //Put Option Greeks
        public double Delta => HelperFunctions.NormalCDF(D1) - 1; // Partial Derivative wrt Security Price
        public double Rho => -K * T * Math.Exp(-R * T) * HelperFunctions.NormalCDF(-D2); // Partial Derivative wrt Short Rate
        public double Theta => -(S * HelperFunctions.NormalPDF(D1) * Sigma) / (2 * Math.Sqrt(T)) + R * Math.Exp(-R * T) * K * HelperFunctions.NormalCDF(-D2); // Partial Derivative wrt Time

        public override double CalculateValue()
        {
            return K * Math.Exp(-R * T) * HelperFunctions.NormalCDF(-D2) - S * HelperFunctions.NormalCDF(-D1);
        }
    }

}
