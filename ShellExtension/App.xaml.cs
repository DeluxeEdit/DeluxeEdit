using Extensions.Util;
using System.Configuration;
using System.Data;
using System.Windows;

namespace ShellExtension
{
 
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            WPFUtil.ShellExecute("powershell.exe", "$path =Get-ChildItem -Recurse -Filter \"elevate.ps1\" | Select-Object -Property Directory");
        }
    }

}
