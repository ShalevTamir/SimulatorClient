using SimulatorClient.Commands;
using SimulatorClient.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;

namespace SimulatorClient.ViewModel
{
    class WindowViewModel: ViewModelBase
    {
        public ICommand ToggleValueCommand { get; private set; }
        public ObservableCollection<TeleParameter> teleParameters { get; private set; }

        public WindowViewModel()
        {
            ToggleValueCommand = new RelayCommand((obj) => Debug.WriteLine("Command activated"));
            teleParameters = new ObservableCollection<TeleParameter>()
            {
                new TeleParameter()
                {
                    Name = "Altitude",
                    Value = 2000,
                    Comparison = TeleComparison.BIGGER
                },
                new TeleParameter()
                {
                    Name = "Longitude",
                    Value = -50,
                    Comparison = TeleComparison.SMALLER
                }
            };
        }
    }
}
