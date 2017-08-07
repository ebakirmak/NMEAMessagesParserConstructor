using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA_MessageParserConstructor.BL.Messages
{
    public class MessageType14 : RootMessages
    {
        private int SourceID { get; set; }
        private string SafetyRelatedText { get; set; }
        private Logger log;
        public MessageType14()
        {
            this.MessageID = 14;
            this.TotalNumberOfBits = 1008;
            this.Description = "Safety related broadcast message";
            this.log = LogManager.GetCurrentClassLogger();
        }

        #region Mesaj yapısında bulunan attributelara, alınan mesajdaki değerleri set ettik.
        public override string[] Parser(string message)
        {
            try
            {
                string[] messageParts = base.Parser(message);
                string content = getContentBinary(messageParts[5], Remove(messageParts[6]));
                int a = content.Length;
                //Message ID
                this.MessageID = Convert.ToByte(getDecimalFromBinary(content, 0, 6));

                //Repeat Indicator
                this.RepeatIndicator = Convert.ToByte(getDecimalFromBinary(content, 6, 2));

                //Source ID - UserID
                this.SourceID = Convert.ToInt32(getDecimalFromBinary(content, 8, 30));

                //Spare
                this.Spare = Convert.ToByte(getDecimalFromBinary(content, 38, 2));

                //Safety Related Text
                this.SafetyRelatedText = Convert.ToString(getStringFromBinary(content, 40, content.Length - 40));

            }
            catch (Exception ex)
            {
                log.Error(ex, " MessageType14 :: Parser()");
                //throw;
                return null;
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
                "Safety Related Text: " + this.SafetyRelatedText;
             


        }
        #endregion

        #region Attributeları döndürür.
        public override List<Tuple<string, string>> getAttributes()
        {
            List<Tuple<string, string>> _listAttribute = base.getAttributes();
            try
            {

                List<Tuple<string, string>> _attributes = new List<Tuple<string, string>> {
                  new Tuple<string, string>("Source ID",this.SourceID.ToString()),
                  new Tuple<string, string>("Safety Related Text",this.SafetyRelatedText.ToString()),
               };

                _listAttribute.AddRange(_attributes);
            }
            catch (Exception ex)
            {
                log.Error(ex, "MessageType14 :: getAttribute");
            }

            return _listAttribute;
        }
        #endregion

    }
}
