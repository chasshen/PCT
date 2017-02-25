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
                case (int)ChannelType.ChannelValue.CoCh4C2h2:
                    channel = new ChannelCoCh4C2h2();
                    break;
                case (int)ChannelType.ChannelValue.O2Co2:
                    channel = new ChannelO2Co2();
                    break;
                case (int)ChannelType.ChannelValue.Pb:
                    channel = new ChannelPb();
                    break;
                case (int)ChannelType.ChannelValue.Pm:
                    channel = new ChannelPm();
                    break;
                case (int)ChannelType.ChannelValue.Ambient:
                    channel = new ChannelAmbient();
                    break;

            }
            return channel;
        }
    }
}
