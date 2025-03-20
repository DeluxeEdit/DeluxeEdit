using Constants;
using DefaultPlugins;
using Extensions;
using Model;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ViewModel;

namespace ViewModel.MainActions
{
    public class ParameterIsSelectedTextModel
    {
        private LoadFile loadFile;
        private ProgressBar progressBar;

        public ParameterIsSelectedTextModel(LoadFile loadFile,ProgressBar progressBar)
        {
            this.loadFile = loadFile;
            this.progressBar=progressBar;
        }
        public async void  Invoke(object o)
        {
         var item=   o as MenuItem;
            if (item == null) throw new NullReferenceException();
            var progress = new Progress<long>(value => progressBar.Value = value);

            var menuText = item.Header.ToString();
            if (menuText == null) throw new NullReferenceException();
            
            var myPlugin = AllPlugins.InvokePlugin(menuText);
            if (myPlugin == null) throw new NullReferenceException();
            if (loadFile.CurrentText == null) throw new NullReferenceException();
            string selectedText = loadFile.CurrentText.SelectedText;
            if (selectedText.Length > SystemConstants.MinimumSelectionLengthToInvoke)
            { 
                var output= await myPlugin.Perform(new ActionParameter(selectedText), progress);
                if (output != null) loadFile.CurrentText.SelectedText=output;
            }

        }

    }
}
