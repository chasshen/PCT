using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCT.Common.Channels
{
    class ChannelPb : ChannelBase
    {
        public ChannelPb()
        {
            isRealTime = false;
        }
        protected override void InitTestObjects()
        {
            ChannelTestObjectVO voPb = new ChannelTestObjectVO();
            voPb.Name = "PB";
            voPb.DisplayName = "箱压PB";
            voPb.DataStart = 13;
            voPb.DataLength = 2;
            voPb.Units = "kpa";
            GetChannelTestObjects().Add(voPb);
        }
        public override string GetSendDataCmd()
        {
            return "F8-00-00-01-05-54";
        }
        public override List<ComDataVO> AnalyzeComData(byte[] bytedata)
        {
            List<ComDataVO> lsData = new List<ComDataVO>();
            if (bytedata.Length > 6)
            {
                int serialnumber = GetReceiveSerialNumber(bytedata);

                if (isRealTime == false && serialnumber % 100 != 0)
                {
                    return lsData;
                }

                foreach (ChannelTestObjectVO voTest in GetChannelTestObjects())
                {
                    int tempdata = GetSomeDataFromReceiveData(bytedata, voTest.DataStart, voTest.DataLength);
                    ComDataVO voData = new ComDataVO();
                    voData.TimeValue = (serialnumber).ToString();
                    voData.DataValue = ((tempdata - 1638) * 50 / 13107 - 25).ToString();
                    lsData.Add(voData);
                }
            }
            return lsData;
        }

    }
}
