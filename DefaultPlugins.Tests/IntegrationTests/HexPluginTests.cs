﻿using System.IO;
using System.Text;
using DefaultPlugins;
using Model;
using Xunit;

namespace DeluxeEdit.DefaultPlugins.Tests.IntegrationTests
{
    public class HexPluginTests
    {
        public static string TestFile = "TestFiles/TextFile1.txt";

        [Fact]
        public async Task HexOpenFileTest()
        {
            var plugin = AllPlugins.InvokePlugin<HexPlugin>(PluginType.Hex);
            if (plugin==null)  return;
            var expected = "EF BB BF 6E 69 6E 6A 61 C3 A5 C3 A4 C3 96";
            string actual=String.Empty;
            var p = new ActionParameter(TestFile, Encoding.UTF8);
            actual =await plugin.Perform(
                p, new Progress<long>()) ;
            Assert.Equal(expected, actual);
        }
    }
}