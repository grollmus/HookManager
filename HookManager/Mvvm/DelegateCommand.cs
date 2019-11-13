using System;
using System.Windows.Input;

namespace HookManager.Mvvm
{
    internal class DelegateCommand : DelegateCommand<object>
    {
        public DelegateCommand(Action<object> executeAction) : base(executeAction)
        {
        }

        public DelegateCommand(Action executeAction)
            : this(_ => executeAction())
        {
        }

        public DelegateCommand(Action executeAction, Func<bool> canExecuteFunc)
            : this(_ => executeAction(), _ => canExecuteFunc())
        {
        }

        public DelegateCommand(Action<object> executeAction, Func<object, bool> canExecuteFunc) : base(executeAction, canExecuteFunc)
        {
        }
    }

    internal class DelegateCommand<TParam> : ICommand
    {
        private readonly Func<TParam, bool> _canExecute;
        private readonly Action<TParam> _execute;

        public DelegateCommand(Action<TParam> executeAction)
            : this(executeAction, x => true)
        {
        }

        public DelegateCommand(Action<TParam> executeAction, Func<TParam, bool> canExecuteFunc)
        {
            _execute = executeAction;
            _canExecute = canExecuteFunc;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute((TParam) parameter);
        }

        public void Execute(object parameter)
        {
            _execute((TParam) parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
}