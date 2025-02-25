﻿using Model;
using System;
using Extensions;
using System.Threading.Tasks;
using Extensions.Util;
using System.Formats.Tar;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using DefaultPlugins;
using DefaultPlugins.PluginHelpers;
using ICSharpCode.AvalonEdit;

namespace ViewModel.MainActions
{

    public class LoadFile
    {
        public FileOpenPlugin openPlugin;
        public FileTypeLoader fileTypeLoader;
        //private MenuBuilder menuBuilder;
        public TabControl tabFiles;
        public MainEditViewModel model;
        public ProgressBar progressBar;
        private ViewAs viewAsModel;

        public TextEditor? CurrentText { get; set; }
        
        public LoadFile(MainEditViewModel model, ProgressBar progressBar, TabControl tab, ViewAs viewAsModel)
        {
            tabFiles = tab;

            this.model = model;
            this.progressBar = progressBar;
            this.viewAsModel=viewAsModel;
            fileTypeLoader = new FileTypeLoader();
            openPlugin = AllPlugins.InvokePlugin<FileOpenPlugin>(PluginType.FileOpen);

        }
        public  async Task<MyEditFile?> Load()
        {

            var action = openPlugin.GuiAction(openPlugin);
            //if user cancelled path is empty 
            if (action == null || !action.Path.HasContent()) return null;

            model.SetStatusText($" File: {action.Path}");
            model.RemoveTabFilesKeyDown();

            var parameter = new ActionParameter(action.Path, action.Encoding);

            var progress = new Progress<long>(value => progressBar.Value = value);

            var result = new MyEditFile();
            result.Path = action.Path;
            result.Content = await openPlugin.Perform(parameter, progress);

            var items = AddMyControlsForExisting(action.Path);
 //           result.Area = fileTypeLoader.CurrentArea;

            result.Text = items.Item1;
            items.Item1.Text = result.Content;
            CurrentText = items.Item1;
            result.Tab = items.Item2;
            MyEditFiles.Add(result);

            MenuBuilder.SaveMenu.IsEnabled=true;

            
             MenuBuilder.SaveAsMenu.IsEnabled = true;
     MenuBuilder.HexViewMenu.IsEnabled = true;



                viewAsModel.SetSelectedPath(result.Path);
           return result;
        }

        public void  MaximizeControl(TextEditor editor)
        { 
        }
        public Tuple<TextEditor, TabItem> AddMyControlsForExisting(string path, string? overrideTabNamePrefix = null)
        {
            var name = new FileInfo(path).Name;
            fileTypeLoader.LoadCurrent(path);
            var text = fileTypeLoader.CurrentText;
            text.IsReadOnly = false;
            text.Name = name.Replace(".", "");
            text.TextChanged += model.Text_TextChanged;
            text.KeyDown += model.Text_KeyDown;  
            text.HorizontalAlignment = HorizontalAlignment.Stretch;
            text.VerticalAlignment = VerticalAlignment.Stretch;
            name = $"{overrideTabNamePrefix}{name}";
            var tab = WPFUtil.AddOrUpdateTab(name, tabFiles, fileTypeLoader.CurrentArea);
            model.ChangeTab(tab);
            var result = new Tuple<TextEditor, TabItem>(text, tab);
            fileTypeLoader.CurrentArea.Focus();
            return result;
        }
    }
} 