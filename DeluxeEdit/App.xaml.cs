using System.Configuration;
using System.Data;
using System.Windows;

namespace DeluxeEdit;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private new void Startup(object sender, StartupEventArgs e)
    {
       var win = new MainWindow(String.Join(", ", e.Args));
        win.Show();
    }
}

