using NMEA_MessageParserConstructor.BL.Dictionarys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NMEA_MessageParserConstructor.BL;
using NMEA_MessageParserConstructor.BL.AnnexClasses;

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
            this.OverallDimensions.setValue(content, 240);
            
            //
            this.TypeOfEPFD = Convert.ToByte(getDecimalFromBinary(content, 270, 4));

            //Estimated time of arrival.
            this.Eta.setValue(content, 274);
       
            
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
                "Overall Dimensions A: " + this.OverallDimensions.getA() +
                                  " B: " + this.OverallDimensions.getB() +
                                  " C: " + this.OverallDimensions.getC() +
                                  " D: " + this.OverallDimensions.getD() + "\n" +
                "Type Of EPFD: " + this.TypeOfEPFD + "\n" +
                this.Eta.ToString() +
                "Max. static draught: " + this.MaxStaticDraught + "\n" +
                "Destination: " + this.Destination + "\n" +
                "DTE: " + this.DTE + "\n" +
                "Spare: " + this.Spare + "\n";
        }
        #endregion

        #region Attributeları döndürür.
        public override List<Tuple<string, string>> getAttributes()
        {
            List<Tuple<string, string>> _listAttribute = base.getAttributes();
            List<Tuple<string, string>> _attributes = new List<Tuple<string, string>> {
                  new Tuple<string, string>("User ID",this.UserID.ToString()),
                  new Tuple<string, string>("AIS Version Indicator",this.AISVersionIndicator.ToString()),
                  new Tuple<string, string>("IMO Number",this.IMONumber.ToString()),
                  new Tuple<string, string>("Call Sign",this.CallSign.ToString()),
                  new Tuple<string, string>("Name",this.Name.ToString()),
                  new Tuple<string, string>("Type Of Ship And Cargo Type",this.TypeOfShipAndCargoType.ToString()),
                  new Tuple<string, string>("Overall Dimensions",this.OverallDimensions.ToString()),
                  new Tuple<string, string>("EPFD Type",this.TypeOfEPFD.ToString()),
                  new Tuple<string, string>("Eta", this.Eta.ToString()),
                  new Tuple<string, string>("MaxStaticDraught",this.MaxStaticDraught.ToString()),
                  new Tuple<string, string>("Destination",this.Destination.ToString()),
                  new Tuple<string, string>("DTE",this.DTE.ToString())
             };
            _listAttribute.AddRange(_attributes);            
            return _listAttribute;
        }
        #endregion
    }
}
