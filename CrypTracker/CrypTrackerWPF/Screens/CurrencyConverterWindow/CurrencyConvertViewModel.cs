using Caliburn.Micro;
using CrypTrackerWPF.Models.LocalizationExtensions;

namespace CrypTrackerWPF.Screens.CurrencyConverterWindow;

public sealed class CurrencyConvertViewModel : Screen
{
    public override string DisplayName
    {
        get => TranslationSource.Instance[Replicas.CurrencyConvertWindowTitle];
    }
}