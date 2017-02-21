using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Windows.Forms;

namespace PCT.Common
{
    /// <summary>
    /// 串口开发辅助类
    /// </summary>
    public class SerialPortUtil
    {      

        #region 构造函数    
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public SerialPortUtil()
        {
            
        } 

	    #endregion

        #region 常用的列表数据获取和绑定操作

        /// <summary>
        /// 封装获取串口号列表
        /// </summary>
        /// <returns></returns>
        public static string[] GetPortNames()
        {
            return SerialPort.GetPortNames();
        }

        /// <summary>
        /// 设置串口号
        /// </summary>
        /// <param name="obj"></param>
        public static void SetPortNameValues(ComboBox obj)
        {
            obj.Items.Clear();
            foreach (string str in SerialPort.GetPortNames())
            {
                obj.Items.Add(str);
            }
        }

        /// <summary>
        /// 设置波特率
        /// </summary>
        public static void SetBauRateValues(ComboBox obj)
        {
            obj.Items.Clear();
            foreach (SerialPortBaudRates rate in Enum.GetValues(typeof(SerialPortBaudRates)))
            {
                obj.Items.Add(((int)rate).ToString());
            }
        }

        /// <summary>
        /// 设置数据位
        /// </summary>
        public static void SetDataBitsValues(ComboBox obj)
        {
            obj.Items.Clear();
            foreach (SerialPortDatabits databit in Enum.GetValues(typeof(SerialPortDatabits)))
            {
                obj.Items.Add(((int)databit).ToString());
            }
        }

        /// <summary>
        /// 设置校验位列表
        /// </summary>
        public static  void SetParityValues(ComboBox obj)
        {
            obj.Items.Clear();
            foreach (string str in Enum.GetNames(typeof(Parity)))
            {
                obj.Items.Add(str);
            }
            //foreach (Parity party in Enum.GetValues(typeof(Parity)))
            //{
            //    obj.Items.Add(((int)party).ToString());
            //}
        }

        /// <summary>
        /// 设置停止位
        /// </summary>
        public static void SetStopBitValues(ComboBox obj)
        {
            obj.Items.Clear();
            foreach (string str in Enum.GetNames(typeof(StopBits)))
            {
                obj.Items.Add(str);
            }
            //foreach (StopBits stopbit in Enum.GetValues(typeof(StopBits)))
            //{
            //    obj.Items.Add(((int)stopbit).ToString());
            //}   
        }

        #endregion

        #region 格式转换
        /// <summary>
        /// 转换十六进制字符串到字节数组
        /// </summary>
        /// <param name="msg">待转换字符串</param>
        /// <returns>字节数组</returns>
        public static byte[] HexToByte(string msg)
        {
            msg = msg.Replace(" ", "");//移除空格

            //create a byte array the length of the
            //divided by 2 (Hex is 2 characters in length)
            byte[] comBuffer = new byte[msg.Length / 2];
            for (int i = 0; i < msg.Length; i += 2)
            {
                //convert each set of 2 characters to a byte and add to the array
                comBuffer[i / 2] = (byte)Convert.ToByte(msg.Substring(i, 2), 16);
            }

            return comBuffer;
        }

        /// <summary>
        /// 转换字节数组到十六进制字符串
        /// </summary>
        /// <param name="comByte">待转换字节数组</param>
        /// <returns>十六进制字符串</returns>
        public static string ByteToHex(byte[] comByte)
        {
            StringBuilder builder = new StringBuilder(comByte.Length * 3);
            foreach (byte data in comByte)
            {
                builder.Append(Convert.ToString(data, 16).PadLeft(2, '0').PadRight(3, ' '));
            }

            return builder.ToString().ToUpper();
        }
        #endregion

        /// <summary>
        /// 检查端口名称是否存在
        /// </summary>
        /// <param name="port_name"></param>
        /// <returns></returns>
        public static bool Exists(string port_name)
        {
            foreach (string port in SerialPort.GetPortNames()) if (port == port_name) return true;
            return false;
        }

        /// <summary>
        /// 格式化端口相关属性
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public static string Format(SerialPort port)
        {
            return String.Format("{0} ({1},{2},{3},{4},{5})", 
                port.PortName, port.BaudRate, port.DataBits, port.StopBits, port.Parity, port.Handshake);
        }
    }    


    /// <summary>
    /// 串口数据位列表（5,6,7,8）
    /// </summary>
    public enum SerialPortDatabits : int
    {
        FiveBits = 5,
        SixBits = 6,
        SeventBits = 7,
        EightBits = 8
    }

    /// <summary>
    /// 串口波特率列表。
    /// 75,110,150,300,600,1200,2400,4800,9600,14400,19200,28800,38400,56000,57600,
    /// 115200,128000,230400,256000
    /// </summary>
    public enum SerialPortBaudRates : int
    {
        BaudRate_75 = 75,
        BaudRate_110 = 110,
        BaudRate_150 = 150,
        BaudRate_300 = 300,
        BaudRate_600 = 600,
        BaudRate_1200 = 1200,
        BaudRate_2400 = 2400,
        BaudRate_4800 = 4800,
        BaudRate_9600 = 9600,
        BaudRate_14400 = 14400,
        BaudRate_19200 = 19200,
        BaudRate_28800 = 28800,
        BaudRate_38400 = 38400,
        BaudRate_56000 = 56000,
        BaudRate_57600 = 57600,
        BaudRate_115200 = 115200,
        BaudRate_128000 = 128000,
        BaudRate_230400 = 230400,
        BaudRate_256000 = 256000
    }
}
