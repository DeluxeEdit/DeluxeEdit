﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class CustomEventArgs : EventArgs
    {

        public EventType Type { get; set; }

        public MyEditFile? Data { get; set; } = null;
    }
}

