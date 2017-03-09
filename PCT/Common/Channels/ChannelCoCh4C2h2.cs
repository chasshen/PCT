using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCT.Common.Channels
{
    class ChannelCoCh4C2h2 : ChannelBase
    {
        public ChannelCoCh4C2h2()
        {
            isRealTime = false;
            onedataLength = 26;
        }

        protected override void InitTestObjects()
        {
            ChannelTestObjectVO voCo = new ChannelTestObjectVO();
            voCo.Name = "CO";
            voCo.DisplayName = "CO";
            voCo.DataStart = 19;
            voCo.DataLength = 2;
            voCo.Units = "digits";
            GetChannelTestObjects().Add(voCo);

            ChannelTestObjectVO voHe = new ChannelTestObjectVO();
            voHe.Name = "CH4";
            voHe.DisplayName = "CH4";
            voHe.DataStart = 17;
            voHe.DataLength = 2;
            voHe.Units = "digits";
            GetChannelTestObjects().Add(voHe);

            ChannelTestObjectVO voC2h2 = new ChannelTestObjectVO();
            voC2h2.Name = "C2H2";
            voC2h2.DisplayName = "C2H2";
            voC2h2.DataStart = 21;
            voC2h2.DataLength = 2;
            voC2h2.Units = "digits";
            GetChannelTestObjects().Add(voC2h2);
        }

        public override string GetSendDataCmd()
        {
            return "F8-00-00-01-0A-54";
        }
        
    }
}
