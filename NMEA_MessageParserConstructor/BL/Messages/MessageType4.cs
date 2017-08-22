using NLog;
using NMEA_MessageParserConstructor.BL.CommunicationState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA_MessageParserConstructor.BL.Messages
{
    public class MessageType4 : RootMessages
    {
        private int UserID { get; set; }
        private int UtcYear { get; set; }
        private byte UtcMonth { get; set; }
        private byte UtcDay { get; set; }
        private byte UtcHour { get; set; }
        private byte UtcMinute { get; set; }
        private byte UtcSecond { get; set; }
        private byte PositionAccuracy { get; set; }
        private double Longitude { get; set; }
        private double Latitude { get; set; }
        //Type Of Electronic Position Fixing Device
        private byte TOEPFD { get; set; }
        //Transmission Control For Long Range Broadcast Message
        private byte TCFLRBM { get; set; }
        private byte RAIMFlag { get; set; }
        private SOTDMA Sotdma;
        private Logger log;

        public MessageType4()
        {
            this.MessageID = 4;
            this.RepeatIndicator = 0;
            this.Description = "Base station report";
            this.Priority = 1;            
            this.TCFLRBM = 0;
            this.TotalNumberOfBits = 168;
            this.Sotdma = new SOTDMA();
            this.log = LogManager.GetCurrentClassLogger();   
        }

        #region Parser

        #region Parser(): Mesaj yapısında bulunan attributelara, alınan mesajdaki değerleri set ettik.
        public override string[] Parser(string message)
        {
            try
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
                this.TCFLRBM = Convert.ToByte(getDecimalFromBinary(content, 138, 1));
                //Spare
                this.Spare = Convert.ToInt32(getDecimalFromBinary(content, 139, 9));
                //RAIM
                this.RAIMFlag = Convert.ToByte(getDecimalFromBinary(content, 148, 1));
                //Communication State            
                this.Sotdma.SyncState = Convert.ToByte(getDecimalFromBinary(content, 149, 2));
                this.Sotdma.SlotTimeOut = Convert.ToByte(getDecimalFromBinary(content, 151, 3));
                //Communication State Sub Message BURADA HATA VARRRR
                this.Sotdma.subMessage.SlotOffset = Convert.ToByte(getDecimalFromBinary(content, 154, 8));
                this.CommunicationState = Sotdma;
            }
            catch (Exception ex)
            {
                log.Error(ex, "MessageType4 :: Parser");     
    
            }
         
            return null;
        }
        #endregion
        
        #region getAttributesAndValues(): Attributeları ve değerlerini döndürür.
        public override List<Tuple<string, string>> getAttributesAndValues()
        {
            List<Tuple<string, string>> _listAttribute = base.getAttributesAndValues();
            List<Tuple<string, string>> _listSotdma = this.Sotdma.getAttributesAndValues();
            List<Tuple<string, string>> _attributes = new List<Tuple<string, string>> {
                  new Tuple<string, string>("User ID",this.UserID.ToString()),
                  new Tuple<string, string>("UTC Year",this.UtcYear.ToString()),
                  new Tuple<string, string>("UTC Month",this.UtcMonth.ToString()),
                  new Tuple<string, string>("UTC Day",this.UtcDay.ToString()),
                  new Tuple<string, string>("UTC Hour",this.UtcHour.ToString()),
                  new Tuple<string, string>("UTC Minute",this.UtcMinute.ToString()),
                  new Tuple<string, string>("UTC Second",this.UtcSecond.ToString()),
                  new Tuple<string, string>("Position Accuracy", this.PositionAccuracy.ToString()),
                  new Tuple<string, string>("Longitude", this.Longitude.ToString()),
                  new Tuple<string, string>("Latitude",this.Latitude.ToString()),
                  new Tuple<string, string>("Type Of Elect. Pos. Fix. Device",this.TOEPFD.ToString()),
                  new Tuple<string, string>("Transmission Control",this.TCFLRBM.ToString()),
                  new Tuple<string, string>("RAIM FLAG",this.RAIMFlag.ToString())
             };
            _listAttribute.AddRange(_attributes);
            foreach (var sotdma in _listSotdma)
            {
                _listAttribute.Add(new Tuple<string, string>(sotdma.Item1, sotdma.Item2));

            }
            return _listAttribute;
        }
        #endregion

        #endregion

        #region Constructor 


         #region getAttributes(): Attributeları döndürür.
        public override List<Tuple<string, string>> getAttributes()
        {
            List<Tuple<string, string>> _listAttribute = base.getAttributes();
            List<Tuple<string, string>> _listSotdma = this.Sotdma.getAttributes();
            List<Tuple<string, string>> _attributes = new List<Tuple<string, string>> {
                  new Tuple<string, string>("User ID",""),
                  new Tuple<string, string>("UTC Year",""),
                  new Tuple<string, string>("UTC Month",""),
                  new Tuple<string, string>("UTC Day",""),
                  new Tuple<string, string>("UTC Hour",""),
                  new Tuple<string, string>("UTC Minute",""),
                  new Tuple<string, string>("UTC Second",""),
                  new Tuple<string, string>("Position Accuracy",""),
                  new Tuple<string, string>("Longitude",""),
                  new Tuple<string, string>("Latitude",""),
                  new Tuple<string, string>("Type Of Elec.",""),
                  new Tuple<string, string>("Broadcast Message",""),
                  new Tuple<string, string>("RAIM Flag",""),
             };
            _listAttribute.AddRange(_attributes);
            foreach (var sotdma in _listSotdma)
            {
                _listAttribute.Add(new Tuple<string, string>(sotdma.Item1, sotdma.Item2));

            }
            return _listAttribute;
        }
        #endregion

         #region Constructor(): Girilen değerlere göre VDM veya VDO mesajı oluşturuluyor.
         public override string Constructor(List<string> _listMessage)
         {
             //Temel mesaj özellikleri alınıyor.
             string Message = base.Constructor(_listMessage);

             #region Datagridview'den alınan değerleri set et.
             string errorMessage = "Error!";
             /////////////////////////////////////////////////////
             if (ControlMessageID(Convert.ToByte(_listMessage[5])))
                 this.MessageID = Convert.ToByte(_listMessage[5]);
             else
                 errorMessage += "\nMessage ID değerini kontrol ediniz.";
             /////////////////////////////////////////////////////   
             if (ControlRepeatIndicator(Convert.ToByte(_listMessage[8])))
                 this.RepeatIndicator = Convert.ToByte(_listMessage[8]);
             else
                 errorMessage += "\nRepeat Indicator değerini kontrol ediniz.";
             ////////////////////////////////////////////////////////////
             this.UserID = Convert.ToInt32(_listMessage[10]);
             if (ControlUTCYear(Convert.ToInt32(_listMessage[11])))
                 this.UtcYear = Convert.ToInt32(_listMessage[11]);
             else
                 errorMessage += "\nUTC Year değerini kontrol ediniz.";
             //////////////////////////////////////////////////////////
             if (ControlUTCMonth(Convert.ToInt32(_listMessage[12])))
                 this.UtcMonth = Convert.ToByte(_listMessage[12]);
             else
                 errorMessage += "\nUTC Month değerini kontrol ediniz.";
             /////////////////////////////////////////////////////////
             if (ControlUTCDay(Convert.ToByte(_listMessage[13])))
                 this.UtcDay = Convert.ToByte(_listMessage[13]);
             else
                 errorMessage += "\nUTC Day değerini kontrol ediniz.";
            //////////////////////////////////////////////////////////
            if (ControlUTCHour(Convert.ToByte(_listMessage[14])))
                this.UtcHour = Convert.ToByte(_listMessage[14]);
            else
                errorMessage += "\nUTC Hour değerini kontrol ediniz.";
            ////////////////////////////////////////////////////////
            if (ControlUTCMinute(Convert.ToByte(_listMessage[15])))
                this.UtcMinute = Convert.ToByte(_listMessage[15]);
            else
                errorMessage += "\nUTC Minute değerini kontrol ediniz.";
            ///////////////////////////////////////////////////
            if (ControlUTCSecond(Convert.ToByte(_listMessage[16])))
                this.UtcSecond = Convert.ToByte(_listMessage[16]);
            else
                errorMessage += "\nUTC Second değerini kontrol ediniz.";
            //////////////////////////////////////////////////////
            if (ControlPositionAccuracy(Convert.ToByte(_listMessage[17])))
                 this.PositionAccuracy = Convert.ToByte(_listMessage[17]);
             else
                 errorMessage += "\nPosition Accuracy değerini kontrol ediniz.";
             //////////////////////////////////////////////////////
             if (ControlLongitude(Convert.ToDouble(_listMessage[18])))
                 this.Longitude = Math.Round(Convert.ToDouble(_listMessage[18]), 7);
             else
                 errorMessage += "\nLongitude değerini kontrol ediniz.";
             ////////////////////////////////////////////////////////
             if (ControlLatitude(Convert.ToDouble(_listMessage[19])))
                 this.Latitude = Math.Round(Convert.ToDouble(_listMessage[19]), 7);
             else
                 errorMessage += "\nLatitude değerini kontrol ediniz.";        
             /////////////////////////////////////////////////////////
             if (ControlTOEPFD(Convert.ToByte(_listMessage[20])))
                 this.TOEPFD = Convert.ToByte(_listMessage[20]);
             else
                 errorMessage += "\nType Of Electronic position fixing device değerini kontrol ediniz.";
            //////////////////////////////////////////////////
            if (ControlTCFLRBM(Convert.ToByte(_listMessage[21])))
                this.RAIMFlag = Convert.ToByte(_listMessage[21]);
            else
                errorMessage += "\nTransmission Control for long-range broadcast message değerini kontrol ediniz.";
            /////////////////////////////////////////////////
            if (ControlRAIM(Convert.ToByte(_listMessage[22])))
                 this.RAIMFlag = Convert.ToByte(_listMessage[22]);
             else
                 errorMessage += "\nRAIM Flag değerini kontrol ediniz.";
            /////////////////////////////////////////////////
            this.Sotdma.setValue(_listMessage, 23);
             #endregion

             #region Bit değerlerine göre binary mesaj oluşturuluyor.
             string binaryMessage = setBinaryToDecimal(this.MessageID).PadLeft(6, '0');
             binaryMessage += setBinaryToDecimal(this.RepeatIndicator).PadLeft(2, '0');
             binaryMessage += setBinaryToDecimal(this.UserID).PadLeft(30, '0');
             binaryMessage += setBinaryToDecimal(this.UtcYear).PadLeft(14, '0');
             binaryMessage += setBinaryToDecimal(this.UtcMonth).PadLeft(4, '0');
             binaryMessage += setBinaryToDecimal(this.UtcDay).PadLeft(5, '0');
             binaryMessage += setBinaryToDecimal(this.UtcHour).PadLeft(5, '0');
             binaryMessage += setBinaryToDecimal(this.UtcMinute).PadLeft(6, '0');
             binaryMessage += setBinaryToDecimal(this.UtcSecond).PadLeft(6, '0');
             binaryMessage += setBinaryToDecimal(this.PositionAccuracy).PadLeft(1, '0');
             binaryMessage += setBinaryToDecimal(MultiplyLongitude(this.Longitude), 28).PadLeft(28, '0');
             binaryMessage += setBinaryToDecimal(MultiplyLatitude(this.Latitude), 27).PadLeft(27, '0');
             binaryMessage += setBinaryToDecimal(this.TOEPFD).PadLeft(4, '0');
             binaryMessage += setBinaryToDecimal(this.TCFLRBM).PadLeft(1, '0');
             binaryMessage += setBinaryToDecimal(this.Spare).PadLeft(9, '0');
             binaryMessage += setBinaryToDecimal(this.RAIMFlag).PadLeft(1, '0');
             binaryMessage += Sotdma.getBinaryToSOTDMAValue();
             #endregion

             #region binary message, SetContent fonksiyonuna gönderilerek, ASCII8 tipinde mesaj content içeriği oluşturuluyor.
             string content = setContent(binaryMessage);
             #endregion

             if (errorMessage.Contains("Error!") && errorMessage.Length > 6)
                 return errorMessage;
             else
                 return Message + content;
         }
         #endregion



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
                "Transmission control..." + this.TCFLRBM + "\n"+   
                "Spare" + this.Spare + "\n" +
                "RAIM" + this.RAIMFlag + "\n" +
                this.CommunicationState.ToString();
        }
        #endregion
    }
}
