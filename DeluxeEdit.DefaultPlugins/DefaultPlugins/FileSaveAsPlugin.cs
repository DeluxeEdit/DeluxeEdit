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

namespace DefaultPlugins
{
    public class FileSaveAsPlugin : INamedActionPlugin
    {
        public bool ParameterIsSelectedText { get; set; } = false;

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

        

        public  Stream InputStream {  get; set; }
        //todo; we might have to implement setcontext for plugins   

        public bool Enabled { get; set; }
        //done:make dynamic
        public bool CanWriteMore { get { return FileSize != 0 && BytesWritten < FileSize && ContentBuffer.Count > 0; } }



        private StreamWriter? writer;

        public bool AsReaOnly { get; set; }
        public Encoding? OpenEncoding { get; set; }
        public string Id { get; set; } = "FileSaveAslugin";
        public string Titel { get; set; } = "";
        public int SortOrder { get; set; }

        public List<string> ContentBuffer;

        public ConfigurationOptions Configuration { get; set; }
        public string Path { get; set; } = "";



        public FileSaveAsPlugin()
        {
            ContentBuffer = new List<string>();
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
        public async Task<IEnumerable<string>> Perform()
        {
            return null;
        }

        public async Task<string> Perform(ActionParameter parameter)
        {
            ContentBuffer = parameter.InData.Split(Environment.NewLine).ToList();
              Parameter = parameter;
            FileSize = File.Exists(parameter.Parameter)? new FileInfo(parameter.Parameter).Length: 0;
            
            if (writer == null)
            {
                using var mmf = MemoryMappedFile.CreateFromFile(parameter.Parameter);
                InputStream= mmf.CreateViewStream();
                writer = OpenEncoding == null ?  new StreamWriter(InputStream) : new StreamWriter(InputStream, OpenEncoding);
            }

            WritesAllPortions(parameter);
            if (ContentBuffer.Count > SystemConstants.ReadBufferSizeLines) ContentBuffer.Clear();

            return "";

        }
        public void WritesAllPortions(ActionParameter parameter)
        {
       
            while (CanWriteMore)
            {
               WritePortion(parameter);
            }

            if (!CanWriteMore)
            {
                writer.Close();
                writer = null;
            }
            
        }

        public async void WritePortion(ActionParameter parameter)
        {

            if (!File.Exists(parameter.Parameter)) throw new FileNotFoundException(parameter.Parameter);
            await writer.WriteLinesMax(ContentBuffer, SystemConstants.ReadPortionBufferSizeLines);
            await writer.FlushAsync();

        }

    }

}
    