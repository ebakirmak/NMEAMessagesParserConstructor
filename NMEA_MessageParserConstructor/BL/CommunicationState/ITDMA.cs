using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA_MessageParserConstructor.BL.CommunicationState
{
    public class ITDMA
    {
        public byte SyncState { get; set; }
        public int SlotIncrement { get; set; }
        public byte NumberOfSlots { get; set; }
        public byte KeepFlag { get; set; }
        private RootMessages root;
        private Logger log;
        public ITDMA()
        {
            this.root = new RootMessages();
            this.log = LogManager.GetCurrentClassLogger();
        }

        #region ToString() methodunu override ettik.
        public override string ToString()
        {
            return
                "Sync State: " + this.SyncState +
                "\nSlot Increment: " + this.SlotIncrement +
                "\nNumber Of Slots: " + this.NumberOfSlots +
                "\nKeep Flag: " + this.KeepFlag;
        }
        #endregion

        #region Attributeları döndürür.
        public List<Tuple<string, string>> getAttributes()
        {
            List<Tuple<string, string>> _listAttribute = new List<Tuple<string, string>>
            {
                new Tuple<string, string>("Sync State",""),
                new Tuple<string, string>("Slot Increment",""),
                new Tuple<string, string>("Number Of Slots",""),
                new Tuple<string, string>("Keep Flag","")
            };
            return _listAttribute;
        }
        #endregion


        #region getAttributesValues(): Attributeları ve değerlerini  döndürür.
        public List<Tuple<string, string>> getAttributesAndValues()
        {
            List<Tuple<string, string>> _listAttribute = new List<Tuple<string, string>>
            {
                new Tuple<string, string>("Sync State",this.SyncState.ToString()),
                new Tuple<string, string>("Slot Increment",this.SlotIncrement.ToString()),
                new Tuple<string, string>("Number Of Slots",this.NumberOfSlots.ToString()),
                new Tuple<string, string>("Keep Flag",this.KeepFlag.ToString()),
            };
            return _listAttribute;
        }
        #endregion

        #region setValue(string content, int start): Attribute'lerin değerlerini set ediyoruz.
        public void setValue(string content, int start)
        {
            try
            {
                this.SyncState = Convert.ToByte(root.getDecimalFromBinary(content, start, 2));
                this.SlotIncrement = Convert.ToByte(root.getDecimalFromBinary(content, start + 2, 13));
                //Application Specific Data - 952 Bit - Taner Bey'e sor.
                this.NumberOfSlots = Convert.ToByte(root.getDecimalFromBinary(content, start + 15, 3));
                this.KeepFlag = Convert.ToByte(root.getDecimalFromBinary(content, start + 18, 1));

            }
            catch (Exception ex)
            {
                log.Error(ex, "ITDMA :: setValue");
                throw;
            }

        }
        #endregion

        #region getValue(): 
        public string getBinaryToITDMAValue()
        {
            try
            {
                return root.setBinaryToDecimal(this.SyncState).PadLeft(2, '0') +
                root.setBinaryToDecimal(this.SlotIncrement).PadLeft(13, '0') +
                 root.setBinaryToDecimal(this.NumberOfSlots).PadLeft(3, '0') +
                 root.setBinaryToDecimal(this.KeepFlag).PadLeft(1, '0');
            }
            catch (Exception ex)
            {
                log.Error(ex, "SOTDMA :: getValue");
                throw;
            }


        }
        #endregion

        #region setValue(): 
        public string setValue(List<string> _listMessage,int startIndex)
        {
            string errorMessage = "";
            if (root.ControlSyncState(Convert.ToByte(_listMessage[startIndex])))
                this.SyncState = Convert.ToByte(_listMessage[startIndex++]);
            else
                errorMessage += "\nSync State değerini kontrol ediniz.";
            ////////////////////////////////////////////////////////////
            if (root.ControlSlotIncrement(Convert.ToByte(_listMessage[startIndex])))
                this.SlotIncrement = Convert.ToByte(_listMessage[startIndex++]);
            else
                errorMessage += "\nSlot Increment değerini kontrol ediniz.";
            /////////////////////////////////////////////////////////////
            if (root.ControlNumberOfSlots(Convert.ToByte(_listMessage[startIndex])))
                this.NumberOfSlots = Convert.ToByte(_listMessage[startIndex++]);
            else
                errorMessage += "\nNumber of slots değerini kontrol ediniz.";
            //////////////////////////////////////////////////////////////////
            if (root.ControlKeepFlag(Convert.ToByte(_listMessage[startIndex])))
                this.KeepFlag = Convert.ToByte(_listMessage[startIndex++]);
            else
                errorMessage += "\nKeep Flag değerini kontrol ediniz.";

            return errorMessage;
        }
        #endregion


    }
}
