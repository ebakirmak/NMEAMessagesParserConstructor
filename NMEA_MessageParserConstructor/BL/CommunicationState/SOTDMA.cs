using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA_MessageParserConstructor.BL.CommunicationState
{
    #region SOTDMA iletişim katmanı (3.3.7.2.1 - Table 18)
    public class SOTDMA
    {
        public SOTDMA()
        {
            this.subMessage = new SubMessage();
            this.root = new RootMessages();
            this.log = LogManager.GetCurrentClassLogger();
        }
        public byte SyncState { get; set; }
        public byte SlotTimeOut { get; set; }
        public SubMessage subMessage { get; set; }
        private RootMessages root;
        private Logger log;
        #region İletişim katmanının alt sınıfı (3.3.7.2.1 - Table 19)
        public class SubMessage
        {
            public byte ReceivedStations { get; set; }
            public byte SlotNumber { get; set; }
            public byte UTCHour { get; set; }
            public byte UTCMinute { get; set; }
            public int SlotOffset { get; set; }

        }
        #endregion



        #region ToString(): methodunu override ettik.
        public override string ToString()
        {
            return
                "Sync State: " + this.SyncState +
                "Slot Time Out: " + this.SlotTimeOut +
                "Receiver Stations: " + this.subMessage.ReceivedStations +
                "Slot Number: " + this.subMessage.SlotNumber +
                "UTC Hour: " + this.subMessage.UTCHour +
                "UTC Minute: " + this.subMessage.UTCMinute +
                "Slot Offset" + this.subMessage.SlotOffset;
        }
        #endregion

        #region Parser methodları

        #region setValue(string content, int start): Parse edilen mesajdaki Attribute'lerin değerlerini set ediyoruz.
        public void setValue(string content, int start)
        {
            try
            {
                this.SyncState = Convert.ToByte(root.getDecimalFromBinary(content, start, 2));
                this.SlotTimeOut = Convert.ToByte(root.getDecimalFromBinary(content, start + 2, 3));
                this.subMessage.SlotOffset = Convert.ToInt32(root.getDecimalFromBinary(content, start + 5, 14));
                //Application Specific Data - 952 Bit - Taner Bey'e sor.
                //this.subMessage.UTCMinute = Convert.ToByte(root.getDecimalFromBinary(content, start + 7, 6));
                //this.subMessage.UTCHour = Convert.ToByte(root.getDecimalFromBinary(content, start + 13, 5));

            }
            catch (Exception ex)
            {
                log.Error(ex, "SOTDMA :: setValue");
                throw;
            }

        }
        #endregion

        #region getAttributesValues(): Attributeları ve değerlerini  döndürür.
        public List<Tuple<string, string>> getAttributesAndValues()
        {
            List<Tuple<string, string>> _listAttribute = new List<Tuple<string, string>>
            {
                new Tuple<string, string>("Sync State",this.SyncState.ToString()),
                new Tuple<string, string>("Slot Time Out",this.SlotTimeOut.ToString()),              
                //new Tuple<string, string>("Received Stations",this.subMessage.ReceivedStations.ToString()),
                //new Tuple<string, string>("Slot Number",this.subMessage.SlotNumber.ToString()),
                //new Tuple<string, string>("UTC Hour",this.subMessage.UTCHour.ToString()),
                //new Tuple<string, string>("UTC Minute",this.subMessage.UTCMinute.ToString()),
                new Tuple<string, string>("Slot Offset",this.subMessage.SlotOffset.ToString())
            };
            return _listAttribute;
        }
        #endregion


        #endregion

        #region Constructor methodları

        #region getBinaryToSOTDMAValue(): SOTDMA Decimal değerleri binary yapısına dönüştürüyor.
        public string getBinaryToSOTDMAValue()
        {
            try
            {
                return root.setBinaryToDecimal(this.SyncState).PadLeft(2, '0') +
                root.setBinaryToDecimal(this.SlotTimeOut).PadLeft(3, '0') +
                root.setBinaryToDecimal(this.subMessage.SlotOffset).PadLeft(14, '0');
            }
            catch (Exception ex)
            {
                log.Error(ex, "SOTDMA :: getValue");
                throw;
            }


        }
        #endregion

        #region getAttributes(): Attributeları döndürür.
        public List<Tuple<string, string>> getAttributes()
        {
            List<Tuple<string, string>> _listAttribute = new List<Tuple<string, string>>
            {
                new Tuple<string, string>("Sync State","0"),
                new Tuple<string, string>("Slot Time Out","0"),
                //new Tuple<string, string>("Received Stations",""),
                ////new Tuple<string, string>("Slot Number",""),
                //new Tuple<string, string>("UTC Hour",""),
                //new Tuple<string, string>("UTC Minute",""),
                new Tuple<string, string>("Slot Offset","0")
            };
            return _listAttribute;
        }
        #endregion

        public string setValue(List<string> _listMessage,byte startIndex)
        {
            string errorMessage = "";

            if (root.ControlSyncState(Convert.ToByte(_listMessage[startIndex])))
                this.SyncState = Convert.ToByte(_listMessage[startIndex++]);
            else
                errorMessage += "\nSync State değerini kontrol ediniz.";
            ////////////////////////////////////////////////////////////
            if (root.ControlSlotTimeOut(Convert.ToByte(_listMessage[startIndex])))
                this.SlotTimeOut = Convert.ToByte(_listMessage[startIndex++]);
            else
                errorMessage += "\nSlot Time Out değerini kontrol ediniz.";
            /////////////////////////////////////////////////////////////
            this.subMessage.SlotOffset = Convert.ToInt32(_listMessage[startIndex]);

            ////////////////////////////////////////////////////////////////
            //if (ControlUTCHour(Convert.ToByte(_listMessage[24])))
            //    this.Sotdma.subMessage.UTCHour = Convert.ToByte(_listMessage[24]);
            //else
            //    errorMessage += "\nUTC Hour değerini kontrol ediniz.";
            ////////////////////////////////////////////////////////////////////
            //if (ControlUTCMinute(Convert.ToByte(_listMessage[25])))
            //    this.Sotdma.subMessage.UTCHour = Convert.ToByte(_listMessage[25]);
            //else
            //    errorMessage += "\nUTC Minute değerini kontrol ediniz.";
            return errorMessage;
        }

        #endregion
    }
    #endregion
}
