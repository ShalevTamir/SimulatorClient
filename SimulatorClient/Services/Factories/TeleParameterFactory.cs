using SimulatorClient.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulatorClient.Services.Factories
{
    internal class TeleParameterFactory
    {
        private static TeleParameterFactory _instance;
        public static TeleParameterFactory Instance
        {
            get
            {
                _instance ??= new TeleParameterFactory();
                return _instance;
            }
            set { }
        }

        private RequestsService _requestsService;
        public TeleParameterFactory()
        {
            _requestsService = RequestsService.Instance;
        }

        public async Task<ObservableCollection<TeleParameter>> BuildTeleParametersAsync()
        {
            var teleParameters = new ObservableCollection<TeleParameter>()
            {
                new TeleParameter()
                {
                    Name = "Altitude",
                    Value = 40000,
                    Comparison = TeleComparison.BIGGER
                },
                new TeleParameter()
                {
                    Name = "Longitude",
                    Value = -10,
                    Comparison = TeleComparison.SMALLER
                },
            };
            string[] existingTeleParameters = await _requestsService.GetAsync<string[]>(Constants.SIMULATOR_URL);
            foreach (var parameterName in existingTeleParameters)
            {
                var filteredParameters = teleParameters.Where((parameter) => parameter.Name == parameterName);
                if(filteredParameters.Any())
                {
                    filteredParameters.First().ConditionActive = true;
                }
            }
            return teleParameters;

        }
    }
}
