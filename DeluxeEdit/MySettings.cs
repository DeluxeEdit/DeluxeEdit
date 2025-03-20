using Constants;
using System.Configuration;

namespace DeluxeEdit
{
    public class MySettings
    {
        public AppInfo AppInfo { get; set; } = SystemConstants.GetAppInfo();
        public string? CurrentEnvironment { get; } = ConfigurationManager.AppSettings["CurrentEnvironment"];
        public MySettings()
        {
            if (CurrentEnvironment != null)
            {
                var parsed = Enum.Parse<AppEnvironment>(CurrentEnvironment);
                var appInfo = SystemConstants.GetAppInfo();
                appInfo.Environment = parsed;
                AppInfo = appInfo;
            }
        }

    }
}