using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PCT.Common.Channels.ChannelType;

namespace PCT.Common.Channels
{
    class ChannelBase : IChannel
    {
        private List<ChannelTestObjectVO> testObjects = new List<ChannelTestObjectVO>();
        public ChannelBase()
        {
            InitTestObjects();
        }
        protected virtual void InitTestObjects()
        {
            throw new NotImplementedException();
        }
        public bool isRealTime { get; set; }

        protected String SpecialCmd { get; set; }

        public bool isSpecial()
        {
            return null != SpecialCmd && SpecialCmd.Equals("") == false ? true : false;
        }
        public String GetCurSpecialCmd()
        {
            string cmd = SpecialCmd;
            //指令执行过就清除
            SpecialCmd = "";
            return cmd;
        }

        public virtual ChannelCheckType GetCheckType()
        {
            return ChannelCheckType.Gain;
        }

        public List<ChannelTestObjectVO> GetChannelTestObjects()
        {
            return testObjects;
        }

        protected int onedataLength { get; set; } //接收数据的位数
        protected List<byte> bytecache = new List<byte>();
        protected bool startread = false;
        protected int datacount = 0;
        protected List<ComDataVO> lsData = new List<ComDataVO>();
        //存储当前channel的串口命令回传检测变量
        protected int checkCmdindex = 0;
        protected String[] checkCmdList = null;
        //检测串口命令回传
        protected bool CheckCmdCallback(byte thebyte)
        {
            if (null == checkCmdList)
            {
                checkCmdList = GetSendDataCmd().Split('-');
            }
            if (checkCmdList[checkCmdindex].Equals(SerialPortUtil.ByteToHex(new byte[] { thebyte })))
            {
                checkCmdindex++;
                if (checkCmdindex == checkCmdList.Length)
                {
                    return true;
                }
            }
            else
            {
                checkCmdindex = 0;
            }
            return false;
        }
        public virtual List<ComDataVO> AnalyzeComData(byte[] bytedata)
        {
            byte[] copybytecache = null;
            foreach (byte b in bytedata)
            {
                if (startread)
                {
                    bytecache.Add(b);
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
                    double temprealdata = double.Parse(GetDataFromByte(copybytecache, voTest));
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

        public virtual string GetDataFromByte(byte[] bytedata, ChannelTestObjectVO voTest)
        {
            return GetSomeDataFromReceiveData(bytedata, voTest.DataStart, voTest.DataLength).ToString();
        }

        public virtual string GetSendDataCmd()
        {
            throw new NotImplementedException();
        }

        public virtual string GetStandbyCmd()
        {
            return "F8-00-00-00-00-54";
        }

        /// <summary>
        /// 获取串口数据的次序号
        /// </summary>
        /// <param name="bytedata"></param>
        /// <returns></returns>
        protected virtual int GetReceiveSerialNumber(byte[] bytedata)
        {
            return GetSomeDataFromReceiveData(bytedata, 5, 4);
        }

        protected int GetSomeDataFromReceiveData(byte[] bytedata, int startindex, int datalength)
        {
            byte[] _bytetemp = new byte[datalength];
            for (int i = 0; i < _bytetemp.Length; i++)
            {
                _bytetemp[i] = bytedata[startindex + i];
            }
            return int.Parse(ComController.Bytes2Hex(_bytetemp).Replace("-",""), NumberStyles.HexNumber);
        }

        public virtual String CalculateRealData(double realdigit, int testobjectindex)
        {
            ChannelTestObjectVO voTO = GetChannelTestObjects()[testobjectindex];
            double gain = (voTO.GainFixData - voTO.ZeroFixData) / (voTO.GainTestData - voTO.ZeroTestData);
            double offset = voTO.GainFixData - gain * voTO.GainTestData;
            double realdata = gain * realdigit + offset;
            return realdata.ToString("F2") + "%";
        }
    }
}
