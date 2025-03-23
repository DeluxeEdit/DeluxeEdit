using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class PluginItem
    {
        public Type PluginType { get; set; } = typeof(PluginItem);

        public string Id { get; set; } = String.Empty;
        public Version FileVersion  { get; set; }=new Version();
     
        public bool Enabled { get; set; }
        public string FilePath { get; set; } = String.Empty;

        public PluginItem()
        {
            
        }
        public override string ToString()
        {
            return "${Id} v.{Version} {MyType }";
        }
    };
}
