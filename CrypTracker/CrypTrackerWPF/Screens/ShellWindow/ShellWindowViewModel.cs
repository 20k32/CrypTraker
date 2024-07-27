using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using CrypTrackerWPF.Models.LocalizationExtensions;
using CrypTrackerWPF.Resources;
using CrypTrackerWPF.Screens.CurrencyConverterWindow;
using CrypTrackerWPF.Screens.DetailedInfoWindow;
using CrypTrackerWPF.Screens.MainWindow;
using CrypTrackerWPF.Screens.SettingsWindow;

namespace CrypTrackerWPF.Screens.ShellWindow;

public sealed class ShellWindowViewModel : Conductor<Screen>.Collection.OneActive
{
    
    private readonly MainWindowViewModel _mainWindow;
    private readonly DetailedInfoWindowViewModel _detailedWindow;
    private readonly CurrencyConvertViewModel _convertWindow;
    private readonly SettingsWindowViewModel _settingsWindow;
    
    public ShellWindowViewModel()
    {
        _mainWindow = new();
        _detailedWindow = new();
        _convertWindow = new();
        _settingsWindow = new(this);
    }
    
    
    
    public string WindowTitle
    {
        get => TranslationSource.Instance[Replicas.Greetings];
    }
    
    public void NotifyWindowNamesChanged() 
    {
        NotifyOfPropertyChange(nameof(WindowTitle));
        _mainWindow.NotifyOfPropertyChange(nameof(_mainWindow.DisplayName));
        _detailedWindow.NotifyOfPropertyChange(nameof(_detailedWindow.DisplayName));
        _convertWindow.NotifyOfPropertyChange(nameof(_convertWindow.DisplayName));
        _settingsWindow.NotifyOfPropertyChange(nameof(_settingsWindow.DisplayName));
    }
    
    protected override Task OnInitializeAsync(CancellationToken cancellationToken)
    {
        TranslationSource.Instance.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
        Items.Add(_mainWindow);
        Items.Add(_detailedWindow);
        Items.Add(_convertWindow);
        Items.Add(_settingsWindow);
        return Task.CompletedTask;
    }
}