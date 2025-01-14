using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace OptionsPricer
{
    internal class BSMTest
    {
        public static void RunTest()
        {
            double S = 100;
            double K = 100;
            double T = 1;
            double R = 0.05;
            double Sigma = 0.2;
            double V = 0;

            var callOption = new CallOptionBSM(S, K, T, R, Sigma, V);
            double callValue = callOption.CalculateValue();
            Console.WriteLine($"Call Option Price: {callValue}");

        }

    }
}
