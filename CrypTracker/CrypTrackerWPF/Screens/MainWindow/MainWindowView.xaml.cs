using System.Windows.Controls;
using System.Windows.Navigation;

namespace CrypTrackerWPF.Screens.MainWindow;

public partial class MainWindowView : UserControl
{
    public MainWindowView()
    {
        InitializeComponent();
    }

    /*private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
        {
            FileName = e.Uri.AbsoluteUri,
            UseShellExecute = true
        });
    }*/
}