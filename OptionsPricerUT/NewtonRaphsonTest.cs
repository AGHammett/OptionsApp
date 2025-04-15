using OptionsPricer;

namespace OptionsPricerUT
{

    [TestClass]
    public class ImpliedVolatilityTest
    {
        [TestMethod]
        public void IVTest()
        {
        
            double expectedValue = 0.2;
            double epsilon = 0.001;

            double S = 100;
            double K = 100;
            double T = 1;
            double R = 0.05;
            double Sigma = 0.0;
            double V = 10.45;

            var callOption = new BSMCall(S, K, T, R, Sigma, V);

            Assert.AreEqual(callOption.CalculateImpliedVolatility(), expectedValue, epsilon);
        }
    }
}
