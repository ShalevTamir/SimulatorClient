using SimulatorClient.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimulatorClient.Models
{
    public class TeleParameter
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public TeleComparison Comparison { get; set; }
        private bool _conditionActive;

        public TeleParameter()
        {
            this._conditionActive = false;
        }

        public void toggleCondition()
        {
            _conditionActive = !this._conditionActive;
        }
        public bool isConditionActive()
        {
            return this._conditionActive;
        }
    }
}
