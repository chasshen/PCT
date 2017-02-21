using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCT.Common.Channels
{
    public class ChannelType
    {
        public enum ChannelValue
        {
            CoHe,
            CoCh4C2h2,
            O2Co2,
            Pb,
            Pm,
            Ambient
        }

        private static Hashtable channeltype = null;
        //private static Hashtable channeltestobjects = null;
        public ChannelType()
        {
            if (null == channeltype)
            {
                channeltype = new Hashtable();
                channeltype.Add("CO/He", ChannelValue.CoHe);
                channeltype.Add("CO/CH4/C2H2", ChannelValue.CoCh4C2h2);
                channeltype.Add("O2/CO2", ChannelValue.O2Co2);
                channeltype.Add("箱压PB", ChannelValue.Pb);
                channeltype.Add("口压PM", ChannelValue.Pm);
                channeltype.Add("环境参数Ambient", ChannelValue.Ambient);
            }
            //if (null == channeltestobjects)
            //{
            //    channeltestobjects = new Hashtable();
            //    channeltestobjects.Add(ChannelValue.CoHe, new String[] { "CO", "He" });
            //    channeltestobjects.Add(ChannelValue.CoCh4C2h2, new String[] { "CO", "CH4", "C2", "H2" });
            //    channeltestobjects.Add(ChannelValue.O2Co2, new String[] { "O2", "CO2" });
            //    channeltestobjects.Add(ChannelValue.Pb, new String[] { "PB" });
            //    channeltestobjects.Add(ChannelValue.Pm, new String[] { "PM" });
            //    channeltestobjects.Add(ChannelValue.Ambient, new String[] { "CO", "He" });
            //}
        }

        public List<String> GetNames()
        {
            List<String> lsNames = new List<string>();
            foreach(DictionaryEntry de in channeltype)
            {
                lsNames.Add(de.Key.ToString());
            }
            return lsNames;
        }

        public List<int> GetValues()
        {
            List<int> lsValues = new List<int>();
            foreach (DictionaryEntry de in channeltype)
            {
                lsValues.Add((int)de.Value);
            }
            return lsValues;
        }

        public int GetValueFromName(String name)
        {
            if (channeltype.Contains(name))
            {
                return (int)channeltype[name];
            }
            return -1;
        }
    }
}
