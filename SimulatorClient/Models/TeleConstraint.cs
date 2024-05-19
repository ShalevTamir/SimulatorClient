using SimulatorClient.Models.Dtos;
using SimulatorClient.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SimulatorClient.Models
{
    public class TeleConstraint: TrackedProperty
    {
        private string _name;
        private int _value;
        private TeleComparison _comparison;
        private bool _conditionActive;
        public string Name { get => _name; set => SetProperty(ref _name, value); }
        public int Value { get => _value; set => SetProperty(ref _value, value); }
        public TeleComparison Comparison { get => _comparison; set => SetProperty(ref _comparison, value); }
        public bool ConditionActive { get => _conditionActive; set => SetProperty(ref _conditionActive, value); }
        public TeleConstraint()
        {
            Name = "Parameter Name";
            this._conditionActive = false;
        }

        public void toggleCondition()
        {
            ConditionActive = !ConditionActive;
        }
    }
}
