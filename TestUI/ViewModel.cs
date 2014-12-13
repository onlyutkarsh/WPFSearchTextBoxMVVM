using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestUI
{
    public class ViewModel
    {
        private DelegateCommand _searchCommand;

        public DelegateCommand SearchNowCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new DelegateCommand(OnSearch));
            }
        }

        private void OnSearch(object obj)
        {

        }
    }

    public class DelegateCommand : System.Windows.Input.ICommand
    {
        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _method;

        public DelegateCommand(Action<object> method)
            : this(method, null)
        {
        }

        public DelegateCommand(Action<object> method, Predicate<object> canExecute)
        {
            _method = method;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }
            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _method.Invoke(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            OnCanExecuteChanged(EventArgs.Empty);
        }

        protected virtual void OnCanExecuteChanged(EventArgs e)
        {
            var canExecuteChanged = CanExecuteChanged;
            if (canExecuteChanged != null)
                canExecuteChanged(this, e);
        }
    }
}
