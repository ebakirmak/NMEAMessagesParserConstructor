using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA_MessageParserConstructor.BL.Messages
{
    public class MessageType17 : RootMessages
    {
        private int SourceID { get; set; }
        private double Longitude { get; set; }
        private double Latitude { get; set; }
        private byte Spare2 { get; set; }
        private CorrectionData Data { get; set; }
        private Logger log { get; set; }

        public MessageType17()
        {
            this.MessageID = 17;
            this.Description = "Global navigation-satellite system broadcast binary message";
            this.Data = new CorrectionData();
            this.log = LogManager.GetCurrentClassLogger();
        }

        class CorrectionData
        {
            public int MessageType { get; set; }
            public int StationID { get; set; }
            public int ZCount { get; set; }
            public int SequenceNumber { get; set; }
            public int N { get; set; }
            public int Health { get; set; }
            public string  DataWord { get; set; }
            public int NumberOfBits { get; set; }
        }


        #region Mesaj yapısında bulunan attributelara, alınan mesajdaki değerleri set ettik.
        public override string[] Parser(string message1, string message2)
        {
            try
            {
                string[] messageParts1 = base.Parser(message1);
                string[] messageParts2 = base.Parser(message2);
                string content = getContentBinary(messageParts1[5], Remove(messageParts1[6]));
                content += getContentBinary(messageParts2[5], Remove(messageParts2[6]));
                //Message ID
                this.MessageID = Convert.ToByte(getDecimalFromBinary(content, 0, 6));
                //Repeat Indicator
                this.RepeatIndicator = Convert.ToByte(getDecimalFromBinary(content, 6, 2));
                //Source ID - UserID
                this.SourceID = Convert.ToInt32(getDecimalFromBinary(content, 8, 30));
                //Spare
                this.Spare = Convert.ToByte(getDecimalFromBinary(content, 38, 2));
                //Longitude
                this.Longitude = Convert.ToDouble(getDecimalFromBinary(content, 40, 18))/600000;
                //Latitude
                this.Latitude = Convert.ToDouble(getDecimalFromBinary(content, 58, 17)) / 600000;
                //Spare
                this.Spare2 = Convert.ToByte(getDecimalFromBinary(content, 75, 5));
                //DATA MessageType
                this.Data.MessageType = Convert.ToInt32(getDecimalFromBinary(content, 80, 6));
                //DATA Station ID
                this.Data.StationID = Convert.ToInt32(getDecimalFromBinary(content, 86, 10));
                //DATA Z Count
                this.Data.ZCount = Convert.ToInt32(getDecimalFromBinary(content, 96, 13));
                //DATA Sequence Number
                this.Data.SequenceNumber = Convert.ToInt32(getDecimalFromBinary(content, 109, 3));
                //DATA N
                this.Data.N = Convert.ToInt32(getDecimalFromBinary(content, 112, 5));
                //Health
                this.Data.Health = Convert.ToInt32(getDecimalFromBinary(content, 117, 3));
                ////DATA DGNSS data word -- HATALI DÜZELTİLECEK.
                this.Data.DataWord = Convert.ToString(getStringFromBinary(content, 120, content.Length - 120));

            }
            catch (Exception ex)
            {
                log.Error(ex, " MessageType16 :: Parser()");
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
                "Longitude: " + this.Longitude + "\n" +
                "Latitude: " + this.Latitude + "\n" +
                "Spare 2 : " + this.Spare2 + "\n" +
                "Data Message Type: " + this.Data.MessageType + "\n" +
                "Data Station ID: " + this.Data.StationID + "\n" +
                "Data ZCount: " + this.Data.ZCount + "\n" +
                "Data Sequence Number: " + this.Data.SequenceNumber + "\n" +
                "Data N: " + this.Data.N + "\n" +
                "Data Health: " + this.Data.Health + "\n" +
                "DGNSS data word: " + this.Data.DataWord + "\n";
        }
        #endregion
    }
}
