using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCT.Common.Channels
{
    class ChannelO2Co2 : ChannelBase
    {
        public ChannelO2Co2()
        {
            isRealTime = false;
            onedataLength = 24;
        }

        protected override void InitTestObjects()
        {
            ChannelTestObjectVO voO2 = new ChannelTestObjectVO();
            voO2.Name = "O2";
            voO2.DisplayName = "O2";
            voO2.DataStart = 13;
            voO2.DataLength = 2;
            voO2.Units = "digits";
            GetChannelTestObjects().Add(voO2);

            ChannelTestObjectVO voCO2 = new ChannelTestObjectVO();
            voCO2.Name = "CO2";
            voCO2.DisplayName = "CO2";
            voCO2.DataStart = 11;
            voCO2.DataLength = 2;
            voCO2.Units = "digits";
            GetChannelTestObjects().Add(voCO2);
        }

        public override string GetSendDataCmd()
        {
            return "F8-00-00-01-06-54";
        }
        
    }
}
