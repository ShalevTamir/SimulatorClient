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
        private string[] existingTeleParameters;
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
            GetExistingTeleParameters();
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
                }
            };

            await Task.WhenAll(teleParameters.Select(AddTeleParameter));
            return teleParameters;
        }

        private async Task AddTeleParameter(TeleParameter teleParameter)
        {
            if (this.existingTeleParameters == default)
            {
                await GetExistingTeleParameters();
            }
            foreach (var parameterName in this.existingTeleParameters)
            {
                if (parameterName.Equals(teleParameter.Name))
                {
                    teleParameter.ConditionActive = true;
                }
            }
        }

        private async Task GetExistingTeleParameters()
        {
            this.existingTeleParameters = await _requestsService.GetAsync<string[]>(Constants.SIMULATOR_URL);
        }
    }
}
