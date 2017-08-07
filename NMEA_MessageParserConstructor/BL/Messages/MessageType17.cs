using NLog;
using NMEA_MessageParserConstructor.BL.AnnexClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA_MessageParserConstructor.BL.Messages
{
    public class MessageType17 : RootMessages
    {
        private int SourceID { get; set; }
        private double Longitude { get; set; }
        private double Latitude { get; set; }
        private byte Spare2 { get; set; }
        private CorrectionData Data { get; set; }
        private Logger log { get; set; }

        public MessageType17()
        {
            this.MessageID = 17;
            this.Description = "Global navigation-satellite system broadcast binary message";
            this.Data = new CorrectionData();
            this.log = LogManager.GetCurrentClassLogger();
        }



        #region Mesaj yapısında bulunan attributelara, alınan mesajdaki değerleri set ettik.
        public override string[] Parser(string message1, string message2)
        {
            try
            {
                string[] messageParts1 = base.Parser(message1);
                string[] messageParts2 = base.Parser(message2);
                string content = getContentBinary(messageParts1[5], Remove(messageParts1[6]));
                content += getContentBinary(messageParts2[5], Remove(messageParts2[6]));
                //Message ID
                this.MessageID = Convert.ToByte(getDecimalFromBinary(content, 0, 6));
                //Repeat Indicator
                this.RepeatIndicator = Convert.ToByte(getDecimalFromBinary(content, 6, 2));
                //Source ID - UserID
                this.SourceID = Convert.ToInt32(getDecimalFromBinary(content, 8, 30));
                //Spare
                this.Spare = Convert.ToByte(getDecimalFromBinary(content, 38, 2));
                //Longitude
                this.Longitude = Convert.ToDouble(getDecimalFromBinary(content, 40, 18))/600000;
                //Latitude
                this.Latitude = Convert.ToDouble(getDecimalFromBinary(content, 58, 17)) / 600000;
                //Spare
                this.Spare2 = Convert.ToByte(getDecimalFromBinary(content, 75, 5));
                //DATA MessageType
                this.Data.setValue(content, 80);

            }
            catch (Exception ex)
            {
                log.Error(ex, " MessageType16 :: Parser()");
                //throw;
                return null;
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
                "Source ID: " + this.SourceID + "\n" +
                "Spare: " + this.Spare + "\n" +
                "Longitude: " + this.Longitude + "\n" +
                "Latitude: " + this.Latitude + "\n" +
                "Spare 2 : " + this.Spare2 + "\n" +
                this.Data.ToString();
                
        }
        #endregion

        #region Attributeları döndürür.
        //new Tuple<string, string>("",this..ToString()),
        public override List<Tuple<string, string>> getAttributes()
        {
            List<Tuple<string, string>> _listAttribute = base.getAttributes();

            List<Tuple<string, string>> _listCorrectionData = this.Data.getAttributes();

            try
            {

                List<Tuple<string, string>> _attributes = new List<Tuple<string, string>> {
                  new Tuple<string, string>("Source ID",this.SourceID.ToString()),
                  new Tuple<string, string>("Longitude",this.Longitude.ToString()),
                  new Tuple<string, string>("Latitude",this.Latitude.ToString()),
                  new Tuple<string, string>("Spare 2",this.Spare2.ToString()),


               };
                _listAttribute.AddRange(_attributes);

                foreach (var correctionData in _listCorrectionData)
                {
                    _listAttribute.Add(new Tuple<string, string>(correctionData.Item1, correctionData.Item2));
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "MessageType17 :: getAttribute");
            }

            return _listAttribute;
        }
        #endregion
    }
}
