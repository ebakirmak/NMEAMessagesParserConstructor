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

namespace NMEA_MessageParserConstructor
{
    public partial class frmMain : Form
    {
        private RootMessages root;
        private AllMessage allMessage;
        private int sentencePartCount;
        public frmMain()
        {
            InitializeComponent();
         
            this.root =  new RootMessages();
            this.allMessage = new AllMessage();
        }


        //string VDM1 = "!AIVDM,2,1,1,A,55?MbV02;H;s<HtKR20EHE:address@hidden@Dn2222222216L961O5Gf0NSQEp6ClRp8,0*1C";
        //string VDM2 = "!AIVDM,2,2,1,A,88888888880,2*25";
        //string VDM1 = "!AIVDM,2,1,0,A,58wt8Ui`g??r21`7S=:22058<v05Htp000000015>8OA;0sk,0*7B";
        //string VDM2 = "!AIVDM,2,2,0,A,eQ8823mDm3kP00000000000,2 * 5D";
        string VDM1 = "!AIVDM,1,1,,B,:6TMCD1GOS60,0*5B,s36310,d-081,T59.01777335";


        private void frmMain_Load(object sender, EventArgs e)
        {
            //2 mesaj kuyruğa ekleniyor.
            allMessage.Enqueue(VDM1);
            //allMessage.Enqueue(VDM2);
            //Kuyruktaki ilk mesaj parse edilmek üzere alınıyor ve kuyruktan siliniyor.          
            string currentVDM = allMessage.Dequeue();
            //Mesajın kaç parçadan oluştuğuna bakılıyor.
            sentencePartCount = root.getSentences(currentVDM);
            //Mesaj ID döndürülüyor.
            byte messageID = root.getMessageID(currentVDM);
            if (sentencePartCount == 1)
                {
               
                    //return edilen Message ID'sine göre ilgili sınıfta işlem yapılacak.
                    if (messageID == 1)
                    {
                        MessageType1 mesaj = new MessageType1();
                        mesaj.Parser(VDM1);
                        MessageBox.Show(mesaj.ToString());
                    }
                    else if (messageID == 2)
                    {
                        MessageType2 mesaj = new MessageType2();
                        mesaj.Parser(VDM1);
                        MessageBox.Show(mesaj.ToString());
                    }
                    else if (messageID == 3)
                    {
                        MessageType3 mesaj = new MessageType3();
                        mesaj.Parser(VDM1);
                        MessageBox.Show(mesaj.ToString());
                    }
                    else if (messageID == 4)
                    {
                        MessageType4 mesaj = new MessageType4();
                        mesaj.Parser(VDM1);
                        MessageBox.Show(mesaj.ToString());
                    }
                    else if (messageID == 6)
                    {
                        MessageType6 mesaj = new MessageType6();
                        mesaj.Parser(VDM1);
                        MessageBox.Show(mesaj.ToString());
                    }
                    else if (messageID == 7)
                    {
                         MessageType7 mesaj = new MessageType7();
                        mesaj.Parser(VDM1);
                        MessageBox.Show(mesaj.ToString());
                    }
                    else if (messageID == 8)
                    {
                        MessageType8 mesaj = new MessageType8();
                        mesaj.Parser(VDM1);
                    }
                    else if (messageID == 9)
                    {
                        MessageType9 mesaj = new MessageType9();
                        mesaj.Parser(VDM1);
                        MessageBox.Show(mesaj.ToString());
                    }
                    else if (messageID == 10)
                    {
                        MessageType10 mesaj = new MessageType10();
                        mesaj.Parser(VDM1);
                        MessageBox.Show(mesaj.ToString());
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
                        mesaj.Parser(currentVDM,allMessage.Dequeue());
                        MessageBox.Show(mesaj.ToString());
                    }
                }
        }
    }
}
