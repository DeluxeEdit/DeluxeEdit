using CmdLine; 
using Constants;
using Model;
using System.Configuration;
using System.Security.Policy;


namespace Shared
{
    public class StartupMySettings
    {

        public AppInfo AppInfo { get; set; } = SystemConstants.GetAppInfo();
        // ConfigurationBuilder builder= new ConfigurationBuilder().AddXmlFile();
        public string? readEnvironment { get; set; } = ConfigurationManager.AppSettings.Get("CurrentEnvironment");
        public string CurrentEnvironment { get; set; } = String.Empty;
        public static string ApplicationAguments { get; set; } = String.Empty;
        public AutoLoadType? GetAutoLoadType()
        {


            throw new NotImplementedException();
        }
        public static void SendArguments(string arguments)
        { 
            ApplicationAguments = arguments;
             var argList= arguments.Split(",").ToList();
            Exception parseExeption = new CommandLineException(arguments);
            CommandLine.CommandSeparators = new List<string> { "--" };

            CommandLine.TryParse(out argList, out parseExeption);
            
            var arg = CommandLine.Args;

        }
        public string? PluginPath { get; set; } = ConfigurationManager.AppSettings.Get("PluginPath");
        public StartupMySettings()
        {

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
        