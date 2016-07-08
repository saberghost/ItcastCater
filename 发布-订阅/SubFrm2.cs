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
    public partial class SubFrm2 : Form
    {
        public SubFrm2()
        {
            InitializeComponent();
        }
        public int ShowMsg(string str)
        {
            textBox1.Text = str;
            return 1;
        }
    }
}
