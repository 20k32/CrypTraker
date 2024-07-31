using System;
using System.Collections.Generic;
using System.Windows;
using Caliburn.Micro;
using CrypTrackerWPF.Models.ApiAccessor;
using CrypTrackerWPF.Screens.CurrencyConverterWindow;
using CrypTrackerWPF.Screens.DetailedInfoWindow;
using CrypTrackerWPF.Screens.MainWindow;
using CrypTrackerWPF.Screens.SettingsWindow;
using CrypTrackerWPF.Screens.ShellWindow;

namespace CrypTrackerWPF.AppContext;

public sealed class Bootstrapper : BootstrapperBase
{
    private SimpleContainer _container = new();
    public Bootstrapper()
    {
        Initialize();
    }
        
    protected override void Configure()
    {
        _container
            .Instance(_container)
            .Singleton<IWindowManager, WindowManager>()
            .Singleton<IEventAggregator, EventAggregator>()
            .AddApiAccessor()
            .Singleton<MainWindowViewModel>()
            .Singleton<DetailedInfoWindowViewModel>()
            .Singleton<CurrencyConvertViewModel>()
            .Singleton<SettingsWindowViewModel>()
            .Singleton<ShellWindowViewModel>();
    }
        
    protected override object GetInstance(Type service, string key)
    {
        return _container.GetInstance(service, key);
    }

    protected override IEnumerable<object> GetAllInstances(Type service)
    {
        return _container.GetAllInstances(service);
    }

    protected override void BuildUp(object instance)
    {
        _container.BuildUp(instance);

    }
    
    protected override async void OnStartup(object sender, StartupEventArgs e)
    {
        await DisplayRootViewForAsync<ShellWindowViewModel>(new Dictionary<string, object>()
        {
            { "WindowStartupLocation", WindowStartupLocation.CenterScreen },
            { "MinHeight", 956},
            { "MinWidth", 970},
        });
    }
}