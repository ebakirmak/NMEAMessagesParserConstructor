using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
namespace NMEA_MessageParserConstructor.BL
{
    public class SeriPort
    {
        private SerialPort myPort { get; set; }
        public SeriPort()
        {
          
        }

        #region Seriport bağlantı noktalarını döndürür.
        public string[] getPortNames()
        {           
            return SerialPort.GetPortNames();
        }
        #endregion

        #region Seriport bağlantı noktasını açar.
        public bool Open(string portName)
        {
            try
            {
                myPort = new SerialPort(portName);
                if (myPort.IsOpen == false)
                {
                    myPort.Open();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
          
        }
        #endregion

        #region Seriport bağlantı noktasını kapar.
        public bool Close()
        {
            try
            {
                if (myPort.IsOpen == true)
                {
                    myPort.Close();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                myPort.Close();
                return false;
                throw ex;
            }
        }
        #endregion

        #region
        
        #endregion
    }
}
