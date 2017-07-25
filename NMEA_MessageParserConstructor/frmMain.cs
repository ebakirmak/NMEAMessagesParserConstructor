using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NMEA_MessageParserConstructor;
using NMEA_MessageParserConstructor.BL;
using NMEA_MessageParserConstructor.BL.Messages;

namespace NMEA_MessageParserConstructor
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

       private void frmMain_Load(object sender, EventArgs e)
        {
            int sentencePartCount;
            string VDM1 = "!AIVDM,2,1,1,A,55?MbV02;H;s<HtKR20EHE:address@hidden@Dn2222222216L961O5Gf0NSQEp6ClRp8,0*1C";
            string VDM2 = "!AIVDM,2,2,1,A,88888888880,2*25";
            //string VDM1 = "!AIVDM,2,1,0,A,58wt8Ui`g??r21`7S=:22058<v05Htp000000015>8OA;0sk,0*7B";
            //string VDM2 = "!AIVDM,2,2,0,A,eQ8823mDm3kP00000000000,2 * 5D";
            string currentVDM;
            Queue<string> mesajQueue = new Queue<string>();
            mesajQueue.Enqueue(VDM1);
            mesajQueue.Enqueue(VDM2);
            do
            {
                if (mesajQueue.Count > 0)
                {
                    currentVDM = mesajQueue.Dequeue();
                     sentencePartCount = Convert.ToInt32(currentVDM.Split(',')[1]);
                }
                else
                {
                    sentencePartCount = 0;
                    currentVDM = null;
                }

                         
                if (sentencePartCount == 1)
                {
                    

                    RootMessages root = new RootMessages();
                    byte messageID = root.getMessageID(currentVDM);
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
                    else
                    {
                        Console.WriteLine("Hatalı Mesaj");
                    }
                }
                else if (sentencePartCount == 2)
                {
                    RootMessages root = new RootMessages();
                    byte messageID = root.getMessageID(currentVDM);
                    if (messageID == 5)
                    {
                        MessageType5 mesaj = new MessageType5();
                        string mesajContext1 = currentVDM.Split(',')[5];
                        string mesajContext2= mesajQueue.Dequeue().Split(',')[5];
                        mesaj.Parser(mesajContext1,mesajContext2);
                        MessageBox.Show(mesaj.ToString());
                    }
                }
            } while (currentVDM!=null);

           
        }
    }
}
