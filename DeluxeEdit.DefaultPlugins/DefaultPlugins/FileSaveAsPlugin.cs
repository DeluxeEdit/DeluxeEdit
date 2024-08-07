﻿using Model;
using Model.Interface;
using Shared;
using System;
using System.IO;
using System.Linq;
using System.Text;
using Extensions;
using System.IO.MemoryMappedFiles;
using System.Collections.Generic;
using CustomFileApiFile;
using DeluxeEdit.DefaultPlugins.Views;
using System.Windows;
using System.Threading.Tasks;
using System.Reflection.Metadata;
using System.Data.Common;
using System.Windows.Media.Imaging;
using System.Diagnostics;
using System.Windows.Controls;

namespace DefaultPlugins
{
    public class FileSaveAsPlugin : FileSavePlugin
    {
        public new bool ParameterIsSelectedText { get; set; } = true;


        public object CreateControl(bool showToo)
        {
            object view = new MainEdit();
            Window win = null;
            var result = view;
            if (showToo)
            {
                win = new Window();
                result = win;

                win.Content = view;
                win.Show();

            }

            return result;
        }



        public Version Version { get; set; }
        public string VersionString { get; set; } = "0.2";

        public long FileSize { get; set; }
        public long BytesWritten { get; set; }

        public ActionParameter? Parameter { get; set; }



        public Stream InputStream { get; set; }
        //todo; we might have to implement setcontext for plugins   

        public bool Enabled { get; set; }
        //done:make dynamic



        private StreamWriter? writer;

        public bool AsReaOnly { get; set; }
        public Encoding? OpenEncoding { get; set; } = null;        public string Id { get; set; } = "FileSaveAslugin";
        public string Titel { get; set; } = "";
        public int SortOrder { get; set; }


        public ConfigurationOptions Configuration { get; set; }
        public string Path { get; set; } = "";



        public FileSaveAsPlugin()
        {
            //  OpenEncoding = Encoding.UTF8;
            Configuration = new ConfigurationOptions();
            Configuration.ShowInMenu = "File";
            Configuration.ShowInMenuItem = "Save As";
            Version = Version.Parse(VersionString);
        }
        public EncodingPath? GuiAction(INamedActionPlugin instance)
        {
            string oldDir = @"c:\";

            if (Parameter != null) oldDir = new DirectoryInfo(Parameter.Parameter).FullName;
            var dialog = new DeluxeFileDialog();
            var result = dialog.ShowFileSaveDialog(oldDir);
            return result;
        }
        public async Task<IEnumerable<string>> Perform(IProgress<long> progresss)
        {

            WritesAllPortions(progresss); ; ;
            return null;
        }

        public async Task<string> Perform(ActionParameter parameter, IProgress<long> progresss)
        {
            WritesAllPortions(progresss); ; ;
            return null;
        }
    }
}   