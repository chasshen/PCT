using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCT.Common.Channels
{
    class ChannelBase : IChannel
    {
        public ChannelBase()
        {
            
        }

        public virtual string GetSendDataCmd()
        {
            return null;
        }

        public virtual string GetStandbyCmd()
        {
            return "F8-00-00-00-00-54";
        }
    }
}
