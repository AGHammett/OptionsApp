using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using OptionsPricer;
using System.Windows.Input;
using UI.Core;

namespace UI.MVVM.ViewModel
{
    public class OptionsPricingViewModel : INotifyPropertyChanged
    {
        private double _s; // Security Price
        private double _k; // Strike price
        private double _t; // Time to maturity
        private double _r; // Short Rate
        private double _sigma; // Volatility
        private double _v; // Option Value
        private double _valueResult;

        public double S
        {
            get => _s;
            set { _s = value; OnPropertyChanged(); }
        }

        public double K
        {
            get => _k;
            set { _k = value; OnPropertyChanged(); }
        }

        public double T
        {
            get => _t;
            set { _t = value; OnPropertyChanged(); }
        }

        public double R
        {
            get => _r;
            set { _r = value; OnPropertyChanged(); }
        }

        public double Sigma
        {
            get => _sigma;
            set { _sigma = value; OnPropertyChanged(); }
        }

        public double V
        {
            get => _v;
            set { _v = value; OnPropertyChanged(); }
        }

        public double ValueResult
        {
            get => _valueResult;
            private set
            {
                _valueResult = value;
                OnPropertyChanged();
            }
        }

        public ICommand CalculateCommand { get; } //Decouple user interaction from function implementation

        public OptionsPricingViewModel()
        {
            CalculateCommand = new RelayCommand(CalculateResult);
        }

        private void CalculateResult(object parameter) //Instantiate backend Call Option and update value
        {
            var call = new BSMCall(S, K, T, R, Sigma, V = 0);
            ValueResult = call.CalculateValue();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) //Let the compiler automatically pass in the Member's name as a string
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); //Update any bound subscribers in the UI when updating a property
        }
    }
}
