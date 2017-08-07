using NMEA_MessageParserConstructor.BL.CommunicationState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA_MessageParserConstructor.BL.Messages
{
    public class MessageType2:RootMessages
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
        private SOTDMA Sotdma;

        public MessageType2()
        {
            this.MessageID = 2;
            this.Description = "Assigned Scheduled position report";
            this.RepeatIndicator = 0;
            this.Priority = 1;
            this.TotalNumberOfBits = 168;
            this.Sotdma = new SOTDMA();
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
            this.Sotdma.SyncState = Convert.ToByte(getDecimalFromBinary(content, 149, 2));
            this.Sotdma.SlotTimeOut = Convert.ToByte(getDecimalFromBinary(content, 151, 3));
            //Communication State Sub Message
            this.Sotdma.subMessage.SlotNumber = Convert.ToInt32(getDecimalFromBinary(content, 154, 14));
            this.CommunicationState = Sotdma;
            //Total Number Of Bits
            this.TotalNumberOfBits = 168;
            return null;
        }
        #endregion

        #region ToString mesajını ezdik. Methodu sınıfa göre tasarladık.
        public override string ToString()
        {
            return this.MessageID + "\n" +
                this.RepeatIndicator + "\n" +
                this.UserID + "\n" +
                this.NavigationalStatus + "\n" +
                this.RateOfTurnROTAIS + "\n" +
                this.SOG + "\n" +
                this.PositionAccuracy + "\n" +
                this.Longitude + "\n" +
                this.Latitude + "\n" +
                this.COG + "\n" +
                this.TrueHeading + "\n" +
                this.TimeStamp + "\n" +
                this.SpecialManoeuvreIndicator + "\n" +
                this.Spare + "\n" +
                this.RAIMFlag + "\n" +
                this.CommunicationState.ToString();
        }
        #endregion

        #region Attributeları döndürür.
        public override List<Tuple<string, string>> getAttributes()
        {
            List<Tuple<string, string>> _listAttribute = base.getAttributes();
            List<Tuple<string, string>> _listSotdma = this.Sotdma.getAttributes();
            List<Tuple<string, string>> _attributes = new List<Tuple<string, string>> {
                  new Tuple<string, string>("User ID",this.UserID.ToString()),
                  new Tuple<string, string>("Navigational Status",this.NavigationalStatus.ToString()),
                  new Tuple<string, string>("Rate Of Turn ROTAIS",this.RateOfTurnROTAIS.ToString()),
                  new Tuple<string, string>("SOG",this.SOG.ToString()),
                  new Tuple<string, string>("PositionAccuracy",this.PositionAccuracy.ToString()),
                  new Tuple<string, string>("Longitude",this.Longitude.ToString()),
                  new Tuple<string, string>("Latitude",this.Latitude.ToString()),
                  new Tuple<string, string>("COG",this.COG.ToString()),
                  new Tuple<string, string>("True Heading", this.TrueHeading.ToString()),
                  new Tuple<string, string>("Time Stamp",this.TimeStamp.ToString()),
                  new Tuple<string, string>("Special Manoeuvre Indicator",this.SpecialManoeuvreIndicator.ToString()),
                  new Tuple<string, string>("RAIM Flag",this.RAIMFlag.ToString()),
                  new Tuple<string, string>("Sub Message","")
             };
            _listAttribute.AddRange(_attributes);
            foreach (var sotdma in _listSotdma)
            {
                _listAttribute.Add(new Tuple<string, string>(sotdma.Item1, sotdma.Item2));

            }
            return _listAttribute;
        }
        #endregion
    }
}
