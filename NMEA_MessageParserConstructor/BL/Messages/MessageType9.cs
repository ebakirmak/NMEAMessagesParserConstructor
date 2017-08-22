using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA_MessageParserConstructor.BL.Messages
{
    public class MessageType9:RootMessages
    {
        private int UserID { get; set; }
        private int Altitude { get; set; }
        private double SOG { get; set; }
        private int PositionAccuracy { get; set; }
        private double Longitude { get; set; }
        private double Latitude { get; set; }
        private double COG { get; set; }
        private int TimeStamp { get; set; }
        private int AltitudeSensor { get; set; }
        private int DTE { get; set; }
        private int AssignedModeFlag { get; set; }
        private int RAIMFlag { get; set; }
        private int CommunicationStateSelectorFlag { get; set; }
        private Logger log { get; set; }


        public MessageType9()
        {
            this.MessageID = 9;
            this.TotalNumberOfBits = 168;
            this.Description = "Standard search and rescue aircraft position report";
            this.TotalNumberOfBits = 168;
            this.log = LogManager.GetCurrentClassLogger();
        }

        #region Constructor 


        #region getAttributes(): Attributeları döndürür.
        public override List<Tuple<string, string>> getAttributes()
        {
            List<Tuple<string, string>> _listAttribute = base.getAttributes();
            List<Tuple<string, string>> _attributes = new List<Tuple<string, string>> {
                  new Tuple<string, string>("User ID","0"),
                  new Tuple<string, string>("Altitude","0"),
                  new Tuple<string, string>("SOG","0"),
                  new Tuple<string, string>("Position Accuracy","0"),
                  new Tuple<string, string>("Longitude","0"),
                  new Tuple<string, string>("Latitude","0"),
                  new Tuple<string, string>("COG","0"),
                  new Tuple<string, string>("Time Stamp","0"),
                  new Tuple<string, string>("Altitude Sensor","0"),
                  new Tuple<string, string>("DTE","0"),
                  new Tuple<string, string>("Assigned Mode Flag","0"),
                  new Tuple<string, string>("RAIM Flag","0"),
                  new Tuple<string, string>("Comm. state selector flag","0"),

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


        #region Parser

        #region Parser(): Mesaj yapısında bulunan attributelara, alınan mesajdaki değerleri set ettik.
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
                this.UserID = Convert.ToInt32(getDecimalFromBinary(content, 8, 30));
                //Altitude (GNSS)
                this.Altitude = Convert.ToInt32(getDecimalFromBinary(content, 38, 12));
                //SOG
                this.SOG = Convert.ToDouble(getDecimalFromBinary(content, 50, 10));
                //PositionAccuracy
                this.PositionAccuracy = Convert.ToInt32(getDecimalFromBinary(content, 60, 1));
                //Longitude
                this.Longitude = Convert.ToDouble(getDecimalFromBinary(content, 61, 28)) / 60 / 10000;
                //Latitude
                this.Latitude = Convert.ToDouble(getDecimalFromBinary(content, 89, 27)) / 60 / 10000;
                //COG
                this.COG = Convert.ToDouble(getDecimalFromBinary(content, 116, 12)) / 10;
                //Time Stamp
                this.TimeStamp = Convert.ToInt32(getDecimalFromBinary(content, 128, 6));
                //Altitude Sensor
                this.AltitudeSensor = Convert.ToInt32(getDecimalFromBinary(content, 134, 1));
                //Spare
                this.Spare = Convert.ToByte(getDecimalFromBinary(content, 135, 7));
                //DTE
                this.DTE = Convert.ToInt32(getDecimalFromBinary(content, 142, 1));
                //Spare
                this.Spare = Convert.ToByte(getDecimalFromBinary(content, 143, 3));
                //Assigned Mode Flag
                this.AssignedModeFlag = Convert.ToInt32(getDecimalFromBinary(content, 146, 1));
                //RAIM Flag
                this.RAIMFlag = Convert.ToInt32(getDecimalFromBinary(content, 147, 1));
                //Communication state selector flag
                this.CommunicationStateSelectorFlag = Convert.ToInt32(getDecimalFromBinary(content, 148, 1));
                //Communication state
                this.CommunicationState = Convert.ToInt32(getDecimalFromBinary(content, 149, 19));
            }
            catch (Exception ex)
            {
                log.Error(ex, " MessageType8 :: Parser()");
                throw;
            }


            return null;

        }
        #endregion

        #region getAttributesAndValues(): Attributeları döndürür.
        public override List<Tuple<string, string>> getAttributesAndValues()
        {
            List<Tuple<string, string>> _listAttribute = base.getAttributesAndValues();
            List<Tuple<string, string>> _attributes = new List<Tuple<string, string>> {
                  new Tuple<string, string>("User ID",this.UserID.ToString()),
                  new Tuple<string, string>("Altitude",this.Altitude.ToString()),
                  new Tuple<string, string>("SOG",this.SOG.ToString()),
                  new Tuple<string, string>("PositionAccuracy",this.PositionAccuracy.ToString()),
                  new Tuple<string, string>("Longitude",this.Longitude.ToString()),
                  new Tuple<string, string>("Latitude",this.Latitude.ToString()),
                  new Tuple<string, string>("COG",this.COG.ToString()),
                  new Tuple<string, string>("Time Stamp",this.TimeStamp.ToString()),
                  new Tuple<string, string>("Altitude Sensor",this.AltitudeSensor.ToString()),
                  new Tuple<string, string>("DTE",this.DTE.ToString()),
                  new Tuple<string, string>("Assigned Mode Flag",this.AssignedModeFlag.ToString()),
                  new Tuple<string, string>("RAIM Flag",this.RAIMFlag.ToString()),
                  new Tuple<string, string>("Communication State Selector Flag",this.CommunicationStateSelectorFlag.ToString())
             };
            _listAttribute.AddRange(_attributes);
            return _listAttribute;
        }
        #endregion

        #endregion

      
        #region ToString mesajını ezdik. Methodu sınıfa göre tasarladık.
        public override string ToString()
        {
            return
                "Message ID: " + this.MessageID + "\n" +
                "Repeat Indicator: " + this.RepeatIndicator + "\n" +
                "User ID: " + this.UserID + "\n" +
                "Altitude(GNSS): " + this.Altitude + "\n" +
                "SOG" + this.SOG + "\n" +
                "PO: " + this.PositionAccuracy + "\n" +
                "Longitude: " + this.Longitude + "\n" +
                "Latitude: " + this.Latitude + "\n" +
                "COG: " + this.COG + "\n" +
                "Time stamp: " + this.TimeStamp + "\n" +
                "Altitude Sensor: " + this.AltitudeSensor + "\n" +
                "Spare: " + this.Spare + "\n" +
                "DTE: " + this.DTE + "\n" +
                "Spare: " + this.Spare + "\n" +
                "Assigned Mode Flag: " + this.AssignedModeFlag + "\n" +
                "RAIM Flag: " + this.RAIMFlag + "\n" +
                "Communication State Selector Flag: " + this.CommunicationStateSelectorFlag + "\n" +
                "Communication State: " + this.CommunicationState + "\n";

        }
        #endregion

        
    }
}
