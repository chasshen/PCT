using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            
        }        
        public bool isRealTime { get; set; }

        public List<ChannelTestObjectVO> GetChannelTestObjects()
        {
            return testObjects;
        }

        public virtual List<ComDataVO> AnalyzeComData(byte[] bytedata)
        {
            return null;
        }

        public virtual string GetSendDataCmd()
        {
            return null;
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
    }
}
