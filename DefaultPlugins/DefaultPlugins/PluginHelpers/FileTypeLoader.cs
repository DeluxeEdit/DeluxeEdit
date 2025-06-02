using Model;
using System.Collections.Generic;
using System.Linq;
using Extensions.Util;
using System;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Editing;
using ICSharpCode.AvalonEdit.Document;
using System.IO;
using System.Collections.ObjectModel;
using Extensions;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using System.Reflection.Metadata;
using ICSharpCode.AvalonEdit.Rendering;
using Constants;
using System.Threading.Tasks;

namespace DefaultPlugins.PluginHelpers
{
    public class FileTypeLoader
    {
        

        //public static List<FileTypeItem> AllFileTypes { get; set; }
        public static string CurrentPath { get; set; } = string.Empty;
        public TextEditor CurrentText { get; set; } = new TextEditor();
        public TextDocument CurrentDocument { get; set; } = new TextDocument();

       // public TextArea CurrentArea { get; set; } = new TextEditor().TextArea;
        static FileTypeLoader()
        {
            
            var registration = new HighlightingRegistrationItem();
            registration.Name = "LogFile";
            registration.Extensions.Add(".log");
            registration.PathToDefinition = SystemConstants.LogFileDefinitionPath;
      LoadDefinitionFromFile(registration);
            RegisterDefinition(registration);
            
        }



        public void LoadCurrent(string path)
        {
            var manager = HighlightingManager.Instance;
            


            var definition = manager.GetDefinitionByExtension(new FileInfo(path).Extension);
            CurrentText = new TextEditor();


     
            var CurrentArea = CurrentText.TextArea;
            CurrentArea.MinHeight = 500;
            CurrentArea.MinWidth = 1000;
            CurrentArea.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            CurrentArea.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            CurrentArea.VerticalContentAlignment = System.Windows.VerticalAlignment.Stretch;
            CurrentArea.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch;
            //CurrentArea.ActiveInputHandler
            // CurrentArea.DefaultInputHandler.Detach();
            //CurrentArea.DefaultInputHandler.Attach();
            //            CurrentArea.MinHeight = 500 ;
            //          CurrentArea.MinWidth = 1000;


            //CurrentText.SyntaxHighlighting = "C#";
            //CurrentDocument= CurrentText.Document;




            CurrentText.SyntaxHighlighting = definition;


            CurrentPath = path;
        }



        public static void LoadDefinitionFromFile(HighlightingRegistrationItem registrationItem)
        {
            using var reader = XmlReader.Create(registrationItem.PathToDefinition);
            Parallel.For(0, 1, i =>
            {
                registrationItem.Definition = HighlightingLoader.Load(reader, HighlightingManager.Instance);
            });
        }
        public static void RegisterDefinition(HighlightingRegistrationItem registrationItem)       
        {
            var manager = HighlightingManager.Instance;
            manager.RegisterHighlighting(registrationItem.Name, registrationItem.Extensions.ToArray() ,registrationItem.Definition);
        }
    }
}
