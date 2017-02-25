using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PCT.Common.Channels.ChannelType;

namespace PCT.Common.Channels
{
    class ChannelAmbient : ChannelBase
    {
        public ChannelAmbient()
        {
            isRealTime = false;
        }
        protected override void InitTestObjects()
        {
            ChannelTestObjectVO voTemperature = new ChannelTestObjectVO();
            voTemperature.Name = "Temperature";
            voTemperature.DisplayName = "温度";
            voTemperature.DataStart = 5;
            voTemperature.DataLength = 2;
            voTemperature.Units = "℃";
            GetChannelTestObjects().Add(voTemperature);

            ChannelTestObjectVO voHumidity = new ChannelTestObjectVO();
            voHumidity.Name = "Humidity";
            voHumidity.DisplayName = "湿度";
            voHumidity.DataStart = 7;
            voHumidity.DataLength = 2;
            voHumidity.Units = "%";
            GetChannelTestObjects().Add(voHumidity);

            ChannelTestObjectVO voPressure = new ChannelTestObjectVO();
            voPressure.Name = "Pressure";
            voPressure.DisplayName = "气压";
            voPressure.DataStart = 5;
            voPressure.DataLength = 2;
            voPressure.Units = "mbar";
            GetChannelTestObjects().Add(voPressure);
        }
        public override string GetSendDataCmd()
        {
            return "F8-00-00-01-0B-54";
        }

        public override ChannelCheckType GetCheckType()
        {
            return ChannelCheckType.Offset;
        }

        private List<ComDataVO> lsData = new List<ComDataVO>();
        public override List<ComDataVO> AnalyzeComData(byte[] bytedata)
        {
            if (bytedata.Length == 10)
            {
                //接受温度、湿度数据时清空数据对象
                lsData = new List<ComDataVO>();
                int[] indexTO = new int[] { 0, 1 };
                foreach (int i in indexTO)
                {
                    ChannelTestObjectVO voTest = GetChannelTestObjects()[i];
                    int tempdata = GetSomeDataFromReceiveData(bytedata, voTest.DataStart, voTest.DataLength);
                    ComDataVO voData = new ComDataVO();
                    voData.TimeValue = "1";
                    voData.DataValue = (double.Parse(tempdata.ToString()) /100).ToString("F2");
                    lsData.Add(voData);
                }
                //装载气压指令
                SpecialCmd = "F8-00-00-01-04-54";
            }
            else if (bytedata.Length == 12)
            {
                ChannelTestObjectVO voTest = GetChannelTestObjects()[2];
                int tempdata = GetSomeDataFromReceiveData(bytedata, voTest.DataStart, voTest.DataLength);
                ComDataVO voData = new ComDataVO();
                voData.TimeValue = "1";
                voData.DataValue = (double.Parse(tempdata.ToString()) / 10).ToString("F2");
                lsData.Add(voData);
            }
            if(lsData.Count == 3)
            {
                return lsData;
            }
            else
            {
                return new List<ComDataVO>();
            }            
        }

        public override String CalculateRealData(double realdigit, int testobjectindex)
        {
            ChannelTestObjectVO voTO = GetChannelTestObjects()[testobjectindex];
            double offset = voTO.ZeroFixData - voTO.ZeroTestData;
            double realdata = realdigit + offset;
            return realdata.ToString("F2") + voTO.Units;
        }
    }
}
