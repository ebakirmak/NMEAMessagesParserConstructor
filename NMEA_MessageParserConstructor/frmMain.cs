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


        string VDM1 = "!AIVDM,1,1,,B,C5N3SRgPEnJGEBT>NhWAwwo862PaLELTBJ:V00000000S0D:R220,0*0B";
        //string VDM2 = "!AIVDM,2,2,5,A,:Oko02TSwu8<:Jbb,0*11";

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
                    else if (messageID == 11)
                    {
                        MessageType11 mesaj = new MessageType11();
                        mesaj.Parser(VDM1);
                        MessageBox.Show(mesaj.ToString());
                    }
                      else if(messageID == 12)
                    {
                        MessageType12 mesaj = new MessageType12();
                        mesaj.Parser(VDM1);
                        MessageBox.Show(mesaj.ToString());
                    }
                    else if (messageID == 13)
                    {
                        MessageType13 mesaj = new MessageType13();
                        mesaj.Parser(VDM1);
                        MessageBox.Show(mesaj.ToString());
                    }
                    else if (messageID == 14)
                    {
                        MessageType14 mesaj = new MessageType14();
                        mesaj.Parser(VDM1);
                        MessageBox.Show(mesaj.ToString());
                    }
                    else if (messageID == 15)
                    {
                        MessageType15 mesaj = new MessageType15();
                        mesaj.Parser(VDM1);
                        MessageBox.Show(mesaj.ToString());
                    }
                    else if (messageID == 16)
                    {
                        MessageType16 mesaj = new MessageType16();
                        mesaj.Parser(VDM1);
                        MessageBox.Show(mesaj.ToString());
                    }
                    else if (messageID == 18)
                    {
                        MessageType18 mesaj = new MessageType18();
                        mesaj.Parser(VDM1);
                        MessageBox.Show(mesaj.ToString());
                    }
                    else if (messageID == 19)
                    {
                        MessageType19 mesaj = new MessageType19();
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
                    else if (messageID == 12)
                    {
                        MessageType12 mesaj = new MessageType12();
                        //mesaj.Parser(VDM1,VDM2);
                        MessageBox.Show(mesaj.ToString());
                    }
                    else if (messageID == 17)
                    {
                        MessageType17 mesaj = new MessageType17();
                        //mesaj.Parser(VDM1, VDM2);
                        MessageBox.Show(mesaj.ToString());
                    }
            }
        }
    }
}
