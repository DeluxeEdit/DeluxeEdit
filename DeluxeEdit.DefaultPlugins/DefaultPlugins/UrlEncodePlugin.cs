﻿using Model.Interface;
using Model;
using System;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DefaultPlugins
{
    public class UrlEncodePlugin :  INamedActionPlugin
    {
        public bool ParameterIsSelectedText { get; set; } = true;


        public Version Version { get; set; }


        public string VersionString { get; set; } = "0.2";

        public ActionParameter Parameter { get; set; }

        public EncodingPath? GuiAction(INamedActionPlugin instance) { return null; }

        public bool Enabled { get; set; }

        public char[] MyKeyCommand { get; set; } = new char[0];

        public string Id { get; set; } = "UrlEncodePlugin";
        public string Titel { get; set; } = "Url encode";
     


        public ConfigurationOptions Configuration { get; set; }
        public string Path { get; set; } = "";
        public string ClassName { get; set; } = "";

        public UrlEncodePlugin()
        {
            Configuration = new ConfigurationOptions();
            Configuration.ShowInMenu = "Plugins";
            Configuration.ShowInMenuItem = "UrlEncode";



            Version = Version.Parse(VersionString);



        }
        public object CreateControl(bool showToo)
        {
            return "";
        }






        public async Task<IEnumerable<string>> Perform()
        {
            return null;
        }





        public async Task<string> Perform(ActionParameter parameter)
        {
            var result = HttpUtility.UrlEncode(parameter.Parameter, Encoding.UTF8);
            return result;
        }
    }


}
