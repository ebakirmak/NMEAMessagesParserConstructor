using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA_MessageParserConstructor.BL.Messages
{
    public class MessageType6:RootMessages
    {
        
        private int SourceID { get; set; }
        private byte SequenceNumber { get; set; }
        private int DestinationID { get; set; }
        private byte RetransmitFlag { get; set; }
        private byte Spare { get; set; }
        private BinaryData binaryData { get; set; }
        private Logger log;
      


        public MessageType6()
        {
            this.MessageID = 6;
            this.RepeatIndicator = 0;
            this.TotalNumberOfBits = 1008;
            binaryData = new BinaryData();
            this.log = LogManager.GetCurrentClassLogger();
        }
        #region BinaryData ' yı saklamak için kullanılıyor.
        class BinaryData
        {
            public int DAC { get; set; }
            public int FID { get; set; }
            public string Data { get; set; }

        }
        #endregion

        #region Mesaj yapısında bulunan attributelara, alınan mesajdaki değerleri set ettik.
        public override string[] Parser(string message)
        {
            try
            {
                string [] messageParts = base.Parser(message);
                string content = getContentBinary(messageParts[5],Remove(messageParts[6]));

                //Message ID
                this.MessageID = Convert.ToByte(getDecimalFromBinary(content, 0, 6));

                //Repeat Indicator
                this.RepeatIndicator = Convert.ToByte(getDecimalFromBinary(content, 6, 2));

                //Source ID - UserID
                this.SourceID = Convert.ToInt32(getDecimalFromBinary(content, 8, 30));

                //Sequence Number
                this.SequenceNumber = Convert.ToByte(getDecimalFromBinary(content, 38, 2));

                //Destination ID
                this.DestinationID = Convert.ToInt32(getDecimalFromBinary(content, 40, 30));

                //Retransmit Flag
                this.RetransmitFlag = Convert.ToByte(getDecimalFromBinary(content, 70, 1));

                //Spare
                this.Spare = Convert.ToByte(getDecimalFromBinary(content, 71, 1));

                //Binary Data
                this.binaryData.DAC = Convert.ToInt32(getDecimalFromBinary(content,72, 10));
                this.binaryData.FID = Convert.ToInt32(getDecimalFromBinary(content, 82, 6));
                //HATA VAR!
                this.binaryData.Data = Convert.ToString(getDecimalFromBinary(content, 88, 5));
            }
            catch (Exception ex)
            {
                log.Error(ex, " MessageType6 :: Parser()");
                throw;
            }


            return null;

        }
        #endregion

        #region ToString mesajını ezdik. Methodu sınıfa göre tasarladık.
        public override string ToString()
        {
            return 
                "Message ID: "+   this.MessageID + "\n" +
                "Repeat Indicator: " + this.RepeatIndicator + "\n" +
                "Source ID: " + this.SourceID + "\n" +
                "Sequence Number: " + this.SequenceNumber + "\n" +
                "Destination ID: " + this.DestinationID + "\n" +
                "Retransmit Flag: " + this.RetransmitFlag + "\n" +
                "Spare: " + this.Spare + "\n" +
                "DAC: " + this.binaryData.DAC + "\n" +
                "FID: " + this.binaryData.FID + "\n" +
                "Data: " + this.binaryData.Data;
        }
        #endregion

    }
}
