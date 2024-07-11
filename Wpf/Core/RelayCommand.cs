using System;
using System.Windows.Input;

namespace Wpf.Core
{
    class RelayCommand : ICommand
    {
        private Action<object> _execute;
        private Func<object, bool> _canExcute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }

        }

        public RelayCommand(Action<object> excute, Func<object, bool> canExcute = null)
        {
            _execute = excute;
            _canExcute = canExcute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExcute == null || _canExcute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
