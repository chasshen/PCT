using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCT.Common.Channels
{
    public class ChannelFactory
    {
        public static IChannel CreateChannelInstance(int channeltype)
        {
            IChannel channel = null;
            switch (channeltype)
            {
                case (int)ChannelType.ChannelValue.CoHe:
                    channel = new ChannelCoHe();
                    break;
            }
            return channel;
        }
    }
}
