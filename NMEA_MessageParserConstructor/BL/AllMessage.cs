using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA_MessageParserConstructor.BL
{
    //Seriportlardan gelecek olan mesajların yönetileceği sınıftır.
   public class AllMessage
    {
        private Queue<string> Message;
        private Logger log;
        private static AllMessage SingleAllMessage;

        public AllMessage()
        {
            this.Message = new Queue<string>();
            this.log = LogManager.GetCurrentClassLogger();
        }

        public static AllMessage getObject()
        {
            if (SingleAllMessage == null)
                SingleAllMessage = new AllMessage();

            return SingleAllMessage;
        }

        #region Kuyruğa eleman ekleme
        public bool Enqueue(string messsage)
        {
            try
            {
                this.Message.Enqueue(messsage);
                return true;
            }
            catch (Exception ex)
            {
                log.Error( ex.Message, "AllMessage :: Enqueue()");
                throw;
                //return false;        
            }
            
        }
        #endregion

        #region Kuyruktan eleman çıkarma
        public string Dequeue()
        {        
            try
            {
                if (this.Count() > 0)
                    return this.Message.Dequeue();
                else
                    return null;
               
            }
            catch (Exception ex)
            {
                log.Error(ex, "AllMessage :: Dequeue()");
                throw;
            }
          
        }
        #endregion

        #region Kuyruktaki eleman sayısı
        public int Count()
        {
            try
            {
              return  this.Message.Count();
            }
            catch (Exception ex)
            {

                log.Error(ex, "AllMessage : Count()");
                throw;
            }
        }
        #endregion

        #region Bir mesajın VDM veya VDO olup olmadığını kontrol ediyor.
        public void ControlVdmAndVdo(string message)
        {
            try
            {
                string[] messageParts = message.Split(',');
                string messageTitle = messageParts[0];
                if (messageTitle == "!AIVDM" || messageTitle == "!AIVDO")
                    Message.Enqueue(message);
            }
            catch (Exception ex)
            {
                log.Error(ex, "AllMessage :: ControlVdmAndVdo()");
                throw;
                               
            }
           
        
        }
        #endregion
    }
}
