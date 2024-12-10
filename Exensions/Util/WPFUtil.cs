﻿using Model;
using System.Windows.Controls;

namespace Extensions.Util
{

    public  static class WPFUtil
    {
        public const nint Minus1 = -1;

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

        public static TabItem? AddOrUpdateTab(string header, TabControl control, object contentControl)
        {
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
            }
            return item;
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
                      if (t != null) header = t.Header.ToString();


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