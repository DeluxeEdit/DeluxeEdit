using Constants;
using System.Configuration;
//using CommandLineParser;

namespace Shared
{
    public class StartupMySettings
    {
        public AppInfo AppInfo { get; set; } = SystemConstants.GetAppInfo();
        // ConfigurationBuilder builder= new ConfigurationBuilder().AddXmlFile();
        public string? readEnvironment { get; set; } = ConfigurationManager.AppSettings.Get("CurrentEnvironment");
        public string CurrentEnvironment { get; set; } = String.Empty;


        public string? PluginPath { get; set; } = ConfigurationManager.AppSettings.Get("PluginPath");
        public StartupMySettings(string arguments)
        {
//            var parser = new CommandLineArgumentsParser();

            if (readEnvironment != null)
                CurrentEnvironment = readEnvironment;
            else
                CurrentEnvironment = SystemConstants.DefaultEnvironment;



                var parsed = Enum.Parse<AppEnvironment>(CurrentEnvironment);
                var appInfo = SystemConstants.GetAppInfo();
                appInfo.Environment = parsed;
                AppInfo = appInfo;

            


        }
    }
}
        