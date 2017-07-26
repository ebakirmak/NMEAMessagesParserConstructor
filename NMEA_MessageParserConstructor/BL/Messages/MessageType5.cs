using NMEA_MessageParserConstructor.BL.Dictionarys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NMEA_MessageParserConstructor.BL;

namespace NMEA_MessageParserConstructor.BL.Messages
{
    public class MessageType5 : RootMessages
    {
        private int UserID { get; set; }
        private byte AISVersionIndicator { get; set; }
        private int IMONumber { get; set; }
        private string CallSign { get; set; }
        private string Name { get; set; }
        private byte TypeOfShipAndCargoType { get; set; }
        //Ship dimensions - Gemi boyutları | Gemi genel boyutları
        private OverallDimension OverallDimensions { get; set; }
        //Type of electronic position fixing device
        private byte TypeOfEPFD { get; set; }
        //Estimated time of arrival; MMDDHHMM UTC | Tahmini varış zamanı
        private ETA Eta { get; set; }
        //Maximum present static draught
        private double MaxStaticDraught { get; set; }
        //Destination
        private string Destination { get; set; }
        //Data terminal equipment
        private byte DTE { get; set; }

        public MessageType5()
        {
            this.MessageID = 5;
            this.Description = "Scheduled  static and voyage related vessel data report";
            this.RepeatIndicator = 0;
            this.Priority = 4;
            this.TotalNumberOfBits = 424;
            this.IMONumber = 0;
            this.Eta = new ETA();
            this.OverallDimensions = new OverallDimension();
        }

        public class ETA
        {
            public byte Minute { get; set; }
            public byte Hour { get; set; }
            public byte Day { get; set; }
            public byte Month { get; set; }
        }

        public class OverallDimension
        {
            public int A { get; set; }
            public int B { get; set; }
            public int C { get; set; }
            public int D { get; set; }

        }

        #region Mesaj yapısında bulunan attributelara, alınan mesajdaki değerleri set ettik.
        public override string[] Parser(string message1,string message2)
        {
            //Mesajı parçalarına ayır.
            string[] messageParts1 = message1.Split(',');
            string[] messageParts2 = message2.Split(',');

            //Context'i oku. Binary yapıda.
            string content = getContentBinary(messageParts1[5], Remove(messageParts1[6]));
            content += getContentBinary(messageParts2[5], Remove(messageParts2[6]));
        

            //MessageID
            this.MessageID = Convert.ToByte(getDecimalFromBinary(content, 0, 6));

            //Repeat indicator
            this.RepeatIndicator = Convert.ToByte(getDecimalFromBinary(content, 6, 2));

            //Source MMSI
            this.UserID = Convert.ToInt32(getDecimalFromBinary(content, 8, 30));

            //AISVersionIndicator
            this.AISVersionIndicator = Convert.ToByte(getDecimalFromBinary(content, 38, 2));

            //IMONumber
            this.IMONumber = Convert.ToInt32(getDecimalFromBinary(content, 40, 30));

            //Call sign - String
            this.CallSign = getStringFromBinary(content, 70, 42).Trim();

            //Name - Bir mesaj örneği için doğru çalışıyor başka bir mesaj örneği için hatalı çalışıyor. Buradan sonrası. Mesaja bağlı.
            this.Name = getStringFromBinary(content, 112, 120).Trim();

            //Type Of Ship And Cargo Type
            this.TypeOfShipAndCargoType = Convert.ToByte((getDecimalFromBinary(content, 232, 8)));

            //Overall Dimensions String Hatalı - DÖKÜMANDA HATALI SOR.
            this.OverallDimensions.A =Convert.ToInt32(getDecimalFromBinary(content, 240, 9));
            this.OverallDimensions.B = Convert.ToInt32(getDecimalFromBinary(content, 249, 9));
            this.OverallDimensions.C = Convert.ToInt32(getDecimalFromBinary(content, 258, 6));
            this.OverallDimensions.D = Convert.ToInt32(getDecimalFromBinary(content, 264, 6));
            
            //
            this.TypeOfEPFD = Convert.ToByte(getDecimalFromBinary(content, 270, 4));

            //Estimated time of arrival.
            this.Eta.Month = Convert.ToByte(getDecimalFromBinary(content, 274, 4));
            this.Eta.Day = Convert.ToByte(getDecimalFromBinary(content, 278, 5));
            this.Eta.Hour = Convert.ToByte(getDecimalFromBinary(content, 283, 5));
            this.Eta.Minute =Convert.ToByte (getDecimalFromBinary(content, 288, 6));
            
            //Maximum present static draught
            this.MaxStaticDraught = Convert.ToDouble(getDecimalFromBinary(content, 294, 8))/10;

            //Destination - String
            this.Destination = getStringFromBinary(content, 302,120);

            //Data Terminal Equipment
            this.DTE = Convert.ToByte(getDecimalFromBinary(content, 422, 1));

            //Spare
            this.Spare = Convert.ToByte(getDecimalFromBinary(content, 423, 1));
      
            return null;
        }
        #endregion

        #region ToString() methodunu override ettik.
        public override string ToString()
        {
            return
                "Message ID: " + this.MessageID + "\n" +
                "RepeatIndicator: " + this.RepeatIndicator + "\n" +
                "User ID / MMSI: " + this.UserID + "\n" +
                "AIS Version Indicator: " + this.AISVersionIndicator + "\n" +
                "IMO Number: " + this.IMONumber + "\n" +
                "Call Sign: " + this.CallSign + "\n" +
                "Name: " + this.Name + "\n" +
                "Type Of Ship And Cargo Type: " + this.TypeOfShipAndCargoType + "\n" +
                "Overall Dimensions A: " + this.OverallDimensions.A + " B: " + this.OverallDimensions.B + " C: " + this.OverallDimensions.C + " D: " + this.OverallDimensions.D + "\n" +
                "Type Of EPFD: " + this.TypeOfEPFD + "\n" +
                "ETA Month: " + this.Eta.Month + "\n" +
                "ETA Day:" + this.Eta.Day + "\n" +
                "ETA Hour: " + this.Eta.Hour + "\n" +
                "ETA Minute: " + this.Eta.Minute + "\n" +
                "Max. static draught: " + this.MaxStaticDraught + "\n" +
                "Destination: " + this.Destination + "\n" +
                "DTE: " + this.DTE + "\n" +
                "Spare: " + this.Spare + "\n";
        }
        #endregion
    }
}
