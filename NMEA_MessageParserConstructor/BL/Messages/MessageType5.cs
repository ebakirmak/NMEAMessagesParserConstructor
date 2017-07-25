using NMEA_MessageParserConstructor.BL.Dictionarys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA_MessageParserConstructor.BL.Messages
{
    public class MessageType5 : RootMessages
    {
        private int UserID { get; set; }
        private byte AISVersionIndicator { get; set; }
        private UInt32 IMONumber { get; set; }
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
            //string[] messages = base.Parser(message);
            //Context'i oku. Binary yapıda.
            string content = getContentBinary(message1);
            content += getContentBinary(message2);
            //Tüm mesajlarda olan özellikleri burada gir.

            //MessageID
            this.MessageID = Convert.ToByte(getSubstringFromBinary(content, 0, 6));

            //Repeat indicator
            this.RepeatIndicator = Convert.ToByte(getSubstringFromBinary(content, 6, 2));

            //Source MMSI
            this.UserID = Convert.ToInt32(getSubstringFromBinary(content, 8, 30));

            //AISVersionIndicator
            this.AISVersionIndicator = Convert.ToByte(getSubstringFromBinary(content, 38, 2));

            //IMONumber
            this.IMONumber = Convert.ToUInt32(getSubstringFromBinary(content, 40, 30));

            //Call sign - String
            this.CallSign = getStringFromBinary(content, 70, 42).Trim();

            //Name
            this.Name = getStringFromBinary(content, 112, 120).Trim();

            //Type Of Ship And Cargo Type
            this.TypeOfShipAndCargoType = Convert.ToByte((getSubstringFromBinary(content, 232, 8)));

            //Overall Dimensions String Hatalı
            this.OverallDimensions.D =Convert.ToInt32(getSubstringFromBinary(content, 240, 6));
            this.OverallDimensions.C = Convert.ToInt32(getSubstringFromBinary(content, 246, 6));
            this.OverallDimensions.B = Convert.ToInt32(getSubstringFromBinary(content, 252, 9));
            this.OverallDimensions.A = Convert.ToInt32(getSubstringFromBinary(content, 261, 9));
            
            //
            this.TypeOfEPFD = Convert.ToByte(getSubstringFromBinary(content, 270, 4));

            //Estimated time of arrival.
            this.Eta.Month = Convert.ToByte(getSubstringFromBinary(content, 274, 4));
            this.Eta.Day = Convert.ToByte(getSubstringFromBinary(content, 278, 5));
            this.Eta.Hour = Convert.ToByte(getSubstringFromBinary(content, 283, 5));
            this.Eta.Minute =Convert.ToByte (getSubstringFromBinary(content, 288, 6));
            
            //Maximum present static draught
            this.MaxStaticDraught = Convert.ToDouble(getSubstringFromBinary(content, 294, 8))/10;

            //Destination - String
            this.Destination = getStringFromBinary(content, 302,120);

            //Data Terminal Equipment
            this.DTE = Convert.ToByte(getSubstringFromBinary(content, 422, 1));

            //Spare
            this.Spare = Convert.ToByte(getSubstringFromBinary(content, 423, 1));
      
            return null;
        }
        #endregion
    }
}
