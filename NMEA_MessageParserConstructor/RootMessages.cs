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
            B,
            MB
        };

        #region VDM ve VDO mesajlarını oluşturacak.
        public void Parse(string messages)
        {
            //Mesajı parçalarına ayır.
            string[] messagesPart = messages.Split(',');


        }
        #endregion
    }
}
