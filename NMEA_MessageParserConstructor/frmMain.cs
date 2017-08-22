using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NMEA_MessageParserConstructor.BL.Messages;
using NMEA_MessageParserConstructor.BL;
using System.Threading;
using NLog;

namespace NMEA_MessageParserConstructor
{
    public partial class frmMain : Form
    {
        private int sentencePartCount;
        private string VDM1;
        private string VDM2;
        private RootMessages root;        
        private AllMessage allMessage;
        private  static SeriPort sp;
        //Oluşan hataları loglamamıza yarayan NLOG kütüphanesi
        private Logger log;
        //Seriport'tan gelen mesajları sürekli okumamıza yarıyor.
        Thread threadRead;

        public frmMain()
        {   
            InitializeComponent();
         
            this.root =  new RootMessages();
            this.allMessage = AllMessage.getObject();
            //log nesnemizi oluşturduk.
            this.log = LogManager.GetCurrentClassLogger();
        }

        #region Form nesneleri ve olayları ile ilgili işlemler

        #region frmMain_Load
        private void frmMain_Load(object sender, EventArgs e)
        {
            cmbPortName.Items.AddRange( SeriPort.getPortNames());
            lstMessages.Items.Add("!AIVDM,1,1,1,B,8>h8nkP0Glr=<hFI0D6??wvlFR06EuOwgwl?wnSwe7wvlOw?sAwwnSGmwvh0,0*17");
            //maritec message 4
            lstMessages.Items.Add("!AIVDM,1,1,,A,400TcdiuiT7VDR>3nIfr6>i00000,0*78");
            tmrMessage.Interval = 500;
            tmrMessage.Start();     
        }
        #endregion

