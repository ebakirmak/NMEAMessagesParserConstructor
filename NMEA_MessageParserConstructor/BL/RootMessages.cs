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
        protected byte MessageID { get; set; }

        protected string Description { get; set; }

        protected byte Priority { get; set; }

        protected string AccessSchema { get; set; }

        protected string CommunicationState { get; set; }

        protected string TransmittedStation { get; set; }

        protected byte RepeatIndicator { get; set; }

        protected short TotalNumberOfBits { get; set; }

        protected byte Spare { get; set; }

        public virtual string[] Parser(string message) {

            //Mesajı parçalarına ayır.
            string[] messagesPart = message.Split(',');
            //Tüm mesajı döndür.
            return messagesPart;
        }


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

        enum enumCommunicitonState
        {
            SOTDMA,
            ITDMA,
            NA
        };

        enum enumTransmittedStation
        {
            M,
            B,
            MB
        };
    }
}
