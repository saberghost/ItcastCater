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
    public partial class SubFrm1 : Form
    {
        public SubFrm1()
        {
            InitializeComponent();
        }
        public void ShowMsg()
        {
            textBox1.Text = "订阅者1";
        }
    }
}
