using MvvmHelpers.Commands;
using Newtonsoft.Json;
using SimulatorClient.Commands;
using SimulatorClient.Models;
using SimulatorClient.Models.Dtos;
using SimulatorClient.Services;
using SimulatorClient.Services.Factories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimulatorClient.ViewModel
{
    class WindowViewModel: ViewModelBase
    {
        const string SIMULATOR_URL = "https://localhost:5001/simulator";
        public AsyncCommand<TeleParameter> ToggleValueCommandAsync { get; private set; }
        public ObservableCollection<TeleParameter> teleParameters { get; private set; }
        private RequestsService _requestsService;
        private TeleGenerationConditionDtoFactory _teleGenerationConditionDtoFactory;

        public WindowViewModel()
        {
            teleParameters = new ObservableCollection<TeleParameter>()
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
                    Value = -50,
                    Comparison = TeleComparison.SMALLER
                }
            };
            _requestsService = new RequestsService();
            _teleGenerationConditionDtoFactory = TeleGenerationConditionDtoFactory.Instance;
            ToggleValueCommandAsync = new AsyncCommand<TeleParameter>(ToggleValue);
        }

        private async Task ToggleValue(TeleParameter teleParameter)
        {
            string res = "";
            try {
                if (!teleParameter.isConditionActive())
                {
                    res = await _requestsService.PostAsync(SIMULATOR_URL + "/apply-condition",
                        _teleGenerationConditionDtoFactory.FromTeleParameter(teleParameter));
                }
                else
                {
                    res = await _requestsService.PostAsync(SIMULATOR_URL + "/remove-condition",
                         teleParameter.Name);
                }
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine(e.Message);
            }
            Debug.WriteLine("RESPONSE " + res);
            teleParameter.toggleCondition();
        }
    }
}
