using NLog;
using NMEA_MessageParserConstructor.BL.CommunicationState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA_MessageParserConstructor.BL.Messages
{
    public class MessageType18 : RootMessages
    {
        private int UserID { get; set; }
        private double SOG { get; set; }
        private int PositionAccuracy { get; set; }
        private double Longitude { get; set; }
        private double Latitude { get; set; }
        private double COG { get; set; }
        private double TrueHeading { get; set; }
        private int TimeStamp { get; set; }
        private byte Spare2 { get; set; }
        private byte ClassBUnitFlag { get; set; }
        private byte ClassBDisplayFlag { get; set; }
        private byte ClassBDSCFlag { get; set; }
        private byte ClassBBandFlag { get; set; }
        private byte ClassBMessage22Flag { get; set; }
        private byte ModeFlag { get; set; }
        private byte RAIM_Flag { get; set; }
        private byte CommunicationStateSelectorFlag { get; set; }
        private SOTDMA Sotdma { get; set; }
        private ITDMA Itdma { get; set; }
        private Logger log { get; set; }

        public MessageType18()
        {
            this.MessageID = 18;
            this.Description = "Standard class B equipment position report";
            this.TotalNumberOfBits = 168;
            this.log = LogManager.GetCurrentClassLogger();
        }

        #region Parser

        #region Parser(): Mesaj yapısında bulunan attributelara, alınan mesajdaki değerleri set ettik.
        public override string[] Parser(string message1)
        {
            try
            {
                string[] messageParts1 = base.Parser(message1);
         
                string content = getContentBinary(messageParts1[5], Remove(messageParts1[6]));

                //Message ID
                this.MessageID = Convert.ToByte(getDecimalFromBinary(content, 0, 6));
                //Repeat Indicator
                this.RepeatIndicator = Convert.ToByte(getDecimalFromBinary(content, 6, 2));
                //Source ID - UserID
                this.UserID = Convert.ToInt32(getDecimalFromBinary(content, 8, 30));
                //Spare
                this.Spare = Convert.ToByte(getDecimalFromBinary(content, 38, 8));
                //SOG
                this.SOG = Convert.ToDouble(getDecimalFromBinary(content, 46, 10))/10;
                //Position Accuracy
                this.PositionAccuracy = Convert.ToInt32(getDecimalFromBinary(content, 56, 1));
                //Longitude
                this.Longitude = Convert.ToDouble(getDecimalFromBinary(content, 57, 28)) / 600000;
                //Latitude
                this.Latitude = Convert.ToDouble(getDecimalFromBinary(content, 85, 27)) / 600000;
                //COG
                this.COG = Convert.ToDouble(getDecimalFromBinary(content, 112, 12))/10;
                //True Heading
                this.TrueHeading = Convert.ToDouble(getDecimalFromBinary(content, 124, 9));
                //Time stamp
                this.TimeStamp = Convert.ToInt32(getDecimalFromBinary(content, 133, 6));
                //Spare
                this.Spare2 = Convert.ToByte(getDecimalFromBinary(content, 139, 2));
                //Class B unit flag
                this.ClassBUnitFlag = Convert.ToByte(getDecimalFromBinary(content, 141, 1));
                //Class B display flag
                this.ClassBDisplayFlag = Convert.ToByte(getDecimalFromBinary(content, 142, 1));
                //Class B DSC flag
                this.ClassBDSCFlag = Convert.ToByte(getDecimalFromBinary(content, 143, 1));
                //Class B band flag
                this.ClassBBandFlag = Convert.ToByte(getDecimalFromBinary(content, 144, 1));
                //Class B Message 22 flag
                this.ClassBMessage22Flag = Convert.ToByte(getDecimalFromBinary(content, 145, 1));
                //Mode Flag
                this.ModeFlag = Convert.ToByte(getDecimalFromBinary(content, 146, 1));
                //RAIM Flag
                this.RAIM_Flag = Convert.ToByte(getDecimalFromBinary(content, 147, 1));
                //Communication state selector flag
                this.CommunicationStateSelectorFlag = Convert.ToByte(getDecimalFromBinary(content, 148, 1));
                if (this.CommunicationStateSelectorFlag == 0)
                {
                    this.Sotdma = new SOTDMA();
                    
                    this.Sotdma.SyncState = Convert.ToByte(getDecimalFromBinary(content, 149, 2));
                    this.Sotdma.SlotTimeOut = Convert.ToByte(getDecimalFromBinary(content, 151, 3));
                    //this.Sotdma.subMessage.ReceivedStations = Convert.ToString(getStringFromBinary(content,154,))
                }
                else if (this.CommunicationStateSelectorFlag == 1)
                {
                    this.Itdma = new ITDMA();
                    this.Itdma.SyncState = Convert.ToByte(getDecimalFromBinary(content,149, 2));
                    this.Itdma.SlotIncrement = Convert.ToByte(getDecimalFromBinary(content, 151, 13));
                    this.Itdma.NumberOfSlots = Convert.ToByte(getDecimalFromBinary(content, 164, 3));
                    this.Itdma.KeepFlag = Convert.ToByte(getDecimalFromBinary(content, 167, 1));
                }

            }
            catch (Exception ex)
            {
                log.Error(ex, " MessageType18 :: Parser()");
                //throw;
                return null;
            }


            return null;
        }
        #endregion

        #region getAttributesAndValues(): Attributeları ve değerlerini döndürür.
        //new Tuple<string, string>("",this..ToString()),
        public override List<Tuple<string, string>> getAttributesAndValues()
        {
            List<Tuple<string, string>> _listAttribute = base.getAttributesAndValues(); 

            try
            {

                List<Tuple<string, string>> _attributes = new List<Tuple<string, string>> {
                  new Tuple<string, string>("User ID",this.UserID.ToString()),
                  new Tuple<string, string>("SOG",this.SOG.ToString()),
                  new Tuple<string, string>("Position Accuracy",this.PositionAccuracy.ToString()),
                  new Tuple<string, string>("Longitude",this.Longitude.ToString()),
                  new Tuple<string, string>("Latitude",this.Latitude.ToString()),
                  new Tuple<string, string>("COG",this.COG.ToString()),
                  new Tuple<string, string>("True Heading",this.TrueHeading.ToString()),
                  new Tuple<string, string>("Time Stamp",this.TimeStamp.ToString()),
                  new Tuple<string, string>("Spare 2",this.Spare2.ToString()),
                  new Tuple<string, string>("Class B Unit Flag",this.ClassBUnitFlag.ToString()),
                  new Tuple<string, string>("Class B Display Flag",this.ClassBDisplayFlag.ToString()),
                  new Tuple<string, string>("Class B DSC Flag",this.ClassBDSCFlag.ToString()),
                  new Tuple<string, string>("Class B Band Flag",this.ClassBBandFlag.ToString()),
                  new Tuple<string, string>("Class B Message 22 Flag ",this.ClassBMessage22Flag.ToString()),
                  new Tuple<string, string>("Mode Flag",this.ModeFlag.ToString()),
                  new Tuple<string, string>("RAIM Flag",this.RAIM_Flag.ToString()),
                  new Tuple<string, string>("Communication State Selector Flag",this.CommunicationStateSelectorFlag.ToString()),

               };
                _listAttribute.AddRange(_attributes);
                List<Tuple<string, string>> _list = null;
                if (this.CommunicationStateSelectorFlag == 0)
                    _list = this.Sotdma.getAttributesAndValues();
                else
                    _list = this.Itdma.getAttributes();
                foreach (var item in _list)
                {
                    _listAttribute.Add(new Tuple<string, string>(item.Item1, item.Item2));
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "MessageType17 :: getAttribute");
            }

            return _listAttribute;
        }
        #endregion

        #endregion

        #region ToString mesajını ezdik. Methodu sınıfa göre tasarladık.
        public override string ToString()
        {
            string com = "";
            if (this.CommunicationStateSelectorFlag == 0)
                com = this.Sotdma.ToString();
            else if (this.CommunicationStateSelectorFlag == 1)
                com = this.Itdma.ToString();

            return
                "Message ID: " + this.MessageID + "\n" +
                "Repeat Indicator: " + this.RepeatIndicator + "\n" +
                "User ID: " + this.UserID + "\n" +
                "Spare: " + this.Spare + "\n" +
                "SOG: " + this.SOG + "\n" +
                "Position Accuracy: " + this.PositionAccuracy + "\n" +
                "Longitude: " + this.Longitude + "\n" +
                "Latitude: " + this.Latitude + "\n" +
                "COG: " + this.COG + "\n" +
                "True Heading: " + this.TrueHeading + "\n" +
                "Time stamp: " + this.TimeStamp + "\n" +
                "Spare 2 : " + this.Spare2 + "\n" +
                "Class B Unit Flag: " + this.ClassBUnitFlag + "\n" +
                "Class B Display Flag: " + this.ClassBDisplayFlag + "\n" +
                "Class B DSC Flag: " + this.ClassBDSCFlag + "\n" +
                "Class B Band Flag: " + this.ClassBBandFlag + "\n" +
                "Class B Message 22 flag: " + this.ClassBMessage22Flag + "\n" +
                "Mode Flag: " + this.ModeFlag + "\n" +
                "RAIM Flag: " + this.RAIM_Flag + "\n" +
                com;

        }
        #endregion

        #region Constructor 


        #region getAttributes(): Attributeları döndürür.
        public override List<Tuple<string, string>> getAttributes()
        {
            List<Tuple<string, string>> _listAttribute = base.getAttributes();
            this.Sotdma = new SOTDMA();
            this.Itdma = new ITDMA();
            List<Tuple<string, string>> _listSotdma = Sotdma.getAttributes();
            List<Tuple<string, string>> _listItdma = Itdma.getAttributes();
            List<Tuple<string, string>> _attributes = new List<Tuple<string, string>> {
                  new Tuple<string, string>("Userr ID",""),
                  new Tuple<string, string>("SOG",""),
                  new Tuple<string, string>("Position Accuracy",""),
                  new Tuple<string, string>("Longitude",""),
                  new Tuple<string, string>("Latitude",""),
                  new Tuple<string, string>("COG",""),
                  new Tuple<string, string>("True Heading",""),
                  new Tuple<string, string>("Time Stamp",""),
                  new Tuple<string, string>("Spare 2",""),
                  new Tuple<string, string>("Class B Unit Flag",""),
                  new Tuple<string, string>("Class B Display Flag",""),
                  new Tuple<string, string>("Class B DSC Flag",""),
                  new Tuple<string, string>("Class B Band Flag",""),
                  new Tuple<string, string>("Class B Message 22 Flag",""),
                  new Tuple<string, string>("Mode Flag",""),
                  new Tuple<string, string>("RAIM Flag",""),
                  new Tuple<string, string>("Communication State Selector Flag",""),
             };
            _listAttribute.AddRange(_attributes);
            foreach (var att in _listSotdma)
            {
                _listAttribute.Add(new Tuple<string, string>(att.Item1, att.Item2));
            }

            foreach (var att in _listItdma)
            {
                _listAttribute.Add(new Tuple<string, string>(att.Item1, att.Item2));
            }

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
