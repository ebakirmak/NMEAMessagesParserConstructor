using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA_MessageParserConstructor.BL.Messages
{
    public class MessageType16 : RootMessages
    {
        public int SourceID { get; set; }
        public int DestinationIDA { get; set; }
        public int OffsetA { get; set; }
        public int IncrementA { get; set; }
        public int DestinationIDB { get; set; }
        public int OffsetB { get; set; }
        public int IncrementB { get; set; }
        public byte Spare2 { get; set; }
        public Logger log { get; set; }

        public MessageType16()
        {
            this.MessageID = 16;
            this.Description = "Assigned Mode Command";
            this.log = LogManager.GetCurrentClassLogger();
        }


        #region Mesaj yapısında bulunan attributelara, alınan mesajdaki değerleri set ettik.
        public override string[] Parser(string message)
        {
            try
            {
                string[] messageParts = base.Parser(message);
                string content = getContentBinary(messageParts[5], Remove(messageParts[6]));
                int a = content.Length;

                //Message ID
                this.MessageID = Convert.ToByte(getDecimalFromBinary(content, 0, 6));
                //Repeat Indicator
                this.RepeatIndicator = Convert.ToByte(getDecimalFromBinary(content, 6, 2));
                //Source ID - UserID
                this.SourceID = Convert.ToInt32(getDecimalFromBinary(content, 8, 30));
                //Spare
                this.Spare = Convert.ToByte(getDecimalFromBinary(content, 38, 2));
                //Destination ID A
                this.DestinationIDA = Convert.ToInt32(getDecimalFromBinary(content, 40, 30));
                //Offset A
                this.OffsetA = Convert.ToInt32(getDecimalFromBinary(content, 70, 12));
                //Increment A
                this.IncrementA = Convert.ToInt32(getDecimalFromBinary(content, 82, 10));
                //Destination ID B
                this.DestinationIDB = Convert.ToByte(getDecimalFromBinary(content, 92, 30));
                //Offset B
                this.OffsetB = Convert.ToInt32(getDecimalFromBinary(content, 122, 12));
                //Increment B
                this.IncrementB = Convert.ToInt32(getDecimalFromBinary(content, 134, 10));
                //Spare 2
                this.Spare2 = Convert.ToByte(getDecimalFromBinary(content, 144, 4));
              

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
                "Destination ID A: " + this.DestinationIDA + "\n" +
                "Offset A: " + this.OffsetA + "\n" +
                "Increment A: " + this.IncrementA + "\n" +
                "Destination ID B: " + this.DestinationIDB + "\n" +
                "Offset B: " + this.OffsetB + "\n" +
                "Increment B: " + this.IncrementB + "\n" +
                "Spare 2: " + this.Spare2 + "\n";
        }
        #endregion

        #region Attributeları döndürür.
        //!AIVDM,1,1,,A,@address@hidden<P00,0*18
        public override List<Tuple<string, string>> getAttributesAndValues()
        {
            List<Tuple<string, string>> _listAttribute = base.getAttributesAndValues();
            try
            {

                List<Tuple<string, string>> _attributes = new List<Tuple<string, string>> {
                  new Tuple<string, string>("Source ID",this.SourceID.ToString()),
                  new Tuple<string, string>("Destination ID A",this.DestinationIDA.ToString()),
                  new Tuple<string, string>("Off Set A",this.OffsetA.ToString()),
                  new Tuple<string, string>("Increment A",this.IncrementA.ToString()),
                  new Tuple<string, string>("Destination ID B",this.DestinationIDB.ToString()),
                  new Tuple<string, string>("Off Set B",this.OffsetB.ToString()),
                  new Tuple<string, string>("Increment B",this.IncrementB.ToString()),
                  new Tuple<string, string>("Spare 2",this.Spare2.ToString()),
                
               };

                _listAttribute.AddRange(_attributes);
            }
            catch (Exception ex)
            {
                log.Error(ex, "MessageType15 :: getAttribute");
            }

            return _listAttribute;
        }
        #endregion

    }
}