        #region frmMain_FormClosing: Program kapatılırken seriporttan mesaj okuma işlemi bitirilir.
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sp != null && sp.isOpen())
                sp.DontReadMessage();
        }
        #endregion

        #region tmrMessage_Tick: Belli bir süre aralıklarla gelen mesajlar ekranda gösterilecek.
        private void tmrMessage_Tick(object sender, EventArgs e)
        {
            if (this.allMessage.Count() > 0)
                lstMessages.Items.Add(this.allMessage.Dequeue());
        }
        #endregion

        #region btnClear_Click: listbox temizlenir.
        private void btnClear_Click(object sender, EventArgs e)
        {
            lstMessages.Items.Clear();
        }
        #endregion

        #endregion

        #region Port İşlemleri

        #region btnPort_Click: Port açılır veya kapanır.
        private void btnPort_Click(object sender, EventArgs e)
        {
            PortOpenAndClose();
        }
        #endregion

        #region PortOpenAndClose():  Port açılır, mesajlar okunmaya başlanır. Port kapatılır, mesajlar durdurulur. Thread kullanılıyor.
        public void PortOpenAndClose()
        {
            if (sp != null)
            {
                if (sp.isOpen())
                {
                    sp.DontReadMessage();
                    btnPort.Text = "Port Aç";
                }
                else
                {
                    threadRead = new Thread(ReadMessage);
                    threadRead.Start();
                    btnPort.Text = "Port Kapa";
                }
            }
        }
        #endregion

        #region ReadMessage(): seriport sınıfı ile gelen mesajları sürekli okuma
        static void ReadMessage()
        {
            sp.ReadMessage();

        }
        #endregion

        #region cmbPortName_SelectedIndexChanged: Port nesnesi oluşturulur.
        private void cmbPortName_SelectedIndexChanged(object sender, EventArgs e)
        {
            sp = new SeriPort(cmbPortName.Text, 38400, 8);
        }
        #endregion


        #endregion

        #region Mesaj Parse/Ayrıştırma Fonksiyonları

        #region lstMessages_DoubleClick: Seçilen mesaj parse edilmek üzere Run()'a gönderilir.
        private void lstMessages_DoubleClick(object sender, EventArgs e)
        {
            if (lstMessages.SelectedItem != null)
            {
                VDM1 = lstMessages.SelectedItem.ToString();
                if (getSentenceCount() == 1)
                    Run();
                else
                {
                    int currenIndex = (lstMessages.SelectedIndex);
                    VDM2 = lstMessages.Items[currenIndex + 1].ToString();
                    Run();
                }
            }
        }
        #endregion

        #region Run(): Mesaj alınır. Mesaj ID bulunur. Mesaj ID'sine göre parse edilir ve Ekranda gösterilir.
        private void Run()
        {
            ////Datagridview işlemleri
            //dgwMessages.ReadOnly = true;

            try
            {
                //Mesaj cümle sayısı döndürülüyor.
                sentencePartCount = getSentenceCount();
                //Mesaj ID döndürülüyor.
                byte messageID = root.getMessageID(VDM1);
                RootMessages mesaj;
                if (sentencePartCount == 1)
                {
                    #region return edilen Message ID'sine göre ilgili sınıfta işlem yapılacak.

                    if (messageID == 1)
                        mesaj = new MessageType1();
                    else if (messageID == 2)
                        mesaj = new MessageType2();
                    else if (messageID == 3)
                        mesaj = new MessageType3();
                    else if (messageID == 4)
                        mesaj = new MessageType4();
                    else if (messageID == 6)
                        mesaj = new MessageType6();
                    else if (messageID == 7)
                        mesaj = new MessageType7();
                    else if (messageID == 8)
                        mesaj = new MessageType8();
                    else if (messageID == 9)
                        mesaj = new MessageType9();
                    else if (messageID == 10)
                        mesaj = new MessageType10();
                    else if (messageID == 11)
                        mesaj = new MessageType11();
                    else if (messageID == 12)
                        mesaj = new MessageType12();
                    else if (messageID == 13)
                        mesaj = new MessageType13();
                    else if (messageID == 14)
                        mesaj = new MessageType14();
                    else if (messageID == 15)
                        mesaj = new MessageType15();
                    else if (messageID == 16)
                        mesaj = new MessageType16();
                    else if (messageID == 18)
                        mesaj = new MessageType18();
                    else if (messageID == 19)
                        mesaj = new MessageType19();
                    else if (messageID == 20)
                        mesaj = new MessageType20();
                    else if (messageID == 22)
                        mesaj = new MessageType22();
                    else if (messageID == 23)
                        mesaj = new MessageType23();
                    else if (messageID == 24)
                    {
                        MessageType24 mesaj24 = new MessageType24();
                        mesaj24.setPartNumber(VDM1);
                        if (mesaj24.getPartNumber() == 0)
                            mesaj = new MessageType24A();
                        else
                            mesaj = new MessageType24B();
                    }
                    else if (messageID == 25)
                        mesaj = new MessageType25();
                    //MessageBox.Show("Mesaj 26 düzelt");   
                    else if (messageID == 26)
                        mesaj = new MessageType26();
                    else if (messageID == 27)
                        mesaj = new MessageType27();
                    else
                    {
                        MessageBox.Show("MESAJ PARSE EDİLEMEDİ.", "HATALI MESAJ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        mesaj = null;
                    }
                    #endregion
                    //Mesaj parse edilecek
                    mesaj.Parser(VDM1);
                    // ve datagridview'e eklenecek.
                    ShowMessageValue(mesaj);
                }
                else if (sentencePartCount == 2 && messageID != 0)
                {

                    #region return edilen Message ID'sine göre ilgili sınıfta işlem yapılacak.
                    if (messageID == 5)
                        mesaj = new MessageType5();
                    else if (messageID == 12)
                        mesaj = new MessageType12();
                    else if (messageID == 17)
                        mesaj = new MessageType17();
                    else if (messageID == 21)
                        mesaj = new MessageType21();
                    else
                        mesaj = null;
                    #endregion
                    //Mesaj parse edilecek 
                    mesaj.Parser(VDM1, VDM2);
                    //ve datagridview'e eklenecek.
                    ShowMessageValue(mesaj);
                }

            }
            catch (Exception ex)
            {
                log.Error(ex, "frmMain :: Run");
                throw;
            }

        }
        #endregion

        #region getSentenceCount(): Mesajın kaç cümleden oluştuğunu gösteriyor.
        private byte getSentenceCount()
        {
            //Mesajın kaç parçadan oluştuğuna bakılıyor.
            if (Convert.ToByte(VDM1.Split(',')[2]) == 1)
                return root.getSentenceCount(VDM1);
            else
                return 0;

        }


        #endregion

        #region ShowMessageValue(): İlgili mesajın bilgilerini Datagridview nesnesine yazdırıyoruz.
        private void ShowMessageValue(RootMessages message)
        {
            dgwMessages.Rows.Clear();
            dgwMessages.Refresh();
            foreach (var item in message.getAttributesAndValues())
            {
                DataGridViewRow row = (DataGridViewRow)dgwMessages.Rows[0].Clone();
                row.Cells[0].Value = item.Item1;
                row.Cells[1].Value = item.Item2;
                dgwMessages.Rows.Add(row);
            }

        }
        #endregion

        #endregion

        #region Mesaj Constructor/Oluşturma Fonksiyonları.

        #region cmbMessageType_SelectedIndexChanged:  Message ID'sine göre ilgili sınıfta mesaj constructor/oluşturma işlemi için hazırlıklar yapılacak.
        private void cmbMessageType_SelectedIndexChanged(object sender, EventArgs e)
        {

            RootMessages messageObject;

            byte messageID = Convert.ToByte(cmbMessageType.SelectedItem.ToString().Split(' ')[2]);

            #region Mesaj ID göre MesajType[MesajID] nesnesi oluşturuluyor.
            if (messageID == 1)
                messageObject = new MessageType1();
            else if (messageID == 2)
                messageObject = new MessageType2();
            else if (messageID == 3)
                messageObject = new MessageType3();
            else if (messageID == 4)
                messageObject = new MessageType4();
            else if (messageID == 5)
                messageObject = new MessageType5();
            else if (messageID == 6)
                messageObject = new MessageType6();
            else if (messageID == 7)
                messageObject = new MessageType7();
            else if (messageID == 8)
                messageObject = new MessageType8();
            else if (messageID == 9)
                messageObject = new MessageType9();
            else if (messageID == 10)
                messageObject = new MessageType10();
            else if (messageID == 11)
                messageObject = new MessageType11();
            else if (messageID == 12)
                messageObject = new MessageType12();
            else if (messageID == 13)
                messageObject = new MessageType13();
            else if (messageID == 14)
                messageObject = new MessageType14();
            else if (messageID == 15)
                messageObject = new MessageType15();
            else if (messageID == 16)
                messageObject = new MessageType16();
            else if (messageID == 17)
                messageObject = new MessageType17();
            else if (messageID == 18)
                messageObject = new MessageType18();
            else if (messageID == 19)
                messageObject = new MessageType19();
            else if (messageID == 20)
                messageObject = new MessageType20();
            else if (messageID == 22)
                messageObject = new MessageType22();
            else if (messageID == 23)
                messageObject = new MessageType23();
            else if (messageID == 24)
            {
                string type = cmbMessageType.SelectedItem.ToString().Split(' ')[3];
              
                if (type == "A")
                    messageObject = new MessageType24A();
                else
                    messageObject = new MessageType24B();
            }
            else if (messageID == 25)
                messageObject = new MessageType25();
            //MessageBox.Show("Mesaj 26 düzelt");   
            else if (messageID == 26)
                messageObject = new MessageType26();
            else if (messageID == 27)
                messageObject = new MessageType27();
            else
            {
                MessageBox.Show("MESAJ PARSE EDİLEMEDİ.", "HATALI MESAJ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                messageObject = null;
            }
            #endregion

            AddMessagesRows(messageObject);

        }
        #endregion

        #region AddMessagesRows Fonk: Mesajın tipine göre attribute'lerini ve value'lerini datagridview ekler.
        private void AddMessagesRows(RootMessages message)
        {
            if (message != null)
            {
                dgwMessages.ReadOnly = false;
                dgwMessages.Rows.Clear();
                dgwMessages.Refresh();
                foreach (var item in message.getAttributes())
                {
                    DataGridViewRow row = (DataGridViewRow)dgwMessages.Rows[0].Clone();
                    row.Cells[0].Value = item.Item1;
                    row.Cells[1].Value = item.Item2;
                    dgwMessages.Rows.Add(row);

                }
            }
        }
        #endregion

        #region btnConstructorMessage_Click:
        private void btnConstructorMessage_Click(object sender, EventArgs e)
        {
            #region return edilen Message ID'sine göre ilgili sınıfta işlem yapılacak.
            RootMessages messageObject;
            byte messageID = Convert.ToByte(cmbMessageType.SelectedItem.ToString().Split(' ')[2]);

            if (messageID == 1)
                messageObject = new MessageType1();
            else if (messageID == 2)
                messageObject = new MessageType2();
            else if (messageID == 3)
                messageObject = new MessageType3();
            else if (messageID == 4)
                messageObject = new MessageType4();
            else if (messageID == 5)
                messageObject = new MessageType5();
            else if (messageID == 6)
                messageObject = new MessageType6();
            else if (messageID == 7)
                messageObject = new MessageType7();
            else if (messageID == 8)
                messageObject = new MessageType8();
            else if (messageID == 9)
                messageObject = new MessageType9();
            else if (messageID == 10)
                messageObject = new MessageType10();
            else if (messageID == 11)
                messageObject = new MessageType11();
            else if (messageID == 12)
                messageObject = new MessageType12();
            else if (messageID == 13)
                messageObject = new MessageType13();
            else if (messageID == 14)
                messageObject = new MessageType14();
            else if (messageID == 15)
                messageObject = new MessageType15();
            else if (messageID == 16)
                messageObject = new MessageType16();
            else if (messageID == 18)
                messageObject = new MessageType18();
            else if (messageID == 19)
                messageObject = new MessageType19();
            else if (messageID == 20)
                messageObject = new MessageType20();
            else if (messageID == 22)
                messageObject = new MessageType22();
            else if (messageID == 23)
                messageObject = new MessageType23();
            else if (messageID == 24)
            {
                MessageType24 mesaj24 = new MessageType24();
                mesaj24.setPartNumber(VDM1);
                if (mesaj24.getPartNumber() == 0)
                    messageObject = new MessageType24A();
                else
                    messageObject = new MessageType24B();
            }
            else if (messageID == 25)
                messageObject = new MessageType25();
            //MessageBox.Show("Mesaj 26 düzelt");   
            else if (messageID == 26)
                messageObject = new MessageType26();
            else if (messageID == 27)
                messageObject = new MessageType27();
            else
            {
                MessageBox.Show("MESAJ PARSE EDİLEMEDİ.", "HATALI MESAJ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                messageObject = null;
            }

            #endregion

            ConstructorMessage(messageObject);
        }
        #endregion

        #region ConstructorMessage(RootMessages message): Datagridview'den alınan bilgileri VDM veya VDO mesajına dönüştürüyor.
        private void ConstructorMessage(RootMessages message)
        {             
                List<string> _listMessageInformation = new List<string>();
            if (message is MessageType1)
                _listMessageInformation = getInformation();
            else if (message is MessageType2)
                _listMessageInformation = getInformation();
            else if (message is MessageType3)
                _listMessageInformation = getInformation();
            else if (message is MessageType4)
                _listMessageInformation = getInformation();

            string state = message.Constructor(AssignmentZero(_listMessageInformation));
            if (state.Contains("Error!") && state.Length > 6)
                MessageBox.Show("Girdiğiniz değerleri lütfen kontrol ediniz." + state, "BİLGİLENDİRME MESAJI", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                lstMessages.Items.Add(state+",0*23");
        }
        #endregion

        #region getInformation(): Datagridview'den sıra ile tüm value'leri alıyor.
        private List<string> getInformation()
        {
            DataGridViewRow row;
            List<string> _listMessageInformation = new List<string>();
            for (int i = 0; i < dgwMessages.Rows.Count- 1; i++)
            {
                row = (DataGridViewRow)dgwMessages.Rows[i];
                _listMessageInformation.Add(row.Cells[1].Value.ToString());
            }

            return _listMessageInformation;
        }
        #endregion

        #region AssignmentZero(List<string> _list): List içindeki değerler boş ise 0 atanıcak.
        private List<string> AssignmentZero(List<string> _list)
        {
            for (int i = 0; i < _list.Count; i++)
            {
                if (_list[i] == "")
                    _list[i] = "0";

                
            }
            return _list;
        }
        #endregion

        #endregion

    }
}
