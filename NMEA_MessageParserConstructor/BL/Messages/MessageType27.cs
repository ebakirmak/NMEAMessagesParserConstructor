using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA_MessageParserConstructor.BL.Messages
{
    public class MessageType27 : RootMessages
    {
        public int UserID { get; set; }
        public byte PositionAccuracy { get; set; }
        public byte RAIMFlag { get; set; }
        public byte NavigationalStatus { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int SOG { get; set; }
        public int COG { get; set; }
        public byte PositionLatency { get; set; }
        public Logger log { get; set; }

        public MessageType27()
        {
            this.MessageID = 27;
            this.Description = "Long-range automatic identification system broadcast message";
            this.TotalNumberOfBits = 96;
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
                //User ID
                this.UserID = Convert.ToInt32(getDecimalFromBinary(content, 8, 30));
                //Position Accuracy
                this.PositionAccuracy = Convert.ToByte(getDecimalFromBinary(content, 38, 1));
                //RAIM Flag
                this.RAIMFlag = Convert.ToByte(getDecimalFromBinary(content, 39, 1));
                //Navigational Statu
                this.NavigationalStatus = Convert.ToByte(getDecimalFromBinary(content, 40, 4));
                //Longitude
                this.Longitude = Convert.ToDouble(getDecimalFromBinary(content, 44, 18))/600;
                //Latitude
                this.Latitude = Convert.ToDouble(getDecimalFromBinary(content, 62, 17))/600;
                //SOG
                this.SOG = Convert.ToInt32(getDecimalFromBinary(content, 79, 6));
                //COG
                this.COG = Convert.ToInt32(getDecimalFromBinary(content, 85, 9));
                //Position Accuracy
                this.PositionLatency = Convert.ToByte(getDecimalFromBinary(content, 94, 1));
                //Spare
                this.Spare = Convert.ToByte(getDecimalFromBinary(content, 95, 1));

            }
            catch (Exception ex)
            {
                log.Error(ex, "MessageType27 :: Parser");
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
                "User ID: " + this.UserID + "\n" +
                "Position Accuracy: " + this.PositionAccuracy + "\n" +
                "RAIM Flag: " + this.RAIMFlag + "\n" +
                "Navigational status: " + this.NavigationalStatus + "\n" +
                "Longitude: " + this.Longitude + "\n" +
                "Latitude: " + this.Latitude + "\n" +
                "SOG: " + this.SOG + "\n" +
                "COG: " + this.COG + "\n" +
                "Position latency: " + this.PositionLatency + "\n" +
                "Spare: " + this.Spare;




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
                new Tuple<string, string>("User ID",this.UserID.ToString()),
                new Tuple<string, string>("Position Accuracy",this.PositionAccuracy.ToString()),
                new Tuple<string, string>("RAIM Flag",this.RAIMFlag.ToString()),
                new Tuple<string, string>("Navigational Status",this.NavigationalStatus.ToString()),
                new Tuple<string, string>("Longitude",this.Longitude.ToString()),
                new Tuple<string, string>("Latitude",this.Latitude.ToString()),
                new Tuple<string, string>("SOG",this.SOG.ToString()),
                new Tuple<string, string>("COG",this.COG.ToString()),
                new Tuple<string, string>("Position Latency",this.PositionLatency.ToString()),
                };
                _listAttribute.AddRange(_attributes);
            }
            catch (Exception ex)
            {
                log.Error(ex, "MessageType27 :: getAttribute");
            }

            return _listAttribute;
        }
        #endregion
    }
}
