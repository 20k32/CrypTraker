using System.CodeDom;
using System.Dynamic;
using System.Reflection;
using System.Windows;
using Localization = CrypTrackerWPF.Resources.Localization;

namespace CrypTrackerWPF.Models.LocalizationExtensions;

public static class Replicas
{
    public static readonly PropertyPath Greetings;
    public static readonly PropertyPath CurrencySign;

    static Replicas()
    {
        Greetings = new(nameof(Localization.Greetings));
        CurrencySign = new(nameof(Localization.CurrencySign));
    }
}