using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionsPricer
{
    public class BSMCall : BSMOption
    {
        public BSMCall(double s, double k, double t, double r, double sigma, double v) :
            base(s, k, t, r, sigma, v)
        {
        }
        //Call Option Greeks
        public override double Delta => HelperFunctions.NormalCDF(D1); // Partial Derivative wrt Security Price
        public override double Rho => K * T * Math.Exp(-R * T) * HelperFunctions.NormalCDF(D2); // Partial Derivative wrt Short Rate
        public override double Theta => -(S * HelperFunctions.NormalPDF(D1) * Sigma) / (2 * Math.Sqrt(T)) - R * Math.Exp(-R * T) * K * HelperFunctions.NormalCDF(D2); // Partial Derivative wrt Time

        public override double CalculateValue()
        {
            V = Math.Max(0, S * HelperFunctions.NormalCDF(D1) - K * Math.Exp(-R * T) * HelperFunctions.NormalCDF(D2));

            return V;
        }
    }
}
