using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCT.Common.Channels
{
    class ChannelCoHe : ChannelBase
    {
        public ChannelCoHe()
        {
            isRealTime = false;
        }

        protected override void InitTestObjects()
        {
            ChannelTestObjectVO voCo = new ChannelTestObjectVO();
            voCo.Name = "CO";
            voCo.DisplayName = "CO";
            voCo.DataStart = 9;
            voCo.DataLength = 2;
            GetChannelTestObjects().Add(voCo);

            ChannelTestObjectVO voHe = new ChannelTestObjectVO();
            voHe.Name = "He";
            voHe.DisplayName = "He";
            voHe.DataStart = 11;
            voHe.DataLength = 2;
            GetChannelTestObjects().Add(voHe);
        }
        public override string GetSendDataCmd()
        {
            return "F8-00-00-01-01-54";
        }

        public override List<ComDataVO> AnalyzeComData(byte[] bytedata)
        {
            List<ComDataVO> lsData = new List<ComDataVO>();
            if (bytedata.Length == 14)
            {
                int serialnumber = GetReceiveSerialNumber(bytedata);

                if (isRealTime == false && serialnumber % 100 != 0)
                {
                    return lsData;
                }

                foreach(ChannelTestObjectVO voTest in GetChannelTestObjects())
                {
                    ComDataVO voData = new ComDataVO();
                    voData.TimeValue = (serialnumber).ToString();
                    voData.DataValue = GetDataFromByte(bytedata, voTest);
                    lsData.Add(voData);                    
                }
            }
            return lsData;
        }

        private string GetDataFromByte(byte[] bytedata, ChannelTestObjectVO voTest)
        {
            return (GetSomeDataFromReceiveData(bytedata, voTest.DataStart, voTest.DataLength) + voTest.DataStart * System.DateTime.Now.Second).ToString();
        }
    }
}
