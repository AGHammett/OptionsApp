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
using UI.MVVM.Model;
using Microsoft.VisualBasic.FileIO;
using ScottPlot.Colormaps;

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
        private double _valueResult; //Result for value calculation

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

        public enum OptionType { Call, Put}; //Create option type data class
        private OptionType _selectedOptionType = OptionType.Call;
        public OptionType SelectedOptionType //Getter/Setter to change/read option data
        {
            get => _selectedOptionType;
            set { _selectedOptionType = value; OnPropertyChanged(); }
        }

        private OptionGreeks _greeks = new OptionGreeks();
        public OptionGreeks Greeks
        {
            get => _greeks;
            set { _greeks = value; OnPropertyChanged(); }
        }

        public ICommand ToggleOptionTypeCommand { get; }
        
        private void ToggleOptionType(object parameter)
        {
            SelectedOptionType = SelectedOptionType == OptionType.Call
                ? OptionType.Put
                : OptionType.Call;
        }

        public ICommand CalculateCommand { get; } //Decouple user interaction from function implementation

        public OptionsPricingViewModel()
        {
            CalculateCommand = new RelayCommand(CalculateResult);
            ToggleOptionTypeCommand = new RelayCommand(ToggleOptionType);
        }

        private void CalculateResult(object parameter) //Calculate value based on option type
        {
            BSMOption option = SelectedOptionType == OptionType.Call
                ? new BSMCall(S, K, T, R, Sigma, V)
                : new BSMPut(S, K, T, R, Sigma, V);

            ValueResult = option.CalculateValue();

            Greeks = new OptionGreeks // Instantiate class to hold values caluclated
            {
                Delta = option.Delta,
                Gamma = option.Gamma,
                Theta = option.Theta,
                Vega = option.Vega,
                Rho = option.Rho
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) //Let the compiler automatically pass in the Member's name as a string
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); //Update any bound subscribers in the UI when updating a property
        }
    }
}
