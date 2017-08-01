using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NMEA_MessageParserConstructor.BL.Messages;
using NLog;

namespace NMEA_MessageParserConstructor.BL.AnnexClasses
{
    public class OverallDimension
    {
        private byte A { get; set; }
        private byte B { get; set; }
        private byte C { get; set; }
        private byte D { get; set; }
        private Logger log { get; set; }
        private RootMessages root { get; set; }
        public OverallDimension()
        {
            this.log = LogManager.GetCurrentClassLogger();
            this.root = new RootMessages();
        }
        

        #region Overall Dimension attribute'lerini set ediyoruz.
        public void setValue(string content, int start)
        {
            try
            {
                this.A = Convert.ToByte(root.getDecimalFromBinary(content, start, 9));
                this.B = Convert.ToByte(root.getDecimalFromBinary(content, start + 9, 9));
                this.C = Convert.ToByte(root.getDecimalFromBinary(content, start + 18, 6));
                this.D = Convert.ToByte(root.getDecimalFromBinary(content, start + 24, 6));
            }
            catch (Exception ex)
            {
                log.Error(ex, "OverallDimension :: setValue");
                throw;
            }
         
        }
        #endregion

        #region A attribute döndür.
        public byte getA()
        {
            return this.A;
        }
        #endregion

        #region B attribute döndür.
        public byte getB()
        {
            return this.B;
        }
        #endregion

        #region C attribute döndür.
        public byte getC()
        {
            return this.C;
        }
        #endregion

        #region D attribute döndür.
        public byte getD()
        {
            return this.D;
        }
        #endregion
    }
}
