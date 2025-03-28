﻿using Constants;
using System.Configuration;

namespace DeluxeEdit
{
    public class StartupMySetting
    {
        public AppInfo AppInfo { get; set; } = SystemConstants.GetAppInfo();
        // ConfigurationBuilder builder= new ConfigurationBuilder().AddXmlFile();
        public string? CurrentEnvironment { get; set; } = ConfigurationManager.AppSettings.Get("CurrentEnvironment");
        public StartupMySetting()
        {
            if (CurrentEnvironment != null)
            { 
                var parsed = Enum.Parse<AppEnvironment>(CurrentEnvironment);
                var appInfo = SystemConstants.GetAppInfo();
                appInfo.Environment = parsed;
                AppInfo = appInfo;
           
            
                        AppInfo = appInfo;
                Environment.SetEnvironmentVariable("CurrentEnvironment", CurrentEnvironment, EnvironmentVariableTarget.User);
           
            }









        }
    }
}