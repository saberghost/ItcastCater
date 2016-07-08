using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 发布_订阅
{
    public partial class PubFrm : Form
    {
        public PubFrm()
        {
            InitializeComponent();
            //myAction += () => { };
            //myAction += delegate ()
            //  {
            //      Console.WriteLine("1");
            //  };
        }
        //系统自带无返回类型委托
        public event Action myAction;
        //系统自带有返回类型委托
        public event Func<string, int> myFunc;
        public delegate void MyDel(string str);
        public event MyDel mydel;
        private void btnSend_Click(object sender, EventArgs e)
        {
            myAction();
        }

        private void btnSend2_Click(object sender, EventArgs e)
        {
            int i=myFunc(textBox1.Text);
            Thread t = new Thread(pop);
            t.Start("ss");
        }


        private void pop(object str)
        {
            MessageBox.Show(str.ToString());
        }
    }
}
