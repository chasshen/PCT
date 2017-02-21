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
        public ChannelType()
        {
            if(null == channeltype)
            {
                channeltype = new Hashtable();
                channeltype.Add("CO/He", ChannelValue.CoHe);
                channeltype.Add("CO/CH4/C2H2", ChannelValue.CoCh4C2h2);
                channeltype.Add("O2/CO2", ChannelValue.O2Co2);
                channeltype.Add("箱压PB", ChannelValue.Pb);
                channeltype.Add("口压PM", ChannelValue.Pm);
                channeltype.Add("环境参数Ambient", ChannelValue.Ambient);
            }
            
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
