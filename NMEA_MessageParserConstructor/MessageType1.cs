using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA_MessageParserConstructor
{
    public class MessageType1:RootMessages
    {
        public MessageType1()
        {
            this.MessageID = 1;
            this.Description = "Scheduled position report";
            this.RepeatIndicator = 0;
            this.Priority = 1;
            this.TotalNumberOfBits = 168;
            
        }
        //MMSI
        private int UserID { get; set; }
        private byte NavigationalStatus { get; set; }
        private byte RateOfTurnROTAIS { get; set; }
        private float SOG { get; set; }
        private byte PositionAccuracy { get; set; }
        private float Longitude { get; set; }
        private float Latitude { get; set; }
        private float COG { get; set; }
        private int TrueHeading { get; set; }
        private byte TimeStamp { get; set; }
        private int SpecialManoeuvreIndicator { get; set; }
        private int RAIMFlag { get; set; }
        

    }
}
