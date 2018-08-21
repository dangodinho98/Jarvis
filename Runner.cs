using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jarvis
{
    public class Runner
    {
        public static void WhatTimeIs()
        {
            Speaker.Speech(DateTime.Now.ToShortTimeString());
        }
        public static void WhatDateIs()
        {
            Speaker.Speech(DateTime.Now.ToShortDateString());
        }
    }
}
