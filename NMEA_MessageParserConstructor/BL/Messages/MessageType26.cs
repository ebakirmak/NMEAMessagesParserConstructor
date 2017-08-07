using NLog;
using NMEA_MessageParserConstructor.BL.CommunicationState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA_MessageParserConstructor.BL.Messages
{
    public class MessageType26 : RootMessages
    {
        private int SourceID { get; set; }
        private byte DestinationIndicator { get; set; }
        private byte BinaryDataFlag { get; set; }
        private int DestinationID { get; set; }
        private string BinaryData { get; set; }
        private string BinaryData2 { get; set; }
        private string BinaryData3 { get; set; }
        private string BinaryData4 { get; set; }
        private string BinaryData5 { get; set; }
        private string Spare2 { get; set; }
        private byte CommunicationStateSelectorFlag { get; set; }
        private SOTDMA Sotdma { get; set; }
        private ITDMA Itdma { get; set; }
        private Logger log { get; set; }

        public MessageType26()
        {
            this.MessageID = 26;
            this.Description = "Multiple slot binary message with communications state";
            this.TotalNumberOfBits = 1064;
            this.log = LogManager.GetCurrentClassLogger();

        }


    }
}
