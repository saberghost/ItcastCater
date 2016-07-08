using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CaterModel;
using CaterBll;

namespace CaterUI
{
    public partial class LoginFrm : Form
    {
        public LoginFrm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string pwd = txtPwd.Text;
            ManagerInfoBll miBll = new ManagerInfoBll();
            int type;
            LoginState state = miBll.Login(name, pwd,out type);
            switch (state)
            {
                case LoginState.Ok:
                    MainFrm mainFrm = new MainFrm();
                    mainFrm.Tag = type;
                    mainFrm.Show();
                    this.Visible = false;
                    break;
                case LoginState.NameError:
                    MessageBox.Show("用户名错误");
                    break;
                case LoginState.PwdError:
                    MessageBox.Show("密码错误");
                    break;
                default:
                    break;
            }
        }

        private void brnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
