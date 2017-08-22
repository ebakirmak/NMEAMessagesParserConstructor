using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA_MessageParserConstructor.BL.AnnexClasses
{
    public class CorrectionData
    {
        private int MessageType { get; set; }
        private int StationID { get; set; }
        private int ZCount { get; set; }
        private int SequenceNumber { get; set; }
        private int N { get; set; }
        private int Health { get; set; }
        private string DataWord { get; set; }
        private int NumberOfBits { get; set; }

        private RootMessages root { get; set; }
        private Logger log { get; set; }

        public CorrectionData()
        {
            this.log = LogManager.GetCurrentClassLogger();
            this.root = new RootMessages();
        }

       

        #region get Methodları
        public int getMessageType() {
            return this.MessageType;
        }

        public int getStationID() {
            return this.StationID;
        }
        public int getZCount() {
            return this.ZCount;
        }
        public int getSequenceNumber() {
            return this.SequenceNumber;
        }
        public int getN() {
            return this.N;
        }
        public int getHealth() {
            return this.Health;
        }
        public string getDataWord() {
            return this.DataWord;
        }
        #endregion

        #region ToString methodunu override ettik.
        public override string ToString()
        {
            return "Data Message Type: " + this.getMessageType() + "\n" +
                "Data Station ID: " + this.getStationID() + "\n" +
                "Data ZCount: " + this.getZCount() + "\n" +
                "Data Sequence Number: " + this.getSequenceNumber() + "\n" +
                "Data N: " + this.getN() + "\n" +
                "Data Health: " + this.getHealth() + "\n" +
                "DGNSS data word: " + this.getDataWord() + "\n";
        }
        #endregion



        #region Parser

            #region getAttributesAndValues(): Attributeları ve value'lerini döndürür.
        public List<Tuple<string, string>> getAttributesAndValues()
        {
            List<Tuple<string, string>> _attributes = new List<Tuple<string, string>> {
                  new Tuple<string, string>("Broadcast Message Type ",this.getMessageType().ToString()),
                  new Tuple<string, string>("Station ID ",this.getStationID().ToString()),
                  new Tuple<string, string>("Z Count",this.getZCount().ToString()),
                  new Tuple<string, string>("Sequence Number",this.getSequenceNumber().ToString()),
                  new Tuple<string, string>("N",this.getN().ToString()),
                  new Tuple<string, string>("Health",this.getHealth().ToString()),
                  new Tuple<string, string>("Data Word",this.getDataWord().ToString()),
             };
            return _attributes;
        }
        #endregion

            #region setValues(): Correction Data attribute'lerini set ediyoruz.
        public void setValues(string content, int start)
        {
            try
            {
                this.MessageType = Convert.ToInt32(root.getDecimalFromBinary(content, start, 6));
                this.StationID = Convert.ToInt32(root.getDecimalFromBinary(content, start + 6, 10));
                this.ZCount = Convert.ToInt32(root.getDecimalFromBinary(content, start + 16, 13));
                this.SequenceNumber = Convert.ToInt32(root.getDecimalFromBinary(content, start + 29, 3));
                this.N = Convert.ToInt32(root.getDecimalFromBinary(content, start + 32, 5));
                this.Health = Convert.ToInt32(root.getDecimalFromBinary(content, start + 37, 3));
                this.DataWord = Convert.ToString(root.getStringFromBinary(content, start + 40, content.Length - (start + 40)));

            }
            catch (Exception ex)
            {
                log.Error(ex, "CorrectionDAta :: setValue");
            }

        }
        #endregion

        #endregion

        #region Constructor
        #region getAttributes(): Attributeları  döndürür.
        public List<Tuple<string, string>> getAttributes()
        {
            List<Tuple<string, string>> _attributes = new List<Tuple<string, string>> {
                  new Tuple<string, string>("Broadcast Message Type",""),
                  new Tuple<string, string>("Station ID",""),
                  new Tuple<string, string>("Z Count",""),
                  new Tuple<string, string>("Sequence Number",""),
                  new Tuple<string, string>("N",""),
                  new Tuple<string, string>("Health",""),
                  new Tuple<string, string>("Data Word",""),
             };
            return _attributes;
        }
        #endregion
        #endregion


    }
}
