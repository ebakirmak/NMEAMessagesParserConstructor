using NLog;
using NMEA_MessageParserConstructor.BL.CommunicationState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA_MessageParserConstructor.BL.Messages
{
    public class MessageType18 : RootMessages
    {
        private int UserID { get; set; }
        private int SOG { get; set; }
        private int PositionAccuracy { get; set; }
        private double Longitude { get; set; }
        private double Latitude { get; set; }
        private double COG { get; set; }
        private double TrueHeading { get; set; }
        private int TimeStamp { get; set; }
        private byte Spare2 { get; set; }
        private byte ClassBUnitFlag { get; set; }
        private byte ClassBDisplayFlag { get; set; }
        private byte ClassBDSCFlag { get; set; }
        private byte ClassBBandFlag { get; set; }
        private byte ClassBMessage22Flag { get; set; }
        private byte ModeFlag { get; set; }
        private byte RAIM_Flag { get; set; }
        private byte CommunicationStateSelectorFlag { get; set; }
        private SOTDMA Sotdma { get; set; }
        private ITDMA Itdma { get; set; }
        private Logger log { get; set; }

        public MessageType18()
        {
            this.MessageID = 18;
            this.Description = "Standard class B equipment position report";
            this.TotalNumberOfBits = 168;
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
                //Source ID - UserID
                this.UserID = Convert.ToInt32(getDecimalFromBinary(content, 8, 30));
                //Spare
                this.Spare = Convert.ToByte(getDecimalFromBinary(content, 38, 8));
                //SOG
                this.SOG = Convert.ToInt32(getDecimalFromBinary(content, 46, 10));
                //Position Accuracy
                this.PositionAccuracy = Convert.ToInt32(getDecimalFromBinary(content, 56, 1));
                //Longitude
                this.Longitude = Convert.ToDouble(getDecimalFromBinary(content, 57, 28)) / 600000;
                //Latitude
                this.Latitude = Convert.ToDouble(getDecimalFromBinary(content, 85, 27)) / 600000;
                //COG
                this.COG = Convert.ToDouble(getDecimalFromBinary(content, 112, 12))/10;
                //True Heading
                this.TrueHeading = Convert.ToDouble(getDecimalFromBinary(content, 124, 9));
                //Time stamp
                this.TimeStamp = Convert.ToInt32(getDecimalFromBinary(content, 133, 6));
                //Spare
                this.Spare2 = Convert.ToByte(getDecimalFromBinary(content, 139, 2));
                //Class B unit flag
                this.ClassBUnitFlag = Convert.ToByte(getDecimalFromBinary(content, 141, 1));
                //Class B display flag
                this.ClassBDisplayFlag = Convert.ToByte(getDecimalFromBinary(content, 142, 1));
                //Class B DSC flag
                this.ClassBDSCFlag = Convert.ToByte(getDecimalFromBinary(content, 143, 1));
                //Class B band flag
                this.ClassBBandFlag = Convert.ToByte(getDecimalFromBinary(content, 144, 1));
                //Class B Message 22 flag
                this.ClassBMessage22Flag = Convert.ToByte(getDecimalFromBinary(content, 145, 1));
                //Mode Flag
                this.ModeFlag = Convert.ToByte(getDecimalFromBinary(content, 146, 1));
                //RAIM Flag
                this.RAIM_Flag = Convert.ToByte(getDecimalFromBinary(content, 147, 1));
                //Communication state selector flag
                this.CommunicationStateSelectorFlag = Convert.ToByte(getDecimalFromBinary(content, 148, 1));
                if (this.CommunicationStateSelectorFlag == 0)
                {
                    this.Sotdma = new SOTDMA();
                    this.Sotdma.SyncState = Convert.ToByte(getDecimalFromBinary(content, 149, 2));
                    this.Sotdma.SlotTimeOut = Convert.ToByte(getDecimalFromBinary(content, 151, 3));
                    //this.Sotdma.subMessage.ReceivedStations = Convert.ToString(getStringFromBinary(content,154,))
                }
                else if (this.CommunicationStateSelectorFlag == 1)
                {
                    this.Itdma = new ITDMA();
                    this.Itdma.SyncState = Convert.ToByte(getDecimalFromBinary(content,149, 2));
                    this.Itdma.SlotIncrement = Convert.ToByte(getDecimalFromBinary(content, 151, 13));
                    this.Itdma.NumberOfSlots = Convert.ToByte(getDecimalFromBinary(content, 164, 3));
                    this.Itdma.KeepFlag = Convert.ToByte(getDecimalFromBinary(content, 167, 1));
                }

            }
            catch (Exception ex)
            {
                log.Error(ex, " MessageType18 :: Parser()");
                //throw;
                return null;
            }


            return null;
        }
        #endregion

        #region ToString mesajını ezdik. Methodu sınıfa göre tasarladık.
        public override string ToString()
        {
            string com = "";
            if (this.CommunicationStateSelectorFlag == 0)
                com = this.Sotdma.ToString();
            else if (this.CommunicationStateSelectorFlag == 1)
                com = this.Itdma.ToString();

            return
                "Message ID: " + this.MessageID + "\n" +
                "Repeat Indicator: " + this.RepeatIndicator + "\n" +
                "User ID: " + this.UserID + "\n" +
                "Spare: " + this.Spare + "\n" +
                "SOG: " + this.SOG + "\n" +
                "Position Accuracy: " + this.PositionAccuracy + "\n" +
                "Longitude: " + this.Longitude + "\n" +
                "Latitude: " + this.Latitude + "\n" +
                "COG: " + this.COG + "\n" +
                "True Heading: " + this.TrueHeading + "\n" +
                "Time stamp: " + this.TimeStamp + "\n" +
                "Spare 2 : " + this.Spare2 + "\n" +
                "Class B Unit Flag: " + this.ClassBUnitFlag + "\n" +
                "Class B Display Flag: " + this.ClassBDisplayFlag + "\n" +
                "Class B DSC Flag: " + this.ClassBDSCFlag + "\n" +
                "Class B Band Flag: " + this.ClassBBandFlag + "\n" +
                "Class B Message 22 flag: " + this.ClassBMessage22Flag + "\n" +
                "Mode Flag: " + this.ModeFlag + "\n" +
                "RAIM Flag: " + this.RAIM_Flag + "\n" +
                com;
        
        }
        #endregion
    }
}
