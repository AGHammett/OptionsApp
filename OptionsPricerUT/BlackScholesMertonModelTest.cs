using OptionsPricer;

namespace OptionsPricerUT
{
    [TestClass]
    public sealed class BlackScholesMertonModelTest
    {
        [TestMethod]
        public void CallOptionValueTest()
        {
            double expectedValue = 10.45;
            double epsilon = 0.001;

            double S = 100;
            double K = 100;
            double T = 1;
            double R = 0.05;
            double Sigma = 0.2;
            double V = 0;

            var callOption = new BSMCall(S, K, T, R, Sigma, V);

            Assert.AreEqual(callOption.CalculateValue(), expectedValue, epsilon);
        }

        [TestMethod]
        public void PutOptionValueTest()
        {
            double expectedValue = 5.57;
            double epsilon = 0.1;

            double S = 100;
            double K = 100;
            double T = 1;
            double R = 0.05;
            double Sigma = 0.2;
            double V = 0;

            var putOption = new BSMPut(S, K, T, R, Sigma, V);

            Assert.AreEqual(putOption.CalculateValue(), expectedValue, epsilon);
        }
    }
   
}
