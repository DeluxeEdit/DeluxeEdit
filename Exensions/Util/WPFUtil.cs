using Model;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Extensions.Util
{

    public  static class WPFUtil
    {

        public static string ShellExecuteWithOutput(string command)
        {
            Process p = new Process();

            // Redirect the output stream of the child process.
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = command;
            p.Start();
            // Do not wait for the child process to exit before
            // reading to the end of its redirected stream.
            // p.WaitForExit();
            // Read the output stream first and then wait.
            string result = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            return result;
        }
        public static MenuItem? GetMenuItemForStartText(MenuItem menuItem, string startText)
        {
            MenuItem? result = null;
            string? headerString = null;
            object? header = null;
            if (menuItem != null) header = menuItem.Header;

            if (header != null) headerString = header.ToString();
            if (menuItem != null && headerString != null && headerString.StartsWith(startText, StringComparison.CurrentCultureIgnoreCase))
            {
                result = menuItem;
            }
            return result;
        }

        public static string FileTypeToExtension(FileType type)
        {
            string result = String.Empty;
            switch (type)
            {

                case FileType.Boo:
                    result = ".boo";
                    break;
                case FileType.CocoR:
                    result = ".r";
                    break;
                case FileType.CPP:
                    result = ".cpp";
                    break;
                case FileType.CS:
                    result = ".cs";
                    break;
                case FileType.HTML:
                    result = ".htm";
                    break;
                case FileType.Java:
                    result = ".java";
                    break;
                case FileType.JavaScript:
                    result = ".js";
                    break;
                case FileType.PatchFiles:
                    result = ".patch";
                    break;
                case FileType.PHP:
                    result = ".php";
                    break;
                case FileType.TeX:
                    result = ".tex";
                    break;
                case FileType.VB:
                    result = ".vb";
                    break;
                case FileType.XML:
                    result = ".xml";
                    break;
            }

            return result;

        }

        public static TabItem AddOrUpdateTab(string header, TabControl control, object contentControl)
        {
            TabItem result= new TabItem();
            TabItem? item = null;
            var index = IndexOfText(control.Items, header);
            if (index.HasValue)
                item =control.Items[index.Value] is TabItem ? control.Items[index.Value] as TabItem : null;
            else
            {
                item = new TabItem();
                control.Items.Add(item);

            }
            if (item != null)
            {
                item.Header = header;

                item.Content = contentControl;
             //   item.MinHeight = 500;

            //    item.MinWidth = 500;
                item.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            item.VerticalContentAlignment = System.Windows.VerticalAlignment.Stretch;

            item.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch;
            item.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;

            }
      if (item !=null) result=item;
      return result;
        }


        public static bool TabÉxist(string header, TabControl control)
        {
            var result = IndexOfText(control.Items, header) != null;
                ;
            return result;
        }
        public static int? IndexOfText(ItemCollection  collection, string text)
        {
            int? result = null;

            for (int i = 0; i < collection.Count; i++)
            { 
                string? header = "";

                if (collection[i] is HeaderedItemsControl)
                {
                    var obj = collection[i] is HeaderedItemsControl ? collection[i] as HeaderedItemsControl : null;


                     header=obj != null && obj.Header!=null ? obj.Header.ToString() :null;
                }
                else if (collection
                    [i] is TabItem)
                {
                    var t = collection[i] as TabItem;
                      if (t != null && t.Header!=null) header = t.Header.ToString();


                }

               if (header == text)  
               {
                   result = i;
                        ;
                        break;
                }
            }


            return result ;
        } 

    }
}