using CmdLine; 
using Constants;
using Model;
using System.Configuration;
using System.Security.Policy;


namespace Shared
{
    public class StartupMySettings
    {

        public AppInfo CurrentAppInfo { get; set; } = SystemConstants.GetAppInfo();
        // ConfigurationBuilder builder= new ConfigurationBuilder().AddXmlFile();
        private  string? readEnvironment  = ConfigurationManager.AppSettings.Get("CurrentEnvironment");
        public string CurrentEnvironment { get; set; } = String.Empty;
        public static string ApplicationAguments { get; set; } = String.Empty;
        private string? readPluginPath =ConfigurationManager.AppSettings.Get("PluginPath");
        public string PluginPath { get; set; } = string.Empty;
        public static AutoLoadType? CurrentAutoLoadType { get; set; } = null;
        public static string CurrentAutoLoadTypePath { get; set; } =string.Empty;

        private static void readAutoLoadType()
        {
            var hex = CommandLine.Args.Where(p => p == "autoload-hex").Select(p => p).FirstOrDefault(); 
            var file = CommandLine.Args.Where(p => p == "autoload").Select(p => p).FirstOrDefault(); 
            if (hex != null)
            {
                CurrentAutoLoadType = AutoLoadType.Hex;
                CurrentAutoLoadTypePath = hex;
            }
            else if (file != null)
            {
                CurrentAutoLoadType = AutoLoadType.File;
                CurrentAutoLoadTypePath =file; 

            }
        }
        public static void SendArguments(string arguments)
        {
            ApplicationAguments = arguments;
            var argList = arguments.Split(",").ToList();
            Exception parseExeption = new CommandLineException(arguments);
            CommandLine.CommandSeparators = new List<string> { "--" };

            var success = CommandLine.TryParse(out argList, out parseExeption);
            if (success == false) throw parseExeption;
            readAutoLoadType();
        }
        public StartupMySettings()
        {

            if (readEnvironment != null)
                CurrentEnvironment = readEnvironment;
            else
                CurrentEnvironment = SystemConstants.DefaultEnvironment.ToString();


            if (readPluginPath   != null)
                PluginPath = readPluginPath;
            else
                PluginPath = SystemConstants.DefaultPluginPath;


            if (CurrentEnvironment != CurrentAppInfo.Environment.ToString())
            {

                var parsed = Enum.Parse<AppEnvironment>(CurrentEnvironment);
                CurrentAppInfo.Environment = parsed;
            }
        }
    }
}    