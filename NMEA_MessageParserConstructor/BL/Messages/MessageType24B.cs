using NMEA_MessageParserConstructor.BL.AnnexClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA_MessageParserConstructor.BL.Messages
{
    public class MessageType24B:MessageType24
    {
        //Type of Ship And Cargo Type
        private byte TypeOfShipAndCargoType { get; set; }
        private string VendorID { get; set; }
        private string CallSign { get; set; }
        private OverallDimension DimensionOfShip { get; set; }
        //Type Of Electronic Position Fixing Device
        private byte TOEPFD { get; set; }

        public MessageType24B()
        {
            this.DimensionOfShip = new OverallDimension();
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
                this.UserID = Convert.ToInt32(getDecimalFromBinary(content, 8, 30));
                //Part Number
                this.PartNumber = Convert.ToByte(getDecimalFromBinary(content, 38, 2));
                //Type Of Ship And Cargo Type
                this.TypeOfShipAndCargoType = Convert.ToByte(getDecimalFromBinary(content, 40, 8));
                //Vendor ID
                this.VendorID = Convert.ToString(getStringFromBinary(content, 48, 42));
                //Call Sign
                this.CallSign = Convert.ToString(getStringFromBinary(content, 90, 42));
                //Dimension of ship/reference for position
                this.DimensionOfShip.setValue(content, 132);
                //Type of Electronic position fixing device
                this.TOEPFD = Convert.ToByte(getDecimalFromBinary(content, 162, 4));
                //Spare
                this.Spare = Convert.ToByte(getDecimalFromBinary(content, 166, 2));

            }
            catch (Exception ex)
            {
                log.Error(ex, "MessageType24B :: Parser");
                //throw;
            }

            return null;
        }
        #endregion

        #region ToString() methodunu override ettik.
        public override string ToString()
        {
            string message = base.ToString();
            return message +=
                "Type of ship and cargo type: " + this.TypeOfShipAndCargoType + "\n" +
                "Vendor ID: " + this.VendorID + "\n" +
                "Call Sign: " + this.CallSign + "\n" +
                "Dimension of ship:" +
                                " A: " + this.DimensionOfShip.getA() +
                                " B: " + this.DimensionOfShip.getB() +
                                " C: " + this.DimensionOfShip.getC() +
                                " D: " + this.DimensionOfShip.getD() + "\n" +
                 "Type of Electronic position fixing device: " + this.TOEPFD + "\n" +
                 "Spare: " + this.Spare;

               


        }
        #endregion
    }
}
