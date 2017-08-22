using NLog;
using NMEA_MessageParserConstructor.BL.AnnexClasses;
using NMEA_MessageParserConstructor.BL.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA_MessageParserConstructor.BL.Messages
{
    public class MessageType21 : RootMessages
    {
        private int ID { get; set; }
        private byte TypeOfAidsToNavigation { get; set; }
        private string NameOfAidsToNavigation { get; set; }
        private byte PositionAccuracy { get; set; }
        private double Longitude { get; set; }
        private double Latitude { get; set; }
        private OverallDimension DimensionOfShip { get; set; }
        //Type Of Electronic Position Fixing Device
        private int TOEPFD { get; set; }
        private byte TimeStamp { get; set; }
        private byte OffPositionIndicator { get; set; }
        private string AtoNStatus { get; set; }
        private byte RAIMFlag { get; set; }
        private byte VirtualAtoNFlag { get; set; }
        private byte AssignedModeFlag { get; set; }
        private string NameOfAidToNavigationExtension { get; set; }
        private byte Spare2 { get; set; }
        private Logger log { get; set; }

        public MessageType21()
        {
            this.MessageID = 21;
            this.Description = "Aids to navigation Report";
            this.DimensionOfShip = new OverallDimension();
            this.log = LogManager.GetCurrentClassLogger();

        }

        #region Parser

        #region Mesaj yapısında bulunan attributelara, alınan mesajdaki değerleri set ettik.
        public override string[] Parser(string message1,string message2)
        {
            try
            {
                string[] messageParts1 = base.Parser(message1);
                string[] messageParts2 = base.Parser(message2);
                //Context'i oku. Binary yapıda.
                string content = getContentBinary(messageParts1[5], Remove(messageParts1[6]));
                content += getContentBinary(messageParts2[5], Remove(messageParts2[6]));
                //Tüm mesajlarda olan özellikleri burada gir.
                //MessageID
                this.MessageID = Convert.ToByte(getDecimalFromBinary(content, 0, 6));
                //Repeat indicator
                this.RepeatIndicator = Convert.ToByte(getDecimalFromBinary(content, 6, 2));
                //Source MMSI
                this.ID = Convert.ToInt32(getDecimalFromBinary(content, 8, 30));
                //Type of aids to navigation
                this.TypeOfAidsToNavigation = Convert.ToByte(getDecimalFromBinary(content, 38, 5));
                //Name of Aids to Navigation
                this.NameOfAidsToNavigation = Convert.ToString(getStringFromBinary(content, 43, 120));
                //PA
                this.PositionAccuracy = Convert.ToByte(getDecimalFromBinary(content, 163, 1));
                //LON - Dakikaya çevrildi ve 10.000 ile çarpıldı.
                this.Longitude = Convert.ToDouble((getDecimalFromBinary(content, 164, 28))) / 600000;
                //LAT - Dakikaya çevrildi ve 10.000 ile çarpıldı.
                this.Latitude = Convert.ToDouble(getDecimalFromBinary(content, 192, 27)) / 600000;
                //Dimension / reference for position2
                this.DimensionOfShip.setValue(content, 219);
                //Type of electronic position Fixing Device
                this.TOEPFD = Convert.ToByte(getDecimalFromBinary(content, 249, 4));
                //Time stamp
                this.TimeStamp = Convert.ToByte(getDecimalFromBinary(content, 253, 6));
                //Off Position Indicator
                this.OffPositionIndicator = Convert.ToByte(getDecimalFromBinary(content, 259, 1));
                //AtoN status
                this.AtoNStatus = content.Substring(260, 8);
                //RAIM Flag
                this.RAIMFlag = Convert.ToByte(getDecimalFromBinary(content, 268, 1));
                //Virtual AtoN Flag
                this.VirtualAtoNFlag = Convert.ToByte(getDecimalFromBinary(content, 269, 1));
                //Assigned mode Flag
                this.AssignedModeFlag = Convert.ToByte(getDecimalFromBinary(content, 270, 1));
                //Spare
                this.Spare = Convert.ToByte(getDecimalFromBinary(content, 271, 1));
                //Name of Aid-to-Navigation Extension
                this.NameOfAidToNavigationExtension = Convert.ToString(getStringFromBinary(content, 272, content.Length - 272));
                if (content.Length > 356) {
                    
                    //Spare 2
                    this.Spare2 = Convert.ToByte(getDecimalFromBinary(content, 356, 4));
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "MessageType21 :: Parser");
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
                new Tuple<string, string>("ID",this.ID.ToString()),
                new Tuple<string, string>("Type Of Aids To Navigation",this.TypeOfAidsToNavigation.ToString()),
                new Tuple<string, string>("Name Of Aids To Navigation",this.NameOfAidsToNavigation.ToString()),
                new Tuple<string, string>("Position Accuracy",this.PositionAccuracy.ToString()),
                new Tuple<string, string>("Longitude",this.Longitude.ToString()),
                new Tuple<string, string>("Latitude",this.Latitude.ToString()),
                new Tuple<string, string>("Dimension Of Ship",this.DimensionOfShip.ToString()),
                new Tuple<string, string>("Type Of Electronic Postion Fixing Device",this.TOEPFD.ToString()),
                new Tuple<string, string>("Time Stamp",this.TimeStamp.ToString()),
                new Tuple<string, string>("Off Position Indicator",this.OffPositionIndicator.ToString()),
                new Tuple<string, string>("AtoN Status",this.AtoNStatus.ToString()),
                new Tuple<string, string>("RAIM Flag",this.RAIMFlag.ToString()),
                new Tuple<string, string>("Virtual AtoN Flag",this.VirtualAtoNFlag.ToString()),
                new Tuple<string, string>("Assigned Mode Flag",this.AssignedModeFlag.ToString()),
                new Tuple<string, string>("Name Of Aid To Navigation Extension",this.NameOfAidToNavigationExtension.ToString()),
                new Tuple<string, string>("Spare 2",this.Spare2.ToString())

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
                "RepeatIndicator" + this.RepeatIndicator + "\n" +
                "User ID / MMSI" + this.ID + "\n" +
                "type of aids to navigation: " + this.TypeOfAidsToNavigation + (TypeOfAidsToNavigation)this.TypeOfAidsToNavigation + "\n" +
                "Name of aids to navigation:" + this.NameOfAidsToNavigation + "\n" +
                "PA" + this.PositionAccuracy + "\n" +
                "Lon" + this.Longitude + "\n" +
                "Lat" + this.Latitude + "\n" +
                "Dimension reference for position:\n\t A: " + this.DimensionOfShip.getA() +
                                                  "\n\tB: " + this.DimensionOfShip.getB() +
                                                  "\n\tC: " + this.DimensionOfShip.getC() +
                                                  "\n\tD: " + this.DimensionOfShip.getD() + "\n" +
                "Type of electronic position fixing device: " + this.TOEPFD + "\n" +
                "Time stamp: " + this.TimeStamp + "\n" +
                "Off position indicator: " + this.OffPositionIndicator + "\n" +
                "AtoN Status: " + this.AtoNStatus + "\n" +
                "RAIM Flag: " + this.RAIMFlag + "\n " +
                "Virtual AtoN Flag: " + this.VirtualAtoNFlag + "\n" +
                "Assigned mode Flag: " + this.AssignedModeFlag + "\n" +
                "Spare: " + this.Spare + "\n" +
                "Name of Aid to Navigation Extension: " + this.NameOfAidToNavigationExtension + "\n" +
                "Spare: " + this.Spare2 + "\n";



        }
        #endregion

        #region Constructor 

        #region getAttributes(): Attributeları döndürür.
        public override List<Tuple<string, string>> getAttributes()
        {
            List<Tuple<string, string>> _listAttribute = base.getAttributes();
            List<Tuple<string, string>> _attributes = new List<Tuple<string, string>> {
                  new Tuple<string, string>("ID",""),
                  new Tuple<string, string>("Type Of Aids To Navigation",""),
                  new Tuple<string, string>("Name Of Aids To Navigation",""),
                  new Tuple<string, string>("Position Accuracy",""),
                  new Tuple<string, string>("Longitude",""),
                  new Tuple<string, string>("Latitude",""),
                  new Tuple<string, string>("Dim A",""),
                  new Tuple<string, string>("Dim B",""),
                  new Tuple<string, string>("Dim C",""),
                  new Tuple<string, string>("Dim D",""),
                  new Tuple<string, string>("Type Of EPFD",""),
                  new Tuple<string, string>("Time Stamp",""),
                  new Tuple<string, string>("Off Position Indicator",""),
                  new Tuple<string, string>("AtoN Status",""),
                  new Tuple<string, string>("RAIM Flag",""),
                  new Tuple<string, string>("Virtual AtoN Flag",""),
                  new Tuple<string, string>("Assigned Mode Flag",""),
                  new Tuple<string, string>("Name Of Aid To Navigation Extension",""),
                  new Tuple<string, string>("Spare 2",""),
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
