﻿using DeluxeEdit.DefaultActions.Actions;
using DeluxeEdit.Interface;
using System;
using System.Collections.Generic;

namespace DeluxeEdit.Shared
{
    public static class ActionList
    {
//todo: use variation MVC whith long running state</li>
//todo: use notepad++-pluging</li>
//  todo: Create INamedActiom
        public static List<INamedAction> GetAllDefaultActions()
        {
            var result = new List<INamedAction>()
           {
               new FileOpenAction{ ShortCutCommand=new List<char> {SystemConstants.ControlKey,'o'  }   },
               new UrlDecodeAction {     },
               new UrlEncodeAction {     }

           };
            return result;
            
        }

    }
}
