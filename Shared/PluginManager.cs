﻿using DeluxeEdit.Extensions;
using Model;
using Model.Interface;
using Exensions;
using System.Data;
using System.IO;
using System.Reflection;

namespace Shared
{
    public class PluginManager
    {
        private static string pluginPath { get; set; } = "C:\\gitroot\\personal\\DeluxeEdit\\DeluxeEdit\\bin\\Debug\\net8.0-windows\\*Plugins**.dll";
        public static List<INamedActionPlugin> Instances = new List<INamedActionPlugin>();
        public static List<PluginFile> SourceFiles = new List<PluginFile>();

        static PluginManager()
        {
            //            pluginPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)}\\DeluxeEdit\\plugins";
            LoadFiles();

        }


        public static List<PluginItem> GetPluginsLocal()
        {
            var files = PluginManager.LoadFiles();
            var result = new List<PluginItem>();
            foreach (var f in files)
            {
                var items = f.MatchingTypes.Select(p => f.LocalPath.CreatePluginItem(p)).ToList();

                result.AddRange(items);
            }
            return result;

        }
        public static List<PluginFile> LoadFiles()
        {
            //            var path=Path.GetFullPath(pluginPath);
            var pos = pluginPath.LastIndexOf("\\");
            if (pos == -1) throw new Exception();
            var expression = pluginPath.Substring(pos + 1); ;
            var path = pluginPath.SubstringPos(0, pos);


            var result = Directory.GetFiles(path, expression)
            .Select(p => LoadPluginFile(p))
            .ToList();

            return result;
        }



        public static INamedActionPlugin CreateObject(Type t)
        {
            object? item = Activator.CreateInstance(t);
            var newItemCasted = item is INamedActionPlugin ? item as INamedActionPlugin : null; ;
            if (newItemCasted == null) throw new NullReferenceException();



            return newItemCasted;
        }

        public static void UnLoadPluginFile(string path)
        {

            throw new NotImplementedException(); 
        }
       



        public static PluginFile LoadPluginFile(string path)
        {
            //done:could be multiple plugis in the same, FILE

            PluginFile? ourSource = SourceFiles.FirstOrDefault(p => String.Equals(p.LocalPath, path, StringComparison.InvariantCultureIgnoreCase));

            if (ourSource == null)
            {
                ourSource = new PluginFile { LocalPath = path };
                ourSource.Assembly = Assembly.LoadFrom(path);

                SourceFiles.Add(ourSource);
            }
            else if (ourSource.Assembly != null)
            {
                ourSource.MatchingTypes = ourSource.Assembly.GetTypes()
                .Where(p => p.Name.ToString().EndsWith("Plugin"))


                .ToList();


            }
            return ourSource;
        }
    }
}