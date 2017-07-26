using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA_MessageParserConstructor.BL.Messages
{
    public class MessageType8: RootMessages
    {
        private int SourceID { get; set; }
        private BinaryData binaryData { get; set; }
        public Logger log { get; set; }

        public MessageType8()
        {
            
            this.MessageID = 8;
            this.Description = "Binary Broadcast Message";
            this.TotalNumberOfBits = 1008;
            this.binaryData = new BinaryData();
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
                
                //Binary Data
                this.binaryData.DAC = Convert.ToInt32(getDecimalFromBinary(content, 40, 10));
                //HATA VAR!
                this.binaryData.FID = Convert.ToInt32(getDecimalFromBinary(content, 50, 6));
                this.binaryData.Data = Convert.ToString(getStringFromBinary(content, 56,12));
            }
            catch (Exception ex)
            {
                log.Error(ex, " MessageType8 :: Parser()");
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
                "DAC: " + this.binaryData.DAC + "\n" +
                "FID: " + this.binaryData.FID + "\n" +
                "Data: " + this.binaryData.Data;
        }
        #endregion
    }
}
