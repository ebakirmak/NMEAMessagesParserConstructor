using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA_MessageParserConstructor.BL.Messages
{
    public class MessageType22 : RootMessages
    {
        public int StationID { get; set; }
        public int ChannelA { get; set; }
        public int ChannelB { get; set; }
        public byte TxRxMode { get; set; }
        public byte Power { get; set; }
        public double Longitude1 { get; set; }
        public double Latitude1 { get; set; }
        public double Longitude2 { get; set; }
        public double Latitude2 { get; set; }
        //Addressed Or Broadcast Message Indicator
        public byte Addressed { get; set; }
        //Channel A Bandwidth
        public byte BandwidthA { get; set; }
        //Channel B Bandwidth
        public int BandwidthB { get; set; }
        //Transitional zone size
        public byte Zone { get; set; }
        public byte Spare2 { get; set; }
        public Logger log { get; set; }

        public MessageType22()
        {
            this.MessageID = 22;
            this.Description = "Channel Management";
            this.log = LogManager.GetCurrentClassLogger();
        }

        #region Parse
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
                //Source MMSI
                this.StationID = Convert.ToInt32(getDecimalFromBinary(content, 8, 30));
                //Spare
                this.Spare = Convert.ToByte(getDecimalFromBinary(content, 38, 2));
                //Channel A
                this.ChannelA = Convert.ToInt32(getDecimalFromBinary(content, 40, 12));
                //Channel B
                this.ChannelB = Convert.ToInt32(getDecimalFromBinary(content, 52, 12));
                //Tx/Rx Mode
                this.TxRxMode = Convert.ToByte(getDecimalFromBinary(content, 64, 4));
                //Power
                this.Power = Convert.ToByte(getDecimalFromBinary(content, 68, 1));
                //Longitude 1
                this.Longitude1 = Convert.ToDouble(getDecimalFromBinary(content, 69, 18))/600;
                //Latitude 1
                this.Latitude1 = Convert.ToDouble(getDecimalFromBinary(content, 87, 17)) / 600;
                //Longitude 2
                this.Longitude2 = Convert.ToDouble(getDecimalFromBinary(content, 104, 18)) / 600;
                //Latitude 2
                this.Latitude2 = Convert.ToDouble(getDecimalFromBinary(content, 122, 17)) / 600;
                //Addressed or broadcast message indicator
                this.Addressed = Convert.ToByte(getDecimalFromBinary(content, 139, 1));
                //Channel A bandwidth
                this.BandwidthA = Convert.ToByte(getDecimalFromBinary(content, 140, 1));
                //Channel B bandwidth
                this.BandwidthB = Convert.ToByte(getDecimalFromBinary(content, 141, 1));
                //Transitional zone size
                this.Zone = Convert.ToByte(getDecimalFromBinary(content, 142, 3));
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
                new Tuple<string, string>("Station ID",this.StationID.ToString()),
                new Tuple<string, string>("Channel A",this.ChannelA.ToString()),
                new Tuple<string, string>("Channel B",this.ChannelB.ToString()),
                new Tuple<string, string>("Tx/Rx Mode",this.TxRxMode.ToString()),
                new Tuple<string, string>("Power",this.Power.ToString()),
                new Tuple<string, string>("Longitude 1",this.Longitude1.ToString()),
                new Tuple<string, string>("Latitude 1",this.Latitude1.ToString()),
                new Tuple<string, string>("Longitude 2",this.Longitude2.ToString()),
                new Tuple<string, string>("Latitude 2",this.Latitude2.ToString()),
                new Tuple<string, string>("Broadcast Message Indicator",this.Addressed.ToString()),
                new Tuple<string, string>("Channel A Bandwidth ",this.BandwidthA.ToString()),
                new Tuple<string, string>("Channel B Bandwidth",this.BandwidthB.ToString()),
                new Tuple<string, string>("Zone",this.Zone.ToString()),
                new Tuple<string, string>("Spare 2",this.Spare2.ToString()),

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
                "Station ID" + this.StationID + "\n" +
                "Spare: " + this.Spare + "\n" +
                "Channel A: " + this.ChannelA + "\n" +
                "Channel B: " + this.ChannelB + "\n" +
                "Tx/Rx Mode: " + this.TxRxMode + "\n" +
                "Power: " + this.Power + "\n" +
                "Longitude 1: " + this.Longitude1 + "\n" +
                "Latitude 1: " + this.Latitude1 + "\n" +
                "Longitude 2: " + this.Longitude2 + "\n" +
                "Latitude 2: " + this.Latitude2 + "\n" +
                "Adressed or Broadcast: " + this.Addressed + "\n" +
                "Channel A Bandwidth: " + this.BandwidthA + "\n" +
                "Channel B Bandwidth: " + this.BandwidthB + "\n" +
                "Transitional Zone: " + this.Zone + "\n" +
                "Spare2: " + this.Spare2 + "\n";



        }
        #endregion

        #region Constructor 

        #region getAttributes(): Attributeları döndürür.
        public override List<Tuple<string, string>> getAttributes()
        {
            List<Tuple<string, string>> _listAttribute = base.getAttributes();
            List<Tuple<string, string>> _attributes = new List<Tuple<string, string>> {
                  new Tuple<string, string>("Station ID",""),
                  new Tuple<string, string>("Channel A",""),
                  new Tuple<string, string>("Channel B",""),
                  new Tuple<string, string>("Tx/Rx Mode",""),
                  new Tuple<string, string>("Power",""),
                  new Tuple<string, string>("Longitude 1",""),
                  new Tuple<string, string>("Latitude 1",""),
                  new Tuple<string, string>("Longitude 2",""),
                  new Tuple<string, string>("Latitude 2",""),
                  new Tuple<string, string>("Adressed",""),
                  new Tuple<string, string>("Bandwidth A",""),
                  new Tuple<string, string>("Bandwidth B",""),
                  new Tuple<string, string>("Zone",""),
                  new Tuple<string, string>("Spare 2","")
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
