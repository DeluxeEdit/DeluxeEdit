﻿using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Tests
{
    public class NugetManagerTests
    {
        public static string PluginFile = @"C:\Program Files\DeluxeEdit\plugins\DefaultPlugins.0.0.1.nupkg";

        [Fact]
        public void FileOpenPluginFileTest1()
        {
            PluginFile file = NugetManager.LoadPluginFile(PluginFile);
            Assert.Equal(file.Name, "DefaultPlugins");
            Assert.Equal(file.Version, Version.Parse("0.0.1"));
            Assert.Equal(file.LocalPath, PluginFile);
        }

        [Fact]
        public async void ReadManifestTest()
        {
            var pack = NugetManager.Create(PluginFile);
            var man = await NugetManager.ReadManifest(pack, PluginFile);
            
            Assert.True(man.Count >0);
        }

    }
}
