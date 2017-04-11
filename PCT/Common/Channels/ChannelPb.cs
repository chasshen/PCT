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
            onedataLength = 24;
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
            return "F8-00-00-02-05-54";
        }
        public override List<ComDataVO> AnalyzeComData(byte[] bytedata)
        {
            byte[] copybytecache = null;
            foreach (byte b in bytedata)
            {
                if (startread)
                {
                    if(bytecache.Count == 0)
                    {
                        if (b == 254)
                        {
                            bytecache.Add(b);
                        }
                    }
                    else
                    {
                        bytecache.Add(b);
                    }
                    
                }
                //从收到串口命令回传后开始处理数据，起到忽略命令回传数据的作用
                if (startread == false && CheckCmdCallback(b))
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
                    if (null != ccvo.IsDebug && ccvo.IsDebug.Equals("1"))
                    {
                        System.IO.StreamWriter sw = new System.IO.StreamWriter("d:\\sc77.txt", true);
                        sw.WriteLine(string.Format("{0}\t{1}\t{2}\t{3}", System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss fff")
                            , SerialPortUtil.ByteToHex(copybytecache)
                            , tempdata
                            , temprealdata)
                            );
                        sw.Close();
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
