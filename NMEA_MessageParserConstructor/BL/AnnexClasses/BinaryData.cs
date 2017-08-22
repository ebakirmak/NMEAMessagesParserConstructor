using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA_MessageParserConstructor.BL.AnnexClasses
{
    #region BinaryData ' yı saklamak için kullanılıyor.
    public class BinaryData
    {
        private int DAC { get; set; }
        private int FID { get; set; }
        private string Data { get; set; }
        private RootMessages root { get; set; }
        private Logger log { get; set; }

        public BinaryData()
        {
            this.log = LogManager.GetCurrentClassLogger();
            this.root = new RootMessages();
        }

        #region setValue(string content, int start): BinarData attribute'lerini set ediyoruz.
        public void setValue(string content, int start)
        {
            try
            {
                this.DAC = Convert.ToInt32(root.getDecimalFromBinary(content, start, 10));
                this.FID = Convert.ToInt32(root.getDecimalFromBinary(content, start + 10, 6));
                //Application Specific Data - 952 Bit - Taner Bey'e sor.
                this.Data = Convert.ToString(root.getDecimalFromBinary(content, start + 16, 5));
              
            }
            catch (Exception ex)
            {
                log.Error(ex, "BinaryData :: setValue");
                throw;
            }

        }
        #endregion

        #region DAC attribute döndür.
        public int getDAC()
        {
            return this.DAC;
        }
        #endregion

        #region FID attribute döndür.
        public int getFID()
        {
            return this.FID;
        }
        #endregion

        #region Data attribute döndür.
        public string getData()
        {
            return this.Data;
        }
        #endregion

        #region ToString methodunu override ediyoruz.
        public override string ToString()
        {
            return
                 "  DAC" + getDAC() +
                 "  FID" + getFID() +
                 "  Data" + getData();
        }
        #endregion

        #region Attributeları döndürür.
        public  List<Tuple<string, string>> getAttributes()
        {
            List < Tuple < string, string>> _attributes = new List<Tuple<string, string>> {
                  new Tuple<string, string>("DAC ",this.getDAC().ToString()),
                  new Tuple<string, string>("FID ",this.getFID().ToString()),
                  new Tuple<string, string>("Data",this.getData().ToString())
             };          
            return _attributes;
        }
        #endregion



    }
    #endregion
}
