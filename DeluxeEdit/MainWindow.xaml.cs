using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Views;

namespace DeluxeEdit;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        Content = new MainEdit();

        SizeToContent = SizeToContent.Width;
        var mySettings = new StartupMySetting();
        MinWidth = 350;
         Title= mySettings.AppInfo.ToString();

    }
}