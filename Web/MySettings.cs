using Constants;
namespace Web
{
    public class MySettings
    {
        public AppInfo AppInfo { get; set; } = SystemConstants.GetAppInfo();
        public string CurrentEnvironment { get; } = String.Empty; 
        public MySettings()
        {
            var built = WebApplication.CreateBuilder().Build();
            CurrentEnvironment= built.Configuration.GetValue("CurrentEnvironment", "Debug");

                var parsed = Enum.Parse<AppEnvironment>(CurrentEnvironment);
                var appInfo = SystemConstants.GetAppInfo();
                appInfo.Environment = parsed;
                AppInfo = appInfo;
        }

    }
}