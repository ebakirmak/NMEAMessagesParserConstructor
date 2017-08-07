using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA_MessageParserConstructor.BL.Messages
{
    public class MessageType25 : RootMessages
    {
        private int SourceID { get; set; }
        private byte DestinationIndicator { get; set; }
        private byte BinaryDataFlag { get; set; }
        private int DestinationID { get; set; }
        private string BinaryData { get; set; }
        private Logger log{ get; set; }

        public MessageType25()
        {
            this.MessageID = 25;
            this.Description = "Single slot binary message";
            this.TotalNumberOfBits = 168;
            this.log = LogManager.GetCurrentClassLogger();
        }


        #region Mesaj yapısında bulunan attributelara, alınan mesajdaki değerleri set ettik.
        public override string[] Parser(string message1)
        {
            try
            {
                string[] messageParts1 = base.Parser(message1);
                //Context'i oku. Binary yapıda.
                string content = getContentBinary(messageParts1[5], Remove(messageParts1[6]));
                //Tüm mesajlarda olan özellikleri burada gir.
                //MessageID
                this.MessageID = Convert.ToByte(getDecimalFromBinary(content, 0, 6));
                //Repeat indicator
                this.RepeatIndicator = Convert.ToByte(getDecimalFromBinary(content, 6, 2));
                //Source ID
                this.SourceID = Convert.ToInt32(getDecimalFromBinary(content, 8, 30));
                //Destination Indicator
                this.DestinationIndicator = Convert.ToByte(getDecimalFromBinary(content, 38, 1));
                //Binary Data Flag
                this.BinaryDataFlag = Convert.ToByte(getDecimalFromBinary(content, 39, 1));
                //Destination ID
                if (DestinationIndicator == 1)
                    this.DestinationID = Convert.ToInt32(getDecimalFromBinary(content, 40, 30));
                //Spare
                this.Spare = Convert.ToByte(getDecimalFromBinary(content, 70, 2));
                //Binary Data -- HATA VAR DÜZELT
                if (DestinationIndicator == 0)
                    this.BinaryData = Convert.ToString(getStringFromBinary(content, 72, content.Length - 72));
                else if (DestinationIndicator == 1)
                    this.BinaryData = Convert.ToString(getStringFromBinary(content, 72, content.Length - 72));
              

            }
            catch (Exception ex)
            {
                log.Error(ex, "MessageType25 :: Parser");
                //throw;
            }

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
                "Destination Indicator: " + this.DestinationIndicator + "\n" +
                "Binary Data Flag: " + this.BinaryDataFlag + "\n" +
                "Destination ID: " + this.DestinationID + "\n" +
                "Spare: " + this.Spare + "\n" +
                "Binary Data" + this.BinaryData;
              



        }
        #endregion

        #region Attributeları döndürür.
        //new Tuple<string, string>("",this..ToString()),
        public override List<Tuple<string, string>> getAttributes()
        {
            List<Tuple<string, string>> _listAttribute = base.getAttributes();
            try
            {
                List<Tuple<string, string>> _attributes = new List<Tuple<string, string>> {
                new Tuple<string, string>("Source ID",this.SourceID.ToString()),
                new Tuple<string, string>("Destination Indicator",this.DestinationIndicator.ToString()),
                new Tuple<string, string>("Binary Data Flag",this.BinaryDataFlag.ToString()),
                new Tuple<string, string>("Destination ID",this.DestinationID.ToString()),
                new Tuple<string, string>("Binary Data",this.BinaryData.ToString()),
                };
                _listAttribute.AddRange(_attributes);
            }
            catch (Exception ex)
            {
                log.Error(ex, "MessageType25 :: getAttribute");
            }

            return _listAttribute;
        }
        #endregion
    }
}
