using System.Windows;
using Localization = CrypTrackerWPF.Resources.Localization;

namespace CrypTrackerWPF.Models.LocalizationExtensions;

public static class Replicas
{
    public static readonly PropertyPath Greetings;
    public static readonly PropertyPath CurrencySign;
    public static readonly PropertyPath MainWindowTitle;
    public static readonly PropertyPath DetailedInfoWindowTitle;
    public static readonly PropertyPath SettingsWindowTitle;
    public static readonly PropertyPath CurrencyConvertWindowTitle;
    public static readonly PropertyPath SubmitButtonName;
    public static readonly PropertyPath EngCultureDescription;
    public static readonly PropertyPath UkrCultureDescription;
    public static readonly PropertyPath ChooseLanguage;
    public static readonly PropertyPath ChooseTheme;
    public static readonly PropertyPath LightTheme;
    public static readonly PropertyPath DarkTheme;
    
    static Replicas()
    {
        Greetings = new(nameof(Localization.Greetings));
        CurrencySign = new(nameof(Localization.CurrencySign));
        MainWindowTitle = new(nameof(Localization.MainWindowTitle));
        DetailedInfoWindowTitle = new(nameof(Localization.DetailedInfoWindowTitle));
        DetailedInfoWindowTitle = new(nameof(Localization.DetailedInfoWindowTitle));
        SettingsWindowTitle = new(nameof(Localization.SettingsWindowTitle));
        CurrencyConvertWindowTitle = new(nameof(Localization.CurrencyConvertWindowTitle));
        SubmitButtonName = new(nameof(Localization.SubmitButtonName));
        EngCultureDescription = new(nameof(Localization.EngCultureDescription));
        UkrCultureDescription = new(nameof(Localization.UkrCultureDescription));
        ChooseTheme = new(nameof(Localization.ChooseTheme));
        LightTheme = new(nameof(Localization.LightTheme));
        DarkTheme = new(nameof(Localization.DarkTheme));
        ChooseLanguage = new(nameof(Localization.ChooseLang));
    }
}