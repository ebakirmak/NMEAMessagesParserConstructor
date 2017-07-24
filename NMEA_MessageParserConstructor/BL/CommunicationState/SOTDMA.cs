using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA_MessageParserConstructor.BL.CommunicationState
{
    #region SOTDMA iletişim katmanı (3.3.7.2.1 - Table 18)
    public class SOTDMA
    {
        public SOTDMA()
        {
            this.subMessage = new SubMessage();
        }
        public byte SyncState { get; set; }
        public byte SlotTimeOut { get; set; }
        public SubMessage subMessage { get; set; }

        #region İletişim katmanının alt sınıfı (3.3.7.2.1 - Table 19)
        public class SubMessage
        {
            public int ReceivedStations { get; set; }
            public int SlotNumber { get; set; }
            public int UTCHour { get; set; }
            public int UTCMinute { get; set; }
            public int SlotOffset { get; set; }

        }
        #endregion

        #region ToString() methodunu override ettik.
        public override string ToString()
        {
            return
                "Sync State: " + this.SyncState +
                "Slot Time Out: " + this.SlotTimeOut +
                "Receiver Stations: " + this.subMessage.ReceivedStations +
                "Slot Number: " + this.subMessage.SlotNumber +
                "UTC Hour: " + this.subMessage.UTCHour +
                "UTC Minute: " + this.subMessage.UTCMinute +
                "Slot Offset" + this.subMessage.SlotOffset;
        }
        #endregion
    }
    #endregion
}
