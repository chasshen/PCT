using System;
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

        public virtual List<ComDataVO> AnalyzeComData(byte[] bytedata)
        {
            List<ComDataVO> lsData = new List<ComDataVO>();
            if (bytedata.Length > 6)
            {
                int serialnumber = GetReceiveSerialNumber(bytedata);

                if (isRealTime == false && serialnumber % 100 != 0)
                {
                    return lsData;
                }

                foreach (ChannelTestObjectVO voTest in GetChannelTestObjects())
                {
                    ComDataVO voData = new ComDataVO();
                    voData.TimeValue = (serialnumber).ToString();
                    voData.DataValue = GetDataFromByte(bytedata, voTest);
                    lsData.Add(voData);
                }
            }
            return lsData;
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
