using Model;
using System;
using System.IO.MemoryMappedFiles;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Shared;
using Extensions;
using CustomFileApiFile;
using System.Windows;
using System.Linq;
using System.Windows.Input;
using Constants;

namespace DefaultPlugins
{
    public class FileOpenPlugin : INamedActionPlugin
    {
        public const string VersionString = "0.2";

        public bool ParameterIsSelectedText { get; set; } = false;


        public Version Version { get; set; } = new Version(VersionString);


        public ActionParameter Parameter { get; set; } = new ActionParameter();

        public bool Enabled { get; set; }

        private MemoryMappedViewStream? myStream = null;
        private StreamReader? reader=null;
        public bool AsReaOnly { get; set; }
    //    public Encoding? OpenEncoding { get; set; }
        public int SortOrder { get; set; }

        public ConfigurationOptions Configuration { get; set; } = new ConfigurationOptions();
        public string Path { get; set; } = "";
        public Type Id { get; set; } = typeof(FileOpenPlugin);

        public static string? OldDirectory {  get; set; }     
        public EncodingPath? GuiAction(INamedActionPlugin instance)
        {

            //if (Parameter != null) oldDir = new DirectoryInfo(Parameter.Parameter).FullName;
            var dialog = new DeluxeFileDialog();
            var result = dialog.ShowFileOpenDialog(OldDirectory);
            if (result!=null) OldDirectory =  System.IO.Path.GetDirectoryName(result.Path);
            return result;
        }


       public void SetConfig()
        {
            Configuration.ShowInMenu = "File";
            Configuration.ShowInMenuItem = "Open"; ;
            Configuration.KeyCommand.Keys = new List<Key> { Key.LeftCtrl, Key.O };
        }
        public FileOpenPlugin()
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
            Parameter= parameter;

            var lines = await ReadAllPortion(progress);         
           return String.Join(Environment.NewLine, lines);

        }

        public async Task<List<string>> ReadAllPortion(IProgress<long> progress ) 
        {
 
            var total = new List<string>();
            try
            {
                if (Parameter == null) throw new ArgumentNullException();
                if (!File.Exists(Parameter.Parameter)) throw new FileNotFoundException(Parameter.Parameter);

                if (reader == null)
                {
                    using var mmf = MemoryMappedFile.CreateFromFile(Parameter.Parameter);
                    myStream = mmf.CreateViewStream();
                    reader = Parameter.Encoding == null ? reader = new StreamReader(myStream, true) : new StreamReader(myStream, Parameter.Encoding);
                }


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
                    myStream= null;
                 }  
            }




            return total;
        }
        public async Task<List<string>?> ReadPortion(IProgress<long> progress)
        {
            if (Parameter == null) throw new ArgumentNullException();

            if (reader == null)
            {
                using var mmf = MemoryMappedFile.CreateFromFile(Parameter.Parameter);
                myStream = mmf.CreateViewStream();
                reader = Parameter.Encoding == null ? reader = new StreamReader(myStream, true) : new StreamReader(myStream, Parameter.Encoding);
            }

            if (myStream == null) throw new ArgumentNullException();




            //todo:how do I share file data between different plugins
            if (Parameter == null) throw new ArgumentNullException();
            if (!File.Exists(Parameter.Parameter)) throw new FileNotFoundException(Parameter.Parameter);

            var buffer = new char[SystemConstants.ReadBufferSizeBytes];
            var readCount = await reader.ReadBlockAsync(buffer, 0, buffer.Length);

            var blockRead=new string(buffer.Take(readCount).ToArray());
            int indexOfNUL=blockRead.IndexOf('\0');
            blockRead=blockRead.SubstringPos(0, indexOfNUL - 1);         
            var result = blockRead.Split(Environment.NewLine).ToList();


            //     var result =  await reader.ReadLinesMax(SystemConstants.ReadBufferSizeLines);

            if (progress != null )
                progress.Report(myStream.Position);


            return result;




        }

    }
}
