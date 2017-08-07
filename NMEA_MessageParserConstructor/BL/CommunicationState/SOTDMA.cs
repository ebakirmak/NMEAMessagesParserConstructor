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

       
        #region Attributeları döndürür.
        public List<Tuple<string, string>> getAttributes()
        {
            List<Tuple<string, string>> _listAttribute = new List<Tuple<string, string>>
            {
                new Tuple<string, string>("Sync State",this.SyncState.ToString()),
                new Tuple<string, string>("Slot Time Out",this.SlotTimeOut.ToString()),              
                new Tuple<string, string>("Received Stations",this.subMessage.ReceivedStations.ToString()),
                new Tuple<string, string>("Slot Number",this.subMessage.SlotNumber.ToString()),
                new Tuple<string, string>("UTC Hour",this.subMessage.UTCHour.ToString()),
                new Tuple<string, string>("UTC Minute",this.subMessage.UTCMinute.ToString()),
                new Tuple<string, string>("Slot Offset",this.subMessage.SlotOffset.ToString())
            };          
            return _listAttribute;
        }
        #endregion

    }
    #endregion
}
