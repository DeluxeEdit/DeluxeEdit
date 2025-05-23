﻿using System;
using System.Collections.Generic;
using Model;
using Shared;
using System.Linq;
using System.IO;

namespace DefaultPlugins
{
    public class AllPlugins
    {
        public static T InvokePlugin<T>(PluginType plugin) where T : class
        {
            Type? myType = null;
            switch (plugin)
            {
                case PluginType.FileOpen:
                    myType = typeof(FileOpenPlugin);

                    break;
                case PluginType.UrlDecode:
                    myType = typeof(UrlDecodePlugin);
                    break;
                case PluginType.UrlEncode:
                    myType = typeof(UrlEncodePlugin);
                    break;
                case PluginType.FileSave:
                    myType = typeof(FileSavePlugin);
                    break;
                case PluginType.FileSaveAs:
                    myType = typeof(FileSaveAsPlugin);
                    break;
                case PluginType.FileNew:
                    myType = typeof(FileNewPlugin);
                    break;
                case PluginType.Hex:
                    myType = typeof(HexPlugin);
                    break;
                case PluginType.ViewAs:
                    myType = typeof(ViewAsPlugin);
                    break;

            }
            if (myType == null) throw new NullReferenceException();

            var result = InvokePlugin<T>(myType);
            return result;
        }
        public static T? InvokePlugin<T>(string type) where T : class
        {
            T? result = default;
            Type? matchedType = PluginManager.SourceFiles.SelectMany(p => p.MatchingTypes)
             .SingleOrDefault(p => p.ToString() == type);
            if (matchedType != null)
            {
                object? objectResult = Activator.CreateInstance(matchedType);
                if (objectResult != null && objectResult is INamedActionPlugin)
                    result = objectResult as T;
            }
            return result;
            ;
        }
        public static T InvokePlugin<T>(Type type) where T : class
        {
            var obj = PluginManager.CreateObject(type);
            T? result = default(T);
            if (obj != null)
            {
                if (obj is T)
                    result= obj as T;
            }
            if (result != null)
                return result;
             else 
                throw new NullReferenceException();

        }


        public static T InvokePlugins<T>(PluginItem item) where T : class
        {
            var result = InvokePlugin<T>(item.PluginType);
            return result;
        }
        public static IEnumerable<INamedActionPlugin> InvokePlugins(IEnumerable<PluginItem> items)
        {
            var result = items.Select(p => InvokePlugin(p.PluginType)).ToList();

            return result;
        }

        public static INamedActionPlugin InvokePlugin(Type type)
        {
            var result = PluginManager.CreateObject(type);
            return result;
        }
        public static INamedActionPlugin InvokePlugin(PluginItem item)
        {
            var result = InvokePlugin(item.PluginType);
            return result;
        }
        /// <summary>
        /// Can also match without the 'Plugin' part of name
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static INamedActionPlugin? InvokePlugin(string type)
        {
            INamedActionPlugin? result = null;
            var allTypes = PluginManager.SourceFiles.SelectMany(p => p.MatchingTypes);
            var match=allTypes.FirstOrDefault(p=>p.Name == type);
            if (match ==null) match = allTypes.FirstOrDefault(p => p.Name == (type+"Plugin"));

            if (match != null)
            {
                object? objecctResult = Activator.CreateInstance(match);
                if (objecctResult != null && objecctResult is INamedActionPlugin)
        
                    result = objecctResult as INamedActionPlugin;
            }
            return result;
        }
        public static INamedActionPlugin InvokePlugin(PluginType plugin)
        {
            Type? myType = null;
            switch (plugin)
            {
                case PluginType.FileOpen:
                    myType = typeof(FileOpenPlugin);

                    break;
                case PluginType.UrlDecode:
                    myType = typeof(UrlDecodePlugin);
                    break;
                case PluginType.UrlEncode:
                    myType = typeof(UrlEncodePlugin);
                    break;
                case PluginType.FileSave:
                    myType = typeof(FileSavePlugin);
                    break;
                case PluginType.FileSaveAs:
                    myType = typeof(FileSaveAsPlugin);
                    break;
                case PluginType.FileNew:
                    myType = typeof(FileNewPlugin);
                    break;
                case PluginType.Hex:
                    myType = typeof(HexPlugin);
                    break;
                case PluginType.ViewAs:
                    myType = typeof(ViewAsPlugin);
                    break;


            }
            if (myType == null) throw new NullReferenceException();

            var result = InvokePlugin(myType);
            return result;
        }

    }
}