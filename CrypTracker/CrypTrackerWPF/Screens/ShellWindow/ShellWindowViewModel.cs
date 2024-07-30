using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using CrypTrackerWPF.Models.ApiAccessor;
using CrypTrackerWPF.Models.EventMessages;
using CrypTrackerWPF.Models.LocalizationExtensions;
using CrypTrackerWPF.Resources;
using CrypTrackerWPF.Screens.CurrencyConverterWindow;
using CrypTrackerWPF.Screens.DetailedInfoWindow;
using CrypTrackerWPF.Screens.MainWindow;
using CrypTrackerWPF.Screens.SettingsWindow;

namespace CrypTrackerWPF.Screens.ShellWindow;

//todo: dispose apiAccessor
public sealed class ShellWindowViewModel : Conductor<Screen>.Collection.OneActive, 
    IHandle<ChangeNamesMessage>
{
    
    private readonly MainWindowViewModel _mainWindow;
    private readonly DetailedInfoWindowViewModel _detailedWindow;
    private readonly CurrencyConvertViewModel _convertWindow;
    private readonly SettingsWindowViewModel _settingsWindow;
    private readonly IEventAggregator _eventAggregator;
    private readonly IApiAccessor _apiAccessor;

    public ShellWindowViewModel(IEventAggregator eventAggregator,
                                IApiAccessor apiAccessor,
                                MainWindowViewModel mainWindow,
                                DetailedInfoWindowViewModel detailedWindow,
                                CurrencyConvertViewModel convertWindow,
                                SettingsWindowViewModel settingsWindow)
    {
        _eventAggregator = eventAggregator;
        _mainWindow = mainWindow;
        _detailedWindow = detailedWindow;
        _convertWindow = convertWindow;
        _settingsWindow = settingsWindow;
        _apiAccessor = apiAccessor;
    }
    
    public string WindowTitle
    {
        get => TranslationSource.Instance[Replicas.Greetings];
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

    public Task HandleAsync(ChangeNamesMessage message, CancellationToken cancellationToken)
    {
        NotifyOfPropertyChange(nameof(WindowTitle));
        _mainWindow.NotifyOfPropertyChange(nameof(_mainWindow.DisplayName));
        _detailedWindow.NotifyOfPropertyChange(nameof(_detailedWindow.DisplayName));
        _convertWindow.NotifyOfPropertyChange(nameof(_convertWindow.DisplayName));
        _settingsWindow.NotifyOfPropertyChange(nameof(_settingsWindow.DisplayName));
        
        return Task.CompletedTask;
    }
    
    protected override Task OnActivateAsync(CancellationToken cancellationToken) {
        _eventAggregator.SubscribeOnUIThread(this);
        return base.OnActivateAsync(cancellationToken);
    }


    protected override Task OnDeactivateAsync(bool close, CancellationToken cancellationToken)
    {
        _eventAggregator.Unsubscribe(this);
        return base.OnDeactivateAsync(close, cancellationToken);
    }
}