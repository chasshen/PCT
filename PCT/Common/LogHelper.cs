using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCT.Common
{
    class LogHelper
    {
        public static void writelog(string txt)
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter("d:\\pctlog.txt", true);
            sw.WriteLine(string.Format("{0}\t{1}", System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss fff"), txt) );
            sw.Close();
        }
    }
}
