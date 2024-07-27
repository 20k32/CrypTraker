using System.ComponentModel;
using System.Globalization;
using System.Resources;
using System.Windows;
using Localization = CrypTrackerWPF.Resources.Localization;

namespace CrypTrackerWPF.Models.LocalizationExtensions;

public sealed class TranslationSource : INotifyPropertyChanged
{
    private static readonly TranslationSource _instance = new();
    
    private readonly ResourceManager _resManager = Localization.ResourceManager;
    private CultureInfo _currentCulture = null;

    public static TranslationSource Instance
    {
        get => _instance;
    }

    public string this[PropertyPath path]
    {
        get => _resManager.GetString(path.Path, this._currentCulture);
    }
    
    public CultureInfo CurrentCulture
    {
        get => _currentCulture;
        set
        {
            if (_currentCulture != value)
            {
                _currentCulture = value;
                OnPropertyChanged();
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public void OnPropertyChanged()
    {
        if (PropertyChanged is null)
            return;
        
        PropertyChanged.Invoke(this, new PropertyChangedEventArgs(string.Empty));
    }
}