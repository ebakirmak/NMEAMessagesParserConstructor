﻿using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA_MessageParserConstructor.BL.Messages
{
    public class MessageType12 : RootMessages
    {

        private int SourceID { get; set; }
        private byte SequenceNumber { get; set; }
        private int DestinationID { get; set; }
        private byte RetransmitFlag { get; set; }
        private string SafetyRelatedText { get; set; }
        private Logger log; 
        public MessageType12()
        {
            this.MessageID = 12;
            this.TotalNumberOfBits = 1008;
            this.Description = "Adressed Safety Related Message";
            this.log = LogManager.GetCurrentClassLogger();
        }

        #region Parser

        #region Parser(): Mesaj yapısında bulunan attributelara, alınan mesajdaki değerleri set ettik.
        public override string[] Parser(string message1, string message2)
        {
            string[] messageParts1 = base.Parser(message1);
            string content = "";
            //Context'i oku. Binary yapıda.
            if (messageParts1 != null)
                content = getContentBinary(messageParts1[5], Remove(messageParts1[6]));

            string[] messageParts2 = { };
            if (message2 != null)
            {
                messageParts2 = base.Parser(message2);
                //Context'i oku. Binary yapıda.       
                content += getContentBinary(messageParts2[5], Remove(messageParts2[6]));
            }


            //Tüm mesajlarda olan özellikleri burada gir.

            //MessageID
            this.MessageID = Convert.ToByte(getDecimalFromBinary(content, 0, 6));
            //Repeat Indicator
            this.RepeatIndicator = Convert.ToByte(getDecimalFromBinary(content, 6, 2));
            //SourceID -- MMSI
            this.SourceID = Convert.ToInt32(getDecimalFromBinary(content, 8, 30));
            //Sequence Number
            this.SequenceNumber = Convert.ToByte(getDecimalFromBinary(content, 38, 2));
            //Destination ID
            this.DestinationID = Convert.ToInt32(getDecimalFromBinary(content, 40, 30));
            //Retransmit Flag
            this.RetransmitFlag = Convert.ToByte(getDecimalFromBinary(content, 70, 1));
            //Spare
            this.Spare = Convert.ToByte(getDecimalFromBinary(content, 71, 1));
            //Safety Related Text
            this.SafetyRelatedText = Convert.ToString(getStringFromBinary(content, 72, content.Length - 72));

            return null;
        }
        #endregion

        #region getAttributesAndValues(): Attributeları ve değerlerini döndürür.
        public override List<Tuple<string, string>> getAttributesAndValues()
        {
            List<Tuple<string, string>> _listAttribute = base.getAttributesAndValues();
            try
            {

                List<Tuple<string, string>> _attributes = new List<Tuple<string, string>> {
                  new Tuple<string, string>("Source ID",this.SourceID.ToString()),
                  new Tuple<string, string>("Sequence Number",this.SequenceNumber.ToString()),
                  new Tuple<string, string>("Destination ID",this.DestinationID.ToString()),
                  new Tuple<string, string>("Retransmit Flag",this.RetransmitFlag.ToString()),
                  new Tuple<string, string>("Safety Related Text",this.SafetyRelatedText.ToString()),

             };

                _listAttribute.AddRange(_attributes);
            }
            catch (Exception ex)
            {
                log.Error(ex, "MessageType12 :: getAttribute");
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
                "Sequnece Number: " + this.SequenceNumber + "\n" +
                "Destination ID: " + this.DestinationID + "\n" +
                "Retransmit Flag: " + this.RetransmitFlag + "\n" +
                "Spare: " + this.Spare + "\n" +
                "Safety Related Text: " + this.SafetyRelatedText + "\n";

         
        }
        #endregion

        #region Constructor 


        #region getAttributes(): Attributeları döndürür.
        public override List<Tuple<string, string>> getAttributes()
        {
            List<Tuple<string, string>> _listAttribute = base.getAttributes();
            List<Tuple<string, string>> _attributes = new List<Tuple<string, string>> {
                  new Tuple<string, string>("Source ID",""),
                  new Tuple<string, string>("Sequence Number",""),
                  new Tuple<string, string>("UTC Month",""),
                  new Tuple<string, string>("Destination ID",""),
                  new Tuple<string, string>("Reransmit Flag",""),
                  new Tuple<string, string>("Safety Related Text",""),
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
