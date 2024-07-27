using System.Windows;
using Caliburn.Micro;
using CrypTrackerWPF.Models.LocalizationExtensions;

namespace CrypTrackerWPF.Screens.DetailedInfoWindow;

public sealed class DetailedInfoWindowViewModel : Screen
{
    public override string DisplayName
    {
        get => TranslationSource.Instance[Replicas.DetailedInfoWindowTitle];
    }
}