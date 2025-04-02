using System.Windows;

namespace DeluxeEdit;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private void Application_Startup(object sender, StartupEventArgs e)
    {
        var win = new MainWindow(String.Join(", ", e.Args.ToList() )) ;
        win.Show();

    }
}

