using MvvmHelpers.Commands;
using MvvmHelpers.Interfaces;
using SimulatorClient.Models;
using SimulatorClient.Services.Factories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace SimulatorClient.ViewModel
{
    internal class AddConstraintVIewModel
    {
        public IAsyncCommand<TeleConstraint> AddConstraintCommand { get; private set; }
        private TeleConstraintHandler _teleConstraintHandler;
        public AddConstraintVIewModel()
        {
            _teleConstraintHandler = TeleConstraintHandler.Instance;
            AddConstraintCommand = new AsyncCommand<TeleConstraint>(AddConstraintAsync);
        }

        public async Task AddConstraintAsync(TeleConstraint parameter)
        {
            await _teleConstraintHandler.AddTeleConstraintAsync(parameter);
        }
    }
}
