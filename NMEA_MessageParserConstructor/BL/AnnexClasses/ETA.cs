using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA_MessageParserConstructor.BL.AnnexClasses
{
    public class ETA
    {
        private byte Minute { get; set; }
        private byte Hour { get; set; }
        private byte Day { get; set; }
        private byte Month { get; set; }
        private RootMessages root { get; set; }
        private Logger log { get; set; }

        public ETA()
        {
            this.log = LogManager.GetCurrentClassLogger();
            this.root = new RootMessages();
        }

        #region ETA attribute'lerini set ediyoruz.
        public void setValue(string content, int start)
        {
            try
            {
                this.Minute = Convert.ToByte(root.getDecimalFromBinary(content, start, 4));
                this.Hour = Convert.ToByte(root.getDecimalFromBinary(content, start + 4, 5));
                this.Day = Convert.ToByte(root.getDecimalFromBinary(content, start + 9, 5));
                this.Month = Convert.ToByte(root.getDecimalFromBinary(content, start + 14, 6));
            }
            catch (Exception ex)
            {
                log.Error(ex, "ETA :: setValue");
                throw;
            }

        }
        #endregion

        #region Minute attribute set ve get.
        public byte getMinute()
        {
            return this.Minute;
        }
        public void setMinute(byte a)
        {
            this.Minute = a;
        }
        #endregion

        #region Hour attribute set ve get.
        public byte getHour()
        {
            return this.Hour;
        }

        public void setHour(byte a)
        {
            this.Hour = a;
        }
        #endregion

        #region Day attribute set ve get.
        public byte getDay()
        {
            return this.Day;
        }

        public void setDay(byte a)
        {
            this.Day = a;
        }
        #endregion

        #region Month  attribute set ve get.
        public byte getMonth()
        {
            return this.Month;
        }
        public void setMonth(byte a)
        {
            this.Month = a;
        }
        #endregion

        #region ToString methodunu override ediyoruz.
        public override string ToString()
        {
            return
                 "  Month:" + getMonth() +
                 "  Day:" + getDay() +
                 "  Hour:" + getHour() +
                 "  Minute:" + getMinute();            
        }
        #endregion
    }
}
