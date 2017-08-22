using NLog;
using NMEA_MessageParserConstructor.BL.AnnexClasses;
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
        private BinaryData binaryData { get; set; }
        private Logger log;
      


        public MessageType6()
        {
            this.MessageID = 6;
            this.Description = "Adressed Binary Message";
            this.RepeatIndicator = 0;
            this.TotalNumberOfBits = 1008;
            binaryData = new BinaryData();
            this.log = LogManager.GetCurrentClassLogger();
        }

        #region Parser

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
                this.binaryData.setValue(content, 72);
            }
            catch (Exception ex)
            {
                log.Error(ex, " MessageType6 :: Parser()");
                throw;
            }


            return null;

        }
        #endregion

        #region Attributeları döndürür.
        public override List<Tuple<string, string>> getAttributesAndValues()
        {
            List<Tuple<string, string>> _listAttribute = base.getAttributesAndValues();
            List<Tuple<string, string>> _listBinaryData = this.binaryData.getAttributes();

            List<Tuple<string, string>> _attributes = new List<Tuple<string, string>> {
                  new Tuple<string, string>("Source ID",this.SourceID.ToString()),
                  new Tuple<string, string>("Sequence Number",this.SequenceNumber.ToString()),
                  new Tuple<string, string>("Destination ID",this.DestinationID.ToString()),
                  new Tuple<string, string>("Retransmit Flag",this.RetransmitFlag.ToString())
             };

            _listAttribute.AddRange(_attributes);
            foreach (var binaryData in _listBinaryData)
            {
                _listAttribute.Add(new Tuple<string, string>(binaryData.Item1, binaryData.Item2));

            }
            return _listAttribute;
        }
        #endregion

        #endregion

        #region ToString mesajını ezdik. Methodu sınıfa göre tasarladık.
        public override string ToString()
        {
            return
                "Message ID: " + this.MessageID + "\n" +
                "Repeat Indicator: " + this.RepeatIndicator + "\n" +
                "Source ID: " + this.SourceID + "\n" +
                "Sequence Number: " + this.SequenceNumber + "\n" +
                "Destination ID: " + this.DestinationID + "\n" +
                "Retransmit Flag: " + this.RetransmitFlag + "\n" +
                "Spare: " + this.Spare + "\n" +
                this.binaryData.ToString();
        }
        #endregion

      

    }
}
