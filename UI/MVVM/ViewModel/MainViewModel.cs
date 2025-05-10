using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Core;

namespace UI.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {

        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand OptionsPricingViewCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public OptionsPricingViewModel OPVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set 
            {
                _currentView = value;
                OnPropetyChanged();
            }
        }

        public MainViewModel() 
        { 
            HomeVM = new HomeViewModel();
            OPVM = new OptionsPricingViewModel();
            CurrentView = OPVM;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVM;
            });

            OptionsPricingViewCommand = new RelayCommand(o =>
            {
                CurrentView = OPVM;
            });

        }
    }
}
