using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCT.Common
{
    public class ComConfigVO
    {
        private Configuration cfa;
        public ComConfigVO()
        {
            cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        }

        public String Port
        {
            get { return cfa.AppSettings.Settings["Port"].Value; }
        }

        public String BaudRates
        {
            get { return cfa.AppSettings.Settings["BaudRates"].Value; }
        }

        public String Parity
        {
            get { return cfa.AppSettings.Settings["Parity"].Value; }
        }

        public String Databits
        {
            get { return cfa.AppSettings.Settings["Databits"].Value; }
        }

        public String StopBits
        {
            get { return cfa.AppSettings.Settings["StopBits"].Value; }
        }
    }
}
