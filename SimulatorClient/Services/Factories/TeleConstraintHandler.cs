using SimulatorClient.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SimulatorClient.Services.Factories
{
    internal class TeleConstraintHandler
    {
        private static TeleConstraintHandler _instance;
        public ObservableCollection<TeleConstraint> TeleConstraints { get; private set; }
        private int[] existingTeleParametersIds;
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
        private TeleConstraintHandler()
        {
            TeleConstraints = [];
            _requestsService = RequestsService.Instance;
            GetExistingTeleParameters();
            AddDefaultConstraintsAsync();
        }

        public async Task AddDefaultConstraintsAsync()
        {
            var teleConstraints = new ObservableCollection<TeleConstraint>()
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

            await Task.WhenAll(teleConstraints.Select(AddTeleConstraintAsync));
        }

        public async Task SyncTeleConstraint(TeleConstraint teleParameter)
        {
            if (this.existingTeleParametersIds == default)
            {
                await GetExistingTeleParameters();
            }
            foreach (var parameterId in this.existingTeleParametersIds)
            {
                if (parameterId == teleParameter.ID)
                {
                    teleParameter.ConditionActive = true;
                }
            }
        }

        public async Task AddTeleConstraintAsync(TeleConstraint teleConstraint)
        {
            await SyncTeleConstraint(teleConstraint);
            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                teleConstraint.ID = TeleConstraints.Count();
                Debug.WriteLine(TeleConstraints.Count());
                TeleConstraints.Add(teleConstraint);
            });
        }

        private async Task GetExistingTeleParameters()
        {
            this.existingTeleParametersIds = await _requestsService.GetAsync<int[]>(Constants.SIMULATOR_URL);
        }
    }
}
