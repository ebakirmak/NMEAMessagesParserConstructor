using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA_MessageParserConstructor.BL.Messages
{
    public class MessageType9:RootMessages
    {
        private int UserID { get; set; }
        private int Altitude { get; set; }
        private double SOG { get; set; }
        private int PositionAccuracy { get; set; }
        private double Longitude { get; set; }
        private double Latitude { get; set; }
        private double COG { get; set; }
        private int TimeStamp { get; set; }
        private int AltitudeSensor { get; set; }
        private int DTE { get; set; }
        private int AssignedModeFlag { get; set; }
        private int RAIMFlag { get; set; }
        private int CommunicationStateSelectorFlag { get; set; }
        private Logger log { get; set; }


        public MessageType9()
        {
            this.MessageID = 9;
            this.TotalNumberOfBits = 168;
            this.Description = "Standard search and rescue aircraft position report";
            this.TotalNumberOfBits = 168;
            this.log = LogManager.GetCurrentClassLogger();
        }

        #region Mesaj yapısında bulunan attributelara, alınan mesajdaki değerleri set ettik.
        public override string[] Parser(string message)
        {
            try
            {
                string[] messageParts = base.Parser(message);
                string content = getContentBinary(messageParts[5], Remove(messageParts[6]));

                //Message ID
                this.MessageID = Convert.ToByte(getDecimalFromBinary(content, 0, 6));
                //Repeat Indicator
                this.RepeatIndicator = Convert.ToByte(getDecimalFromBinary(content, 6, 2));
                //Source ID - UserID
                this.UserID = Convert.ToInt32(getDecimalFromBinary(content, 8, 30));
                //Altitude (GNSS)
                this.Altitude = Convert.ToInt32(getDecimalFromBinary(content, 38, 12));
                //SOG
                this.SOG = Convert.ToDouble(getDecimalFromBinary(content, 50, 10));
                //PositionAccuracy
                this.PositionAccuracy = Convert.ToInt32(getDecimalFromBinary(content, 60, 1));
                //Longitude
                this.Longitude = Convert.ToDouble(getDecimalFromBinary(content, 61, 28)) / 60 / 10000;
                //Latitude
                this.Latitude = Convert.ToDouble(getDecimalFromBinary(content, 89, 27)) / 60 / 10000;
                //COG
                this.COG = Convert.ToDouble(getDecimalFromBinary(content, 116, 12))/10;
                //Time Stamp
                this.TimeStamp = Convert.ToInt32(getDecimalFromBinary(content, 128, 6));
                //Altitude Sensor
                this.AltitudeSensor = Convert.ToInt32(getDecimalFromBinary(content, 134, 1));
                //Spare
                this.Spare = Convert.ToByte(getDecimalFromBinary(content, 135, 7));
                //DTE
                this.DTE = Convert.ToInt32(getDecimalFromBinary(content, 142, 1));
                //Spare
                this.Spare = Convert.ToByte(getDecimalFromBinary(content, 143, 3));
                //Assigned Mode Flag
                this.AssignedModeFlag = Convert.ToInt32(getDecimalFromBinary(content, 146, 1));
                //RAIM Flag
                this.RAIMFlag = Convert.ToInt32(getDecimalFromBinary(content, 147, 1));
                //Communication state selector flag
                this.CommunicationStateSelectorFlag = Convert.ToInt32(getDecimalFromBinary(content, 148, 1));
                //Communication state
                this.CommunicationState = Convert.ToInt32(getDecimalFromBinary(content, 149, 19));
            }
            catch (Exception ex)
            {
                log.Error(ex, " MessageType8 :: Parser()");
                throw;
            }


            return null;

        }
        #endregion

        #region ToString mesajını ezdik. Methodu sınıfa göre tasarladık.
        public override string ToString()
        {
            return
                "Message ID: " + this.MessageID + "\n" +
                "Repeat Indicator: " + this.RepeatIndicator + "\n" +
                "User ID: " + this.UserID + "\n" +
                "Altitude(GNSS): " + this.Altitude + "\n" +
                "SOG" + this.SOG + "\n" +
                "PO: " + this.PositionAccuracy + "\n" +
                "Longitude: " + this.Longitude + "\n" +
                "Latitude: " + this.Latitude + "\n" +
                "COG: " + this.COG + "\n" +
                "Time stamp: " + this.TimeStamp + "\n" +
                "Altitude Sensor: " + this.AltitudeSensor + "\n" +
                "Spare: " + this.Spare + "\n" +
                "DTE: " + this.DTE + "\n" +
                "Spare: " + this.Spare + "\n" +
                "Assigned Mode Flag: " + this.AssignedModeFlag + "\n" +
                "RAIM Flag: " + this.RAIMFlag + "\n" +
                "Communication State Selector Flag: " + this.CommunicationStateSelectorFlag + "\n" +
                "Communication State: " + this.CommunicationState + "\n";

        }
        #endregion

        #region Attributeları döndürür.
        public override List<Tuple<string, string>> getAttributes()
        {
            List<Tuple<string, string>> _listAttribute = base.getAttributes();
            List<Tuple<string, string>> _attributes = new List<Tuple<string, string>> {
                  new Tuple<string, string>("User ID",this.UserID.ToString()),
                  new Tuple<string, string>("Altitude",this.Altitude.ToString()),
                  new Tuple<string, string>("SOG",this.SOG.ToString()),
                  new Tuple<string, string>("PositionAccuracy",this.PositionAccuracy.ToString()),
                  new Tuple<string, string>("Longitude",this.Longitude.ToString()),
                  new Tuple<string, string>("Latitude",this.Latitude.ToString()),
                  new Tuple<string, string>("COG",this.COG.ToString()),
                  new Tuple<string, string>("Time Stamp",this.TimeStamp.ToString()),
                  new Tuple<string, string>("Altitude Sensor",this.AltitudeSensor.ToString()),
                  new Tuple<string, string>("DTE",this.DTE.ToString()),
                  new Tuple<string, string>("Assigned Mode Flag",this.AssignedModeFlag.ToString()),
                  new Tuple<string, string>("RAIM Flag",this.RAIMFlag.ToString()),
                  new Tuple<string, string>("Communication State Selector Flag",this.CommunicationStateSelectorFlag.ToString())
             };
            _listAttribute.AddRange(_attributes);
            return _listAttribute;
        }
        #endregion
    }
}
