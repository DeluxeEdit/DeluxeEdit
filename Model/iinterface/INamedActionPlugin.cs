﻿
namespace Model
{

    
    
    
    public interface INamedActionPlugin
    {
        ConfigurationOptions Configuration { get; set; }


        bool ParameterIsSelectedText        { get; set; }
        bool Enabled { get; set; }
        Version Version { get; set; }

        const string VersionString = "";


        Type Id { get; set; }
        ActionParameter  Parameter { get; set; }


        Task<string> Perform(ActionParameter parameter, IProgress<long> progresss);

       Task<IEnumerable<string>> Perform(IProgress<long> progress);
         void SetConfig();

      

        EncodingPath? GuiAction(INamedActionPlugin instance);
        string Path { get; set; } 
  
        
        }

   
}
