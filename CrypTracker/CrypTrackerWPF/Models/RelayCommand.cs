using System;
using System.Windows.Input;

namespace CrypTrackerWPF.Models;

public class RelayCommand : ICommand
{
    private Action<object> execute = null;
    private Predicate<object> canExecute = null;

    public RelayCommand(Action<object> Execute, Predicate<object> CanExecute = null)
    {
        execute = Execute;
        canExecute = CanExecute;
    }

    public event EventHandler CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public bool CanExecute(object parameter)
    {
        return canExecute == null || canExecute.Invoke(parameter = null!);
    }

    public void Execute(object parameter)
    {
        execute?.Invoke(parameter);
    }
}