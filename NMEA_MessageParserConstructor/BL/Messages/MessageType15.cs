using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA_MessageParserConstructor.BL.Messages
{
    public class MessageType15 : RootMessages
    {
        private int SourceID { get; set; }
        private int DestinationID1 { get; set; }
        private int MessageID1_1 { get; set; }
        private int SlotOffset1_1 { get; set; }
        private byte Spare2 { get; set; }
        private int MessageID1_2 { get; set; }
        private int SlotOffset1_2 { get; set; }
        private byte Spare3 { get; set; }
        private int DestinationID2 { get; set; }
        private int MessageID2_1 { get; set; }
        private int SlotOffset2_1 { get; set; }
        private byte Spare4 { get; set; }
        private Logger log;

        public MessageType15()
        {
            this.MessageID = 15;
            this.Description = "Interrogation";
            this.TotalNumberOfBits = 160;
            log = LogManager.GetCurrentClassLogger();
        }

        #region Parser

        #region Parser(): Mesaj yapısında bulunan attributelara, alınan mesajdaki değerleri set ettik.
        public override string[] Parser(string message)
        {
            try
            {
                string[] messageParts = base.Parser(message);
                string content = getContentBinary(messageParts[5], Remove(messageParts[6]));
                int a = content.Length;

                //Message ID
                this.MessageID = Convert.ToByte(getDecimalFromBinary(content, 0, 6));
                //Repeat Indicator
                this.RepeatIndicator = Convert.ToByte(getDecimalFromBinary(content, 6, 2));
                //Source ID - UserID
                this.SourceID = Convert.ToInt32(getDecimalFromBinary(content, 8, 30));
                //Spare
                this.Spare = Convert.ToByte(getDecimalFromBinary(content, 38, 2));
                //Destination ID 1
                this.DestinationID1 = Convert.ToInt32(getDecimalFromBinary(content, 40, 30));
                //Message ID 1.1
                this.MessageID1_1 = Convert.ToInt32(getDecimalFromBinary(content, 70, 6));
                //Slot Offset 1.1
                this.SlotOffset1_1 = Convert.ToInt32(getDecimalFromBinary(content, 76, 12));
                //Spare 2
                this.Spare2 = Convert.ToByte(getDecimalFromBinary(content, 88, 2));
                //Message ID 1.2
                this.MessageID1_2 = Convert.ToInt32(getDecimalFromBinary(content, 90, 6));
                //Slot Offset 1.2
                this.SlotOffset1_2 = Convert.ToInt32(getDecimalFromBinary(content, 96, 12));
                //Spare 3
                this.Spare3 = Convert.ToByte(getDecimalFromBinary(content, 108, 2));
                //Destination ID 2
                this.DestinationID2 = Convert.ToInt32(getDecimalFromBinary(content, 110, 30));
                //Message ID 2.1
                this.MessageID2_1 = Convert.ToInt32(getDecimalFromBinary(content, 140, 6));
                //Slot Offset 2.1
                this.SlotOffset2_1 = Convert.ToInt32(getDecimalFromBinary(content, 146, 12));
                //Spare 4
                this.Spare4 = Convert.ToByte(getDecimalFromBinary(content, 158, 2));

            }
            catch (Exception ex)
            {
                log.Error(ex, " MessageType15 :: Parser()");
                //throw;
                return null;
            }


            return null;
        }
        #endregion

        #region getAttrbitesAndValues(): Attributeları ve değerlerini döndürür.
        public override List<Tuple<string, string>> getAttributesAndValues()
        {
            List<Tuple<string, string>> _listAttribute = base.getAttributesAndValues();
            try
            {

                List<Tuple<string, string>> _attributes = new List<Tuple<string, string>> {
                  new Tuple<string, string>("Source ID",this.SourceID.ToString()),
                  new Tuple<string, string>("Destination ID 1",this.DestinationID1.ToString()),
                  new Tuple<string, string>("Message ID 1.1",this.MessageID1_1.ToString()),
                  new Tuple<string, string>("Slot Off Set 1.1",this.SlotOffset1_1.ToString()),
                  new Tuple<string, string>("Spare 2",this.Spare2.ToString()),
                  new Tuple<string, string>("Message ID 1.2",this.MessageID1_2.ToString()),
                  new Tuple<string, string>("Slot Off Set 1ç2",this.SlotOffset1_2.ToString()),
                  new Tuple<string, string>("Spare 3",this.Spare3.ToString()),
                  new Tuple<string, string>("Destination ID 2",this.DestinationID2.ToString()),
                  new Tuple<string, string>("Message ID 2.1",this.MessageID2_1.ToString()),
                  new Tuple<string, string>("Slot Off Set 2.1",this.SlotOffset2_1.ToString()),
                  new Tuple<string, string>("Spare 4",this.Spare4.ToString()),
               };

                _listAttribute.AddRange(_attributes);
            }
            catch (Exception ex)
            {
                log.Error(ex, "MessageType15 :: getAttribute");
            }

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
                "Source ID: " + this.SourceID + "\n" +
                "Spare: " + this.Spare + "\n" +
                "Destination ID 1: " + this.DestinationID1 + "\n" +
                "Message ID 1.1: " + this.MessageID1_1 + "\n" +
                "Slot Offset 1.1: " + this.SlotOffset1_1 + "\n" +
                "Spare2: " + this.Spare2 + "\n" +
                "Message ID 1.2: " + this.MessageID1_2 + "\n" +
                "Slot Offset 1.2: " + this.SlotOffset1_2 + "\n" +
                "Spare 3: " + this.Spare3 + "\n" +
                "Destination ID 2: " + this.DestinationID2 + "\n" +
                "Message ID 2.1" + this.MessageID1_2 + "\n" +
                "Slot Offset 2.1 " + this.SlotOffset2_1 + "\n" +
                "Spare 4: " + this.Spare4 + "\n";



        }
        #endregion

        #region Constructor 


        #region getAttributes(): Attributeları döndürür.
        public override List<Tuple<string, string>> getAttributes()
        {
            List<Tuple<string, string>> _listAttribute = base.getAttributes();
            List<Tuple<string, string>> _attributes = new List<Tuple<string, string>> {
                  new Tuple<string, string>("Source ID",""),
                  new Tuple<string, string>("Destination ID 1",""),
                  new Tuple<string, string>("Message ID 1.1",""),
                  new Tuple<string, string>("Slot Off Set 1.1",""),
                  new Tuple<string, string>("Spare 2",""),
                  new Tuple<string, string>("Message ID 1.2",""),
                  new Tuple<string, string>("Slot Off Set 1.2",""),
                  new Tuple<string, string>("Spare 3",""),
                  new Tuple<string, string>("Destination ID 2",""),
                  new Tuple<string, string>("Message ID 2.1",""),
                  new Tuple<string, string>("Slot Off Set 2.1",""),
                  new Tuple<string, string>("Spare 4",""),
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
