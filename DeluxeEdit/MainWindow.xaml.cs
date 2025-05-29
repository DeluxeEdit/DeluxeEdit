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
    public MainWindow(string argumments = "")
    {
        InitializeComponent();

        Content = new MainEdit(argumments);

        SizeToContent = SizeToContent.Width;
        var mySettings = new StartupMySettings(argumments);
        MinWidth = 350;
         Title= mySettings.AppInfo.ToString();

    }
}