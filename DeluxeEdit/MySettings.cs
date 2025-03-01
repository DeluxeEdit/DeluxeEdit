using Extensions.Model;
using Shared;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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