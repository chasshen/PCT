using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCT.Common;

namespace PCT.UI
{
    public interface IView
    {
        void SetController(ComController controller);
        //Open serial port event
        void OpenComEvent(Object sender, SerialPortEventArgs e);
        //Close serial port event
        void CloseComEvent(Object sender, SerialPortEventArgs e);
        //Serial port receive data event
        void ComReceiveDataEvent(Object sender, SerialPortEventArgs e);
    }
}
