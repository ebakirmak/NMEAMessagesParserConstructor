using NLog;
using NMEA_MessageParserConstructor.BL.CommunicationState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA_MessageParserConstructor.BL.Messages
{
    public class MessageType3 : RootMessages
    {
        //MMSI
        private int UserID { get; set; }
        private byte NavigationalStatus { get; set; }
        private double RateOfTurnROTAIS { get; set; }
        private double SOG { get; set; }
        private byte PositionAccuracy { get; set; }
        private double Longitude { get; set; }
        private double Latitude { get; set; }
        private float COG { get; set; }
        private int TrueHeading { get; set; }
        private byte TimeStamp { get; set; }
        private int SpecialManoeuvreIndicator { get; set; }
        private byte RAIMFlag { get; set; }
        private ITDMA Itdma;
        private Logger log;

        public MessageType3()
        {
            this.MessageID = 3;
            this.Description = "Special position report, response to interrogation.";
            this.RepeatIndicator = 0;
            this.Priority = 1;
            this.TotalNumberOfBits = 168;
            this.Itdma = new ITDMA();
            this.log = LogManager.GetCurrentClassLogger();
        }

        #region Parser
        #region Parser(string message): Mesaj yapısında bulunan attributelara, alınan mesajdaki değerleri set ettik.
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
                //NavigationalStatus - Nav Status
                this.NavigationalStatus = Convert.ToByte(getDecimalFromBinary(content, 38, 4));
                //RateOfTurnROTAIS - ROT
                this.RateOfTurnROTAIS = float.Parse(getDecimalFromBinary(content, 42, 8));
                //SOG - **** 10'a böldük. Doküman.
                this.SOG = float.Parse(getDecimalFromBinary(content, 50, 10)) / 10;
                //PA
                this.PositionAccuracy = Convert.ToByte(getDecimalFromBinary(content, 60, 1));
                //LON - Dakikaya çevrildi ve 10.000 ile çarpıldı. Bir yanlışlık var.
                this.Longitude = Convert.ToDouble((getDecimalFromBinary(content, 61, 28))) / 60 / 10000;
                //LAT - Dakikaya çevrildi ve 10.000 ile çarpıldı. Bir yanlışlık var.
                this.Latitude = Convert.ToDouble(getDecimalFromBinary(content, 89, 27)) / 60 / 10000;
                //COG **** 10'a böldük. Doküman.
                this.COG = float.Parse(getDecimalFromBinary(content, 116, 12)) / 10;
                //True Heaading
                this.TrueHeading = Convert.ToInt32(getDecimalFromBinary(content, 128, 9));
                //Time stamp
                this.TimeStamp = Convert.ToByte(getDecimalFromBinary(content, 137, 6));
                //Spe Man
                this.SpecialManoeuvreIndicator = Convert.ToInt32(getDecimalFromBinary(content, 143, 2));
                //Spare
                this.Spare = Convert.ToByte(getDecimalFromBinary(content, 145, 3));
                //RAIM
                this.RAIMFlag = Convert.ToByte(getDecimalFromBinary(content, 148, 1));
                //Communication State  --- HATA Var
                this.Itdma.SyncState = Convert.ToByte(getDecimalFromBinary(content, 149, 2));
                if (content.Length > 149)
                {
                    this.Itdma.SlotIncrement = Convert.ToInt32(getDecimalFromBinary(content, 151, 13));
                    //Communication State Sub Message
                    this.Itdma.NumberOfSlots = Convert.ToByte(getDecimalFromBinary(content, 164, 3));
                    this.Itdma.KeepFlag = Convert.ToByte(getDecimalFromBinary(content, 167, 1));
                    this.CommunicationState = Itdma;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "MessageType3 :: Parser");   
            }
           
         
            return null;
        }
        #endregion

        #region getAttributesAndValues(): Attributeları ve değerlerini döndürür.
        public override List<Tuple<string, string>> getAttributesAndValues()
        {
            List<Tuple<string, string>> _listAttribute = base.getAttributesAndValues();
            List<Tuple<string, string>> _listSotdma = this.Itdma.getAttributesAndValues();
            List<Tuple<string, string>> _attributes = new List<Tuple<string, string>> {
                  new Tuple<string, string>("User ID",this.UserID.ToString()),
                  new Tuple<string, string>("Navigational Status",this.NavigationalStatus.ToString()),
                  new Tuple<string, string>("Rate Of Turn ROTAIS",this.RateOfTurnROTAIS.ToString()),
                  new Tuple<string, string>("SOG",this.SOG.ToString()),
                  new Tuple<string, string>("PositionAccuracy",this.PositionAccuracy.ToString()),
                  new Tuple<string, string>("Longitude",this.Longitude.ToString()),
                  new Tuple<string, string>("Latitude",this.Latitude.ToString()),
                  new Tuple<string, string>("COG", this.COG.ToString()),
                  new Tuple<string, string>("True Heading", this.TrueHeading.ToString()),
                  new Tuple<string, string>("Time Stamp",this.TimeStamp.ToString()),
                  new Tuple<string, string>("Spe. Man. Indicator",this.SpecialManoeuvreIndicator.ToString()),
                  new Tuple<string, string>("RAIM Flag",this.RAIMFlag.ToString()),
                  new Tuple<string, string>("Sub Message","")
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

        #region Constructor(): Girilen değerlere göre VDM veya VDO mesajı oluşturuluyor.
        public override string Constructor(List<string> _listMessage)
        {
            //Temel mesaj özellikleri alınıyor.
            string Message = base.Constructor(_listMessage);

            #region Datagridview'den alınan değerleri set et.
            string errorMessage = "Error!";
            ///////////////////////////////////////////////////////
            if (ControlMessageID(Convert.ToByte(_listMessage[5])))
                this.MessageID = Convert.ToByte(_listMessage[5]);
            else
                errorMessage += "\nMessage ID değerini kontrol ediniz.";
            ///////////////////////////////////////////////////////   
            if (ControlRepeatIndicator(Convert.ToByte(_listMessage[8])))
                this.RepeatIndicator = Convert.ToByte(_listMessage[8]);
            else
                errorMessage += "\nRepeat Indicator değerini kontrol ediniz.";
            //////////////////////////////////////////////////////////////
                this.UserID = Convert.ToInt32(_listMessage[10]);
            //////////////////////////////////////////////////////////////
            if (ControlNavigational(Convert.ToByte(_listMessage[11])))
                this.NavigationalStatus = Convert.ToByte(_listMessage[11]);
            else
                errorMessage += "\nNavigational Status değerini kontrol ediniz.";
            ////////////////////////////////////////////////////////////

            if (ControlRateOfTurn(float.Parse(_listMessage[12])))
                this.RateOfTurnROTAIS = Convert.ToDouble(_listMessage[12]);
            else
                errorMessage += "\nRate Of Turn ROTAIS değerini kontrol ediniz.";
            ///////////////////////////////////////////////////////////
            if (ControlSOG(Convert.ToDouble(_listMessage[13])))
                this.SOG = Convert.ToDouble(_listMessage[13]);
            else
                errorMessage += "\nSOG değerini kontrol ediniz.";
            ////////////////////////////////////////////////////////
            if (ControlPositionAccuracy(Convert.ToByte(_listMessage[14])))
                this.PositionAccuracy = Convert.ToByte(_listMessage[14]);
            else
                errorMessage += "\nPosition Accuracy değerini kontrol ediniz.";
            ////////////////////////////////////////////////////////
            if (ControlLongitude(Convert.ToDouble(_listMessage[15])))
                this.Longitude = Math.Round(Convert.ToDouble(_listMessage[15]), 7);
            else
                errorMessage += "\nLongitude değerini kontrol ediniz.";
            //////////////////////////////////////////////////////////
            if (ControlLatitude(Convert.ToDouble(_listMessage[16])))
                this.Latitude = Math.Round(Convert.ToDouble(_listMessage[16]), 7);
            else
                errorMessage += "\nLatitude değerini kontrol ediniz.";
            ////////////////////////////////////////////////////////////
            if (ControlCOG(float.Parse(_listMessage[17])))
                this.COG = float.Parse(_listMessage[17]);
            else
                errorMessage += "\nCOG değerini kontrol ediniz.";
            //////////////////////////////////////////////////////////
            if (ControlTrueHeading(Convert.ToInt32(_listMessage[18])))
                this.TrueHeading = Convert.ToInt32(_listMessage[18]);
            else
                errorMessage += "\nTrue Heading değerini kontrol ediniz.";
            /////////////////////////////////////////////////////
            if (ControlTimeStamp(Convert.ToByte(_listMessage[19])))
                this.TimeStamp = Convert.ToByte(_listMessage[19]);
            else
                errorMessage += "\nTime Stamp değerini kontrol ediniz.";
            ///////////////////////////////////////////////////////////
            if (ControlSpecialManoeuvre(Convert.ToByte(_listMessage[20])))
                this.SpecialManoeuvreIndicator = Convert.ToByte(_listMessage[20]);
            else
                errorMessage += "\nSpecial Man Indicator değerini kontrol ediniz.";
            ////////////////////////////////////////////////////
            if (ControlRAIM(Convert.ToByte(_listMessage[21])))
                this.RAIMFlag = Convert.ToByte(_listMessage[21]);
            else
                errorMessage += "\nRAIM Flag değerini kontrol ediniz.";
            ///////////////////////////////////////////////////
            this.Itdma.setValue(_listMessage, 22);
            #endregion

            #region Bit değerlerine göre binary mesaj oluşturuluyor.
            string binaryMessage = setBinaryToDecimal(this.MessageID).PadLeft(6, '0');
            binaryMessage += setBinaryToDecimal(this.RepeatIndicator).PadLeft(2, '0');
            binaryMessage += setBinaryToDecimal(this.UserID).PadLeft(30, '0');
            binaryMessage += setBinaryToDecimal(this.NavigationalStatus).PadLeft(4, '0');
            //HATA
            binaryMessage += setBinaryToDecimal(Convert.ToInt32(this.RateOfTurnROTAIS)).PadLeft(8, '0');
            binaryMessage += setBinaryToDecimal(MultiplySOG(this.SOG)).PadLeft(10, '0');
            binaryMessage += setBinaryToDecimal(this.PositionAccuracy).PadLeft(1, '0');
            binaryMessage += setBinaryToDecimal(MultiplyLongitude(this.Longitude), 28).PadLeft(28, '0');
            binaryMessage += setBinaryToDecimal(MultiplyLatitude(this.Latitude), 27).PadLeft(27, '0');
            binaryMessage += setBinaryToDecimal(MultiplyCOG(this.COG)).PadLeft(12, '0');
            binaryMessage += setBinaryToDecimal(this.TrueHeading).PadLeft(9, '0') +
                setBinaryToDecimal(this.TimeStamp).PadLeft(6, '0') +
                setBinaryToDecimal(this.SpecialManoeuvreIndicator).PadLeft(2, '0') +
                setBinaryToDecimal(this.Spare).PadLeft(3, '0') +
                setBinaryToDecimal(this.RAIMFlag).PadLeft(1, '0');
            binaryMessage += Itdma.getBinaryToITDMAValue();
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

        #region getAttributes(): Attributeları döndürür.
        public override List<Tuple<string, string>> getAttributes()
        {
            List<Tuple<string, string>> _listAttribute = base.getAttributes();
            List<Tuple<string, string>> _listSotdma = this.Itdma.getAttributes();
            List<Tuple<string, string>> _attributes = new List<Tuple<string, string>> {
                  new Tuple<string, string>("User ID",""),
                  new Tuple<string, string>("Navigational Status",""),
                  new Tuple<string, string>("Rate Of Turn ROTAIS",""),
                  new Tuple<string, string>("SOG",""),
                  new Tuple<string, string>("PositionAccuracy",""),
                  new Tuple<string, string>("Longitude",""),
                  new Tuple<string, string>("Latitude",""),
                  new Tuple<string, string>("COG",""),
                  new Tuple<string, string>("True Heading",""),
                  new Tuple<string, string>("Time Stamp",""),
                  new Tuple<string, string>("Spe. Man. Indicator",""),
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
        
        #endregion

        #region ToString mesajını ezdik. Methodu sınıfa göre tasarladık.
        public override string ToString()
        {
            return
              "Message ID: " + this.MessageID + "\n" +
              "RepeatIndicator" + this.RepeatIndicator + "\n" +
              "User ID / MMSI" + this.UserID + "\n" +
              "Navigational Status" + this.NavigationalStatus + "\n" +
              "ROT" + this.RateOfTurnROTAIS + "\n" +
              "SOG" + this.SOG + "\n" +
              "PA" + this.PositionAccuracy + "\n" +
              "Lon" + this.Longitude + "\n" +
              "Lat" + this.Latitude + "\n" +
              "COG" + this.COG + "\n" +
              "True Heading" + this.TrueHeading + "\n" +
              "TimeStamp" + this.TimeStamp + "\n" +
              "Spe Man" + this.SpecialManoeuvreIndicator + "\n" +
              "Spare" + this.Spare + "\n" +
              "RAIM" + this.RAIMFlag + "\n" +
              this.Itdma.ToString();
        }
        #endregion
    }
}
