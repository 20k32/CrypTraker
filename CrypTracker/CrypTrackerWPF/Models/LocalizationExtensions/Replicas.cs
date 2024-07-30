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
    public static readonly PropertyPath EmptyValueError;
    public static readonly PropertyPath ClientSideError;
    public static readonly PropertyPath ServerSideError;
    public static readonly PropertyPath Name;
    public static readonly PropertyPath ShortName;
    public static readonly PropertyPath Price;
    public static readonly PropertyPath DescriptionToPrice;
    public static readonly PropertyPath DisplayTop;
    public static readonly PropertyPath NextPage;
    public static readonly PropertyPath PreviousPage;
    public static readonly PropertyPath ResetButtonName;
    public static readonly PropertyPath Volume;
    public static readonly PropertyPath PriceChange;
    public static readonly PropertyPath CurrencyName;
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
        EmptyValueError = new(nameof(Localization.EmptyValueError));
        ClientSideError = new(nameof(Localization.ClientSideError));
        ServerSideError = new(nameof(Localization.ServerSideError));
        Name = new(nameof(Localization.Name));
        ShortName = new(nameof(Localization.ShortName));
        Price = new(nameof(Localization.Price));
        DescriptionToPrice = new(nameof(Localization.DescriptionToPrice));
        DisplayTop = new(nameof(Localization.DisplayTop));
        NextPage = new(nameof(Localization.NextPage));
        PreviousPage = new(nameof(Localization.PreviousPage));
        ResetButtonName = new(nameof(Localization.ResetButtonName));
        Volume = new(nameof(Localization.Volume));
        PriceChange = new(nameof(Localization.PriceChange));
        CurrencyName = new(nameof(Localization.CurrencyName));
    }
}