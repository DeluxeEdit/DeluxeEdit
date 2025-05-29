
using Constants;
using Extensions;
using Extensions.Util;
using Model;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Shared
{
    public class PluginManager
    {
        private static string pluginPath { get; set; } = SystemConstants.DefaultPluginPath;
        private static string arguments { get; set; } = string.Empty;

        public static List<INamedActionPlugin> Instances = new List<INamedActionPlugin>();
        public static List<PluginFile> SourceFiles = new List<PluginFile>();

        static  PluginManager()
        {
            var mySettings = new StartupMySettings();
           
            if (mySettings.PluginPath!=null) 
                pluginPath = mySettings.PluginPath;

            LoadFiles();

//            GetPluginsRemote();

        }

        public static List<PluginItem> GetPluginsRemote()
        {
            var result = new List<PluginItem>();
            var outputItems = WPFUtil.ShellExecuteWithOutput("nuget.exe", $"search {SystemConstants.NugetPackageStartName}").ToList();
            var wanted = outputItems.Where(p => p.StartsWith(">")).Select(p => p.Substring(1).Trim()).ToList();
            foreach (string read in wanted)
            {
                var splitted = read.Split("|");
                var name = splitted[0].Trim(); ;
                var ver = splitted[1].Trim(); ;
                //read    "runtime.opensuse.13.2-x64.runtime.native.System.Security.Cryptography.OpenSsl | 4.3.3 | Downloads: 1ÿ959ÿ016ÿ175"  string

                result.Add(
                    new PluginItem
                    {
                        Id = name,

                        FileVersion = Version.Parse(ver)
                        
                    }
                );
            }




            return result;
        }
        public static void NugetInstallMyPackages()
        {
            var myPlugins = GetPluginsRemote();
            foreach (var plugin in myPlugins) 
                WPFUtil.ShellExecuteWithOutput("nuget.exe", $"install´{plugin.Id}").ToList();
            



        }

        public static List<PluginItem> GetPluginsLocal()
        {
            var files = PluginManager.LoadFiles();
            var result = new List<PluginItem>();
            foreach (var file in files)
            {
                var items = file.MatchingTypes.Select(p => file.LocalPath.CreatePluginItem(file.Version ,p)).ToList();

                result.AddRange(items);
            }
            return result;

        }
        public static List<PluginFile> LoadFiles()
        {
            //            var path=Path.GetFullPath(pluginPath);
            /*
            var pos = pluginPath.LastIndexOf("\\");

            if (pos == -1) throw new Exception();
            var expression = pluginPath.Substring(pos + 1); ;
            var path = pluginPath.SubstringPos(0, pos);
            ´*/


            var result = Directory.GetFiles(pluginPath, SystemConstants.WantedPluginFileFilter)
            .Select(p => RefreshPluginFile(p))
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

       



        public static PluginFile RefreshPluginFile(string path)
        {
            //done:could be multiple plugis in the same, FILE

            PluginFile? ourSource = SourceFiles.FirstOrDefault(p => String.Equals(p.LocalPath, path, StringComparison.InvariantCultureIgnoreCase));

            if (ourSource == null)
            {
                ourSource = new PluginFile { LocalPath = path };
                Parallel.For(0, 1, i =>
                {

                    ourSource.Assembly = Assembly.LoadFrom(path);

                });
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