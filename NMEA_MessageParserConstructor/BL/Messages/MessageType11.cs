using NMEA_MessageParserConstructor.BL.CommunicationState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA_MessageParserConstructor.BL.Messages
{
    public class MessageType11 : RootMessages
    {
        private int UserID { get; set; }
        private ushort UtcYear { get; set; }
        private byte UtcMonth { get; set; }
        private byte UtcDay { get; set; }
        private byte UtcHour { get; set; }
        private byte UtcMinute { get; set; }
        private byte UtcSecond { get; set; }
        private byte PositionAccuracy { get; set; }
        private double Longitude { get; set; }
        private double Latitude { get; set; }
        //Type Of Electronic Position Fi
        private byte TOEPFD { get; set; }
        private byte TransmissionControlForLongRangeBroadcastMessage { get; set; }
        private byte RAIMFlag { get; set; }
        private SOTDMA Sotdma;

        public MessageType11()
        {
            this.MessageID = 4;
            this.RepeatIndicator = 0;
            this.Description = "Coordinated Universal Time and Date Inquiry";
            this.Priority = 1;
            this.TransmissionControlForLongRangeBroadcastMessage = 0;
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
            //UTC Year
            this.UtcYear = Convert.ToUInt16(getDecimalFromBinary(content, 38, 14));
            //UTC Month
            this.UtcMonth = Convert.ToByte(getDecimalFromBinary(content, 52, 4));
            //UTC Day
            this.UtcDay = Convert.ToByte(getDecimalFromBinary(content, 56, 5));
            //UTC Hour
            this.UtcHour = Convert.ToByte(getDecimalFromBinary(content, 61, 5));
            //UTC Minute
            this.UtcMinute = Convert.ToByte(getDecimalFromBinary(content, 66, 6));
            //UTC Second
            this.UtcSecond = Convert.ToByte(getDecimalFromBinary(content, 72, 6));
            //PA
            this.PositionAccuracy = Convert.ToByte(getDecimalFromBinary(content, 78, 1));
            //LON - Dakikaya çevrildi ve 10.000 ile çarpıldı.
            this.Longitude = Convert.ToDouble((getDecimalFromBinary(content, 79, 28)));
            this.Longitude /= 60;
            this.Longitude /= 10000;
            //LAT - Dakikaya çevrildi ve 10.000 ile çarpıldı.
            this.Latitude = Convert.ToDouble(getDecimalFromBinary(content, 107, 27));
            this.Latitude /= 60;
            this.Latitude /= 10000;
            //Fix Type
            this.TOEPFD = Convert.ToByte(getDecimalFromBinary(content, 134, 4));
            //Transmission control for long - range broadcast mesage
            this.TransmissionControlForLongRangeBroadcastMessage = Convert.ToByte(getDecimalFromBinary(content, 138, 1));
            //Spare
            this.Spare = Convert.ToByte(getDecimalFromBinary(content, 139, 9));
            //RAIM
            this.RAIMFlag = Convert.ToByte(getDecimalFromBinary(content, 148, 1));
            //Communication State            
            this.Sotdma.SyncState = Convert.ToByte(getDecimalFromBinary(content, 149, 2));
            this.Sotdma.SlotTimeOut = Convert.ToByte(getDecimalFromBinary(content, 151, 3));
            //Communication State Sub Message
            this.Sotdma.subMessage.SlotNumber = Convert.ToInt32(getDecimalFromBinary(content, 154, 14));
            this.CommunicationState = Sotdma;
            return null;
        }
        #endregion

        #region ToString() methodunu override ettik.
        public override string ToString()
        {
            return
                "Message ID: " + this.MessageID + "\n" +
                "RepeatIndicator" + this.RepeatIndicator + "\n" +
                "User ID / MMSI" + this.UserID + "\n" +
                "UTC Year" + this.UtcYear + "\n" +
                "UTC Month" + this.UtcMonth + "\n" +
                "UTC Day" + this.UtcDay + "\n" +
                "UTC Hour" + this.UtcHour + "\n" +
                "UTC Minute" + this.UtcMinute + "\n" +
                "UTC Second" + this.UtcSecond + "\n" +
                "PA" + this.PositionAccuracy + "\n" +
                "Lon" + this.Longitude + "\n" +
                "Lat" + this.Latitude + "\n" +
                "Type of electronic position fixing device" + this.TOEPFD + "\n" +
                "Transmission control..." + this.TransmissionControlForLongRangeBroadcastMessage + "\n" +
                "Spare" + this.Spare + "\n" +
                "RAIM" + this.RAIMFlag + "\n" +
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
                  new Tuple<string, string>("UTC Year",this.UtcYear.ToString()),
                  new Tuple<string, string>("UTC Month",this.UtcMonth.ToString()),
                  new Tuple<string, string>("UTC Day",this.UtcDay.ToString()),
                  new Tuple<string, string>("UTC Hour",this.UtcHour.ToString()),
                  new Tuple<string, string>("UTC Minute",this.UtcMinute.ToString()),
                  new Tuple<string, string>("UTC Second",this.UtcSecond.ToString()),
                  new Tuple<string, string>("Position Accuracy",this.PositionAccuracy.ToString()),
                  new Tuple<string, string>("Longitude",this.Longitude.ToString()),
                  new Tuple<string, string>("Latitude",this.Latitude.ToString()),
                  new Tuple<string, string>("Type Of Electronic Position Fix",this.TOEPFD.ToString()),
                  new Tuple<string, string>("Transmission Control",this.TransmissionControlForLongRangeBroadcastMessage.ToString()),
                  new Tuple<string, string>("RAIM Flag",this.RAIMFlag.ToString()),

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
