using DefaultPlugins;
using DefaultPlugins.PluginHelpers;
using Extensions;
using Extensions.Util;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Rendering;
using Model;
using Shared;
using System.Formats.Tar;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
//busing Microsoft.Extensions.Configuration;

namespace ViewModel.MainActions
{
    public class AutoLoad
    {
        private LoadFile loadFile;
        private HexView hex;

        public AutoLoad(LoadFile loadFile, HexView
            hex)
        {
            this.loadFile = loadFile;
            this.hex = hex;
         
        }


        public async Task<MyEditFile?> StartAutoLoad(string arguments)
        {
            var settings = new StartupMySettings();
            settings.GetAutoLoadType();
            // var parser = new CommandLine();
            string path = String.Empty;
            string action = String.Empty;
            var split = arguments.Split(" ");
            if (split.Length == 0)
                return null;

            if (split.Length > 0)
                path = split[0];
            if (split.Length > 1)
                action = split[1];

            if (path.HasContent() && action.Equals("hex", StringComparison.OrdinalIgnoreCase))
                return await hex.AutoLoad(path);
            if (path.HasContent())
                return await loadFile.AutoLoad(path);
            else
                return null;


        }

        public async void HandleÁutoLoad(string arguments)
        {
            await StartAutoLoad(arguments);

        }

    }
}