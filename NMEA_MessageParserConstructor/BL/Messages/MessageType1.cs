using NLog;
using NMEA_MessageParserConstructor.BL;
using NMEA_MessageParserConstructor.BL.CommunicationState;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA_MessageParserConstructor
{
    public class MessageType1:RootMessages
    {   
        
        //MMSI
        private int UserID { get; set; }
        private byte NavigationalStatus { get; set; }
        private double RateOfTurnROTAIS { get; set; }
        private double SOG { get; set; }
        private byte PositionAccuracy { get; set; }
        private double Longitude { get; set; }
        private double Latitude { get; set; }
        private double COG { get; set; }
        private int TrueHeading { get; set; }
        private byte TimeStamp { get; set; }
        private byte SpecialManoeuvreIndicator { get; set; }
        private byte RAIMFlag { get; set; }
        private SOTDMA Sotdma;
        public Logger log { get; set; }

        public MessageType1()
        {
            this.MessageID = 1;
            this.Description = "Scheduled position report";
            this.RepeatIndicator = 0;
            this.Priority = 1;
            this.TotalNumberOfBits = 168;
            this.Sotdma = new SOTDMA();
            this.log = LogManager.GetCurrentClassLogger();
        }

        #region  Parser
        #region Parser(string message): Mesaj yapısında bulunan attributelara, alınan mesajdaki değerleri set ettik.
        public override string[] Parser(string message)
        {            
            string[] messageParts = base.Parser(message);    
            //Context'i oku. Binary yapıda.
            string content = getContentBinary(messageParts[5], Remove(messageParts[6]));
            //Tüm mesajlarda olan özellikleri burada gir.
            try
            {
                //MessageID
                this.MessageID = Convert.ToByte(getDecimalFromBinary(content, 0, 6));
                //Repeat indicator
                this.RepeatIndicator = Convert.ToByte(getDecimalFromBinary(content, 6, 2));
                //Source MMSI
                this.UserID = Convert.ToInt32(getDecimalFromBinary(content, 8, 30));
                //NavigationalStatus - Nav Status
                this.NavigationalStatus = Convert.ToByte(getDecimalFromBinary(content, 38, 4));
                //RateOfTurnROTAIS - ROT
                this.RateOfTurnROTAIS = ProperRateOfTurnROTAIS(Convert.ToDouble(getDecimalFromBinary(content, 42, 8)));
                //SOG - **** 10'a böldük. Doküman.
                this.SOG = float.Parse(getDecimalFromBinary(content, 50, 10)) / 10;
                //PA
                this.PositionAccuracy = Convert.ToByte(getDecimalFromBinary(content, 60, 1));
                //LON - 600.000 ' e bölündü.
                this.Longitude = ProperLongitude(Convert.ToDouble((getDecimalFromBinary(content, 61, 28))));
                //LAT - Dakikaya çevrildi ve 10.000 ile çarpıldı.
                this.Latitude = ProperLatitude(Convert.ToDouble(getDecimalFromBinary(content, 89, 27)));
                //COG **** 10'a böldük. Doküman.
                this.COG = float.Parse(getDecimalFromBinary(content, 116, 12)) / 10;
                //True Heaading
                this.TrueHeading = Convert.ToInt32(getDecimalFromBinary(content, 128, 9));
                //Time stamp
                this.TimeStamp = Convert.ToByte(getDecimalFromBinary(content, 137, 6));
                //Spe Man
                this.SpecialManoeuvreIndicator = Convert.ToByte(getDecimalFromBinary(content, 143, 2));
                //Spare
                this.Spare = Convert.ToByte(getDecimalFromBinary(content, 145, 3));
                //RAIM
                this.RAIMFlag = Convert.ToByte(getDecimalFromBinary(content, 148, 1));
                //Communication State            
                Sotdma.setValue(content, 149);
            }
            catch (Exception ex)
            {
                log.Error(ex, "MessageType1 :: Parser");
                throw;
            }
           
            //Total Number Of Bits
            this.TotalNumberOfBits = 168;
            return null;
        }
        #endregion

        #region Attributeları ve değerlerini döndürür.
        public override List<Tuple<string, string>> getAttributesAndValues()
        {
            List<Tuple<string, string>> _listAttribute = base.getAttributesAndValues();
            List<Tuple<string, string>> _listSotdma = this.Sotdma.getAttributesAndValues();
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
            this.Sotdma.setValue(_listMessage, 22);
            #endregion

            #region Bit değerlerine göre binary mesaj oluşturuluyor.
            string binaryMessage = setBinaryToDecimal(this.MessageID).PadLeft(6, '0');
            binaryMessage += setBinaryToDecimal(this.RepeatIndicator).PadLeft(2, '0');
            binaryMessage += setBinaryToDecimal(this.UserID).PadLeft(30, '0');
            binaryMessage += setBinaryToDecimal(this.NavigationalStatus).PadLeft(4, '0');
            //HATA
            binaryMessage += setBinaryToDecimal(Convert.ToInt32(this.RateOfTurnROTAIS)).PadLeft(8, '0');
            Console.WriteLine(binaryMessage.Length);

            binaryMessage += setBinaryToDecimal(MultiplySOG(this.SOG)).PadLeft(10, '0');
            Console.WriteLine(binaryMessage.Length);

            binaryMessage += setBinaryToDecimal(this.PositionAccuracy).PadLeft(1, '0');
            Console.WriteLine(binaryMessage.Length);

            binaryMessage += setBinaryToDecimal(MultiplyLongitude(this.Longitude), 28).PadLeft(28, '0');
            Console.WriteLine(binaryMessage.Length);

            binaryMessage += setBinaryToDecimal(MultiplyLatitude(this.Latitude), 27).PadLeft(27, '0');
            Console.WriteLine(binaryMessage.Length);

            binaryMessage += setBinaryToDecimal(MultiplyCOG(this.COG)).PadLeft(12, '0');
            Console.WriteLine(binaryMessage.Length);

            binaryMessage += setBinaryToDecimal(this.TrueHeading).PadLeft(9, '0') +
                setBinaryToDecimal(this.TimeStamp).PadLeft(6, '0') +
                setBinaryToDecimal(this.SpecialManoeuvreIndicator).PadLeft(2, '0') +
                setBinaryToDecimal(this.Spare).PadLeft(3, '0') +
                setBinaryToDecimal(this.RAIMFlag).PadLeft(1, '0');
            binaryMessage += Sotdma.getBinaryToSOTDMAValue();

            #endregion
            Console.WriteLine(binaryMessage.Length);
            #region binary message, SetContent fonksiyonuna gönderilerek, ASCII8 tipinde mesaj content içeriği oluşturuluyor.
            string content = setContent(binaryMessage);
            #endregion

            if (errorMessage.Contains("Error!") && errorMessage.Length > 6)
                return errorMessage;
            else
                return Message + content;
        }
        #endregion

        #region Attributeları döndürür.
        public override List<Tuple<string, string>> getAttributes()
        {
            List<Tuple<string, string>> _listAttribute = base.getAttributes();
            List<Tuple<string, string>> _listSotdma = this.Sotdma.getAttributes();
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
                ( "Mesaj ID: ")+ this.MessageID + "\n" +
                "Repeat Indicator: "+ this.RepeatIndicator + "\n" +
                "User ID: "+this.UserID + "\n" +
                "Navigational Status: " + this.NavigationalStatus + "\n" +
                "Rate Of Turn ROTAIS: "+ this.RateOfTurnROTAIS + "\n" +
                "SOG: "+ this.SOG + "\n" +
                this.PositionAccuracy + "\n" +
                this.Longitude + "\n" +
                this.Latitude + "\n" +
                this.COG + "\n" +
                this.TrueHeading + "\n" +
                this.TimeStamp + "\n" +
                this.SpecialManoeuvreIndicator + "\n" +
                this.Spare + "\n" +
                this.RAIMFlag + "\n" +
                this.CommunicationState.ToString();
        }
        #endregion

      

      

      
    }
}
