using CrypTrackerWPF.AppContext;
using System.Windows;

namespace CrypTrackerWPF;

public partial class App : Application
{
    private Bootstrapper _bootstrapper;
    public App()
    {
        _bootstrapper = new();
    }
}