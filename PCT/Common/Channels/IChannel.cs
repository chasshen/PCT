using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCT.Common.Channels
{
    interface IChannel
    {
        void start();
        void stop();
        //SerialPortUtil getSerialPort();
    }
}
