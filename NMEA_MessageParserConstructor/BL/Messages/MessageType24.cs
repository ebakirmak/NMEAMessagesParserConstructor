using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA_MessageParserConstructor.BL.Messages
{
    public class MessageType24 : RootMessages
    {
        public int UserID { get; set; }
        public byte PartNumber { get; set; }
        public Logger log { get; set; }

        public MessageType24()
        {
            this.MessageID = 24;
            this.Description = "Static Data Report ";
            this.log = LogManager.GetCurrentClassLogger();
        }

        #region  Mesajı pars ediyoruz ve PartNumber değerini buluyoruz.
        public override string[] Parser(string message)
        {
            string[] messageParts = base.Parser(message);                 
            return messageParts;
        }
        #endregion

        #region Part Number değerini set ediyor.
        public void setPartNumber(string message)
        {
            string[] messageParts = Parser(message);
            //Context'i oku. Binary yapıda.
            string content = getContentBinary(messageParts[5], Remove(messageParts[6]));
            this.PartNumber = Convert.ToByte(getDecimalFromBinary(content, 38, 2));
        }
        #endregion

        #region Part Number değerini döndürüyor.
        public byte getPartNumber()
        {
            return this.PartNumber;
        }
        #endregion

        #region ToString() methodunu override ettik.
        public override string ToString()
        {

            return
                "Message ID: " + this.MessageID + "\n" +
                "RepeatIndicator: " + this.RepeatIndicator + "\n" +
                "Source ID: " + this.UserID + "\n" +
                "Part Number: " + this.PartNumber + "\n";
        }
        #endregion

        #region Attributeları döndürür.
        //new Tuple<string, string>("",this..ToString()),
        public override List<Tuple<string, string>> getAttributesAndValues()
        {
            List<Tuple<string, string>> _listAttribute = base.getAttributesAndValues();
            try
            {
                List<Tuple<string, string>> _attributes = new List<Tuple<string, string>> {
                new Tuple<string, string>("User ID",this.UserID.ToString()),
                new Tuple<string, string>("Part Number",this.PartNumber.ToString()),                
                };
                _listAttribute.AddRange(_attributes);
            }
            catch (Exception ex)
            {
                log.Error(ex, "MessageType24 :: getAttribute");
            }

            return _listAttribute;
        }
        #endregion
    }
}
