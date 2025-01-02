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
        public double V {  get; set; } //Option Value

        //Computed Properties
        public double D1 => (Math.Log(S/K) + (R + Math.Pow(Sigma, 2) / 2) / (Sigma * Math.Sqrt(T)));
        public double D2 => D1 - (Sigma * Math.Sqrt(T));


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
        internal CallOptionBSM(double s, double k, double t, double r, double sigma, double v):
            base(s, k, t, r, sigma, v) 
        { 
        }

        public override double CalculateValue()
        {
            return K * Math.Exp(-R * T) * HelperFunctions.NormalCDF(D1) - S * HelperFunctions.NormalCDF(D2);
        }
    }

        

}
