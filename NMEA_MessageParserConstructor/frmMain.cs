using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            MessageType1 m = new MessageType1();
            m.Parse("!AIVDM,1,1,,A,13u?etPv2;0n:dDPwUM1U1Cb069D,0*23");
        }
    }
}
