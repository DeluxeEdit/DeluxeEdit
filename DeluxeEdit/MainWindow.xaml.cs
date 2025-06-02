using Shared;
using System.Text;
using System.Windows;
using Views;

namespace DeluxeEdit;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow(string arguments = "")
    {
        InitializeComponent();

        Content = new MainEdit(arguments);

        SizeToContent = SizeToContent.Width;

        MinWidth = 350;
        var mySettings = new StartupMySettings();
        Title = mySettings.CurrentAppInfo.ToString();


    }
}