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
            voCo.DataStart = 19;
            voCo.DataLength = 2;
            voCo.Units = "digits";
            GetChannelTestObjects().Add(voCo);

            ChannelTestObjectVO voHe = new ChannelTestObjectVO();
            voHe.Name = "He";
            voHe.DisplayName = "He";
            voHe.DataStart = 17;
            voHe.DataLength = 2;
            voHe.Units = "digits";
            GetChannelTestObjects().Add(voHe);
        }
        public override string GetSendDataCmd()
        {
            return "F8-00-00-00-09-54";
        }

        public override List<ComDataVO> AnalyzeComData(byte[] bytedata)
        {            
            if (bytedata.Length == 24)
            {
                if (datacount == 100)
                {
                    datacount = 0;
                    lsData = new List<ComDataVO>();
                }
                datacount++;
                int serialnumber = GetReceiveSerialNumber(bytedata);
                for(int i=0;i< GetChannelTestObjects().Count; i++)
                {
                    ChannelTestObjectVO voTest = GetChannelTestObjects()[i];
                    if (lsData.Count == GetChannelTestObjects().Count)
                    {
                        lsData[i].DataValue += double.Parse(GetDataFromByte(bytedata, voTest));
                    }
                    else
                    {
                        ComDataVO voData = new ComDataVO();
                        voData.TimeValue = (serialnumber).ToString();
                        voData.DataValue = double.Parse(GetDataFromByte(bytedata, voTest));
                        lsData.Add(voData);
                    }
                }
            }
            if(datacount == 100)
            {
                for(int i = 0; i < lsData.Count; i++)
                {
                    lsData[i].DataValue = lsData[i].DataValue / 100;
                }
                return lsData;
            }
            return new List<ComDataVO>();
        }

        public override string GetDataFromByte(byte[] bytedata, ChannelTestObjectVO voTest)
        {
            //return (GetSomeDataFromReceiveData(bytedata, voTest.DataStart, voTest.DataLength) + voTest.DataStart * System.DateTime.Now.Second).ToString();
            return GetSomeDataFromReceiveData(bytedata, voTest.DataStart, voTest.DataLength).ToString();
        }

        public override String CalculateRealData(double realdigit, int testobjectindex)
        {
            ChannelTestObjectVO voTO = GetChannelTestObjects()[testobjectindex];
            double gain = (voTO.GainFixData - voTO.ZeroFixData) / (voTO.GainTestData - voTO.ZeroTestData);
            double offset = voTO.GainFixData - gain * voTO.GainTestData;
            double realdata = gain * realdigit + offset;
            return realdata.ToString("F2")+"%";
        }
    }
}
