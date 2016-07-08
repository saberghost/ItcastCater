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
    public partial class MemberInfoFrm : Form
    {
        private MemberInfoFrm()
        {
            InitializeComponent();
        }
        private static MemberInfoFrm _frm=null;
        public static MemberInfoFrm Create()
        {
            if (_frm == null)
            {
                _frm = new MemberInfoFrm();
            }
            return _frm;
        }
        MemberInfoBll miBll = new MemberInfoBll();
        private void MemberInfoFrm_Load(object sender, EventArgs e)
        {
            LoadList();
            LoadTypeList();
        }

        private void LoadTypeList()
        {
            MemberTypeInfoBll mtiBll = new MemberTypeInfoBll();
            ddlType.DataSource = mtiBll.GetList();
            //设置显示值
            ddlType.DisplayMember = "MTitle";
            //设置实际值
            ddlType.ValueMember = "MId";
        }

        private void LoadList()
        {
            dgvList.AutoGenerateColumns = false;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            if (txtNameSearch.Text != "")
            {
                dic.Add("MName", txtNameSearch.Text);
            }
            if (txtPhoneSearch.Text != "")
            {
                dic.Add("MPhone", txtPhoneSearch.Text);
            }
            dgvList.DataSource = miBll.GetList(dic);
        }

        private void txtNameSearch_TextChanged(object sender, EventArgs e)
        {
            LoadList();
        }

        private void txtPhoneSearch_TextChanged(object sender, EventArgs e)
        {
            LoadList();
        }

        private void btnSearchAll_Click(object sender, EventArgs e)
        {
            txtNameSearch.Text = "";
            txtPhoneSearch.Text = "";
            LoadList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            MemberInfo mi = new MemberInfo()
            {
                MName = txtNameAdd.Text,
                MTypeId = Convert.ToInt32(ddlType.SelectedValue),
                MPhone = txtPhoneAdd.Text,
                MMoney=Convert.ToDecimal(txtMoney.Text)
            };
            if (txtId.Text.Equals("添加时无编号"))
            {
                //添加
                if (miBll.Add(mi))
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
                mi.MId = Convert.ToInt32(txtId.Text);
                if (miBll.Edit(mi))
                {
                    LoadList();
                }
                else
                {
                    MessageBox.Show("修改失败");
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtId.Text = "添加时无编号";
            txtNameAdd.Text = "";
            ddlType.SelectedIndex = 0;
            txtPhoneAdd.Text = "";
            txtMoney.Text = "";
            btnSave.Text = "添加";
        }

        private void dgvList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtId.Text = dgvList.SelectedCells[0].Value.ToString();
            txtNameAdd.Text = dgvList.SelectedCells[1].Value.ToString();
            ddlType.Text = dgvList.SelectedCells[2].Value.ToString();
            txtPhoneAdd.Text = dgvList.SelectedCells[3].Value.ToString();
            txtMoney.Text = dgvList.SelectedCells[4].Value.ToString();
            btnSave.Text = "修改";
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int Mid = Convert.ToInt32(dgvList.SelectedCells[0].Value);
            if (miBll.Remove(Mid))
            {
                LoadList();
            }
            else
            {
                MessageBox.Show("删除失败");
            }
        }

        private void btnAddType_Click(object sender, EventArgs e)
        {
            MemberTypeInfoFrm mtiFrm = new MemberTypeInfoFrm();
            DialogResult result= mtiFrm.ShowDialog();
            if (result == DialogResult.OK)
            {
                LoadTypeList();
                LoadList();
            }
        }

        private void MemberInfoFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _frm = null;
        }
    }
}
