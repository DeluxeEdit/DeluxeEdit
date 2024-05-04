﻿
using Model.Interface;
using System;

namespace Model
{

    public class CustomMenuItem
    {
        public INamedActionPlugin? Plugin { get; set; }= null;
        public Type? MyType { get; set; }

        public string Title { get; set; } = "";
        public ActionParameter Parameter { get; set; } = new ActionParameter();
    }
}