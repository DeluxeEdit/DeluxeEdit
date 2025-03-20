using Model;
using System;
using System.IO.MemoryMappedFiles;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Shared;
using System.Linq;
using Extensions;
using CustomFileApiFile;
using System.Windows;
using System.Windows.Input;
using Constants;

namespace DefaultPlugins
{
    public class FileNewPlugin : INamedActionPlugin
    {
        public bool ParameterIsSelectedText { get; set; } = false;




        public const string VersionString = "0.2";

        public Version Version { get; set; } = new Version(VersionString);


        public ActionParameter Parameter { get; set; } = new ActionParameter();

        public Stream? InputStream { get; set; } = null;

        public bool Enabled { get; set; }



        private StreamWriter? writer;

        public bool AsReaOnly { get; set; }

        public int SortOrder { get; set; }


        public ConfigurationOptions Configuration { get; set; } = new ConfigurationOptions();
        public string Path { get; set; } = "";
        public Type Id { get; set; } = typeof(FileNewPlugin);



        public void SetConfig()
        {
            Configuration.ShowInMenu = "File";
            Configuration.ShowInMenuItem = "New";
            Configuration.KeyCommand.Keys = new List<Key> { Key.LeftCtrl, Key.N };
        }

        public FileNewPlugin()
        {
            SetConfig();
        }

        public EncodingPath? GuiAction(INamedActionPlugin instance)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<string>> Perform(IProgress<long> progress)
        {

            WritesAllPortions(progress);
            var result = await Task.FromResult(new List<string> { });
            return result;
        }


        public async Task<string> Perform(ActionParameter parameter, IProgress<long> progress)
        {
            var result = await Task.FromResult(new List<string> { });

            WritesAllPortions(progress);


            return String.Empty;
        }
        public void WritesAllPortions(IProgress<long> progress)
        {
            if (Parameter == null) throw new ArgumentNullException();

            if (!File.Exists(Parameter.Parameter)) throw new FileNotFoundException(Parameter.Parameter);
            if (writer == null)
            {
                if (Parameter == null) throw new ArgumentNullException();

                using var mmf = MemoryMappedFile.CreateFromFile(Parameter.Parameter);
                InputStream = mmf.CreateViewStream();
                writer = Parameter.Encoding == null ? new StreamWriter(InputStream) : new StreamWriter(InputStream, Parameter.Encoding);
            }
            for (int i = 0; i < Parameter.InData.Count / SystemConstants.ReadPortionBufferSizeLines; i++)
            {
                var batch = Parameter.InData.Take(SystemConstants.ReadPortionBufferSizeLines).ToList();
                WritePortion(batch, progress);
            }



        }

        public async void WritePortion(List<string> indata, IProgress<long> progress)
        {
            if (Parameter == null) throw new ArgumentNullException();

            if (!File.Exists(Parameter.Parameter)) throw new FileNotFoundException(Parameter.Parameter);




            if (writer == null)
            {
                using var mmf = MemoryMappedFile.CreateFromFile(Parameter.Parameter);
                InputStream = mmf.CreateViewStream();
                writer = Parameter.Encoding == null ? new StreamWriter(InputStream) : new StreamWriter(InputStream, Parameter.Encoding);
            }


            int lineCount = await writer.WriteLinesMax(indata, SystemConstants.ReadPortionBufferSizeLines);
            if (progress != null) progress.Report(lineCount);

            await writer.FlushAsync();





        }

    }
}