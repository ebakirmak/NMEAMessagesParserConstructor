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
        public RootMessages()
        {
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
            //Tüm mesajı döndür.
            return messagesPart;
        }
        #endregion

        #region VDM veya VDO mesajını parçalarına ayrıştıracak ve geri döndürecek.
        public virtual string[] Parser(string message1, string message2)
        {

            //Mesajı parçalarına ayır.
            string[] messagesPart1 = message1.Split(',');
            string[] messagePart2 = message2.Split(',');
      
            //Tüm mesajı döndür.
            return messagesPart1;
        }
        #endregion

        #region Message ID geri döndür. Gönderilen Message ID'ye göre ilgili sınıfta işlem yapılacak.
        public byte getMessageID(string message)
        {
            //ascii8 içeriğini ascii6 dönüştür. Binary yapıda al. 2 lik tabandan 10'luk tabana çevir.
            return Convert.ToByte(getSubstringFromBinary(getContentBinary(Parser(message)[5]), 0, 6));
        }
        #endregion

        #region ascii8 karakterlerini ascii6 tablosuna çevir. Daha sonra bunların 6 basamaklı binary yapılarını bul.  Örnek 010100 gibi.
        public string getContentBinary(string content)
        {
            string bits = "";
            string currentBits = "";
            int ascii;
            foreach (var letter in content)
            {
                ascii = Convert.ToInt32(letter);
                if (ascii > 88)
                    ascii -= 56;
                else
                    ascii -= 48;


                currentBits = Convert.ToString(ascii, 2);
                if (currentBits.Count() == 1)
                    bits += currentBits.PadLeft(6, '0');
                else if (currentBits.Count() == 2)
                    bits += currentBits.PadLeft(6, '0');
                else if (currentBits.Count() == 3)
                    bits += currentBits.PadLeft(6, '0');
                else if (currentBits.Count() == 4)
                    bits += currentBits.PadLeft(6, '0');
                else if (currentBits.Count() == 5)
                    bits += currentBits.PadLeft(6, '0');
                else
                    bits += currentBits;
            }
            return bits;
        }
        #endregion

        #region Alınan bir binary içeriğini (010101 gibi) 2'lik tabandan 10'luk tabana çevir.
        public string getSubstringFromBinary(string binary, int start, int length)
        {
    
            return Convert.ToInt32(binary.Substring(start, length), 2).ToString();
        }
        #endregion


        public string getStringFromBinary(string binarys, int start, int length)
        {
            //00110011 01000110 01001111 01000110 00111000 
            string context = binarys.Substring(start, length);
            int binary = 0;
            int x = 0;
            Ascii6 ascii = new Ascii6();
            for (int i = 0; i < context.Length; i += 6)
            {
                binary = Convert.ToInt32(context.Substring(i, 6), 2);
                Console.WriteLine(binary + " -- " + Convert.ToString(binary, 2));
                if (binary < 88)
                    binary += 56;
                else

                    binary += 48;
                x++;
                Console.WriteLine(binary +" -- "+ Convert.ToString(binary,2));
                //Console.WriteLine(ascii.getStringBinarySix(Convert.ToString(binary,2)));
                
            }

            return "0";
        }

  
























        enum enumPriority
        {
            PriorityOne,
            PriorityTwo,
            PriorityThree,
            PriorityFour

        };

        enum enumAccessSchema
        {
            SOTDMA,
            RATDMA,
            CSTDMA,
            MSSA,
            FATDMA,
            ITDMA
        };

        enum enumCommunicitonState
        {
            SOTDMA,
            ITDMA,
            NA
        };

        enum enumTransmittedStation
        {
            M,
            B,
            MB
        };
    }
}
