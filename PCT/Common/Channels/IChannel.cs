using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCT.Common.Channels
{
    public interface IChannel
    {
        String GetSendDataCmd();
        String GetStandbyCmd();
        List<ComDataVO> AnalyzeComData(byte[] bytedata);
        List<ChannelTestObjectVO> GetChannelTestObjects();
    }
}
