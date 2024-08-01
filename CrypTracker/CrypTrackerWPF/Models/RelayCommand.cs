using System;
using System.Windows.Input;

namespace CrypTrackerWPF.Models;

public class RelayCommand : ICommand
{
    private Action<object> _execute = null;
    private Predicate<object> _canExecute = null;

    public RelayCommand(Action<object> Execute, Predicate<object> CanExecute = null)
    {
        _execute = Execute;
        _canExecute = CanExecute;
    }

    public event EventHandler CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public bool CanExecute(object parameter)
    {
        return _canExecute == null || _canExecute.Invoke(parameter = null!);
    }

    public void Execute(object parameter)
    {
        _execute?.Invoke(parameter);
    }
}