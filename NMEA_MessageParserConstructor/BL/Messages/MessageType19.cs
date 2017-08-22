using NLog;
using NMEA_MessageParserConstructor.BL.AnnexClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA_MessageParserConstructor.BL.Messages
{
    public class MessageType19 : RootMessages
    {

        private int UserID { get; set; }
        private double SOG { get; set; }
        private byte PositionAccuracy { get; set; }
        private double Longitude { get; set; }
        private double Latitude { get; set; }
        private double COG { get; set; }
        private double TrueHeading { get; set; }
        private int TimeStamp { get; set; }
        private byte Spare2 { get; set; }
        private string Name { get; set; }
        private byte TypeOfShipAndCargoType { get; set; }
        //Dimension Of Ship/reference for position 
        private OverallDimension DimensionOfShip { get; set; }
        //Type of electronic position fixing device
        private byte TypeOfElectronicPositionFixing { get; set; }
        //RAIM FLag
        private byte RAIMFlag { get; set; }
        private byte DTE { get; set; }
        private byte AssignedModeFlag { get; set; }
        private byte Spare3 { get; set; }
        private Logger log { get; set; }

     

        public MessageType19()
        {
            this.MessageID = 19;
            this.Description = "Extended class B equipment position report";
            this.DimensionOfShip = new OverallDimension();
            this.log = LogManager.GetCurrentClassLogger();
        }

        #region Parser
        #region Mesaj yapısında bulunan attributelara, alınan mesajdaki değerleri set ettik.
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
                this.PositionAccuracy = Convert.ToByte(getDecimalFromBinary(content, 56, 1));
                //Longitude
                this.Longitude = Convert.ToDouble(getDecimalFromBinary(content, 57, 28)) / 600000;
                //Latitude
                this.Latitude = Convert.ToDouble(getDecimalFromBinary(content, 85, 27)) / 600000;
                //COG
                this.COG = Convert.ToDouble(getDecimalFromBinary(content, 112, 12)) / 10;
                //True Heading
                this.TrueHeading = Convert.ToDouble(getDecimalFromBinary(content, 124, 9));
                //Time stamp
                this.TimeStamp = Convert.ToInt32(getDecimalFromBinary(content, 133, 6));
                //Spare
                this.Spare2 = Convert.ToByte(getDecimalFromBinary(content, 139, 4));
                //Name
                this.Name = Convert.ToString(getStringFromBinary(content, 143, 120));
                //Type of ship and cargo type
                this.TypeOfShipAndCargoType = Convert.ToByte(getDecimalFromBinary(content, 263, 8));
                //Dimension of ship/reference for position
                this.DimensionOfShip.setValue(content, 271);
                //Type of Electronic position fixing device
                this.TypeOfElectronicPositionFixing = Convert.ToByte(getDecimalFromBinary(content, 301, 4));
                //RAIM Flag
                this.RAIMFlag = Convert.ToByte(getDecimalFromBinary(content, 305, 1));
                //DTE
                this.DTE = Convert.ToByte(getDecimalFromBinary(content, 306, 1));
                //ASsigned Mode Flag
                this.AssignedModeFlag = Convert.ToByte(getDecimalFromBinary(content, 307, 1));
                //Spare
                this.Spare3 = Convert.ToByte(getDecimalFromBinary(content, 308, 4));
              

            }
            catch (Exception ex)
            {
                log.Error(ex, " MessageType19 :: Parser()");
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
                 "Name: " + this.Name + "\n" +
                 "Type of ship and cargo type: " + this.TypeOfShipAndCargoType + "\n" +
                 "Dimension of ship: " + this.DimensionOfShip.getA() 
                                      + this.DimensionOfShip.getB()
                                      + this.DimensionOfShip.getC()
                                      + this.DimensionOfShip.getD() + "\n" +
                 "Type of electronic position fixing device: " + this.TypeOfElectronicPositionFixing + "\n" +
                 "RAIM Flag: " + this.RAIMFlag + "\n" +
                 "DTE: " + this.DTE + "\n" +
                 "Assigned mode flag: " + this.AssignedModeFlag + "\n" +
                 "Spare: " + this.Spare3 + "\n";
               

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
                  new Tuple<string, string>("User ID",this.UserID.ToString()),
                  new Tuple<string, string>("SOG",this.SOG.ToString()),
                  new Tuple<string, string>("Position Accuracy",this.PositionAccuracy.ToString()),
                  new Tuple<string, string>("Longitude",this.Longitude.ToString()),
                  new Tuple<string, string>("Latitude",this.Latitude.ToString()),
                  new Tuple<string, string>("COG",this.COG.ToString()),
                  new Tuple<string, string>("True Heading",this.TrueHeading.ToString()),
                  new Tuple<string, string>("Time Stamp",this.TimeStamp.ToString()),
                  new Tuple<string, string>("Spare 2",this.Spare2.ToString()),
                  new Tuple<string, string>("Name",this.Name.ToString()),
                  new Tuple<string, string>("Type of ship and cargo type",this.TypeOfShipAndCargoType.ToString()),
                  new Tuple<string, string>("Dimension Of Ship",this.DimensionOfShip.ToString()),
                  new Tuple<string, string>("Type Of Electronic Position Fixing",this.TypeOfElectronicPositionFixing.ToString()),
                  new Tuple<string, string>("RAIM Flag",this.RAIMFlag.ToString()),
                  new Tuple<string, string>("DTE",this.DTE.ToString()),
                  new Tuple<string, string>("Assigned Mode Flag",this.AssignedModeFlag.ToString()),
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

        #region Constructor 

        #region getAttributes(): Attributeları döndürür.
        public override List<Tuple<string, string>> getAttributes()
        {
            List<Tuple<string, string>> _listAttribute = base.getAttributes();
            List<Tuple<string, string>> _attributes = new List<Tuple<string, string>> {
                  new Tuple<string, string>("User ID",""),
                  new Tuple<string, string>("SOG",""),
                  new Tuple<string, string>("Position Accuracy",""),
                  new Tuple<string, string>("Longitude",""),
                  new Tuple<string, string>("Latitude",""),
                  new Tuple<string, string>("COG",""),
                  new Tuple<string, string>("True Heading",""),
                  new Tuple<string, string>("Time Stamp",""),
                  new Tuple<string, string>("Spare 2",""),
                  new Tuple<string, string>("Name",""),
                  new Tuple<string, string>("Ship and Cargo",""),
                  new Tuple<string, string>("Dim A",""),
                  new Tuple<string, string>("Dim B",""),
                  new Tuple<string, string>("Dim C",""),
                  new Tuple<string, string>("Dim D",""),
                  new Tuple<string, string>("Fix Type",""),
                  new Tuple<string, string>("RAIM Flag",""),
                  new Tuple<string, string>("DTE",""),
                  new Tuple<string, string>("Assigned Mode Flag",""),
                  new Tuple<string, string>("Spare 3",""),
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
