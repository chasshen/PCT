﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCT.Common.Channels
{
    public class ChannelTestObjectVO
    {
        public ChannelTestObjectVO()
        {

        }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public int DataStart { get; set; }
        public int DataLength { get; set; }
    }
}