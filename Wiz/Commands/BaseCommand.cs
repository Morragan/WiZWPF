using System;
using System.Windows.Forms;
using System.Windows.Input;

namespace Wiz.Commands
{
    public abstract class BaseCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private readonly Action<object?>? _execute;
        private readonly Predicate<object?>? _canExecute;

        public BaseCommand(Action<object?>? execute, Predicate<object?>? canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            if (_canExecute == null) return true;
            return _canExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            _execute?.Invoke(parameter);
        }
    }
}
