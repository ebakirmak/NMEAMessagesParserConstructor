using NLog;
using NMEA_MessageParserConstructor.BL;
using NMEA_MessageParserConstructor.BL.Dictionarys;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA_MessageParserConstructor
{
    public class RootMessages
    {
        #region Sonradan eklenenler
        protected string NMEAString { get; set; }

        protected string Checksum { get; set; }

        protected string Prefix { get; set; }

        protected string Talker { get; set; }

        protected string Sentence { get; set; }

        protected int TotalMessageCount { get; set; }

        protected int CurrentMessageNumber { get; set; }

        protected string Channel { get; set; }

        protected string Content { get; set; }

        #endregion
        protected byte MessageID { get; set; }

        protected string Description { get; set; }

        protected byte Priority { get; set; }

        protected string AccessSchema { get; set; }

        protected object CommunicationState { get; set; }

        protected string TransmittedStation { get; set; }

        protected byte RepeatIndicator { get; set; }

        protected short TotalNumberOfBits { get; set; }

        protected int Spare { get; set; }

        private Logger log;

        public RootMessages()
        {
            this.log = LogManager.GetCurrentClassLogger();
        }        

        #region Mesajın VDM veya VDO mesajı olup olmadığını kontrol edecek
        public bool CheckMessage(string message)
        {



            string [] messageParts = message.Split(',');
            if(messageParts.Length == 7)
            {
                if (messageParts[0] == "!AIVDO" || messageParts[0] == "!AIVDM")
                {
                    #region
                    this.NMEAString = message;
                    //Hata
                    this.Checksum = messageParts[6];
                    this.Prefix = messageParts[0].Substring(0, 1);
                    this.Talker = messageParts[0].Substring(1, 2);
                    this.Sentence = messageParts[0].Substring(3, 3);
                    this.TotalMessageCount = Convert.ToInt32(messageParts[1]);
                    this.CurrentMessageNumber = Convert.ToInt32(messageParts[2]);
                    this.Channel = messageParts[4];
                    this.Content = messageParts[5];
                    #endregion
                    return true;
                }
            
                else
                    return false;
            }
            return false;
        }
        #endregion

        #region VDM veya VDO mesajını parçalarına ayrıştıracak ve geri döndürecek.
        public virtual string[] Parser(string message) {

            if (CheckMessage(message) != true)
                return null;
            //Mesajı parçalarına ayır.
            string[] messagesPart = message.Split(',');
            if(messagesPart.Count()!=7)
            {
                log.Warn("RootMessages : Parser() -- Dizinin eleman sayısı doğru değil.");
                return null;
            }
            //Tüm mesajı döndür.
            return messagesPart;
        }
        #endregion

        #region VDM veya VDO mesajını parçalarına ayrıştıracak ve geri döndürecek.
        public virtual string[] Parser(string message1, string  message2)
        {
            //Tüm mesajı döndür.
            return null;
        }
        #endregion

        #region VDM veya VDO mesajı kaç parça olduğunu döndür.
        public byte getSentenceCount(string message)
        {
            try
            {                
                return Convert.ToByte(message.Split(',')[1]);
            }
            catch (Exception ex)
            {
                log.Error(ex, "RootMessages : getSentences()");
                return 0;
            }
        }
        #endregion

        #region Message ID geri döndür. Gönderilen Message ID'ye göre ilgili sınıfta işlem yapılacak.
        public byte getMessageID(string message)
        {
            try
            {
                if (message == null)
                    return 0;
                else
                    //ascii8 içeriğini ascii6 dönüştür. Binary yapıda al. 2 lik tabandan 10'luk tabana çevir.
                    return Convert.ToByte(getDecimalFromBinary(getContentBinary(Parser(message)[5], 0), 0, 6));
            }
            catch (Exception ex)
            {
                log.Error(ex, "RootMessages :: getMessageID");
                throw;
            }
           
        }
        #endregion

        #region ascii8 karakterlerini ascii6 tablosuna çevir. Daha sonra bunların 6 basamaklı binary yapılarını bul.  Örnek 010100 gibi.
        //Removezero değiştir.
        public string getContentBinary(string content,int removeZero)
        {
            string bits = "";
            string currentBits = "";
            int ascii;
            content = content.Trim();
            foreach (var letter in content)
            {
                //İlgili karakteri ascii8 tablosunda ki değerinden ascii6 tablosundaki değerine çevirdik.
                ascii = Convert.ToInt32(letter);
                if (ascii > 88)
                    ascii -= 56;
                else
                    ascii -= 48;


                currentBits = Convert.ToString(ascii, 2);
                //6 bit haline dönüştürdük.
                bits += currentBits.PadLeft(6, '0');
               
            }
            //return bits;
            return bits.Remove(bits.Length-removeZero,removeZero);
        }
        #endregion

        #region ascii6 karakterlerini ascii 8 tablosuna çevir. Daha sonra bir content oluştur.
        public virtual string setContent(string binaryMessage)
        {
            int ascii;
            string content = "";
            for (int i = 0; i < binaryMessage.Length; i+=6)
            {
                ascii = Convert.ToInt32(binaryMessage.Substring(i, 6),2);
                if (ascii < 40)
                    ascii += 48;
                else
                    ascii += 56;

                content += Convert.ToChar(ascii);
            }
            return content;
        }
        #endregion

        #region Alınan bir binary içeriğini (010101 gibi) 2'lik tabandan 10'luk tabana çevir ve decimal değerini döndürür.
        public string getDecimalFromBinary(string binary, int start, int length)
        {
            //string a = binary.Substring(start, length).ToString();
            //Console.WriteLine(binary.Substring(start, length));
            return Convert.ToInt32(binary.Substring(start, length), 2).ToString();
        }


        //public string getDoubleFromBinary(string binary, int start, int length)
        //{
        //    //string a = binary.Substring(start, length).ToString();
        //    //Console.WriteLine(binary.Substring(start, length));
        //    return Convert.ToDouble(binary.Substring(start, length), 2).ToString();
        //}

        #endregion

        #region Alınan bir binary içeriğini (000001) 2'lik tabandan 10'luk tabana çevir ve ascii 6 tablosunda ki karakter değerini döndürür.
        public string getStringFromBinary(string binarys, int start, int length)
                    {
                        //string ifadeler
                        string metin = binarys.Substring(start, length), context = "";
                        Ascii6 ascii6 = new Ascii6();
                        //Ascii6 tablosundan, binary değerine göre ilgili karakteri döndürüyor.
                        for (int i = 0; i < metin.Length; i += 6) {
                            try
                            {
                                context += ascii6.getStringBinarySix(metin.Substring(i, 6));
                            }
                            catch (Exception ex)
                            {
                                log.Error(ex, "RootMessages :: getStringFromBinary()");
                                //throw;
                            }
               
                        }

                        return context;
                    }
                    #endregion

        #region Sondaki 6'nın katına eşitlemek için koyulan fazladan karakterleri at. Fonk ismini değiştir.
        public int Remove(string messagePart )
        {
            string[] messageParts = messagePart.Split('*');
            return Convert.ToInt32(messageParts[0]);
        }

        #endregion

        #region Attribute sayısını döndürür.
        public virtual byte getAttributeCount()
        {
            return 4;
        }
        #endregion

        #region Parser 

        #region Attributeları döndürür.
        // new Tuple<string, string>("",this..ToString()),
        public virtual List<Tuple<string, string>> getAttributesAndValues()
        {
            List<Tuple<string, string>> _attributes = new List<Tuple<string, string>> {
                new Tuple<string, string>("NMEA String",this.NMEAString.ToString()),
                new Tuple<string, string>("Checksum",this.Checksum.ToString()),
                new Tuple<string, string>("Prefix",this.Prefix.ToString()),
                new Tuple<string, string>("Talker",this.Talker.ToString()),
                new Tuple<string, string>("Sentence",this.Sentence.ToString()),
                new Tuple<string, string>("Total Message Count",this.TotalMessageCount.ToString()),
                new Tuple<string, string>("Channel",this.Channel.ToString()),
                new Tuple<string, string>("Content",this.Content.ToString()),
                new Tuple<string, string> ("Message ID", this.MessageID.ToString() ),
                new Tuple<string,string>( "Description",this.Description),
                new Tuple<string, string>("Priority",this.Priority.ToString()),
                new Tuple<string, string>("Repeat Indicator",this.RepeatIndicator.ToString()),
                new Tuple<string, string>("Spare",this.Spare.ToString()),
           };
            return _attributes;
        }
        #endregion

        #endregion



        #region Mesaj oluşturmak için ilgili attribute'ları dön.
        public virtual List<Tuple<string, string>> getAttributes()
        {
            List<Tuple<string, string>> _attributes = new List<Tuple<string, string>> {
                new Tuple<string, string>("Prefix","!"),
                new Tuple<string, string>("Talker","AI"),
                new Tuple<string, string>("Sentence","VDM"),
                new Tuple<string, string>("Total Message Count","1"),
                new Tuple<string, string>("Channel","A"),
                new Tuple<string, string> ("Message ID", this.MessageID.ToString() ),
                new Tuple<string,string>( "Description",this.Description),
                new Tuple<string, string>("Priority",this.Priority.ToString()),
                new Tuple<string, string>("Repeat Indicator",this.RepeatIndicator.ToString()),
                new Tuple<string, string>("Spare",this.Spare.ToString()),
           };
            return _attributes;
        }
        #endregion



        #region Ondalık bir değeri binary dönüştürür.
        //public virtual string setBinaryToDouble (double item)
        //{
        //    return BitConverter.ToDouble(item).ToString();
        //}
        #endregion

        #region Constructor methodları.

        #region Girilen değerlere göre VDM veya VDO mesajı oluşturuluyor.
        public virtual string Constructor(List<string> _listMessage)
        {
            this.Prefix = _listMessage[0];
            this.Talker = _listMessage[1];
            this.Sentence = _listMessage[2];
            this.TotalMessageCount = Convert.ToInt32(_listMessage[3]);
            this.CurrentMessageNumber = 1;
            this.Channel = _listMessage[4];
            this.Spare = Convert.ToInt32(_listMessage[9]);

            return this.Prefix +
                   this.Talker +
                   this.Sentence + "," +
                   this.TotalMessageCount + "," +
                   this.CurrentMessageNumber + ", ," +
                   this.Channel + ",";
        }
        #endregion

        #region Decimal bir değeri binary  dönüştürür.
        public virtual string setBinaryToDecimal(int item)
        {
                string binary = "";
              
                if (item < 0)
                {
                    item *= -1;
                    binary = setBinaryToComplement(item,0);
                    return binary;
                }
                return Convert.ToString(item,2);
        }

        public virtual string setBinaryToDecimal(int item,int padLeft)
        {

            string binary = "";
            if (item < 0)
            {
                item *= -1;
                binary = setBinaryToComplement(item, padLeft);
                return binary;
            }
            return Convert.ToString(item, 2);
        }

        #endregion

        public string setBinaryToComplement(int item, int padLeft)
        {
            string binary = "", complementBinary = "";


            binary = Convert.ToString(item, 2).PadLeft(padLeft, '0');
            foreach (var bit in binary)
            {
                if (bit == '1')
                    complementBinary += "0";
                else if (bit == '0')
                    complementBinary += "1";
            }


            return Convert.ToString(complementBinary);
        }
        
        #endregion






        #region Attributelar, verilen özel değerler ile işleme sokularak asıl değerlerine ulaşılır.

        #region ProperRateOfTurnROTAIS(double value): (ROTAIS / 4.733)^2 HATA VAR
        protected double ProperRateOfTurnROTAIS(double value)
        {
            if (value > 127)
            {
                string binary = Convert.ToString(Convert.ToInt32(value),2);
                string newBinary = "0";
                foreach (var bit in binary)
                {
                    if (bit == '0')
                        newBinary += "1";
                    else
                        newBinary += "0";
                }
                value = Convert.ToInt32(newBinary, 2);
            }
            value =value / 4.733;
            return Math.Pow(value,2);
        }
        #endregion

        #region  SOG değerini 10'a böl.
        protected double ProperSOG (int value)
        {
            return value / 10;
        }
        #endregion

        #region  Longitude değerini 1/10.000 min => 600.000 böldük.
        protected double ProperLongitude (double value)
        {
            return value /= 600000;
        }
        #endregion

        #region  Latitude değerini 1/10.000 min => 600.000 böldük.
        protected double ProperLatitude(double value)
        {
            return value / 600000;
        }
        #endregion

        #region COG değerini 10'a böldük.
        protected double ProperCOG (float value)
        {
            return value / 10;
        }
        #endregion

        #endregion








        #region Attribute value'lerinin kontrolleri

        #region MessageID Kontrol
        protected bool ControlMessageID(byte value)
        {
            if (value >= 1 && value < 28)
                return true;
            else
                return false;
        }
        #endregion

        #region Repeat Indicator Kontrol
        protected bool ControlRepeatIndicator(byte value)
        {
            if (value < 3 && value >= 0)
                return true;
            else
                return false;
        }
        #endregion

        #region Navigational Status Kontrol
        protected bool ControlNavigational(byte value)
        {
            if (value >= 0 && value < 16)
                return true;
            else
                return false;
        }
        #endregion

        #region Position Accuracy Kontrol
        protected bool ControlPositionAccuracy(byte value)
        {
            if (value == 0 || value == 1)
                return true;
            else
                return false;
        }
        #endregion

        #region Special maoeuvre Indicator Kontrol
        protected bool ControlSpecialManoeuvre(byte value)
        {
            if (value >= 0 && value < 4)
                return true;
            else
                return false;
        }
        #endregion

        #region RAIM Flag Kontrol
        protected bool ControlRAIM(byte value)
        {
            if (value == 0 || value == 1)
                return true;
            else
                return false;
        }
        #endregion

        #region Sync State        
        public bool ControlSyncState(byte value)
        {
            if (value >= 0 && value < 4)
                return true;
            else
                return false;
        }
        #endregion

        #region Slot Time Out
        public bool ControlSlotTimeOut(byte value)
        {
            if (value >= 0 && value < 8)
                return true;
            else
                return false;
        }
        #endregion

        #region Rate of turn ROTAIS
        protected bool ControlRateOfTurn(float value)
        {
            if (value >= -127 && value <= 127)
                return true;
            else
                return false;
        }

        #endregion

        #region SOG
        protected bool ControlSOG (double value)
        {
            if (value < 1023)
                return true;
            else
                return false;
        }
        #endregion

        #region Longitutude
        protected bool ControlLongitude(double value)
        {
            if (value >= -180 && value <= 180)
                return true;
            else
                return false;
        }
        #endregion

        #region Latitude
        protected bool ControlLatitude(double value)
        {
            if (value >= -90 && value <= 90)
                return true;
            else
                return false;
        }
        #endregion

        #region COG
        protected bool ControlCOG(float value)
        {
            if (value >= 0 && value <= 3599)
                return true;
            else
                return false;
        }
        #endregion

        #region True Heading
        protected bool ControlTrueHeading(int value)
        {
            if (value >= 0 && value <= 359)
                return true;
            else
                return false;
        }
        #endregion

        #region Time Stamp
        protected bool ControlTimeStamp(byte value)
        {
            if (value > 0 && value < 64)
                return true;
            else
                return false;
        }
        #endregion

        #region Slot Increment       
        public bool ControlSlotIncrement(byte value)
        {
            if (value >= 0 && value < 4)
                return true;
            else
                return false;
        }
        #endregion

        #region Number of slots      
        public bool ControlNumberOfSlots(byte value)
        {
            if (value >= 0 && value < 8)
                return true;
            else
                return false;
        }
        #endregion

        #region Keep Flag   
        public bool ControlKeepFlag(byte value)
        {
            if (value >= 0 && value < 2)
                return true;
            else
                return false;
        }
        #endregion

        #region UTC Hour
        protected bool ControlUTCHour(byte value)
        {
            if (value >= 0 && value < 24)
                return true;
            else
                return false;
        }
        #endregion

        #region UTC Minute
        protected bool ControlUTCMinute(byte value)
        {
            if (value >= 0 && value < 60)
                return true;
            else
                return false;
        }
        #endregion

        #region UTC Year
        protected bool ControlUTCYear(int value)
        {
            if (value > 0 && value < 10000)
                return true;
            else
                return false;
        }


        #endregion

        #region UTC Month
        protected bool ControlUTCMonth(int value)
        {
            if (value > 0 && value < 13)
                return true;
            else
                return false;
        }
        #endregion

        #region UTC Day
        protected bool ControlUTCDay(int value)
        {
            if (value > 0 && value < 32)
                return true;
            else
                return false;
        }
        #endregion

        #region UTC Second
        protected bool ControlUTCSecond(byte value)
        {
            if (value >= 0 && value < 60)
                return true;
            else
                return false;
        }
        #endregion

        #region Type of Electronic position ficing device
        protected bool ControlTOEPFD(byte value)
        {
            if (value >= 0 && value < 16)
                return true;
            else
                return false;
        }
        #endregion

        #region Transmission control for long range broadcast message
        protected bool ControlTCFLRBM(byte value)
        {
            if (value >= 0 && value < 2)
                return true;
            else
                return false;
        }
        #endregion


        #endregion





        #region Attribute value'lerini ilgili değerler ile çarpıp binary çevir.

        #region SOG 10 ile çarpılacak.
        protected int MultiplySOG(double value)
        {
            return Convert.ToInt32( value * 10);
        }
        #endregion

        #region
        protected  int MultiplyLongitude(double value)
        {
            return Convert.ToInt32(value * 600000);
        }
        #endregion

        #region
        protected int MultiplyLatitude(double value)
        {
            return Convert.ToInt32(value * 600000);
        }
        #endregion

        #region
        protected short MultiplyCOG(double value)
        {
            return Convert.ToInt16(value * 10);
        }
        #endregion

        #endregion



    }

}

