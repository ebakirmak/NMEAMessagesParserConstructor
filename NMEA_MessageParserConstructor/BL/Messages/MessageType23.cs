using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA_MessageParserConstructor.BL.Messages
{
    public class MessageType23 : RootMessages
    {
        private int SourceID { get; set; }
        private double Longitude1 { get; set; }
        private double Latitude1 { get; set; }
        private double Longitude2 { get; set; }
        private double Latitude2 { get; set; }
        private byte StationType { get; set; }
        private byte TypeOfShipAndCargoType { get; set; }
        private byte Spare2 { get; set; }
        private byte TxRxMode { get; set; }
        private byte ReportingInterval { get; set; }
        private byte QuietTime { get; set; }
        private byte Spare3 { get; set; }
        private Logger log { get; set; }

        public MessageType23()
        {
            this.MessageID = 23;
            this.Description = "Group Assignment Command";
            this.log = LogManager.GetCurrentClassLogger();
        }

        #region Parser
        #region Mesaj yapısında bulunan attributelara, alınan mesajdaki değerleri set ettik.
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
                //Source ID
                this.SourceID = Convert.ToInt32(getDecimalFromBinary(content, 8, 30));
                //Spare
                this.Spare = Convert.ToByte(getDecimalFromBinary(content, 38, 2));
                //Longitude 1
                this.Longitude1 = Convert.ToDouble(getDecimalFromBinary(content, 40, 18)) / 600;
                //Latitude 1
                this.Latitude1 = Convert.ToDouble(getDecimalFromBinary(content, 58, 17)) / 600;
                //Longitude 2
                this.Longitude2 = Convert.ToDouble(getDecimalFromBinary(content, 75, 18)) / 600;
                //Latitude 2
                this.Latitude2 = Convert.ToDouble(getDecimalFromBinary(content, 93, 17)) / 600;
                //Station Type
                this.StationType = Convert.ToByte(getDecimalFromBinary(content, 110, 4));
                //Type of Ship and cargo type
                this.TypeOfShipAndCargoType = Convert.ToByte(getDecimalFromBinary(content, 114, 8));
                //Spare 2
                this.Spare2 = Convert.ToByte(getDecimalFromBinary(content, 122, 22));
                //Tx/Rx Mode
                this.TxRxMode = Convert.ToByte(getDecimalFromBinary(content, 144, 2));
                //Reporting interval
                this.ReportingInterval = Convert.ToByte(getDecimalFromBinary(content, 146, 4));
                //Quiet time
                this.QuietTime = Convert.ToByte(getDecimalFromBinary(content, 150, 4));
                //Spare
                this.Spare3 = Convert.ToByte(getDecimalFromBinary(content, 154, 6));
               
                //Spare 2
                this.Spare2 = Convert.ToByte(getDecimalFromBinary(content, 145, 23));

            }
            catch (Exception ex)
            {
                log.Error(ex, "MessageType22 :: Parser");
                //throw;
            }

            return null;
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
                new Tuple<string, string>("Source ID",this.SourceID.ToString()),
                new Tuple<string, string>("Longitude 1",this.Longitude1.ToString()),
                new Tuple<string, string>("Latitude 1",this.Latitude1.ToString()),
                new Tuple<string, string>("Longitude 2",this.Longitude2.ToString()),
                new Tuple<string, string>("Latitude 2",this.Latitude2.ToString()),
                new Tuple<string, string>("Station Type",this.StationType.ToString()),
                new Tuple<string, string>("Type Of Ship And Cargo Type",this.TypeOfShipAndCargoType.ToString()),
                new Tuple<string, string>("Spare 2",this.Spare2.ToString()),
                new Tuple<string, string>("Tx/Rx Mode",this.TxRxMode.ToString()),
                new Tuple<string, string>("Reporting Interval",this.ReportingInterval.ToString()),
                new Tuple<string, string>("Quiet Time",this.QuietTime.ToString()),
                new Tuple<string, string>("Spare 3",this.Spare3.ToString()),
                };
                _listAttribute.AddRange(_attributes);



            }
            catch (Exception ex)
            {
                log.Error(ex, "MessageType19 :: getAttribute");
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
                "Source ID: " + this.SourceID + "\n" +
                "Spare: " + this.Spare + "\n" +
                "Longitude 1: " + this.Longitude1 + "\n" +
                "Latitude 1: " + this.Latitude1 + "\n" +
                "Longitude 2: " + this.Longitude2 + "\n" +
                "Latitude 2: " + this.Latitude2 + "\n" +
                "StationType : " + this.StationType + "\n" +
                "Type Of Ship And Cargo Type: " + this.TypeOfShipAndCargoType + "\n" +
                "Spare 2: " + this.Spare2 + "\n" +
                "Tx/Rx Mode: " + this.TxRxMode + "\n" +
                "Reporting Interval: " + this.ReportingInterval + "\n" +
                "Quiet Time: " + this.QuietTime + "\n" +
                "Spare 3: " + this.Spare3 + "\n";



        }
        #endregion

        #region Constructor 

        #region getAttributes(): Attributeları döndürür.
        public override List<Tuple<string, string>> getAttributes()
        {
            List<Tuple<string, string>> _listAttribute = base.getAttributes();
            List<Tuple<string, string>> _attributes = new List<Tuple<string, string>> {
                  new Tuple<string, string>("Source ID",""),
                  new Tuple<string, string>("Longitude 1",""),
                  new Tuple<string, string>("Latitude 1",""),
                  new Tuple<string, string>("Longitude 2",""),
                  new Tuple<string, string>("Latitude 2",""),
                  new Tuple<string, string>("Station Type",""),
                  new Tuple<string, string>("Ship And Cargo Type",""),
                  new Tuple<string, string>("Spare 2",""),
                  new Tuple<string, string>("Tx/Rx Mode",""),
                  new Tuple<string, string>("Reporting Interval",""),
                  new Tuple<string, string>("Quiet Time",""),
                  new Tuple<string, string>("Spare 3","")
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
