using NMEA_MessageParserConstructor.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA_MessageParserConstructor
{
    public class MessageType1:MessagePacket
    {
        public MessageType1()
        {
            this.MessageID = 1;
            this.Description = "Scheduled position report";
            this.RepeatIndicator = 0;
            this.Priority = 1;
            this.TotalNumberOfBits = 168;
            this.sotdma = new SOTDMA();
        }

        //SOTDMA Parametreleri
        public class SOTDMA
        {
            public SOTDMA()
            {
                subMessage = new SubMessage();
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

        //MMSI
        private int UserID { get; set; }
        private byte NavigationalStatus { get; set; }
        private float RateOfTurnROTAIS { get; set; }
        private float SOG { get; set; }
        private byte PositionAccuracy { get; set; }
        private float Longitude { get; set; }
        private float Latitude { get; set; }
        private float COG { get; set; }
        private int TrueHeading { get; set; }
        private byte TimeStamp { get; set; }
        private int SpecialManoeuvreIndicator { get; set; }
        private byte RAIMFlag { get; set; }
        private SOTDMA sotdma;
       
        public override string[] Parser(string message)
        {
            string[] messages = base.Parser(message);
            string content = getContentBinary(messages[5]);
            //MessageID
            this.MessageID = Convert.ToByte(getSubstringFromBinary(content, 0, 6));
            //Repeat indicator
            this.RepeatIndicator = Convert.ToByte(getSubstringFromBinary(content, 6, 2));
            //Source MMSI
            this.UserID = Convert.ToInt32(getSubstringFromBinary(content, 8, 30));
            //NavigationalStatus - Nav Status
            this.NavigationalStatus = Convert.ToByte(getSubstringFromBinary(content, 38, 4));
            //RateOfTurnROTAIS - ROT
            this.RateOfTurnROTAIS = float.Parse(getSubstringFromBinary(content, 42, 8));
            //SOG - **** 10'a böldük. Doküman.
            this.SOG = float.Parse(getSubstringFromBinary(content, 50, 10))/10;
            //PA
            this.PositionAccuracy = Convert.ToByte(getSubstringFromBinary(content, 60, 1));
            //LON
            this.Longitude = float.Parse(getSubstringFromBinary(content, 61, 28));
            //LAT
            this.Latitude = float.Parse(getSubstringFromBinary(content, 89, 27));
            //COG **** 10'a böldük. Doküman.
            this.COG = float.Parse(getSubstringFromBinary(content, 116, 12)) / 10;
            //True Heaading
            this.TrueHeading = Convert.ToInt32(getSubstringFromBinary(content, 128, 9));
            //Time stamp
            this.TimeStamp = Convert.ToByte(getSubstringFromBinary(content, 137, 6));
            //Spe Man
            this.SpecialManoeuvreIndicator = Convert.ToInt32(getSubstringFromBinary(content, 143, 2));
            //Spare
            this.Spare = Convert.ToByte(getSubstringFromBinary(content, 145, 3));
            //RAIM
            this.RAIMFlag = Convert.ToByte(getSubstringFromBinary(content, 148, 1));
            //CommunicitionState            
            this.sotdma.SyncState= Convert.ToByte(getSubstringFromBinary(content, 149, 2));
            this.sotdma.SlotTimeOut = Convert.ToByte(getSubstringFromBinary(content, 151, 3));
            this.CommunicationState = Convert.ToString(getSubstringFromBinary(content, 154, 14));
            return null;
        }
    }
}
