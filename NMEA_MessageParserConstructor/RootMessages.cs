using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA_MessageParserConstructor
{
    public abstract class RootMessages
    {
        public RootMessages()
        {
            this.RepeatIndicator = 0;
        }
        protected string MessageID { get; set; }

        protected string Description { get; set; }

        public string Priority { get; set; }

        protected string AccessSchema { get; set; }

        protected string CommunicationState { get; set; }

        protected string TransmittedStation { get; set; }

        protected byte RepeatIndicator { get; set; }

        protected string NumberOfBits { get; set; }

        protected int Spare { get; set; }

        enum enumPriority
        {
          PriorityOne,
          PriorityTwo,
          PriorityThree,    
          PriorityFour

        };

        enum enumAccessSchema
        {
            SOTDMA,
            RATDMA,
            CSTDMA,
            MSSA,
            FATDMA,
            ITDMA
        };

        enum enumCommunicitonState {
            SOTDMA,
            ITDMA,
            NA
        };

        enum enumTransmittedStation
        {
            M,
            B
        };
    }
}
