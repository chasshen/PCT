using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCT.Common.Channels
{
    class ChannelBase : IChannel
    {
        protected SerialPortUtil serialportutil;
        protected string _defaulthexcmdstop = "F8 00 00 00 00 54";
        private byte[] cmdstart;
        private byte[] cmdstop;
        protected int[] _pointdata = new int[2];

        public event DataReceivedEventHandler DataReceived;
        public delegate void DataReceivedEventHandler(DataReceivedEventArgs e);
        public class DataReceivedEventArgs : EventArgs
        {
            public int[] DataReceived;
            public DataReceivedEventArgs(int[] m_DataReceived)
            {
                this.DataReceived = m_DataReceived;
            }
        }

        public ChannelBase()
        {
            Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            serialportutil = new SerialPortUtil(cfa.AppSettings.Settings["Port"].Value
                                                , cfa.AppSettings.Settings["BaudRates"].Value
                                                , cfa.AppSettings.Settings["Parity"].Value
                                                , cfa.AppSettings.Settings["Databits"].Value                                                
                                                , cfa.AppSettings.Settings["StopBits"].Value);
            serialportutil.ComPortInstance.DataReceived += new SerialDataReceivedEventHandler(comPort_DataReceived);
            serialportutil.ComPortInstance.ErrorReceived += new SerialErrorReceivedEventHandler(comPort_ErrorReceived);
            cmdstop = SerialPortUtil.HexToByte(_defaulthexcmdstop);
        }

        protected void setCmdStart(string cmd)
        {
            cmdstart = SerialPortUtil.HexToByte(cmd);
        }

        protected void setCmdStop(string cmd)
        {
            cmdstop = SerialPortUtil.HexToByte(cmd);
        }

        public void start()
        {
            serialportutil.OpenPort();
            serialportutil.WriteData(cmdstart);
        }

        public void stop()
        {
            serialportutil.WriteData(cmdstop);
        }

        public bool isStopData(List<byte> _byteData)
        {
            byte[] lastsixbyte = new byte[6];
            for(int i = 0; i < 6; i++)
            {
                lastsixbyte[i] = _byteData[_byteData.Count - 6 + i];
            }
            if (SerialPortUtil.ByteToHex(lastsixbyte).Equals(_defaulthexcmdstop))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 数据接收处理
        /// </summary>
        public virtual void comPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (DataReceived != null)
            {
                DataReceived(new DataReceivedEventArgs(_pointdata));
            }
        }

        /// <summary>
        /// 错误处理函数
        /// </summary>
        public virtual void comPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {

        }

        public virtual int GetPointTime(List<byte> _byteData)
        {
            return 0;
        }

        public virtual int GetPointData(List<byte> _byteData)
        {
            return 0;
        }

        ///<summary>Convert a string of hex digits (ex: E4 CA B2) to a byte array. </summary>  
        ///<param name="s">The string containing the hex digits (with or without spaces).</param>  
        ///<returns>Returns an array of bytes.</returns>  
        //protected byte[] HexStringToByteArray(string s)
        //{
        //    s = s.Replace("   ", " ");
        //    string[] hexs = s.Split(' ');
        //    byte[] buffer = new byte[hexs.Length];
        //    for (int i = 0; i<hexs.Length; i++){
        //        buffer[i] = (byte)Convert.ToByte(hexs[i], 16);
        //    }
        //    return buffer;
        //}
    }
}
