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
            if (_canExecuteAction != null)
            {
                return _canExecuteAction.Invoke(); 
            }
            return true;
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