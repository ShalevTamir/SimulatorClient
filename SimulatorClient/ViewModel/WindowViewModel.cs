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
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimulatorClient.ViewModel
{
    class WindowViewModel
    {
        public AsyncCommand<TeleParameter> ToggleValueCommandAsync { get; private set; }
        public ObservableCollection<TeleParameter> teleParameters { get; private set; }
        private RequestsService _requestsService;
        private TeleGenerationConditionDtoFactory _teleGenerationConditionDtoFactory;
        public WindowViewModel()
        {
            this.teleParameters = new ObservableCollection<TeleParameter>();
            TeleParameterFactory.Instance.BuildTeleParametersAsync().ContinueWith(async completedTask =>
            {
                foreach (var teleParameter in completedTask.Result)
                {
                    await App.Current.Dispatcher.InvokeAsync(() =>
                    {
                        this.teleParameters.Add(teleParameter);
                    });
                }
            });
            _requestsService = new RequestsService();
            _teleGenerationConditionDtoFactory = TeleGenerationConditionDtoFactory.Instance;
            ToggleValueCommandAsync = new AsyncCommand<TeleParameter>(ToggleValue);
        }

        private async Task ToggleValue(TeleParameter teleParameter)
        {
            try {
                if (teleParameter.ConditionActive)
                {
                    await _requestsService.PostAsync(Constants.SIMULATOR_URL + "/remove-condition",
                         teleParameter.Name);
                }
                else
                {
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
