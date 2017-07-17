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
            string VDM = "!AIVDM,1,1,,D,18AlJpwP00Qtb18F0ELv4?wl20S7,0*2B";

            RootMessages root = new RootMessages();
            byte messageID = root.getMessageID(VDM);


            //return edilen Message ID'sine göre ilgili sınıfta işlem yapılacak.
            if (messageID==1)
            {
                MessageType1 mesaj = new MessageType1();
                mesaj.Parser(VDM);
                MessageBox.Show(mesaj.ToString());
            }
            else if (messageID == 2)
            {
                Console.WriteLine(messageID);
            }
            else
            {
                Console.WriteLine("Hatalı Mesaj");
            }
        }
    }
}
