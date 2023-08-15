﻿using Model.Interface;
using Model;
using System;
using System.Net;
using System.Web;
using System.Text;

namespace DefaultPlugins
{
    public class UrlEncodePlugin :  INamedActionPlugin
    {
        public Version Version { get; set; }

        public ActionParameter Parameter { get; set; }

        public object? Control { get; set; }
        public Type? ControlType { get; set; } = null;
        public EncodingPath? GuiAction(INamedActionPlugin instance) { return null; }

        public bool Enabled { get; set; }

        public char[] MyKeyCommand { get; set; } = new char[0];

        public string Id { get; set; } = "UrlEncode";
        public string Titel { get; set; } = "Url Eeclode";
     

        public ConfigurationOptions Configuration { get; set; }
        public string Path { get; set; } = "";
        public string ClassName { get; set; } = "";

        public UrlEncodePlugin()
        {
            Configuration = new ConfigurationOptions();
            Version = Version.Parse("v0.1");

        }

        public string Perform(ActionParameter parameter)
        {
            var result = HttpUtility.UrlEncode(parameter.Parameter, Encoding.UTF8);
            return result;
        }
    }


}
