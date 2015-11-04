using System;
using System.Windows.Input;

namespace SummitLog.UI.Common
{
    public class RelayCommand : ICommand
    {
        private readonly Func<bool> _canExecuteAction;
        private readonly Action _executeAction;

        public RelayCommand(Action executeAction, Func<bool> canExecuteAction)
        {
            _executeAction = executeAction;
            _canExecuteAction = canExecuteAction;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecuteAction.Invoke();
        }

        public void Execute(object parameter)
        {
            _executeAction.Invoke();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}