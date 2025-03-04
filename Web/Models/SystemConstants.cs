using Web.Models;
using Webs.Models;

namespace Web.Models{
    public class SystemConstants
    {
        public const char NullCharacter = '\0';
        public const Char ControlKey = (char)136;
        public const int ReadPortionBufferSizeLines = 8;
        public const int ReadBufferSizeLines = 32;
        public const int ReadBufferSizeBytes = 32 * 1024;
        public const int MinimumSelectionLengthToInvoke = 1;
        public const string AppName = "DeluxeEdit";
        public readonly static string ApplicationPath  = $"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)}\\DeluxeEdit";
        public readonly static string PluginPath = $"{ApplicationPath}\\plugins";
        public readonly static Version AppVersion = new Version("1.0");
        public static AppInfo GetAppInfo()
        {
            var result = new AppInfo { Environment = AppEnvironment.Debug, Name = SystemConstants.AppName, Version = SystemConstants.AppVersion };
            return result;   

        }
    }
}