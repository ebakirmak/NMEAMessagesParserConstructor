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
        private byte TOEPFD { get; set; }
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


        #region Parser

        #region Mesaj yapısında bulunan attributelara, alınan mesajdaki değerleri set ettik.
        public override string[] Parser(string message1,string message2)
        {
            //Mesajı parçalarına ayır.
            string[] messageParts1 = message1.Split(',');
            string[] messageParts2 = message2.Split(',');


            //
            string[] messageParts = base.Parser(message1);
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
            this.TOEPFD = Convert.ToByte(getDecimalFromBinary(content, 270, 4));

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

        #region getAttributesAndValues(): Attributeları döndürür.
        public override List<Tuple<string, string>> getAttributesAndValues()
        {
            List<Tuple<string, string>> _listAttribute = base.getAttributesAndValues();
            List<Tuple<string, string>> _attributes = new List<Tuple<string, string>> {
                  new Tuple<string, string>("User ID",this.UserID.ToString()),
                  new Tuple<string, string>("AIS Version Indicator",this.AISVersionIndicator.ToString()),
                  new Tuple<string, string>("IMO Number",this.IMONumber.ToString()),
                  new Tuple<string, string>("Call Sign",this.CallSign.ToString()),
                  new Tuple<string, string>("Name",this.Name.ToString()),
                  new Tuple<string, string>("Type Of Ship And Cargo Type",this.TypeOfShipAndCargoType.ToString()),
                  new Tuple<string, string>("Overall Dimensions",this.OverallDimensions.ToString()),
                  new Tuple<string, string>("EPFD Type",this.TOEPFD.ToString()),
                  new Tuple<string, string>("Eta", this.Eta.ToString()),
                  new Tuple<string, string>("MaxStaticDraught",this.MaxStaticDraught.ToString()),
                  new Tuple<string, string>("Destination",this.Destination.ToString()),
                  new Tuple<string, string>("DTE",this.DTE.ToString())
             };
            _listAttribute.AddRange(_attributes);
            return _listAttribute;
        }
        #endregion

        #endregion

        #region Constructor 


        #region getAttributes(): Attributeları döndürür.
        public override List<Tuple<string, string>> getAttributes()
        {
            List<Tuple<string, string>> _listAttribute = base.getAttributes(2);
            List<Tuple<string, string>> _attributes = new List<Tuple<string, string>> {
                  new Tuple<string, string>("User ID","603916439"),
                  new Tuple<string, string>("AIS Version Indicator","0"),
                  new Tuple<string, string>("IMO Number","439303422"),
                  new Tuple<string, string>("Call Sign","ZA83R"),
                  new Tuple<string, string>("Name","ARCO AVON"),
                  new Tuple<string, string>("Type Of Ship And Cargo Type","69"),
                  new Tuple<string, string>("Overall Dimensions A","113"),
                  new Tuple<string, string>("Overall Dimensions B","31"),
                  new Tuple<string, string>("Overall Dimensions C","17"),
                  new Tuple<string, string>("Overall Dimensions D","11"),
                  new Tuple<string, string>("EPFD Type","0"),
                  new Tuple<string, string>("ETA Month","3"),
                  new Tuple<string, string>("ETA Day","23"),
                  new Tuple<string, string>("ETA Hour","19"),
                  new Tuple<string, string>("ETA Minute","45"),
                  new Tuple<string, string>("Max. static draught","13,2"),
                  new Tuple<string, string>("Destination","HOUSTON"),
                  new Tuple<string, string>("DTE (availability)","0"),
             };
            _listAttribute.AddRange(_attributes);
            return _listAttribute;
        }
        #endregion

        #region Constructor(): Girilen değerlere göre VDM veya VDO mesajı oluşturuluyor. Bir mesajın uzunluğu maksimum 289.
        public override string Constructor(List<string> _listMessage)
        {
            //Temel mesaj özellikleri alınıyor.
            string Message = base.Constructor(_listMessage);
            string Message2 = base.Constructor(_listMessage);
            #region Datagridview'den alınan değerleri set et.
            string errorMessage = "Error!";
            /////////////////////////////////////////////////////
            if (ControlMessageID(Convert.ToByte(_listMessage[5])))
                this.MessageID = Convert.ToByte(_listMessage[5]);
            else
             return   errorMessage += "\nMessage ID değerini kontrol ediniz.";
            /////////////////////////////////////////////////////   
            if (ControlRepeatIndicator(Convert.ToByte(_listMessage[8])))
                this.RepeatIndicator = Convert.ToByte(_listMessage[8]);
            else
             return  errorMessage += "\nRepeat Indicator değerini kontrol ediniz.";
            ////////////////////////////////////////////////////////////
            this.UserID = Convert.ToInt32(_listMessage[10]);
            ///////////////////////////////////////////////////////////
            if (ControlAISVersionIndicator(Convert.ToByte(_listMessage[11])))
                this.AISVersionIndicator = Convert.ToByte(_listMessage[11]);
            else
                return errorMessage += "\nAIS Version Indicator değerini kontrol ediniz.";
            //////////////////////////////////////////////////////////
            if (ControlIMONumber(Convert.ToInt32(_listMessage[12])))
                this.IMONumber = Convert.ToInt32(_listMessage[12]);
            else
                return errorMessage += "\nIMO Number değerini kontrol ediniz.";
            /////////////////////////////////////////////////////////
            if (ControlCallSign(Convert.ToString(_listMessage[13])))
                this.CallSign = Convert.ToString(_listMessage[13]);
            else
                return errorMessage += "\nCall Sign değerini kontrol ediniz.";
            //////////////////////////////////////////////////////////
            if (ControlName(Convert.ToString(_listMessage[14])))
                this.Name = Convert.ToString(_listMessage[14]).PadRight(20,'@');
            else
                return errorMessage += "\nName değerini kontrol ediniz.";
            ////////////////////////////////////////////////////////
            if (ControlTOSACT(Convert.ToByte(_listMessage[15])))
                this.TypeOfShipAndCargoType = Convert.ToByte(_listMessage[15]);
            else
                return errorMessage += "\nType of ship and cargo type değerini kontrol ediniz.";
            ///////////////////////////////////////////////////
            if (ControlOverallDimensionAB(Convert.ToByte(_listMessage[16])))
                this.OverallDimensions.setA( Convert.ToByte(_listMessage[16]));
            else
                return errorMessage += "\nDim A değerini kontrol ediniz.";
            //////////////////////////////////////////////////////
            if (ControlOverallDimensionAB(Convert.ToByte(_listMessage[17])))
                this.OverallDimensions.setB( Convert.ToByte(_listMessage[17]));
            else
                return errorMessage += "\nDim B değerini kontrol ediniz.";
            //////////////////////////////////////////////////////
            if (ControlOverallDimensionCD(Convert.ToByte(_listMessage[18])))
                this.OverallDimensions.setC(Convert.ToByte(Math.Round(Convert.ToDouble(_listMessage[18]), 7)));
            else
                return errorMessage += "\nDim C değerini kontrol ediniz.";
            ////////////////////////////////////////////////////////
            if (ControlOverallDimensionCD(Convert.ToByte(_listMessage[19])))
                this.OverallDimensions.setD(Convert.ToByte((Math.Round(Convert.ToDouble(_listMessage[19]), 7))));
            else
                return errorMessage += "\nDim D değerini kontrol ediniz.";
            /////////////////////////////////////////////////////////
            if (ControlTOEPFD(Convert.ToByte(_listMessage[20])))
                this.TOEPFD = Convert.ToByte(_listMessage[20]);
            else
                return errorMessage += "\nType Of Electronic position fixing device değerini kontrol ediniz.";
            //////////////////////////////////////////////////
            if (ControlUTCMinute(Convert.ToByte(_listMessage[24])))
                this.Eta.setMinute(Convert.ToByte(_listMessage[24]));
            else
                return errorMessage += "\nETA Minute değerini kontrol ediniz.";
            /////////////////////////////////////////////////
            if (ControlUTCHour(Convert.ToByte(_listMessage[23])))
                this.Eta.setHour(Convert.ToByte(_listMessage[23]));
            else
                return errorMessage += "\nETA Hour değerini kontrol ediniz.";
            /////////////////////////////////////////////////
            if (ControlUTCDay(Convert.ToByte(_listMessage[22])))
                this.Eta.setDay(Convert.ToByte(_listMessage[22]));
            else
                return errorMessage += "\nETA Day değerini kontrol ediniz.";
            /////////////////////////////////////////////////
            if (ControlUTCMonth(Convert.ToByte(_listMessage[21])))
                this.Eta.setMonth(Convert.ToByte(_listMessage[21]));
            else
                return errorMessage += "\nETA Month değerini kontrol ediniz.";
            /////////////////////////////////////////////////
            if (ControlDraught(Convert.ToDouble(_listMessage[25])))
                this.MaxStaticDraught=Convert.ToDouble(_listMessage[25]);
            else
                return errorMessage += "\nDraught değerini kontrol ediniz.";
            /////////////////////////////////////////////////
            if (ControlDestination(Convert.ToString(_listMessage[26])))
                this.Destination=Convert.ToString(_listMessage[26]).PadRight(20,'@');
            else
                return errorMessage += "\nDestination değerini kontrol ediniz.";
            /////////////////////////////////////////////////
            if (ControlDTE(Convert.ToByte(_listMessage[27])))
                this.DTE = Convert.ToByte(_listMessage[27]);
            else
                return errorMessage += "\nDTE değerini kontrol ediniz.";
            #endregion

            #region Bit değerlerine göre binary mesaj oluşturuluyor.
            string binaryMessage = setBinaryToDecimal(this.MessageID).PadLeft(6, '0');
            binaryMessage += setBinaryToDecimal(this.RepeatIndicator).PadLeft(2, '0');
            binaryMessage += setBinaryToDecimal(this.UserID).PadLeft(30, '0');
            binaryMessage += setBinaryToDecimal(this.AISVersionIndicator).PadLeft(2, '0');
            binaryMessage += setBinaryToDecimal(this.IMONumber).PadLeft(30, '0');
            binaryMessage += setBinaryToString(this.CallSign).PadLeft(42, '0');
            binaryMessage += setBinaryToString(this.Name).PadLeft(120, '0');
            binaryMessage += setBinaryToDecimal(this.TypeOfShipAndCargoType).PadLeft(8, '0');
            binaryMessage += setBinaryToDecimal(this.OverallDimensions.getA()).PadLeft(9, '0');
            binaryMessage += setBinaryToDecimal(this.OverallDimensions.getB()).PadLeft(9, '0');
            binaryMessage += setBinaryToDecimal(this.OverallDimensions.getC()).PadLeft(6, '0');
            binaryMessage += setBinaryToDecimal(this.OverallDimensions.getD()).PadLeft(6, '0');
            binaryMessage += setBinaryToDecimal(this.TOEPFD).PadLeft(4, '0');
            binaryMessage += setBinaryToDecimal(this.Eta.getMonth()).PadLeft(4, '0');
            binaryMessage += setBinaryToDecimal(this.Eta.getDay()).PadLeft(5, '0');
            binaryMessage += setBinaryToDecimal(this.Eta.getHour()).PadLeft(5, '0');

            string binaryMessageContinue = setBinaryToDecimal(this.Eta.getMinute()).PadLeft(6, '0');
            binaryMessageContinue += setBinaryToDecimal(MultiplyDraught(this.MaxStaticDraught)).PadLeft(8, '0');
            binaryMessageContinue += setBinaryToString(this.Destination).PadLeft(120, '0');
            binaryMessageContinue += setBinaryToDecimal(this.DTE).PadLeft(1, '0');
            binaryMessageContinue += setBinaryToDecimal(this.Spare).PadLeft(1, '0');
            #endregion

            #region binary message, SetContent fonksiyonuna gönderilerek, ASCII8 tipinde mesaj content içeriği oluşturuluyor.
            string content = setContent(binaryMessage);
            string content2 = setContent(binaryMessageContinue);
            #endregion

            if (errorMessage.Contains("Error!") && errorMessage.Length > 6)
                return errorMessage;
            else
                return Message + content +"\n"+Message2+ content2;
        }
        #endregion



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
                "Type Of EPFD: " + this.TOEPFD + "\n" +
                this.Eta.ToString() +
                "Max. static draught: " + this.MaxStaticDraught + "\n" +
                "Destination: " + this.Destination + "\n" +
                "DTE: " + this.DTE + "\n" +
                "Spare: " + this.Spare + "\n";
        }
        #endregion

       
    }
}
