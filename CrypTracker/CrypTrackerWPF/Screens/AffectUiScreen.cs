using System;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace CrypTrackerWPF.Screens;

public class AffectUiScreen : Screen
{
    private bool _isUiEnabled;

    public bool IsUiEnabled
    {
        get => _isUiEnabled;
        set
        {
            _isUiEnabled = value;
            NotifyOfPropertyChange();
        }
    }

    public void ExecuteInUiContext(System.Action action)
    {
        IsUiEnabled = false;
        action?.Invoke();
        IsUiEnabled = true;
    }

    public async Task ExecuteInUiContextAsync(Func<Task> actionAsync)
    {
        IsUiEnabled = false;
        await (actionAsync?.Invoke() ?? Task.CompletedTask);
        IsUiEnabled = true;
    }

    public AffectUiScreen()
    {
        IsUiEnabled = true;
    }
}