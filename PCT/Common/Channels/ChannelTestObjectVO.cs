using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCT.Common.Channels
{
    public class ChannelTestObjectVO
    {
        public ChannelTestObjectVO()
        {

        }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public int DataStart { get; set; }
        public int DataLength { get; set; }
        public string Units { get; set; }
        //零点数据
        private double[] zerodata = new double[2] { 0.00, 0.00 };
        public double ZeroTestData
        {
            get { return zerodata[0]; }
            set { zerodata[0] = value; }
        }
        public double ZeroFixData
        {
            get { return zerodata[1]; }
            set { zerodata[1] = value; }
        }
        //增益数据
        private double[] gaindata = new double[2] { 0.00, 0.00 };
        public double GainTestData
        {
            get { return gaindata[0]; }
            set { gaindata[0] = value; }
        }
        public double GainFixData
        {
            get { return gaindata[1]; }
            set { gaindata[1] = value; }
        }
        //真实数据
        //public double RealGainData { get; set; }
        //public double RealZeroData { get; set; }
    }
}
