using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA_MessageParserConstructor.BL.Messages
{
    public class MessageType15 : RootMessages
    {
        private int SourceID { get; set; }
        private int DestinationID1 { get; set; }
        private int MessageID1_1 { get; set; }
        private int SlotOffset1_1 { get; set; }
        private byte Spare2 { get; set; }
        private int MessageID1_2 { get; set; }
        private int SlotOffset1_2 { get; set; }
        private byte Spare3 { get; set; }
        private int DestinationID2 { get; set; }
        private int MessageID2_1 { get; set; }
        private int SlotOffset2_1 { get; set; }
        private byte Spare4 { get; set; }
        private Logger log;

        public MessageType15()
        {
            this.MessageID = 15;
            this.Description = "Interrogation";
            this.TotalNumberOfBits = 160;
            log = LogManager.GetCurrentClassLogger();
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
                //Destination ID 1
                this.DestinationID1 = Convert.ToInt32(getDecimalFromBinary(content, 40, 30));
                //Message ID 1.1
                this.MessageID1_1 = Convert.ToInt32(getDecimalFromBinary(content, 70, 6));
                //Slot Offset 1.1
                this.SlotOffset1_1 = Convert.ToInt32(getDecimalFromBinary(content, 76, 12));
                //Spare 2
                this.Spare2 = Convert.ToByte(getDecimalFromBinary(content, 88, 2));
                //Message ID 1.2
                this.MessageID1_2 = Convert.ToInt32(getDecimalFromBinary(content, 90, 6));
                //Slot Offset 1.2
                this.SlotOffset1_2 = Convert.ToInt32(getDecimalFromBinary(content, 96, 12));
                //Spare 3
                this.Spare3 = Convert.ToByte(getDecimalFromBinary(content, 108, 2));
                //Destination ID 2
                this.DestinationID2 = Convert.ToInt32(getDecimalFromBinary(content, 110, 30));
                //Message ID 2.1
                this.MessageID2_1 = Convert.ToInt32(getDecimalFromBinary(content, 140, 6));
                //Slot Offset 2.1
                this.SlotOffset2_1 = Convert.ToInt32(getDecimalFromBinary(content, 146, 12));
                //Spare 4
                this.Spare4 = Convert.ToByte(getDecimalFromBinary(content, 158, 2));

            }
            catch (Exception ex)
            {
                log.Error(ex, " MessageType15 :: Parser()");
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
                "Destination ID 1: " + this.DestinationID1 + "\n" +
                "Message ID 1.1: " + this.MessageID1_1 + "\n" +
                "Slot Offset 1.1: " + this.SlotOffset1_1 + "\n" +
                "Spare2: " + this.Spare2 + "\n" +
                "Message ID 1.2: " + this.MessageID1_2 + "\n" +
                "Slot Offset 1.2: " + this.SlotOffset1_2 + "\n" +
                "Spare 3: " + this.Spare3 + "\n" +
                "Destination ID 2: " + this.DestinationID2 + "\n" +
                "Message ID 2.1" + this.MessageID1_2 + "\n" +
                "Slot Offset 2.1 " + this.SlotOffset2_1 + "\n" +
                "Spare 4: " + this.Spare4 + "\n";



        }
        #endregion

        #region Attributeları döndürür.
        public override List<Tuple<string, string>> getAttributes()
        {
            List<Tuple<string, string>> _listAttribute = base.getAttributes();
            try
            {

                List<Tuple<string, string>> _attributes = new List<Tuple<string, string>> {
                  new Tuple<string, string>("Source ID",this.SourceID.ToString()),
                  new Tuple<string, string>("Destination ID 1",this.DestinationID1.ToString()),
                  new Tuple<string, string>("Message ID 1.1",this.MessageID1_1.ToString()),
                  new Tuple<string, string>("Slot Off Set 1.1",this.SlotOffset1_1.ToString()),
                  new Tuple<string, string>("Spare 2",this.Spare2.ToString()),
                  new Tuple<string, string>("Message ID 1.2",this.MessageID1_2.ToString()),
                  new Tuple<string, string>("Slot Off Set 1ç2",this.SlotOffset1_2.ToString()),
                  new Tuple<string, string>("Spare 3",this.Spare3.ToString()),
                  new Tuple<string, string>("Destination ID 2",this.DestinationID2.ToString()),
                  new Tuple<string, string>("Message ID 2.1",this.MessageID2_1.ToString()),
                  new Tuple<string, string>("Slot Off Set 2.1",this.SlotOffset2_1.ToString()),
                  new Tuple<string, string>("Spare 4",this.Spare4.ToString()),
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
