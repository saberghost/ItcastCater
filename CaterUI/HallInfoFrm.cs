using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CaterBll;
using CaterModel;

namespace CaterUI
{
    public partial class HallInfoFrm : Form
    {
        public HallInfoFrm()
        {
            InitializeComponent();
        }
        HallInfoBll hiBll = new HallInfoBll();
        public event Action updateInfo;
        private void HallInfoFrm_Load(object sender, EventArgs e)
        {
            LoadList();
        }

        private void LoadList()
        {
            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = hiBll.GetList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            HallInfo hi = new HallInfo()
            {
                HTitle = txtTitle.Text
            };
            if(txtId.Text== "添加时无编号")
            {
                //添加
                if (hiBll.Add(hi))
                {
                    LoadList();
                }
                else
                {
                    MessageBox.Show("添加失败");
                }
            }
            else
            {
                //修改
                hi.HId = Convert.ToInt32(txtId.Text);
                if (hiBll.Edit(hi))
                {
                    LoadList();
                }
                else
                {
                    MessageBox.Show("修改失败");
                }
            }
            updateInfo();
            txtId.Text = "添加时无编号";
            txtTitle.Text = "";
            btnSave.Text = "添加";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtId.Text = "添加时无编号";
            txtTitle.Text = "";
            btnSave.Text = "添加";
        }

        private void dgvList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtId.Text = dgvList.SelectedCells[0].Value.ToString();
            txtTitle.Text = dgvList.SelectedCells[1].Value.ToString();
            btnSave.Text = "修改";
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int Hid = Convert.ToInt32(dgvList.SelectedCells[0].Value);
            DialogResult result = MessageBox.Show("确定要删除此条记录吗？", "提示", MessageBoxButtons.OKCancel);
            if (result == DialogResult.Cancel)
            {
                return;
            }
            if (hiBll.Remove(Hid))
            {
                LoadList();
            }
            else
            {
                MessageBox.Show("删除失败");
            }
            updateInfo();
        }
    }
}
