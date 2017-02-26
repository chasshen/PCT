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
                if (datacount == 100)
                {
                    datacount = 0;
                    lsData = new List<ComDataVO>();
                }
                datacount++;
                int serialnumber = GetReceiveSerialNumber(bytedata);
                for (int i = 0; i < GetChannelTestObjects().Count; i++)
                {
                    ChannelTestObjectVO voTest = GetChannelTestObjects()[i];
                    int tempdata = GetSomeDataFromReceiveData(bytedata, voTest.DataStart, voTest.DataLength);
                    double temprealdata = double.Parse(((tempdata - 1638) * 50 / 13107 - 25).ToString());
                    if (lsData.Count == GetChannelTestObjects().Count)
                    {
                        lsData[i].DataValue += temprealdata;
                    }
                    else
                    {
                        ComDataVO voData = new ComDataVO();
                        voData.TimeValue = (serialnumber).ToString();
                        voData.DataValue = temprealdata;
                        lsData.Add(voData);
                    }
                }
            }
            if (datacount == 100)
            {
                for (int i = 0; i < lsData.Count; i++)
                {
                    lsData[i].DataValue = lsData[i].DataValue / 100;
                }
                return lsData;
            }
            return new List<ComDataVO>();
        }
    }
}
