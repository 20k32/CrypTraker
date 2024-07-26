using System.Windows;
using System.Windows.Data;

namespace CrypTrackerWPF.Models.LocalizationExtensions;

public sealed class LocalizationBinding : Binding
{
    public LocalizationBinding(PropertyPath path) : base('[' + path.Path + ']')
    {
        Mode = BindingMode.OneWay;
        Source = TranslationSource.Instance;
    }
}