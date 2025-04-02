using Constants;
using System.Configuration;

namespace Shared
{
    public class StartupMySetting
    {
        public AppInfo AppInfo { get; set; } = SystemConstants.GetAppInfo();
        // ConfigurationBuilder builder= new ConfigurationBuilder().AddXmlFile();
        public string? CurrentEnvironment { get; set; } = ConfigurationManager.AppSettings.Get("CurrentEnvironment");
        public string? PluginPath { get; set; } = ConfigurationManager.AppSettings.Get("PluginPath");
        public StartupMySetting()
        {
            if (CurrentEnvironment != null)
            { 
                var parsed = Enum.Parse<AppEnvironment>(CurrentEnvironment);
                var appInfo = SystemConstants.GetAppInfo();
                appInfo.Environment = parsed;
                AppInfo = appInfo;
           
            
                        AppInfo = appInfo;
                Environment.SetEnvironmentVariable("CurrentEnvironmentDeluxeEdit", CurrentEnvironment, EnvironmentVariableTarget.User);
           
            }









        }
    }
}