using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using CrypTrackerWPF.Models.ListBoxItemModels;
using CrypTrackerWPF.Models.LocalizationExtensions;
using CrypTrackerWPF.Screens.ShellWindow;
using Localization = CrypTrackerWPF.Resources.Localization;

namespace CrypTrackerWPF.Screens.SettingsWindow;

public sealed class SettingsWindowViewModel : Screen
{
    private static readonly ResourceDictionary _darkTheme = new ResourceDictionary()
        { Source = new Uri("Resources/Themes/Dark.xaml", UriKind.Relative) };

    private static readonly ResourceDictionary _lightTheme = new ResourceDictionary()
        { Source = new Uri("Resources/Themes/Light.xaml", UriKind.Relative) };

    private readonly ShellWindowViewModel _shellWindow;

    #region LocalizationListBox

    public BindableCollection<LocalizationItemModel> Localizations { get; init; }

    private LocalizationItemModel _selectedLocalization;

    public LocalizationItemModel SelectedLocalization
    {
        get => _selectedLocalization;
        set
        {
            _selectedLocalization = value;
            NotifyOfPropertyChange();
            if (TranslationSource.Instance.CurrentCulture != SelectedLocalization.Culture)
            {
                ChangeLocalization();
            }
        }
    }

    #endregion

    public override string DisplayName
    {
        get => TranslationSource.Instance[Replicas.SettingsWindowTitle];
    }

    public SettingsWindowViewModel(ShellWindowViewModel shellWindow)
    {
        _shellWindow = shellWindow;
        Localizations = new();
    }

    protected override Task OnInitializeAsync(CancellationToken cancellationToken)
    {
        Localizations.Add(new(CultureInfo.GetCultureInfo("en-US"),
            TranslationSource.Instance[Replicas.EngCultureDescription]));
        Localizations.Add(new(CultureInfo.GetCultureInfo("uk-UA"),
            TranslationSource.Instance[Replicas.UkrCultureDescription]));
        CanLightTheme = false;
        CanDarkTheme = true;
        SelectedLocalization = Localizations.First();
        return Task.CompletedTask;
    }

    public void ChangeLocalization()
    {
        TranslationSource.Instance.CurrentCulture = SelectedLocalization.Culture;
        Localizations[1].Name = TranslationSource.Instance[Replicas.UkrCultureDescription];
        Localizations[0].Name = TranslationSource.Instance[Replicas.EngCultureDescription];
        Localizations.Refresh();
        _shellWindow.NotifyWindowNamesChanged();
    }


    #region DarkThemeCommand
    private bool _canDarkTheme;

    public bool CanDarkTheme
    {
        get => _canDarkTheme;
        set
        {
            _canDarkTheme = value;
            NotifyOfPropertyChange();
        }
    }
    
    public void DarkTheme()
    {
        App.Current.Resources.Clear();
        App.Current.Resources.MergedDictionaries.Add(_darkTheme);
        CanLightTheme = true;
        CanDarkTheme = false;
    }
    #endregion
    

    #region LightThemeCommand

    private bool _canLightTheme;

    public bool CanLightTheme
    {
        get => _canLightTheme;
        set
        {
            _canLightTheme = value;
            NotifyOfPropertyChange();
        }
    }
    
    public void LightTheme()
    {
        App.Current.Resources.Clear();
        App.Current.Resources.MergedDictionaries.Add(_lightTheme);
        CanLightTheme = false;
        CanDarkTheme = true;
    }

    #endregion

    
}