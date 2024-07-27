using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using Caliburn.Micro;
using CrypTrackerWPF.Models.LocalizationExtensions;
using CrypTrackerWPF.Screens.ShellWindow;

namespace CrypTrackerWPF.Screens.MainWindow;

public sealed class MainWindowViewModel : Screen
{
    public override string DisplayName 
    { 
        get => TranslationSource.Instance[Replicas.MainWindowTitle];
    }
    
    public MainWindowViewModel()
    {
        
    }
    
    public void Submit()
    {
        
    }
}