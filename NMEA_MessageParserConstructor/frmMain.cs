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
        private string VDM2= "!AIVDM,2,2,5,B,:D44QDlp0C1DU00,2*36";
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
            sp = new SeriPort("COM4", 38400, 8);
            //ReadMessage fonksiyonunu bir thread'e atıyoruz.
            threadRead  = new Thread(ReadMessage);
            //log nesnemizi oluşturduk.
            this.log = LogManager.GetCurrentClassLogger();
        }

        #region seriport sınıfı ile gelen mesajları sürekli okuma
        static void ReadMessage()
        {          
             sp.ReadMessage();           
    
        }
        #endregion
        
        #region FormLoad
        private void frmMain_Load(object sender, EventArgs e)
        {           
            threadRead.Start();
            tmrMessage.Interval = 500;
            tmrMessage.Start();
            ScreenResize();
     
        }
        #endregion

        #region İlgili mesajın bilgilerini Datagridview nesnesine yazdırıyoruz.
        private void AddRows(RootMessages message)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();     
            foreach (var item in message.getAttributes())
            {
                DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                row.Cells[0].Value = item.Item1;
                row.Cells[1].Value = item.Item2;
                dataGridView1.Rows.Add(row);
            }

        }
        #endregion     

        #region Belli bir süre aralıklarla gelen mesajlar ekranda gösterilecek.
        private void tmrMessage_Tick(object sender, EventArgs e)
        {
            if (this.allMessage.Count() > 0)
                listBox1.Items.Add(this.allMessage.Dequeue());
        }
        #endregion

        #region Program kapatılırken seriporttan mesaj okuma işlemi bitirilir.
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            sp.DontReadMessage();
        }
        #endregion
   
        #region Ekranın boyutunu ayarlıyoruz.
        private void ScreenResize()
        {
            this.WindowState = FormWindowState.Maximized;
        }


        #endregion

        #region Listboxtaki ilgili mesaja tıklayınca çalışacak komut.
        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                VDM1 = listBox1.SelectedItem.ToString();
                if(getSentenceCount()==1)
                    Run();
                else
                {
                    int currenIndex = (listBox1.SelectedIndex);
                    VDM2 = listBox1.Items[currenIndex + 1].ToString();
                    Run();
                }
            }
        }
        #endregion

        #region Mesajın kaç cümleden oluştuğunu gösteriyor.
        private byte getSentenceCount()
        {
            //Mesajın kaç parçadan oluştuğuna bakılıyor.
            return root.getSentenceCount(VDM1);
            
        }
        #endregion

        #region İlgili mesaja göre Datagridview dolacak.
        private void Run()
        {
            try
            {
         

                //Mesaj ID döndürülüyor.
                byte messageID = root.getMessageID(VDM1);

                //Mesaj cümle sayısı döndürülüyor.
                sentencePartCount = root.getSentenceCount(VDM1);

                if (sentencePartCount == 1)
                {

                    //return edilen Message ID'sine göre ilgili sınıfta işlem yapılacak.
                    if (messageID == 1)
                    {
                        MessageType1 mesaj = new MessageType1();
                        mesaj.Parser(VDM1);
                        MessageBox.Show(mesaj.ToString());
                        AddRows(mesaj);

                    }
                    else if (messageID == 2)
                    {
                        MessageType2 mesaj = new MessageType2();
                        mesaj.Parser(VDM1);
                        MessageBox.Show(mesaj.ToString());
                        AddRows(mesaj);
                    }
                    else if (messageID == 3)
                    {
                        MessageType3 mesaj = new MessageType3();
                        mesaj.Parser(VDM1);
                        MessageBox.Show(mesaj.ToString());
                        AddRows(mesaj);
                    }
                    else if (messageID == 4)
                    {
                        MessageType4 mesaj = new MessageType4();
                        mesaj.Parser(VDM1);
                        MessageBox.Show(mesaj.ToString());
                        AddRows(mesaj);
                    }
                    else if (messageID == 6)
                    {
                        MessageType6 mesaj = new MessageType6();
                        mesaj.Parser(VDM1);
                        MessageBox.Show(mesaj.ToString());
                        AddRows(mesaj);
                    }
                    else if (messageID == 7)
                    {
                        MessageType7 mesaj = new MessageType7();
                        mesaj.Parser(VDM1);
                        MessageBox.Show(mesaj.ToString());
                        AddRows(mesaj);
                    }
                    else if (messageID == 8)
                    {
                        MessageType8 mesaj = new MessageType8();
                        mesaj.Parser(VDM1);
                        MessageBox.Show(mesaj.ToString());
                        AddRows(mesaj);
                    }
                    else if (messageID == 9)
                    {
                        MessageType9 mesaj = new MessageType9();
                        mesaj.Parser(VDM1);
                        MessageBox.Show(mesaj.ToString());
                        AddRows(mesaj);
                    }
                    else if (messageID == 10)
                    {
                        MessageType10 mesaj = new MessageType10();
                        mesaj.Parser(VDM1);
                        MessageBox.Show(mesaj.ToString());
                        AddRows(mesaj);
                    }
                    else if (messageID == 11)
                    {
                        MessageType11 mesaj = new MessageType11();
                        mesaj.Parser(VDM1);
                        MessageBox.Show(mesaj.ToString());
                        AddRows(mesaj);
                    }
                    else if (messageID == 12)
                    {
                        MessageType12 mesaj = new MessageType12();
                        mesaj.Parser(VDM1,null);
                        MessageBox.Show(mesaj.ToString());
                        AddRows(mesaj);
                    }
                    else if (messageID == 13)
                    {
                        MessageType13 mesaj = new MessageType13();
                        mesaj.Parser(VDM1);
                        MessageBox.Show(mesaj.ToString());
                        AddRows(mesaj);
                    }
                    else if (messageID == 14)
                    {
                        MessageType14 mesaj = new MessageType14();
                        mesaj.Parser(VDM1);
                        MessageBox.Show(mesaj.ToString());
                        AddRows(mesaj);
                    }
                    else if (messageID == 15)
                    {
                        MessageType15 mesaj = new MessageType15();
                        mesaj.Parser(VDM1);
                        MessageBox.Show(mesaj.ToString());
                        AddRows(mesaj);
                    }
                    else if (messageID == 16)
                    {
                        MessageType16 mesaj = new MessageType16();
                        mesaj.Parser(VDM1);
                        MessageBox.Show(mesaj.ToString());
                        AddRows(mesaj);
                    }
                    else if (messageID == 18)
                    {
                        MessageType18 mesaj = new MessageType18();
                        mesaj.Parser(VDM1);
                        MessageBox.Show(mesaj.ToString());
                        AddRows(mesaj);
                    }
                    else if (messageID == 19)
                    {
                        MessageType19 mesaj = new MessageType19();
                        mesaj.Parser(VDM1);
                        MessageBox.Show(mesaj.ToString());
                        AddRows(mesaj);
                    }
                    else if (messageID == 20)
                    {
                        MessageType20 mesaj = new MessageType20();
                        mesaj.Parser(VDM1);
                        MessageBox.Show(mesaj.ToString());
                        AddRows(mesaj);
                    }
                    else if (messageID == 22)
                    {
                        MessageType22 mesaj = new MessageType22();
                        mesaj.Parser(VDM1);
                        MessageBox.Show(mesaj.ToString());
                        AddRows(mesaj);
                    }
                    else if (messageID == 23)
                    {
                        MessageType23 mesaj = new MessageType23();
                        mesaj.Parser(VDM1);
                        MessageBox.Show(mesaj.ToString());
                        AddRows(mesaj);
                    }
                    else if (messageID == 24)
                    {
                        MessageType24 mesaj = new MessageType24();
                        mesaj.setPartNumber(VDM1);
                        if (mesaj.getPartNumber() == 0)
                        {
                            MessageType24A mesajA = new MessageType24A();
                            mesajA.Parser(VDM1);
                            MessageBox.Show(mesajA.ToString());
                            AddRows(mesajA);

                        }
                        else if (mesaj.getPartNumber() == 1)
                        {
                            MessageType24B mesajB = new MessageType24B();
                            mesajB.Parser(VDM1);
                            MessageBox.Show(mesajB.ToString());
                            AddRows(mesajB);
                        }
                     
                    }
                    else if (messageID == 25)
                    {
                        MessageType25 mesaj = new MessageType25();
                        mesaj.Parser(VDM1);
                        MessageBox.Show(mesaj.ToString());
                        AddRows(mesaj);
                    }
                    else if (messageID == 26)
                    {
                        MessageBox.Show("Mesaj 26 düzelt");
                    }
                    else if (messageID == 27)
                    {
                        MessageType27 mesaj = new MessageType27();
                        mesaj.Parser(VDM1);
                        MessageBox.Show(mesaj.ToString());
                        AddRows(mesaj);
                    }
                    else
                    {
                        Console.WriteLine("Hatalı Mesaj");
                    }


                }
                else if (sentencePartCount == 2)
                {

                    if (messageID == 5)
                    {
                        MessageType5 mesaj = new MessageType5();
                        mesaj.Parser(VDM1, VDM2);
                        MessageBox.Show(mesaj.ToString());
                        AddRows(mesaj);
                    }
                    else if (messageID == 12)
                    {
                        MessageType12 mesaj = new MessageType12();
                        //mesaj.Parser(VDM1,VDM2);
                        MessageBox.Show(mesaj.ToString());
                    }
                    else if (messageID == 17)
                    {
                        MessageType17 mesaj = new MessageType17();
                        mesaj.Parser(VDM1, VDM2);
                        MessageBox.Show(mesaj.ToString());
                        AddRows(mesaj);
                    }
                    else if (messageID == 21)
                    {
                        MessageType21 mesaj = new MessageType21();
                        mesaj.Parser(VDM1, VDM2);
                        MessageBox.Show(mesaj.ToString());
                        AddRows(mesaj);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "frmMain :: Run");
               
            }
           
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            VDM1 = textBox1.Text;
            Run();
        }
    }
}
