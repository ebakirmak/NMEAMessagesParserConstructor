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
        private int A { get; set; }
        private int B { get; set; }
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

        #region A attribute set ve get.
        public int getA()
        {
            return this.A;
        }

        public void setA(int a)
        {
            this.A = a;
        }
        #endregion

        #region B attribute set ve get.
        public int getB()
        {
            return this.B;
        }

        public void setB(int b)
        {
            this.B = b;
        }
        #endregion

        #region C attribute set ve get.
        public byte getC()
        {
            return this.C;
        }
        public void setC(byte C)
        {
            this.C = C;
        }
        #endregion

        #region D attribute set ve get.
        public byte getD()
        {
            return this.D;
        }

        public void setD(byte d)
        {
            this.D = d;
        }
        #endregion

        #region ToString methodunu override ettik.
        public override string ToString()
        {
            return "  A:" + getA() + 
                   "  B:" + getB() +
                   "  C:" + getC() +
                   "  D:" + getD();
        }
        #endregion

    }
}
