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
        public List<Tuple<string, string>> getAttributea()
        {
            List<Tuple<string, string>> _listAttribute = new List<Tuple<string, string>>
            {
                new Tuple<string, string>("Sync State",this.SyncState.ToString()),
                new Tuple<string, string>("Slot Increment",this.SlotIncrement.ToString()),
                new Tuple<string, string>("Number Of Slots",this.NumberOfSlots.ToString()),
                new Tuple<string, string>("Keep Flag",this.KeepFlag.ToString())
            };
            return _listAttribute;
        }
        #endregion
    }
}
