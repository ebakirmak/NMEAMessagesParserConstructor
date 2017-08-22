using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA_MessageParserConstructor.BL.Messages
{
    public class MessageType20 : RootMessages
    {
        private int SourceStationID { get; set; }
        private int OffsetNumber1 { get; set; }
        private byte NumberOfSlots1 { get; set; }
        private byte TimeOut1 { get; set; }
        private byte Increment1 { get; set; }
        private int OffsetNumber2 { get; set; }
        private int NumberOfSlots2 { get; set; }
        private byte TimeOut2 { get; set; }
        private byte Increment2 { get; set; }
        private int OffsetNumber3 { get; set; }
        private int NumberOfSlots3 { get; set; }
        private byte TimeOut3 { get; set; }
        private byte Increment3 { get; set; }
        private int OffsetNumber4 { get; set; }
        private int NumberOfSlots4 { get; set; }
        private byte TimeOut4 { get; set; }
        private byte Increment4 { get; set; }
        private Logger log { get; set; }

        public MessageType20()
        {
            this.MessageID = 20;
            this.Description = "Data Link Management Message";
            this.log = LogManager.GetCurrentClassLogger();
        }

        #region Mesaj yapısında bulunan attributelara, alınan mesajdaki değerleri set ettik.
        public override string[] Parser(string message1)
        {
            try
            {
                string[] messageParts1 = base.Parser(message1);
                string content = getContentBinary(messageParts1[5], Remove(messageParts1[6]));
                //Message ID
                this.MessageID = Convert.ToByte(getDecimalFromBinary(content, 0, 6));
                //Repeat Indicator
                this.RepeatIndicator = Convert.ToByte(getDecimalFromBinary(content, 6, 2));
                //Source Station ID
                this.SourceStationID = Convert.ToInt32(getDecimalFromBinary(content, 8, 30));
                //Spare
                this.Spare = Convert.ToByte(getDecimalFromBinary(content, 38, 2));


                //Offset Number 1 
                this.OffsetNumber1 = Convert.ToInt32(getDecimalFromBinary(content, 40, 12));
                //Number of Slots 1
                this.NumberOfSlots1 = Convert.ToByte(getDecimalFromBinary(content, 52, 4));
                //Time out 1
                this.TimeOut1 = Convert.ToByte(getDecimalFromBinary(content, 56, 3));
                //Increment 1
                this.Increment1 = Convert.ToByte(getDecimalFromBinary(content, 59, 11));


                //Offset Number 2
                this.OffsetNumber2 = Convert.ToInt32(getDecimalFromBinary(content, 70, 12));
                //Number of Slots 2
                this.NumberOfSlots2 = Convert.ToByte(getDecimalFromBinary(content, 82, 4));
                //Time out 2
                this.TimeOut2 = Convert.ToByte(getDecimalFromBinary(content, 86, 3));
                //Increment 2
                this.Increment2 = Convert.ToByte(getDecimalFromBinary(content, 89, 11));


                //Offset Number 3
                this.OffsetNumber3 = Convert.ToInt32(getDecimalFromBinary(content, 100, 12));
                //Number of Slots 3
                this.NumberOfSlots3 = Convert.ToByte(getDecimalFromBinary(content, 112, 4));
                //Time out 3
                this.TimeOut3 = Convert.ToByte(getDecimalFromBinary(content, 116, 3));
                //Increment 3
                this.Increment3 = Convert.ToByte(getDecimalFromBinary(content, 119, 11));


                //Offset Number 4
                this.OffsetNumber4 = Convert.ToInt32(getDecimalFromBinary(content, 130, 12));
                //Number of Slots 4
                this.NumberOfSlots4 = Convert.ToByte(getDecimalFromBinary(content, 142, 4));
                //Time out 4
                this.TimeOut4 = Convert.ToByte(getDecimalFromBinary(content, 146, 3));
                //Increment 4
                this.Increment4 = Convert.ToByte(getDecimalFromBinary(content, 1499, 11));

            }
            catch (Exception ex)
            {
                log.Error(ex, " MessageType20 :: Parser()");
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
                 "Source Station ID: " + this.SourceStationID + "\n" +
                 "Spare: " + this.Spare + "\n" +
                 "Off set Number 1: " + this.OffsetNumber1 + "\n" +
                 "Number Of Slot 1: " + this.NumberOfSlots1 + "\n" +
                 "Time Out 1: " + this.TimeOut1 + "\n" +
                 "Increment 1: " + this.Increment1 + "\n" +
                 "Off set Number 2: " + this.OffsetNumber2 + "\n" +
                 "Number Of Slot 2: " + this.NumberOfSlots2 + "\n" +
                 "Time Out 2: " + this.TimeOut2 + "\n" +
                 "Increment 2: " + this.Increment2 + "\n" +
                 "Off set Number 3: " + this.OffsetNumber3 + "\n" +
                 "Number Of Slot 3: " + this.NumberOfSlots3 + "\n" +
                 "Time Out 3: " + this.TimeOut3 + "\n" +
                 "Increment 3: " + this.Increment3 + "\n" +
                 "Off set Number 4: " + this.OffsetNumber4 + "\n" +
                 "Number Of Slot 4: " + this.NumberOfSlots4 + "\n" +
                 "Time Out 4: " + this.TimeOut4 + "\n" +
                 "Increment 4: " + this.Increment4 + "\n";




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
                new Tuple<string, string>("Source Station",this.SourceStationID.ToString()),
                new Tuple<string, string>("Off Set Number 1",this.OffsetNumber1.ToString()),
                new Tuple<string, string>("Number of Slots 1",this.NumberOfSlots1.ToString()),
                new Tuple<string, string>("TimeOut 1",this.TimeOut1.ToString()),
                new Tuple<string, string>("Increment 1",this.Increment1.ToString()),
                new Tuple<string, string>("Off Set Number 2",this.OffsetNumber2.ToString()),
                new Tuple<string, string>("Number Of Slots 2",this.NumberOfSlots2.ToString()),
                new Tuple<string, string>("Timeout 2",this.TimeOut2.ToString()),
                new Tuple<string, string>("Increment 2",this.Increment2.ToString()),
                new Tuple<string, string>("Off Set Number 3",this.OffsetNumber3.ToString()),
                new Tuple<string, string>("Number Of Slots 3",this.NumberOfSlots3.ToString()),
                new Tuple<string, string>("Timeout 3",this.TimeOut3.ToString()),
                new Tuple<string, string>("Increment 3",this.Increment3.ToString()),
                new Tuple<string, string>("Off Set Number 4",this.OffsetNumber4.ToString()),
                new Tuple<string, string>("Number Of Slots 4",this.NumberOfSlots4.ToString()),
                new Tuple<string, string>("Timeout 4",this.TimeOut4.ToString()),
                new Tuple<string, string>("Increment 4",this.Increment4.ToString()),

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
