using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA_MessageParserConstructor.BL
{
   public class MessagePacket:RootMessages
    {
        #region VDM ve VDO mesajlarının (okunabilir yapıya) ayrıştıracak ve mesaj id geri döndürecek.
        public override string[] Parser(string message)
        {
            return base.Parser(message);         
          
        }
        #endregion

        #region ascii8 karakterlerini ascii6 tablosuna çevir. Daha sonra bunların binary yapılarını bul
        public string getContentBinary(string content)
        {
            string bits="";
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
                    bits +=  currentBits.PadLeft(6, '0');
                else if (currentBits.Count() == 2)
                    bits +=  currentBits.PadLeft(6, '0');
                else if (currentBits.Count() == 3)
                    bits +=  currentBits.PadLeft(6, '0');
                else if (currentBits.Count() == 4)
                    bits +=  currentBits.PadLeft(6, '0');
                else if (currentBits.Count() == 5)
                    bits +=  currentBits.PadLeft(6, '0');
                else
                    bits += currentBits;
                Console.WriteLine("Ascii->"+ascii +"\tBit Değeri:"+ currentBits);
            }
            Console.WriteLine(bits);
           
            return bits;
        }
        #endregion

        #region Message ID geri döndür.
        public byte getMessageID(string message)
        {
            
            //ascii8 içeriğini ascii6 dönüştür. Binary yapıda al. 2 lik tabandan 10'luk tabana çevir.
            return Convert.ToByte(getSubstringFromBinary( getContentBinary(Parser(message)[5]),0,6));
        }
        #endregion

        #region 2'lik tabandan 10'luk tabana çevir.
        public string getSubstringFromBinary(string binary,int start,int length)
        {
           
            return Convert.ToInt32(binary.Substring(start,length), 2).ToString();
            
        }
        #endregion
    }
}
