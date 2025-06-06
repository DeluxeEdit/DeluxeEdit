﻿namespace Constants 
{ 
    public class SystemConstants
    {
        public const string GithubProjectUrl = "https://github.com/DeluxeEdit/DeluxeEdit";
        public const string WantedPluginFileFilter  = "*Plugins*.dll";



        public const char NullCharacter = '\0';
        public const Char ControlKey = (char)136;
        public const int ReadPortionBufferSizeLines = 8;
        public const int ReadBufferSizeLines = 32;
        public const int ReadBufferSizeBytes = 32 * 1024;
        public const int MinimumSelectionLengthToInvoke = 1;
        public const string AppName = "DeluxeEdit";
        public const string NugetPackageStartName = $"{AppName}.";
        public readonly static string DefaultPluginPath = "."; 
        public readonly static Version AppVersion = new Version("1.0");
        public const AppEnvironment DefaultEnvironment = AppEnvironment.Production;
        public const string LogFileDefinitionPath = "./DefaultPlugins/PluginHelpers/LogFileDefinition.xshd";


        public static AppInfo GetAppInfo()
        {
            var result = new AppInfo { Environment = AppEnvironment.Production, Name = SystemConstants.AppName, Version = SystemConstants.AppVersion };
            return result;   

        }
    }
}