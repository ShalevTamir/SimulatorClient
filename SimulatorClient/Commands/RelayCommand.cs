using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SimulatorClient.Commands
{
    class RelayCommand : ICommand
    {
        private Action<object> _execute;
        private Predicate<object> _canExecute;
        public RelayCommand(Action<object> execute, Predicate<object> canExecute) 
        {
            this._execute = execute;
            this._canExecute = canExecute;
        }

        public RelayCommand(Action<object> execute): this(execute, null) { }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return this._canExecute == null ? true: this._canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
