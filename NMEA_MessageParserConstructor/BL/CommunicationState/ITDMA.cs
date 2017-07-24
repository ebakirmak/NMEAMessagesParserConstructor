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
        public byte SlotIncrement { get; set; }
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
    }
}
