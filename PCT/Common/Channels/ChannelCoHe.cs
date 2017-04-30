using System;
using System.Collections;
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
            onedataLength = 24;//11111
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
            return "F8-03-00-02-09-54";
        }      

        public override List<ComDataVO> AnalyzeComData(byte[] bytedata)
        {
            if (datacount == 0 && lsData.Count>0)
            {
                lsData = new List<ComDataVO>();
            }
            byte[] copybytecache = null;
            foreach (byte b in bytedata)
            {
                if (startread)
                {
                    if (bytecache.Count == 0)
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
                if(bytecache.Count == onedataLength)
                {
                    copybytecache = bytecache.ToArray();
                    bytecache.Clear();
                }
            }
            if (null != copybytecache && copybytecache.Length == onedataLength)
            {
                //if (datacount == 100)
                //{
                //    datacount = 0;
                //    lsData = new List<ComDataVO>();
                //}
                datacount++;
                int serialnumber = GetReceiveSerialNumber(copybytecache);
                for(int i=0;i< GetChannelTestObjects().Count; i++)
                {
                    ChannelTestObjectVO voTest = GetChannelTestObjects()[i];
                    //当所有的测试项目都添加后，新数据进来就要和对应老数据做累加
                    if (lsData.Count == GetChannelTestObjects().Count)
                    {
                        lsData[i].DataValue += double.Parse(GetDataFromByte(copybytecache, voTest));
                    }
                    else
                    {
                        ComDataVO voData = new ComDataVO();
                        voData.TimeValue = (serialnumber).ToString();
                        voData.DataValue = double.Parse(GetDataFromByte(copybytecache, voTest));
                        lsData.Add(voData);
                    }
                    if (null != ccvo.IsDebug && ccvo.IsDebug.Equals("1"))
                    {
                        System.IO.StreamWriter sw = new System.IO.StreamWriter("d:\\sc77.txt", true);
                        sw.WriteLine(string.Format("{0}\t{1}\t{2}\t{3}", System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss fff")
                            , SerialPortUtil.ByteToHex(copybytecache)
                            , double.Parse(GetDataFromByte(copybytecache, voTest))
                            , datacount)
                            );
                        sw.Close();
                    }
                }
            }
            if(datacount == 100)
            {
                for(int i = 0; i < lsData.Count; i++)
                {
                    lsData[i].DataValue = lsData[i].DataValue / 100;                 
                }

                if (null != ccvo.IsDebug && ccvo.IsDebug.Equals("1"))
                {
                    System.IO.StreamWriter sw8 = new System.IO.StreamWriter("d:\\sc88.txt", true);
                    if(lsData.Count == 2)
                    {
                        sw8.WriteLine(string.Format("{0}\t{1}\t{2}\t{3}"
                                                , System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss fff")
                                                , datacount
                                                , lsData[0].DataValue
                                                , lsData[1].DataValue)
                        );
                    }else
                    {
                        sw8.WriteLine(string.Format("{0}\t{1}\t{2}"
                                                , System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss fff")
                                                , datacount
                                                , lsData.Count)
                        );
                    }
                    
                    sw8.Close();
                }
                datacount = 0;
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
