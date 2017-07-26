using NMEA_MessageParserConstructor.BL.CommunicationState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA_MessageParserConstructor.BL.Messages
{
    public class MessageType3 : RootMessages
    {
        //MMSI
        private int UserID { get; set; }
        private byte NavigationalStatus { get; set; }
        private float RateOfTurnROTAIS { get; set; }
        private float SOG { get; set; }
        private byte PositionAccuracy { get; set; }
        private double Longitude { get; set; }
        private double Latitude { get; set; }
        private float COG { get; set; }
        private int TrueHeading { get; set; }
        private byte TimeStamp { get; set; }
        private int SpecialManoeuvreIndicator { get; set; }
        private byte RAIMFlag { get; set; }
        private ITDMA Itdma;

        public MessageType3()
        {
            this.MessageID = 3;
            this.Description = "Special position report, response to interrogation.";
            this.RepeatIndicator = 0;
            this.Priority = 1;
            this.TotalNumberOfBits = 168;
            this.Itdma = new ITDMA();
        }

        #region Mesaj yapısında bulunan attributelara, alınan mesajdaki değerleri set ettik.
        public override string[] Parser(string message)
        {
            string[] messageParts = base.Parser(message);
            //Context'i oku. Binary yapıda.
            string content = getContentBinary(messageParts[5], Remove(messageParts[6]));
            //Tüm mesajlarda olan özellikleri burada gir.
            //MessageID
            this.MessageID = Convert.ToByte(getDecimalFromBinary(content, 0, 6));
            //Repeat indicator
            this.RepeatIndicator = Convert.ToByte(getDecimalFromBinary(content, 6, 2));
            //Source MMSI
            this.UserID = Convert.ToInt32(getDecimalFromBinary(content, 8, 30));
            //NavigationalStatus - Nav Status
            this.NavigationalStatus = Convert.ToByte(getDecimalFromBinary(content, 38, 4));
            //RateOfTurnROTAIS - ROT
            this.RateOfTurnROTAIS = float.Parse(getDecimalFromBinary(content, 42, 8));
            //SOG - **** 10'a böldük. Doküman.
            this.SOG = float.Parse(getDecimalFromBinary(content, 50, 10)) / 10;
            //PA
            this.PositionAccuracy = Convert.ToByte(getDecimalFromBinary(content, 60, 1));
            //LON - Dakikaya çevrildi ve 10.000 ile çarpıldı. Bir yanlışlık var.
            this.Longitude = Convert.ToDouble((getDecimalFromBinary(content, 61, 28))) / 60 / 10000;
            //LAT - Dakikaya çevrildi ve 10.000 ile çarpıldı. Bir yanlışlık var.
            this.Latitude = Convert.ToDouble(getDecimalFromBinary(content, 89, 27)) / 60 / 10000;
            //COG **** 10'a böldük. Doküman.
            this.COG = float.Parse(getDecimalFromBinary(content, 116, 12)) / 10;
            //True Heaading
            this.TrueHeading = Convert.ToInt32(getDecimalFromBinary(content, 128, 9));
            //Time stamp
            this.TimeStamp = Convert.ToByte(getDecimalFromBinary(content, 137, 6));
            //Spe Man
            this.SpecialManoeuvreIndicator = Convert.ToInt32(getDecimalFromBinary(content, 143, 2));
            //Spare
            this.Spare = Convert.ToByte(getDecimalFromBinary(content, 145, 3));
            //RAIM
            this.RAIMFlag = Convert.ToByte(getDecimalFromBinary(content, 148, 1));
            //Communication State            
            this.Itdma.SyncState = Convert.ToByte(getDecimalFromBinary(content, 149, 2));
            this.Itdma.SlotIncrement = Convert.ToByte(getDecimalFromBinary(content, 151, 13));
            //Communication State Sub Message
            this.Itdma.NumberOfSlots = Convert.ToByte(getDecimalFromBinary(content, 164, 3));
            this.Itdma.KeepFlag = Convert.ToByte(getDecimalFromBinary(content, 167, 1));
            this.CommunicationState = Itdma;
            //Total Number Of Bits
            this.TotalNumberOfBits = 168;
            return null;
        }
        #endregion

        #region ToString mesajını ezdik. Methodu sınıfa göre tasarladık.
        public override string ToString()
        {
            return
              "Message ID: " + this.MessageID + "\n" +
              "RepeatIndicator" + this.RepeatIndicator + "\n" +
              "User ID / MMSI" + this.UserID + "\n" +
              "Navigational Status" + this.NavigationalStatus + "\n" +
              "ROT" + this.RateOfTurnROTAIS + "\n" +
              "SOG" + this.SOG + "\n" +
              "PA" + this.PositionAccuracy + "\n" +
              "Lon" + this.Longitude + "\n" +
              "Lat" + this.Latitude + "\n" +
              "COG" + this.COG + "\n" +
              "True Heading" + this.TrueHeading + "\n" +
              "TimeStamp" + this.TimeStamp + "\n" +
              "Spe Man" + this.SpecialManoeuvreIndicator + "\n" +
              "Spare" + this.Spare + "\n" +
              "RAIM" + this.RAIMFlag + "\n" +
              this.Itdma.ToString();
        }
        #endregion
    }
}
