using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using Caliburn.Micro;
using CrypTrackerWPF.Screens.MainWindow;
using CrypTrackerWPF.Resources;
using Localization = CrypTrackerWPF.Resources.Localization;

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
            .Singleton<MainWindowViewModel>();
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
        var cultures = Localization.Culture;
        CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("uk-UA");
        var locale = Localization.Greetings;
        
        await DisplayRootViewForAsync<MainWindowViewModel>(new Dictionary<string, object>()
        {
            { "WindowStartupLocation", WindowStartupLocation.CenterScreen },
            { "MinHeight", 800},
            { "MinWidth", 650},
        });
    }
}