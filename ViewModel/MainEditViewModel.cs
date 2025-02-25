﻿using DefaultPlugins;
using Extensions.Util;
using Extensions;
using Model;
using Shared;
using System.Windows.Controls;
using System.Windows;
using ViewModel.MainActions;
using System.Net.WebSockets;

namespace ViewModel
{
    public partial class MainEditViewModel
    {
        private ProgressBar progressBar;
      private TabControl tabFiles;
        private TextBlock statusText;
        private NewFile newFile;
        private DoWhenTextChange textChange;
        private LoadFile loadFile;
        private ParameterIsSelectedTextModel paramerIsSelectedText;
        private SaveFile saveFile;
        private HexView hex;
        private ViewAs viewAsModel;
//        private MenuItem viewAsRoot;

        //      private EventData viewData;


        private List<INamedActionPlugin> relevantPlugins;
        private MenuBuilder menuBuilder;
        private MenuItem viewAsRoot;

        public MainEditViewModel(TabControl tab, ProgressBar bar, TextBlock statusText, MenuItem viewAsRoot, MenuBuilder menuBuilder)
        {
            this.menuBuilder= menuBuilder;
            this.viewAsRoot = viewAsRoot;
;            this.progressBar = bar;
            this.viewAsModel = new ViewAs(this.progressBar, viewAsRoot);
            tabFiles = tab;
            tabFiles.SelectionChanged += TabFiles_SelectionChanged;
            tabFiles.KeyDown += TabFiles_KeyDown; ; ;
            //it's important for the initial key down
            tabFiles.Focus();  
            this.statusText = statusText;
            newFile = new NewFile(this, tab);
            textChange=new DoWhenTextChange();
            menuBuilder.AdaptToStandardMenu();
            MenuBuilder.SaveMenu.IsEnabled = false;
            MenuBuilder.SaveMenu.Click += SaveMenu_Click;
            MenuBuilder.SaveAsMenu.IsEnabled = false;
            MenuBuilder.SaveAsMenu.Click += SaveAsMenu_Click;
            MenuBuilder.HexViewMenu.IsEnabled = false;
            MenuBuilder.HexViewMenu.Click += HexViewMenu_Click; ;
            MenuBuilder.OpenMenu.Click += OpenMenu_Click;
            MenuBuilder.NewMenu.Click += NewMenu_Click; ;

            this.loadFile = new LoadFile(this, bar, tab, viewAsModel);
            this.paramerIsSelectedText= new ParameterIsSelectedTextModel(loadFile, bar);
            this.saveFile = new SaveFile(this, this.progressBar, textChange);
            this.hex = new HexView(this, this.progressBar, this.tabFiles, viewAsModel);
            relevantPlugins = AllPlugins.InvokePlugins(PluginManager.GetPluginsLocal())
                .Where(p => p.Configuration.KeyCommand.Keys.Count > 0).ToList();
              

            foreach(var item in MenuBuilder.ItemsForSelectedText)
                item.Click += ItemForSelectedText;
        }

        

        
        public void SetStatusText(string statusText)
        {
            this.statusText.Text = statusText;

        }
        public void RemoveTabFilesKeyDown()
        {
            tabFiles.KeyDown -= TabFiles_KeyDown;

        }

        public async Task<string> DoCommand(MenuItem item)
        {
            string result = String.Empty;
            var header=item!=null && item.Header!=null ? item.Header.ToString() : String.Empty;
            
            var myMenuItem = MenuBuilder.CustomMainMenu.SelectMany(p => p.MenuItems)
                 .Single(p => p != null && p.Title!=null && p.Title ==header);
           
//            var actions = new SetupMenuActions(this, tabFiles, progressBar, viewAsRoot, menuBuilder);
  //          actions.SetMenuAction(myMenuItem);
        //    if (myMenuItem.MenuActon != null)
///                await myMenuItem.MenuActon.Invoke();
  //         else
                result=await HandleOtherPlugins(myMenuItem);
            
            return result;
        }
        public async Task<string> HandleOtherPlugins(  CustomMenuItem? myMenuItem)
        {
            var progress = new Progress<long>(value => progressBar.Value = value);

            string selectedText = loadFile.CurrentText != null ? loadFile.CurrentText.SelectedText : String.Empty;
            string result=String.Empty;
            if (myMenuItem != null && myMenuItem.Plugin != null && myMenuItem.Plugin.ParameterIsSelectedText && selectedText.HasContent())
                result = await myMenuItem.Plugin.Perform(new ActionParameter(selectedText), progress);
            else if (myMenuItem != null && myMenuItem.Plugin != null && myMenuItem.Plugin.Parameter != null)
                result = await myMenuItem.Plugin.Perform(myMenuItem.Plugin.Parameter, progress);
        return result;
        }
        public void ChangeTab(TabItem item)
        {
            if (item == null) throw new NullReferenceException();
            tabFiles.SelectedItem=item;
            var header = item != null && item.Header != null ? item.Header.ToString() : String.Empty;

            MyEditFiles.Current = MyEditFiles.Files.FirstOrDefault(p => p.Header == header );

        }
    }
}