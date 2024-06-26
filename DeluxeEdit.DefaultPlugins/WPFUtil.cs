﻿using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.Xml;
using System.Windows.Controls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DeluxeEdit.DefaultPlugins
{

    public  static class WPFUtil
    {
        public const nint Minus1 = -1;


        public static void AddOrUpddateTab(string header, TabControl control)
        {
            if (WPFUtil.TabÉxist(header, control) == false)
            {
                var item = new TabItem { Header = header };
                control.Items.Add(item);
            }
        }

        public static bool TabÉxist(string header, TabControl control)
        {
            var result = IndexOfText(control.Items, header) != null;
                ;
            return result;
        }
        public static int? IndexOfText(ItemCollection  collection, string text)
        {

            for (int i = 0; i < collection.Count; i++)
            { 
                string header = "";

                if (collection[i] is HeaderedItemsControl)
                {
                    header = (collection[i] as HeaderedItemsControl).Header.ToString();
                }
                 else if (collection[i] is TabItem) 
                {
                    header = (collection[i] as TabItem).Header.ToString();
                }


               if (header == text)  
               {
                    return i;
                    break;
                }
            }



            return null ;
        } 

    }
}