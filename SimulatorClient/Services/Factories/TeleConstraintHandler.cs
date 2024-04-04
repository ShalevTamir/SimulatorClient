using SimulatorClient.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulatorClient.Services.Factories
{
    internal class TeleConstraintHandler
    {
        private static TeleConstraintHandler _instance;
        private string[] existingTeleParameters;
        public static TeleConstraintHandler Instance
        {
            get
            {
                _instance ??= new TeleConstraintHandler();
                return _instance;
            }
            set { }
        }

        private RequestsService _requestsService;
        public TeleConstraintHandler()
        {
            _requestsService = RequestsService.Instance;
            GetExistingTeleParameters();
        }

        public async Task<ObservableCollection<TeleConstraint>> AddDefaultConstraints()
        {
            var teleParameters = new ObservableCollection<TeleConstraint>()
            {
                new TeleConstraint()
                {
                    Name = "Altitude",
                    Value = 40000,
                    Comparison = TeleComparison.BIGGER
                },
                new TeleConstraint()
                {
                    Name = "Longitude",
                    Value = -10,
                    Comparison = TeleComparison.SMALLER
                }
            };

            await Task.WhenAll(teleParameters.Select(AddTeleConstraintAsync));
            return teleParameters;
        }

        public async Task AddTeleConstraintAsync(TeleConstraint teleParameter)
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
