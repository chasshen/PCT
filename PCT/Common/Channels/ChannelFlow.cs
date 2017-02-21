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
        private string _cmdstart = "F8 00 00 01 01 54";
        public ChannelFlow()
        {
            base.setCmdStart(_cmdstart);
        }

        public override void comPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            List<byte> _byteData = base.serialportutil.GetPortData();

            System.IO.StreamWriter sw = new System.IO.StreamWriter("d:\\sc22.txt", true);
            sw.WriteLine(string.Format("{0}\t{1}", System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss fff"), SerialPortUtil.ByteToHex(_byteData.ToArray())));
            sw.Close();

            if (isStopData(_byteData) == false)
            {
                _pointdata[0] = GetPointTime(_byteData);
                _pointdata[1] = GetPointData(_byteData);
            }
            base.comPort_DataReceived(sender, e);
        }

        public override int GetPointTime(List<byte> _byteData)
        {
            byte[] _bytetime = new byte[4];
            for(int i = 0; i < _bytetime.Length; i++)
            {
                _bytetime[i] = _byteData[_byteData.Count - 9 + i];
            }
            return int.Parse(SerialPortUtil.ByteToHex(_bytetime).Replace(" ",""), NumberStyles.HexNumber)/100;
            //BitConverter.ToInt32(_bytetime, 0) / 100;
        }

        public override int GetPointData(List<byte> _byteData)
        {
            byte[] _pointdata = new byte[2];
            for (int i = 0; i < _pointdata.Length; i++)
            {
                _pointdata[i] = _byteData[_byteData.Count - 5 + i];
            }
            return int.Parse(SerialPortUtil.ByteToHex(_pointdata).Replace(" ", ""), NumberStyles.HexNumber);
            //BitConverter.ToInt32(_pointdata, 0);
        }

        public override void comPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {

        }
    }
}
