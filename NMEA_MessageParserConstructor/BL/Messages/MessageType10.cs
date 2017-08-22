using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA_MessageParserConstructor.BL.Messages
{
    public class MessageType10 :RootMessages
    {

        public int SourceID { get; set; }
        public int DestinationID { get; set; }
        public Logger log { get; set; }

        public MessageType10()
        {
            this.MessageID = 10;
            this.Description = "Coordinated universal time and date inquiry";
            this.TotalNumberOfBits = 72;
            this.log = LogManager.GetCurrentClassLogger();
        }


        #region Mesaj yapısında bulunan attributelara, alınan mesajdaki değerleri set ettik.
        public override string[] Parser(string message)
        {
            try
            {
                string[] messageParts = base.Parser(message);
                string content = getContentBinary(messageParts[5], Remove(messageParts[6]));

                //Message ID
                this.MessageID = Convert.ToByte(getDecimalFromBinary(content, 0, 6));
                //Repeat Indicator
                this.RepeatIndicator = Convert.ToByte(getDecimalFromBinary(content, 6, 2));
                //Source ID - UserID
                this.SourceID = Convert.ToInt32(getDecimalFromBinary(content, 8, 30));
                //Spare
                this.Spare = Convert.ToByte(getDecimalFromBinary(content, 38, 2));
                //Destination ID
                this.DestinationID = Convert.ToInt32(getDecimalFromBinary(content, 40, 30));
                //Spare
                this.Spare = Convert.ToByte(getDecimalFromBinary(content, 70, 2));   
                
                          
            }
            catch (Exception ex)
            {
                log.Error(ex, " MessageType10 :: Parser()");
                throw;
            }


            return null;

        }
        #endregion

        #region ToString mesajını ezdik. Methodu sınıfa göre tasarladık.
        public override string ToString()
        {
            return
                "Message ID: " + this.MessageID + "\n" +
                "Repeat Indicator: " + this.RepeatIndicator + "\n" +
                "Source ID: " + this.SourceID + "\n" +
                "Spare: " + this.Spare + "\n" +
                "Destination ID: " + this.DestinationID + "\n" +
                "Spare: " + this.Spare + "\n";

        }
        #endregion

        #region Attributeları döndürür.
        public override List<Tuple<string, string>> getAttributesAndValues()
        {
            List<Tuple<string, string>> _listAttribute = base.getAttributesAndValues();
            List<Tuple<string, string>> _attributes = new List<Tuple<string, string>> {
                  new Tuple<string, string>("User ID",this.SourceID.ToString()),
                  new Tuple<string, string>("Navigational Status",this.DestinationID.ToString())
             };
            _listAttribute.AddRange(_attributes);          
            return _listAttribute;
        }
        #endregion
    }
}
