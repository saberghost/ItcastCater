using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 发布_订阅
{
    public partial class MainFrm : Form
    {
        public MainFrm()
        {
            InitializeComponent();
        }
        PubFrm pubFrm = new PubFrm();
        private void btnPub_Click(object sender, EventArgs e)
        {
            pubFrm.Show();
        }

        private void btnSub1_Click(object sender, EventArgs e)
        {
            SubFrm1 subFrm1 = new SubFrm1();
            subFrm1.Show();
            pubFrm.myAction += subFrm1.ShowMsg;
        }

        private void btnSub2_Click(object sender, EventArgs e)
        {
            SubFrm2 subFrm2 = new SubFrm2();
            subFrm2.Show();
            pubFrm.myFunc += subFrm2.ShowMsg;
        }
    }
}
