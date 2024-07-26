using System.Globalization;
using Caliburn.Micro;
using CrypTrackerWPF.Models.LocalizationExtensions;

namespace CrypTrackerWPF.Screens.MainWindow;

public class MainWindowViewModel : Screen
{
    public bool CanSubmit => true;
    public void Submit()
    {
        TranslationSource.Instance.CurrentCulture = CultureInfo.GetCultureInfo("uk-UA");
    }
}