﻿using Model.Interface;
using Model;
using System;

using Extensions;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using Shared;
using System.Windows.Controls;
using DeluxeEdit.DefaultPlugins;
using MS.WindowsAPICodePack.Internal;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace DefaultPlugins.ViewModel
{
    public class NewFileViewModel
    {
        private INamedActionPlugin plugin;
        private TabControl currentTab;

        public NewFileViewModel(TabControl tab)
        {
            plugin = AllPlugins.InvokePlugin(PluginType.FileNew);
            currentTab = tab;
        }
        public TextBox AddNewTextControlAndSubscribe(string filename)
        {

            WPFUtil.AddOrUpddateTab(filename, currentTab);

            var text = new TextBox();
            text.Name = Path.GetFileNameWithoutExtension(filename);
            text.KeyDown += Text_KeyDown;
            currentTab.Items.Add(text);

            return text;
        }

        public MyEditFile GetNewFile()
        {
               var result = new MyEditFile { Header = "newfile.txt", Content = "", IsNewFile=true };

            MyEditFiles.Files.Add(new MyEditFile { Header = result.Header });
            var text=AddNewTextControlAndSubscribe (result.Header);
            MyEditFiles.Add(result); 
               


            return result;


        }
        private void Text_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            var keyeddata = KeyDown();
            if (keyeddata == null) e.Handled = false;
            else
            {


                e.Handled = true;
            }
        }
        public MyEditFile? KeyDown()
        {
            //done:cast enum from int
            MyEditFile result = null;
            bool keysOkProceed = false;
            var matchCount = plugin.Configuration.KeyCommand.Keys
                .Cast<System.Windows.Input.Key>()
                .Count(p => System.Windows.Input.Keyboard.IsKeyDown(p));

            keysOkProceed = matchCount == plugin.Configuration.KeyCommand.Keys.Count && plugin.Configuration.KeyCommand.Keys.Count > 0;
            if (keysOkProceed) result = GetNewFile();


            return result;


        }
    }
}