using NLog;
using NMEA_MessageParserConstructor.BL.AnnexClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA_MessageParserConstructor.BL.Messages
{
    public class MessageType21 : RootMessages
    {
        private int ID { get; set; }
        private byte TypeOfAidsToNavigation { get; set; }
        private string NameOfAidsToNavigation { get; set; }
        private byte PositionAccuracy { get; set; }
        private double Longitude { get; set; }
        private double Latitude { get; set; }
        private OverallDimension DimensionOfShip { get; set; }
        //Type Of Electronic Position Fixing Device
        private int TOEPFD { get; set; }
        private byte TimeStamp { get; set; }
        private byte OffPositionIndicator { get; set; }
        private string AtoNStatus { get; set; }
        private byte RAIMFlag { get; set; }
        private byte VirtualAtoNFlag { get; set; }
        private byte AssignedModeFlag { get; set; }
        private string NameOfAidToNavigationExtension { get; set; }
        private byte Spare2 { get; set; }
        private Logger log { get; set; }

        public MessageType21()
        {
            this.MessageID = 21;
            this.Description = "Aids to navigation Report";
            this.DimensionOfShip = new OverallDimension();
            this.log = LogManager.GetCurrentClassLogger();

        }

        #region Mesaj yapısında bulunan attributelara, alınan mesajdaki değerleri set ettik.
        public override string[] Parser(string message1,string message2)
        {
            try
            {
                string[] messageParts1 = base.Parser(message1);
                string[] messageParts2 = base.Parser(message2);
                //Context'i oku. Binary yapıda.
                string content = getContentBinary(messageParts1[5], Remove(messageParts1[6]));
                content += getContentBinary(messageParts2[5], Remove(messageParts2[6]));
                //Tüm mesajlarda olan özellikleri burada gir.
                //MessageID
                this.MessageID = Convert.ToByte(getDecimalFromBinary(content, 0, 6));
                //Repeat indicator
                this.RepeatIndicator = Convert.ToByte(getDecimalFromBinary(content, 6, 2));
                //Source MMSI
                this.ID = Convert.ToInt32(getDecimalFromBinary(content, 8, 30));
                //Type of aids to navigation
                this.TypeOfAidsToNavigation = Convert.ToByte(getDecimalFromBinary(content, 38, 5));
                //Name of Aids to Navigation
                this.NameOfAidsToNavigation = Convert.ToString(getStringFromBinary(content, 43, 120));
                //PA
                this.PositionAccuracy = Convert.ToByte(getDecimalFromBinary(content, 163, 1));
                //LON - Dakikaya çevrildi ve 10.000 ile çarpıldı.
                this.Longitude = Convert.ToDouble((getDecimalFromBinary(content, 164, 28))) / 600000;
                //LAT - Dakikaya çevrildi ve 10.000 ile çarpıldı.
                this.Latitude = Convert.ToDouble(getDecimalFromBinary(content, 192, 27)) / 600000;
                //Dimension / reference for position2
                this.DimensionOfShip.setValue(content, 219);
                //Type of electronic position Fixing Device
                this.TOEPFD = Convert.ToByte(getDecimalFromBinary(content, 249, 4));
                //Time stamp
                this.TimeStamp = Convert.ToByte(getDecimalFromBinary(content, 253, 6));
                //Off Position Indicator
                this.OffPositionIndicator = Convert.ToByte(getDecimalFromBinary(content, 259, 1));
                //AtoN status
                this.AtoNStatus = content.Substring(260, 8);
                //RAIM Flag
                this.RAIMFlag = Convert.ToByte(getDecimalFromBinary(content, 268, 1));
                //Virtual AtoN Flag
                this.VirtualAtoNFlag = Convert.ToByte(getDecimalFromBinary(content, 269, 1));
                //Assigned mode Flag
                this.AssignedModeFlag = Convert.ToByte(getDecimalFromBinary(content, 270, 1));
                //Spare
                this.Spare = Convert.ToByte(getDecimalFromBinary(content, 271, 1));
                //Name of Aid-to-Navigation Extension
                this.NameOfAidToNavigationExtension = Convert.ToString(getStringFromBinary(content, 272, 84));
                //Spare 2
                this.Spare2 = Convert.ToByte(getDecimalFromBinary(content, 356, 4));
            }
            catch (Exception ex)
            {
                log.Error(ex, "MessageType21 :: Parser");
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
                "RepeatIndicator" + this.RepeatIndicator + "\n" +
                "User ID / MMSI" + this.ID + "\n" +
                "type of aids to navigation: " + this.TypeOfAidsToNavigation + "\n" +
                "Name of aids to navigation:" + this.NameOfAidsToNavigation + "\n" +
                "PA" + this.PositionAccuracy + "\n" +
                "Lon" + this.Longitude + "\n" +
                "Lat" + this.Latitude + "\n" +
                "Dimension reference for position:\n\t A: " + this.DimensionOfShip.getA() +
                                                  "\n\tB: " + this.DimensionOfShip.getB() +
                                                  "\n\tC: " + this.DimensionOfShip.getC() +
                                                  "\n\tD: " + this.DimensionOfShip.getD() + "\n" +
                "Type of electronic position fixing device: " + this.TOEPFD + "\n" +
                "Time stamp: " + this.TimeStamp + "\n" +
                "Off position indicator: " + this.OffPositionIndicator + "\n" +
                "AtoN Status: " + this.AtoNStatus + "\n" +
                "RAIM Flag: " + this.RAIMFlag + "\n " +
                "Virtual AtoN Flag: " + this.VirtualAtoNFlag + "\n" +
                "Assigned mode Flag: " + this.AssignedModeFlag + "\n" +
                "Spare: " + this.Spare + "\n" +
                "Name of Aid to Navigation Extension: " + this.NameOfAidToNavigationExtension + "\n" +
                "Spare: " + this.Spare2 + "\n";


               
        }
        #endregion
    }
}
