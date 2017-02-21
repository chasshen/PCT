using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCT.Common.Channels
{
    class ChannelFlow : ChannelBase
    {
        public ChannelFlow()
        {
            isRealTime = false;
        }
        protected override void InitTestObjects()
        {
            //ChannelTestObjectVO voFlow = new ChannelTestObjectVO();
            //voFlow.ShowName = "Flow";
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

                ComDataVO flowvo = new ComDataVO();
                flowvo.TimeValue = (serialnumber).ToString();
                flowvo.DataValue = GetFlowData(bytedata);
                lsData.Add(flowvo);

                ComDataVO densityvo = new ComDataVO();
                densityvo.TimeValue = (serialnumber).ToString();
                densityvo.DataValue = GetDensityData(bytedata);
                lsData.Add(densityvo);
            }                
            return lsData;
        }

        private string GetFlowData(byte[] bytedata)
        {
            return (GetSomeDataFromReceiveData(bytedata, 9, 2) + 15 * System.DateTime.Now.Second).ToString();
        }

        private string GetDensityData(byte[] bytedata)
        {
            return (GetSomeDataFromReceiveData(bytedata, 11, 2)+10*System.DateTime.Now.Second).ToString();
        }
    }
}
