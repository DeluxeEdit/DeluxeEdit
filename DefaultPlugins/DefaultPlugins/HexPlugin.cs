using Model;
using System;
using System.IO.MemoryMappedFiles;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Shared;
using System.Windows;
using System.Linq;
using CustomFileApiFile;
using System.Windows.Input;
using Constants;

namespace DefaultPlugins
{
    public class HexPlugin : INamedActionPlugin
    {

        public bool ParameterIsSelectedText { get; set; } = false;
        public const string VersionString = "0.2";


        public Version Version { get; set; } = new Version(VersionString);
     


        public ActionParameter Parameter { get; set; } = new ActionParameter();

        public bool Enabled { get; set; }

        private MemoryMappedViewStream? myStream = null;
        private StreamReader? reader;
        public bool AsReaOnly { get; set; }=true;
        public int SortOrder { get; set; }

        public ConfigurationOptions Configuration { get; set; } = new ConfigurationOptions();
        public string Path { get; set; } = "";
       public string Title ="A plugin where you can view file as hexadecimal format";

        public Type Id { get; set; } = typeof(HexPlugin);

        public EncodingPath? GuiAction(INamedActionPlugin instance)
        {
            string oldDir = @"c:\";

            //if (Parameter != null) oldDir = new DirectoryInfo(Parameter.Parameter).FullName;
            var dialog = new DeluxeFileDialog();
            var result = dialog.ShowFileOpenDialog(oldDir);
            return result;
        }





        public void SetConfig()
        {
            Configuration.ShowInMenu = "File";
            Configuration.ShowInMenuItem = "Hex View"; ;
            Configuration.KeyCommand.Keys = new List<Key> { Key.LeftCtrl, Key.H };
            Version = Version.Parse(VersionString);
        }
        public HexPlugin()
        {
            SetConfig();
        }


        public async Task<IEnumerable<string>> Perform(IProgress<long> progress)
        {
            var result = await ReadAllPortion(progress);
            return result;
        }

        public async Task<string> Perform(ActionParameter parameter, IProgress<long> progress)
        {
            if (parameter == null) throw new ArgumentNullException();
            Parameter = parameter;

            var lines = await ReadAllPortion(progress);
            return String.Join(Environment.NewLine, lines);

        }

        public async Task<List<string>> ReadAllPortion(IProgress<long> progress)
        {
            var total = new List<string>();

            try
            {

                         if (Parameter == null) throw new ArgumentNullException();
                if (File.Exists(Parameter.Parameter) == false) throw new FileNotFoundException(Parameter.Parameter);

                if (reader == null)
                {
                    using var mmf = MemoryMappedFile.CreateFromFile(Parameter.Parameter);
                    myStream = mmf.CreateViewStream();


                    reader = Parameter.Encoding == null ? reader = new StreamReader(myStream, true) : new StreamReader(myStream, Parameter.Encoding);
                }
                if (myStream == null) throw new ArgumentNullException();

                long fileSize = new FileInfo(Parameter.Parameter).Length;
                for (int i = 0; i <= fileSize / SystemConstants.ReadBufferSizeBytes; i++)
                {
                    var result = await ReadPortion(progress);
                    if (result != null)
                        total.AddRange(result);

                }
            }
            finally
            { 
                if (reader != null)
                {
                    reader.Close();
                    reader = null;
                }
                if (myStream != null)
                {
                    myStream.Close();
                    myStream = null;
                }
            }

            return total;
        }
        public async Task<List<string>?> ReadPortion(IProgress<long> progress)
        {
            if (Parameter == null) throw new ArgumentNullException();
           
            if (File.Exists(Parameter.Parameter) == false) throw new FileNotFoundException(Parameter.Parameter);

            if (reader == null)
            {
                using var mmf = MemoryMappedFile.CreateFromFile(Parameter.Parameter);
                myStream = mmf.CreateViewStream();
                reader = Parameter.Encoding == null ? reader = new StreamReader(myStream, true) : new StreamReader(myStream, Parameter.Encoding);
            }

            if (myStream == null) throw new ArgumentNullException();

            var oldBuffer = new byte[SystemConstants.ReadBufferSizeBytes];
            var readCount = await myStream.ReadAsync(oldBuffer);
            var buffer = oldBuffer.Take(readCount).ToArray();

            var result = new List<string>();
            var sb= new StringBuilder();

            foreach (byte b in buffer)
            {
                if (b == SystemConstants.NullCharacter) break;
                sb.AppendFormat("{0:x2} ", b);
            }
             
            string myOutput = sb.ToString().ToUpper().TrimStart().TrimEnd();


            result.Add(myOutput);
            //todo:how do I share file data between different plugins
            if (progress != null)
                progress.Report(myStream.Position);


            return result;
        
           


        }

 }
}
