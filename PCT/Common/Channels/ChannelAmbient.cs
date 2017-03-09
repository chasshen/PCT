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
            onedataLength = 10;
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

        //private List<ComDataVO> lsData = new List<ComDataVO>();
        public override List<ComDataVO> AnalyzeComData(byte[] bytedata)
        {
            byte[] copybytecache = null;
            foreach (byte b in bytedata)
            {
                if (startread)
                {
                    bytecache.Add(b);
                }
                //从收到第一个54后开始处理数据，起到忽略命令回传数据的作用
                if (startread == false && SerialPortUtil.HexToByte("54")[0] == b)
                {
                    startread = true;
                }
                if (bytecache.Count == onedataLength)
                {
                    copybytecache = bytecache.ToArray();
                    bytecache.Clear();
                }
            }
            if (null != copybytecache && copybytecache.Length == 10)
            {
                //接受温度、湿度数据时清空数据对象
                lsData = new List<ComDataVO>();
                int[] indexTO = new int[] { 0, 1 };
                foreach (int i in indexTO)
                {
                    ChannelTestObjectVO voTest = GetChannelTestObjects()[i];
                    int tempdata = GetSomeDataFromReceiveData(copybytecache, voTest.DataStart, voTest.DataLength);
                    ComDataVO voData = new ComDataVO();
                    voData.TimeValue = "1";
                    voData.DataValue = double.Parse(tempdata.ToString()) /100;
                    lsData.Add(voData);
                }
                //装载气压指令
                SpecialCmd = "F8-00-00-01-04-54";
                onedataLength = 12;
            }
            else if (null != copybytecache && copybytecache.Length == 12)
            {
                ChannelTestObjectVO voTest = GetChannelTestObjects()[2];
                int tempdata = GetSomeDataFromReceiveData(copybytecache, voTest.DataStart, voTest.DataLength);
                ComDataVO voData = new ComDataVO();
                voData.TimeValue = "1";
                voData.DataValue = double.Parse(tempdata.ToString()) / 10;
                lsData.Add(voData);
                //改回温湿度数据长度
                onedataLength = 10;
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
