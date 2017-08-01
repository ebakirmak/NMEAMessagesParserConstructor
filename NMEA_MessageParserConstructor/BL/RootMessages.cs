using NLog;
using NMEA_MessageParserConstructor.BL;
using NMEA_MessageParserConstructor.BL.Dictionarys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA_MessageParserConstructor
{
    public class RootMessages
    {

        private Logger log;

        public RootMessages()
        {
            this.log = LogManager.GetCurrentClassLogger();
        }

        protected byte MessageID { get; set; }

        protected string Description { get; set; }

        protected byte Priority { get; set; }

        protected string AccessSchema { get; set; }

        protected object CommunicationState { get; set; }

        protected string TransmittedStation { get; set; }

        protected byte RepeatIndicator { get; set; }

        protected short TotalNumberOfBits { get; set; }

        protected byte Spare { get; set; }



        #region VDM veya VDO mesajını parçalarına ayrıştıracak ve geri döndürecek.
        public virtual string[] Parser(string message) {

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
        public byte getSentences(string message)
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

        #region Alınan bir binary içeriğini (010101 gibi) 2'lik tabandan 10'luk tabana çevir ve decimal değerini döndürür.
        public string getDecimalFromBinary(string binary, int start, int length)
        {
            //string a = binary.Substring(start, length).ToString();
            return Convert.ToInt32(binary.Substring(start, length), 2).ToString();
        }
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





    }

}

