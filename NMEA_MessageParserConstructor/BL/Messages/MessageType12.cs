using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA_MessageParserConstructor.BL.Messages
{
    public class MessageType12 : RootMessages
    {

        private int SourceID { get; set; }
        private byte SequenceNumber { get; set; }
        private int DestinationID { get; set; }
        private byte RetransmitFlag { get; set; }
        private string SafetyRelatedText { get; set; }

        public MessageType12()
        {
            this.MessageID = 12;
            this.TotalNumberOfBits = 1008;
            this.Description = "Adressed Safety Related Message";
        }

        #region Mesaj yapısında bulunan attributelara, alınan mesajdaki değerleri set ettik.
        public override string[] Parser(string message1,string message2)
        {
            string[] messageParts1 = base.Parser(message1);
            string content = "";
            //Context'i oku. Binary yapıda.
            if (messageParts1 != null)
               content = getContentBinary(messageParts1[5], Remove(messageParts1[6]));

            string[] messageParts2 = base.Parser(message2);
            //Context'i oku. Binary yapıda.
            if(messageParts2 != null)
                content += getContentBinary(messageParts2[5], Remove(messageParts2[6]));


            //Tüm mesajlarda olan özellikleri burada gir.

            //MessageID
            this.MessageID = Convert.ToByte(getDecimalFromBinary(content, 0, 6));
            //Repeat Indicator
            this.RepeatIndicator = Convert.ToByte(getDecimalFromBinary(content, 6, 2));
            //SourceID -- MMSI
            this.SourceID = Convert.ToInt32(getDecimalFromBinary(content, 8, 30));
            //Sequence Number
            this.SequenceNumber = Convert.ToByte(getDecimalFromBinary(content, 38, 2));
            //Destination ID
            this.DestinationID = Convert.ToInt32(getDecimalFromBinary(content, 40, 30));
            //Retransmit Flag
            this.RetransmitFlag = Convert.ToByte(getDecimalFromBinary(content, 70, 1));
            //Spare
            this.Spare = Convert.ToByte(getDecimalFromBinary(content, 71, 1));
            //Safety Related Text
            this.SafetyRelatedText = Convert.ToString(getStringFromBinary(content, 72, 36));

            return null;
        }
        #endregion

        #region ToString() methodunu override ettik.
        public override string ToString()
        {
            return
                "Message ID: " + this.MessageID + "\n" +
                "RepeatIndicator: " + this.RepeatIndicator + "\n" +
                "Source ID: " + this.SourceID + "\n" +
                "Sequnece Number: " + this.SequenceNumber + "\n" +
                "Destination ID: " + this.DestinationID + "\n" +
                "Retransmit Flag: " + this.RetransmitFlag + "\n" +
                "Spare: " + this.Spare + "\n" +
                "Safety Related Text: " + this.SafetyRelatedText + "\n";

         
        }
        #endregion

    }
}
