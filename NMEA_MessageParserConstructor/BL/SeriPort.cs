using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using NLog;

namespace NMEA_MessageParserConstructor.BL
{
    //Seriport girişlerinin yönetileceği kısım.
    public class SeriPort
    {
        private byte State { get; set; }
        private SerialPort myPort { get; set; }
        private Logger log{ get; set; }
        private AllMessage allMessage;
        private RootMessages rootMessages;


        public SeriPort(string portName,int BaundRate, byte bits )
        {
            this.State = 1;
            this.myPort = new SerialPort(portName,BaundRate,Parity.None, bits, StopBits.One);
            this.log = LogManager.GetCurrentClassLogger();
            this.allMessage = AllMessage.getObject();
            this.rootMessages = new RootMessages();
        }

        #region Seriport bağlantı noktalarını döndürür.
        public static string[] getPortNames()
        {           
            return SerialPort.GetPortNames();
        }
        #endregion

        #region Seriport bağlantı noktasını açar.
        private bool Open()
        {
            try
            {
             
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
                log.Error(ex, "SeriPort :: Open");
                return false;
                throw ex;
            }
          
        }
        #endregion

        #region Seriport bağlantı noktasını kapar.
        private bool Close()
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
                log.Error(ex, "SeriPort :: Close");
                myPort.Close();
                return false;
                throw ex;
            }
        }
        #endregion

        #region Tüm satırı okuma
        public void ReadMessage()
        {
            try
            {
                this.Open();
                string message = "";

                while (State == 1)
                {
                    if (myPort.BytesToRead > 0)
                    {
                        message = myPort.ReadLine();
                        if(this.rootMessages.CheckMessage(message)==true)
                            allMessage.Enqueue(message);
                    }
                }
                this.Close();
            }
            catch (Exception ex)
            {
                log.Error(ex, " SeriPort :: ReadMessage");
                throw;
            }
           
         
        }
        #endregion

        #region Mesajların okunmasını bitirir.
        public void DontReadMessage()
        {
            this.State = 0;
        }
        #endregion

   
    }
}
