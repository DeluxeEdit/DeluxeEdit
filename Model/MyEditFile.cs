﻿using System;
namespace Model
{
  public class MyEditFile
    {
        public string BufferPath { get { return Path+".buff"; } }
        public string Path { get; set; } = "";
        public string Content { get; set; } = "";
        pSublic string Header { get; set; } = "";
        public bool  IsNewFile { get; set; } 
        public object? Text { get; set; }
        public object? Tab { get; set; }
    }



}
