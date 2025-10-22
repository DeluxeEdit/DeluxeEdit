using SharpShell.SharpContextMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace ShellExtensions
{
    /// <summary>
    /// The Count Lines Context Menu Extension
    /// </summary>
    public class DeluxeHexViewExtension : SharpContextMenu
    {
        const string CommandToRun = "PyDeluxeEdit --hex";
        protected override bool CanShowMenu()
        {
            //  We will always show the menu
            return true;
        }

        protected override ContextMenuStrip CreateMenu()
        {
            //  Create the menu strip
            var menu = new ContextMenuStrip();
            
          
            //  Create a 'count lines' item
            var itemCountLines = new ToolStripMenuItem
            {
                Text = "DeluxeHexView..."
            };

            //  When we click, we'll call the 'CountLines' function
            itemCountLines.Click += (sender, args) => runProgram();

            //  Add the item to the context menu
            menu.Items.Add(itemCountLines);

            //  Return the menu
            return menu;
        }

        private void runProgram()
        {
            var selectedPath = SelectedItemPaths.FirstOrDefault();
            if (selectedPath != null)
            { 
            }
            //  Go through each fileS
                //  Count the lines
        }

    }

}
