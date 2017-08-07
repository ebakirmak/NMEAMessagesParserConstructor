using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA_MessageParserConstructor.BL.Messages
{
    public class MessageType13 : RootMessages
    {
        private int SourceID { get; set; }
        private int DestinationID1 { get; set; }
        private int SeqNumForID1 { get; set; }
        private int DestinationID2 { get; set; }
        private int SeqNumForID2 { get; set; }
        private int DestinationID3 { get; set; }
        private int SeqNumForID3 { get; set; }
        private int DestinationID4 { get; set; }
        private int SeqNumForID4 { get; set; }
        private Logger log;

        public MessageType13()
        {
            this.MessageID = 7;
            this.TotalNumberOfBits = 168;
            this.Description = "Safety related acknowledge";
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

                //Destination ID 1
                this.DestinationID1 = Convert.ToInt32(getDecimalFromBinary(content, 40, 30));

                //Sequence Number For ID 1
                this.SeqNumForID1 = Convert.ToInt32(getDecimalFromBinary(content, 70, 2));

                if (content.Length > 72)
                {
                    //Destination ID 2
                    this.DestinationID2 = Convert.ToInt32(getDecimalFromBinary(content, 72, 30));

                    //Sequence Number For ID 2
                    this.SeqNumForID2 = Convert.ToInt32(getDecimalFromBinary(content, 102, 2));
                }

                if (content.Length > 104)
                {
                    //Destination ID 3
                    this.DestinationID3 = Convert.ToByte(getDecimalFromBinary(content, 104, 30));

                    //Sequence Number For ID 3
                    this.SeqNumForID3 = Convert.ToInt32(getDecimalFromBinary(content, 134, 2));
                }

                if (content.Length > 136)
                {
                    //Destination ID 4
                    this.DestinationID4 = Convert.ToByte(getDecimalFromBinary(content, 136, 30));

                    //Sequence Number For ID 4
                    this.SeqNumForID4 = Convert.ToInt32(getDecimalFromBinary(content, 166, 2));
                }


            }
            catch (Exception ex)
            {
                log.Error(ex, " MessageType7 :: Parser()");
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
                "Sequence Number for ID 1: " + this.SeqNumForID1 + "\n" +
                "Destination ID 2: " + this.DestinationID2 + "\n" +
                "Sequence Number for ID 2 " + this.SeqNumForID2 + "\n" +
                "Destination ID 3: " + this.DestinationID3 + "\n" +
                "Sequence Number for ID 3: " + this.SeqNumForID3 + "\n" +
                "Destination ID 4: " + this.DestinationID4 + "\n" +
                "Sequence Number for ID 4: " + this.SeqNumForID4 + "\n";


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
                  new Tuple<string, string>("Seqence Number For ID 1",this.SeqNumForID1.ToString()),
                  new Tuple<string, string>("Destination ID 2",this.DestinationID2.ToString()),
                  new Tuple<string, string>("Seqence Number For ID 2",this.SeqNumForID2.ToString()),
                  new Tuple<string, string>("Destination ID 3",this.DestinationID3.ToString()),
                  new Tuple<string, string>("Seqence Number For ID 3",this.SeqNumForID3.ToString()),
                  new Tuple<string, string>("Destination ID 4",this.DestinationID4.ToString()),
                  new Tuple<string, string>("Seqence Number For ID 4",this.SeqNumForID4.ToString()),

             };

                _listAttribute.AddRange(_attributes);
            }
            catch (Exception ex)
            {
                log.Error(ex, "MessageType12 :: getAttribute");
            }

            return _listAttribute;
        }
        #endregion
    }
}
