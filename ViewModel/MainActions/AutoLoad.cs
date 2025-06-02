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

            var currentAutoLoadTypePath= StartupMySettings.CurrentAutoLoadTypePath;
            if (StartupMySettings.CurrentAutoLoadType.HasValue==false ) return null;

            if (StartupMySettings.CurrentAutoLoadType.Value== AutoLoadType.Hex)
                    return await hex.AutoLoad(currentAutoLoadTypePath);
         
            return await loadFile.AutoLoad(currentAutoLoadTypePath);
         

        }

        public async void HandleÁutoLoad(string arguments)
        {
            await StartAutoLoad(arguments);

        }

    }
}