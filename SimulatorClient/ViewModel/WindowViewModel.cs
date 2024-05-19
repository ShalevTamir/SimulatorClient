using MvvmHelpers.Commands;
using MvvmHelpers.Interfaces;
using Newtonsoft.Json;
using SimulatorClient.Commands;
using SimulatorClient.Models;
using SimulatorClient.Models.Dtos;
using SimulatorClient.Services;
using SimulatorClient.Services.Factories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SimulatorClient.ViewModel
{
    class WindowViewModel
    {
        public IAsyncCommand<TeleConstraint> ToggleValueCommandAsync { get; private set; }
        public TeleConstraintHandler TeleConstraintHandler { get; private set; }
        private RequestsService _requestsService;
        private TeleGenerationConditionDtoFactory _teleGenerationConditionDtoFactory;
        public WindowViewModel()
        {
            TeleConstraintHandler = TeleConstraintHandler.Instance;
            _requestsService = new RequestsService();
            _teleGenerationConditionDtoFactory = TeleGenerationConditionDtoFactory.Instance;
            ToggleValueCommandAsync = new AsyncCommand<TeleConstraint>(ToggleValue);            
        }

        private async Task ToggleValue(TeleConstraint teleParameter)
        {
            try {
                if (teleParameter.ConditionActive)
                {

                    Debug.WriteLine(teleParameter.Name + " " + teleParameter.Value);
                    await _requestsService.PostAsync(Constants.SIMULATOR_URL + "/remove-condition",
                         teleParameter.ID);
                }
                else
                {
                    Debug.WriteLine(teleParameter.Name + " " + teleParameter.Value);

                    await _requestsService.PostAsync(Constants.SIMULATOR_URL + "/apply-condition",
                        _teleGenerationConditionDtoFactory.FromTeleParameter(teleParameter));
                }
                teleParameter.toggleCondition();
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine(e.Message);
            }
        }
    }
}
