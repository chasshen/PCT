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
            onedataLength = 25;
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
            if (null != copybytecache && copybytecache.Length == onedataLength)
            {
                if (datacount == 100)
                {
                    datacount = 0;
                    lsData = new List<ComDataVO>();
                }
                datacount++;
                int serialnumber = GetReceiveSerialNumber(copybytecache);
                for (int i = 0; i < GetChannelTestObjects().Count; i++)
                {
                    ChannelTestObjectVO voTest = GetChannelTestObjects()[i];
                    int tempdata = GetSomeDataFromReceiveData(copybytecache, voTest.DataStart, voTest.DataLength);
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
