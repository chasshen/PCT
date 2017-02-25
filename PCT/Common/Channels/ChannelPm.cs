using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCT.Common.Channels
{
    class ChannelPm : ChannelBase
    {
        public ChannelPm()
        {
            isRealTime = false;
        }
        protected override void InitTestObjects()
        {
            ChannelTestObjectVO voPm = new ChannelTestObjectVO();
            voPm.Name = "PM";
            voPm.DisplayName = "口压PM";
            voPm.DataStart = 11;
            voPm.DataLength = 2;
            voPm.Units = "kpa";
            GetChannelTestObjects().Add(voPm);
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
