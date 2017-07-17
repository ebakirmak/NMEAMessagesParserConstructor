using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA_MessageParserConstructor.BL.CommunicationState
{
    public class SOTDMA
    {
        public SOTDMA()
        {
            this.subMessage = new SubMessage();
        }
        public byte SyncState { get; set; }
        public byte SlotTimeOut { get; set; }
        public SubMessage subMessage { get; set; }

        public class SubMessage
        {
            public int ReceivedStations { get; set; }
            public int SlotNumber { get; set; }
            public int UTCHour { get; set; }
            public int UTCMinute { get; set; }
            public int SlotOffset { get; set; }

        }
    }
}
