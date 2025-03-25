using Constants;
using System.Configuration;

namespace DeluxeEdit
{
    public class MySettings
    {
        public AppInfo AppInfo { get; set; } = SystemConstants.GetAppInfo();
       // ConfigurationBuilder builder= new ConfigurationBuilder().AddXmlFile();
           
        public string? CurrentEnvironment { get; } = ConfigurationManager.AppSettings["CurrentEnvironment"];
        public MySettings()
        {
            if (CurrentEnvironment == null) CurrentEnvironment = "Debug";
            if (CurrentEnvironment != null)
            { 
                var parsed = Enum.Parse<AppEnvironment>(CurrentEnvironment);
                var appInfo = SystemConstants.GetAppInfo();
                appInfo.Environment = parsed;
                AppInfo = appInfo;
                Environment.SetEnvironmentVariable("CurrentEnvironment", CurrentEnvironment, EnvironmentVariableTarget.Process );
            }
        }

    }
}