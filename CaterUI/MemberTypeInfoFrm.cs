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
    public partial class MemberTypeInfoFrm : Form
    {
        public MemberTypeInfoFrm()
        {
            InitializeComponent();
        }
        MemberTypeInfoBll mtiBll = new MemberTypeInfoBll();
        private DialogResult result = DialogResult.Cancel;
        private void MemberTypeInfoFrm_Load(object sender, EventArgs e)
        {
            LoadList();
        }

        public void LoadList()
        {
            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = mtiBll.GetList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            MemberTypeInfo mti = new MemberTypeInfo();
            if (txtId.Text == "添加时无编号")
            {
                //添加
                mti.MTitle = txtTitle.Text;
                mti.MDiscount = Convert.ToDecimal(txtDiscount.Text);
                if (mtiBll.Add(mti))
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
                mti.MId = Convert.ToInt32(txtId.Text);
                mti.MTitle = txtTitle.Text;
                mti.MDiscount = Convert.ToDecimal(txtDiscount.Text);
                if (mtiBll.Edit(mti))
                {
                    LoadList();
                }
                else
                {
                    MessageBox.Show("修改失败");
                }
            }
            txtId.Text = "添加时无编号";
            txtTitle.Text = "";
            txtTitle.Text = "";
            txtDiscount.Text = "";
            btnSave.Text = "添加";
            result = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtId.Text = "添加时无编号";
            txtTitle.Text = "";
            txtTitle.Text = "";
            txtDiscount.Text = "";
            btnSave.Text = "添加";
        }

        private void dgvList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtId.Text = dgvList.SelectedCells[0].Value.ToString();
            txtTitle.Text = dgvList.SelectedCells[1].Value.ToString();
            txtDiscount.Text = dgvList.SelectedCells[2].Value.ToString();
            btnSave.Text = "修改";
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int Mid = Convert.ToInt32(dgvList.SelectedCells[0].Value);
            DialogResult result = MessageBox.Show("确定要删除吗", "提示", MessageBoxButtons.OKCancel);
            if (result == DialogResult.Cancel)
            {
                return;
            }
            if (mtiBll.Remove(Mid))
            {
                LoadList();
            }
            else
            {
                MessageBox.Show("删除失败");
            }
            result = DialogResult.OK;
        }

        private void MemberTypeInfoFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = result;
        }
    }
}
