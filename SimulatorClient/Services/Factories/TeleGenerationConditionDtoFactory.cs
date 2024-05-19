using SimulatorClient.Models;
using SimulatorClient.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimulatorClient.Services.Factories
{
    internal class TeleGenerationConditionDtoFactory
    {
        private static TeleGenerationConditionDtoFactory _instance;
        public static TeleGenerationConditionDtoFactory Instance
        {
            get
            {
                _instance ??= new TeleGenerationConditionDtoFactory();
                return _instance;
            }
            private set { }
        }
        public TeleGenerationConditionDto FromTeleParameter(TeleConstraint teleParameter)
        {
            int? minValue = null, maxValue = null;
            if (teleParameter.Comparison == TeleComparison.BIGGER)
                minValue = teleParameter.Value;
            else if (teleParameter.Comparison == TeleComparison.SMALLER)
                maxValue = teleParameter.Value;
            return new TeleGenerationConditionDto()
            {
                ID = teleParameter.ID,
                Name = teleParameter.Name,
                BottomRestriction = minValue,
                TopRestriction = maxValue,
            };
        }
    }
}
