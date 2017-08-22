using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA_MessageParserConstructor.BL.Messages
{
    public class MessageType23 : RootMessages
    {
        private int SourceID { get; set; }
        private double Longitude1 { get; set; }
        private double Latitude1 { get; set; }
        private double Longitude2 { get; set; }
        private double Latitude2 { get; set; }
        private byte StationType { get; set; }
        private byte TypeOfShipAndCargoType { get; set; }
        private byte Spare2 { get; set; }
        private byte TxRxMode { get; set; }
        private byte ReportingInterval { get; set; }
        private byte QuietTime { get; set; }
        private byte Spare3 { get; set; }
        private Logger log { get; set; }

        public MessageType23()
        {
            this.MessageID = 23;
            this.Description = "Group Assignment Command";
            this.log = LogManager.GetCurrentClassLogger();
        }

        #region Mesaj yapısında bulunan attributelara, alınan mesajdaki değerleri set ettik.
        public override string[] Parser(string message1)
        {
            try
            {
                string[] messageParts1 = base.Parser(message1);
                //Context'i oku. Binary yapıda.
                string content = getContentBinary(messageParts1[5], Remove(messageParts1[6]));
                //Tüm mesajlarda olan özellikleri burada gir.
                //MessageID
                this.MessageID = Convert.ToByte(getDecimalFromBinary(content, 0, 6));
                //Repeat indicator
                this.RepeatIndicator = Convert.ToByte(getDecimalFromBinary(content, 6, 2));
                //Source ID
                this.SourceID = Convert.ToInt32(getDecimalFromBinary(content, 8, 30));
                //Spare
                this.Spare = Convert.ToByte(getDecimalFromBinary(content, 38, 2));
                //Longitude 1
                this.Longitude1 = Convert.ToDouble(getDecimalFromBinary(content, 40, 18)) / 600;
                //Latitude 1
                this.Latitude1 = Convert.ToDouble(getDecimalFromBinary(content, 58, 17)) / 600;
                //Longitude 2
                this.Longitude2 = Convert.ToDouble(getDecimalFromBinary(content, 75, 18)) / 600;
                //Latitude 2
                this.Latitude2 = Convert.ToDouble(getDecimalFromBinary(content, 93, 17)) / 600;
                //Station Type
                this.StationType = Convert.ToByte(getDecimalFromBinary(content, 110, 4));
                //Type of Ship and cargo type
                this.TypeOfShipAndCargoType = Convert.ToByte(getDecimalFromBinary(content, 114, 8));
                //Spare 2
                this.Spare2 = Convert.ToByte(getDecimalFromBinary(content, 122, 22));
                //Tx/Rx Mode
                this.TxRxMode = Convert.ToByte(getDecimalFromBinary(content, 144, 2));
                //Reporting interval
                this.ReportingInterval = Convert.ToByte(getDecimalFromBinary(content, 146, 4));
                //Quiet time
                this.QuietTime = Convert.ToByte(getDecimalFromBinary(content, 150, 4));
                //Spare
                this.Spare3 = Convert.ToByte(getDecimalFromBinary(content, 154, 6));
               
                //Spare 2
                this.Spare2 = Convert.ToByte(getDecimalFromBinary(content, 145, 23));

            }
            catch (Exception ex)
            {
                log.Error(ex, "MessageType22 :: Parser");
                //throw;
            }

            return null;
        }
        #endregion

        #region ToString() methodunu override ettik.
        public override string ToString()
        {

            return
                "Message ID: " + this.MessageID + "\n" +
                "RepeatIndicator: " + this.RepeatIndicator + "\n" +
                "Source ID: " + this.SourceID + "\n" +
                "Spare: " + this.Spare + "\n" +
                "Longitude 1: " + this.Longitude1 + "\n" +
                "Latitude 1: " + this.Latitude1 + "\n" +
                "Longitude 2: " + this.Longitude2 + "\n" +
                "Latitude 2: " + this.Latitude2 + "\n" +
                "StationType : " + this.StationType + "\n" +
                "Type Of Ship And Cargo Type: " + this.TypeOfShipAndCargoType + "\n" +
                "Spare 2: " + this.Spare2 + "\n" +
                "Tx/Rx Mode: " + this.TxRxMode + "\n" +
                "Reporting Interval: " + this.ReportingInterval + "\n" +
                "Quiet Time: " + this.QuietTime + "\n" +
                "Spare 3: " + this.Spare3 + "\n";



        }
        #endregion

        #region Attributeları döndürür.
        //new Tuple<string, string>("",this..ToString()),
        public override List<Tuple<string, string>> getAttributesAndValues()
        {
            List<Tuple<string, string>> _listAttribute = base.getAttributesAndValues();
            try
            {

                List<Tuple<string, string>> _attributes = new List<Tuple<string, string>> {
                new Tuple<string, string>("Source ID",this.SourceID.ToString()),
                new Tuple<string, string>("Longitude 1",this.Longitude1.ToString()),
                new Tuple<string, string>("Latitude 1",this.Latitude1.ToString()),
                new Tuple<string, string>("Longitude 2",this.Longitude2.ToString()),
                new Tuple<string, string>("Latitude 2",this.Latitude2.ToString()),
                new Tuple<string, string>("Station Type",this.StationType.ToString()),
                new Tuple<string, string>("Type Of Ship And Cargo Type",this.TypeOfShipAndCargoType.ToString()),
                new Tuple<string, string>("Spare 2",this.Spare2.ToString()),
                new Tuple<string, string>("Tx/Rx Mode",this.TxRxMode.ToString()),
                new Tuple<string, string>("Reporting Interval",this.ReportingInterval.ToString()),
                new Tuple<string, string>("Quiet Time",this.QuietTime.ToString()),
                new Tuple<string, string>("Spare 3",this.Spare3.ToString()),
                };
                _listAttribute.AddRange(_attributes);



            }
            catch (Exception ex)
            {
                log.Error(ex, "MessageType19 :: getAttribute");
            }

            return _listAttribute;
        }
        #endregion
    }
}
