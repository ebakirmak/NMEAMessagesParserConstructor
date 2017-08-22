using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA_MessageParserConstructor.BL.Messages
{
    public class MessageType27 : RootMessages
    {
        public int UserID { get; set; }
        public byte PositionAccuracy { get; set; }
        public byte RAIMFlag { get; set; }
        public byte NavigationalStatus { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int SOG { get; set; }
        public int COG { get; set; }
        public byte PositionLatency { get; set; }
        public Logger log { get; set; }

        public MessageType27()
        {
            this.MessageID = 27;
            this.Description = "Long-range automatic identification system broadcast message";
            this.TotalNumberOfBits = 96;
            this.log = LogManager.GetCurrentClassLogger();
        }

        #region Parser

        #region Parser(): Mesaj yapısında bulunan attributelara, alınan mesajdaki değerleri set ettik.
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
                //User ID
                this.UserID = Convert.ToInt32(getDecimalFromBinary(content, 8, 30));
                //Position Accuracy
                this.PositionAccuracy = Convert.ToByte(getDecimalFromBinary(content, 38, 1));
                //RAIM Flag
                this.RAIMFlag = Convert.ToByte(getDecimalFromBinary(content, 39, 1));
                //Navigational Statu
                this.NavigationalStatus = Convert.ToByte(getDecimalFromBinary(content, 40, 4));
                //Longitude
                this.Longitude = Convert.ToDouble(getDecimalFromBinary(content, 44, 18))/600;
                //Latitude
                this.Latitude = Convert.ToDouble(getDecimalFromBinary(content, 62, 17))/600;
                //SOG
                this.SOG = Convert.ToInt32(getDecimalFromBinary(content, 79, 6));
                //COG
                this.COG = Convert.ToInt32(getDecimalFromBinary(content, 85, 9));
                //Position Accuracy
                this.PositionLatency = Convert.ToByte(getDecimalFromBinary(content, 94, 1));
                //Spare
                this.Spare = Convert.ToByte(getDecimalFromBinary(content, 95, 1));

            }
            catch (Exception ex)
            {
                log.Error(ex, "MessageType27 :: Parser");
                //throw;
            }

            return null;
        }
        #endregion

        #region getAttributesAndValues(): Attributeları ve value'leri döndürür.
        //new Tuple<string, string>("",this..ToString()),
        public override List<Tuple<string, string>> getAttributesAndValues()
        {
            List<Tuple<string, string>> _listAttribute = base.getAttributesAndValues();
            try
            {
                List<Tuple<string, string>> _attributes = new List<Tuple<string, string>> {
                new Tuple<string, string>("User ID",this.UserID.ToString()),
                new Tuple<string, string>("Position Accuracy",this.PositionAccuracy.ToString()),
                new Tuple<string, string>("RAIM Flag",this.RAIMFlag.ToString()),
                new Tuple<string, string>("Navigational Status",this.NavigationalStatus.ToString()),
                new Tuple<string, string>("Longitude",this.Longitude.ToString()),
                new Tuple<string, string>("Latitude",this.Latitude.ToString()),
                new Tuple<string, string>("SOG",this.SOG.ToString()),
                new Tuple<string, string>("COG",this.COG.ToString()),
                new Tuple<string, string>("Position Latency",this.PositionLatency.ToString()),
                };
                _listAttribute.AddRange(_attributes);
            }
            catch (Exception ex)
            {
                log.Error(ex, "MessageType27 :: getAttribute");
            }

            return _listAttribute;
        }
        #endregion

        #endregion

        #region ToString() methodunu override ettik.
        public override string ToString()
        {

            return
                "Message ID: " + this.MessageID + "\n" +
                "RepeatIndicator: " + this.RepeatIndicator + "\n" +
                "User ID: " + this.UserID + "\n" +
                "Position Accuracy: " + this.PositionAccuracy + "\n" +
                "RAIM Flag: " + this.RAIMFlag + "\n" +
                "Navigational status: " + this.NavigationalStatus + "\n" +
                "Longitude: " + this.Longitude + "\n" +
                "Latitude: " + this.Latitude + "\n" +
                "SOG: " + this.SOG + "\n" +
                "COG: " + this.COG + "\n" +
                "Position latency: " + this.PositionLatency + "\n" +
                "Spare: " + this.Spare;




        }
        #endregion

        #region Constructor 

        #region getAttributes(): Attributeları döndürür.
        public override List<Tuple<string, string>> getAttributes()
        {
            List<Tuple<string, string>> _listAttribute = base.getAttributes();
            List<Tuple<string, string>> _attributes = new List<Tuple<string, string>> {
                  new Tuple<string, string>("User ID",""),
                  new Tuple<string, string>("Position Accuracy",""),
                  new Tuple<string, string>("RAIM Flag",""),
                  new Tuple<string, string>("Navigational Status",""),
                  new Tuple<string, string>("Longitude",""),
                  new Tuple<string, string>("Latitude",""),
                  new Tuple<string, string>("SOG",""),
                  new Tuple<string, string>("COG",""),
                  new Tuple<string, string>("Position Latency",""),
             };
            _listAttribute.AddRange(_attributes);
            return _listAttribute;
        }
        #endregion

        //#region Constructor(): Girilen değerlere göre VDM veya VDO mesajı oluşturuluyor.
        //public override string Constructor(List<string> _listMessage)
        //{
        //    //Temel mesaj özellikleri alınıyor.
        //    string Message = base.Constructor(_listMessage);

        //    #region Datagridview'den alınan değerleri set et.
        //    string errorMessage = "Error!";
        //    /////////////////////////////////////////////////////
        //    if (ControlMessageID(Convert.ToByte(_listMessage[5])))
        //        this.MessageID = Convert.ToByte(_listMessage[5]);
        //    else
        //        errorMessage += "\nMessage ID değerini kontrol ediniz.";
        //    /////////////////////////////////////////////////////   
        //    if (ControlRepeatIndicator(Convert.ToByte(_listMessage[8])))
        //        this.RepeatIndicator = Convert.ToByte(_listMessage[8]);
        //    else
        //        errorMessage += "\nRepeat Indicator değerini kontrol ediniz.";
        //    ////////////////////////////////////////////////////////////
        //    this.UserID = Convert.ToInt32(_listMessage[10]);
        //    if (ControlUTCYear(Convert.ToInt32(_listMessage[11])))
        //        this.UtcYear = Convert.ToInt32(_listMessage[11]);
        //    else
        //        errorMessage += "\nUTC Year değerini kontrol ediniz.";
        //    //////////////////////////////////////////////////////////
        //    if (ControlUTCMonth(Convert.ToInt32(_listMessage[12])))
        //        this.UtcMonth = Convert.ToByte(_listMessage[12]);
        //    else
        //        errorMessage += "\nUTC Month değerini kontrol ediniz.";
        //    /////////////////////////////////////////////////////////
        //    if (ControlUTCDay(Convert.ToByte(_listMessage[13])))
        //        this.UtcDay = Convert.ToByte(_listMessage[13]);
        //    else
        //        errorMessage += "\nUTC Day değerini kontrol ediniz.";
        //    //////////////////////////////////////////////////////////
        //    if (ControlUTCHour(Convert.ToByte(_listMessage[14])))
        //        this.UtcHour = Convert.ToByte(_listMessage[14]);
        //    else
        //        errorMessage += "\nUTC Hour değerini kontrol ediniz.";
        //    ////////////////////////////////////////////////////////
        //    if (ControlUTCMinute(Convert.ToByte(_listMessage[15])))
        //        this.UtcMinute = Convert.ToByte(_listMessage[15]);
        //    else
        //        errorMessage += "\nUTC Minute değerini kontrol ediniz.";
        //    ///////////////////////////////////////////////////
        //    if (ControlUTCSecond(Convert.ToByte(_listMessage[16])))
        //        this.UtcSecond = Convert.ToByte(_listMessage[16]);
        //    else
        //        errorMessage += "\nUTC Second değerini kontrol ediniz.";
        //    //////////////////////////////////////////////////////
        //    if (ControlPositionAccuracy(Convert.ToByte(_listMessage[17])))
        //        this.PositionAccuracy = Convert.ToByte(_listMessage[17]);
        //    else
        //        errorMessage += "\nPosition Accuracy değerini kontrol ediniz.";
        //    //////////////////////////////////////////////////////
        //    if (ControlLongitude(Convert.ToDouble(_listMessage[18])))
        //        this.Longitude = Math.Round(Convert.ToDouble(_listMessage[18]), 7);
        //    else
        //        errorMessage += "\nLongitude değerini kontrol ediniz.";
        //    ////////////////////////////////////////////////////////
        //    if (ControlLatitude(Convert.ToDouble(_listMessage[19])))
        //        this.Latitude = Math.Round(Convert.ToDouble(_listMessage[19]), 7);
        //    else
        //        errorMessage += "\nLatitude değerini kontrol ediniz.";
        //    /////////////////////////////////////////////////////////
        //    if (ControlTOEPFD(Convert.ToByte(_listMessage[20])))
        //        this.TOEPFD = Convert.ToByte(_listMessage[20]);
        //    else
        //        errorMessage += "\nType Of Electronic position fixing device değerini kontrol ediniz.";
        //    //////////////////////////////////////////////////
        //    if (ControlTCFLRBM(Convert.ToByte(_listMessage[21])))
        //        this.RAIMFlag = Convert.ToByte(_listMessage[21]);
        //    else
        //        errorMessage += "\nTransmission Control for long-range broadcast message değerini kontrol ediniz.";
        //    /////////////////////////////////////////////////
        //    if (ControlRAIM(Convert.ToByte(_listMessage[22])))
        //        this.RAIMFlag = Convert.ToByte(_listMessage[22]);
        //    else
        //        errorMessage += "\nRAIM Flag değerini kontrol ediniz.";
        //    /////////////////////////////////////////////////
        //    this.Sotdma.setValue(_listMessage, 23);
        //    #endregion

        //    #region Bit değerlerine göre binary mesaj oluşturuluyor.
        //    string binaryMessage = setBinaryToDecimal(this.MessageID).PadLeft(6, '0');
        //    binaryMessage += setBinaryToDecimal(this.RepeatIndicator).PadLeft(2, '0');
        //    binaryMessage += setBinaryToDecimal(this.UserID).PadLeft(30, '0');
        //    binaryMessage += setBinaryToDecimal(this.UtcYear).PadLeft(14, '0');
        //    binaryMessage += setBinaryToDecimal(this.UtcMonth).PadLeft(4, '0');
        //    binaryMessage += setBinaryToDecimal(this.UtcDay).PadLeft(5, '0');
        //    binaryMessage += setBinaryToDecimal(this.UtcHour).PadLeft(5, '0');
        //    binaryMessage += setBinaryToDecimal(this.UtcMinute).PadLeft(6, '0');
        //    binaryMessage += setBinaryToDecimal(this.UtcSecond).PadLeft(6, '0');
        //    binaryMessage += setBinaryToDecimal(this.PositionAccuracy).PadLeft(1, '0');
        //    binaryMessage += setBinaryToDecimal(MultiplyLongitude(this.Longitude), 28).PadLeft(28, '0');
        //    binaryMessage += setBinaryToDecimal(MultiplyLatitude(this.Latitude), 27).PadLeft(27, '0');
        //    binaryMessage += setBinaryToDecimal(this.TOEPFD).PadLeft(4, '0');
        //    binaryMessage += setBinaryToDecimal(this.TCFLRBM).PadLeft(1, '0');
        //    binaryMessage += setBinaryToDecimal(this.Spare).PadLeft(9, '0');
        //    binaryMessage += setBinaryToDecimal(this.RAIMFlag).PadLeft(1, '0');
        //    binaryMessage += Sotdma.getBinaryToSOTDMAValue();
        //    #endregion

        //    #region binary message, SetContent fonksiyonuna gönderilerek, ASCII8 tipinde mesaj content içeriği oluşturuluyor.
        //    string content = setContent(binaryMessage);
        //    #endregion

        //    if (errorMessage.Contains("Error!") && errorMessage.Length > 6)
        //        return errorMessage;
        //    else
        //        return Message + content;
        //}
        //#endregion



        #endregion

    }
}
