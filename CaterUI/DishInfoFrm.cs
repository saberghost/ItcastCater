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
using CaterCommon;

namespace CaterUI
{
    public partial class DishInfoFrm : Form
    {
        public DishInfoFrm()
        {
            InitializeComponent();
        }
        DishInfoBll diBll = new DishInfoBll();
        private void DishInfoFrm_Load(object sender, EventArgs e)
        {
            LoadTypeList();
            LoadList();
        }

        private void LoadTypeList()
        {
            DishTypeInfoBll dtiBll = new DishTypeInfoBll();
            List<DishTypeInfo> list = dtiBll.GetList();
            list.Insert(0, new DishTypeInfo()
            {
                DId=0,
                DTitle="全部"
            });
            ddlTypeSearch.DataSource = list;
            ddlTypeSearch.ValueMember = "DId";
            ddlTypeSearch.DisplayMember = "DTitle";
            ddlTypeAdd.DataSource = dtiBll.GetList();
            ddlTypeAdd.ValueMember = "DId";
            ddlTypeAdd.DisplayMember = "DTitle";
        }

        private void LoadList()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            if (txtTitleSearch.Text != "")
            {
                dic.Add("DTitle", txtTitleSearch.Text);
            }
            if (ddlTypeSearch.SelectedValue.ToString() != "0")
            {
                dic.Add("DTypeId", ddlTypeSearch.SelectedValue.ToString());
            }
            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = diBll.GetList(dic);
        }

        private void txtTitleSearch_TextChanged(object sender, EventArgs e)
        {
            LoadList();
        }

        private void ddlTypeSearch_SelectedValueChanged(object sender, EventArgs e)
        {
            LoadList();
        }

        private void btnSearchAll_Click(object sender, EventArgs e)
        {
            txtTitleSearch.Text = "";
            ddlTypeSearch.SelectedValue = 0;
            LoadList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DishInfo di = new DishInfo()
            {
                DTitle = txtTitleSave.Text,
                DTypeId = Convert.ToInt32(ddlTypeAdd.SelectedValue),
                DPrice = Convert.ToDecimal(txtPrice.Text),
                DChar = txtChar.Text
            };
            if(txtId.Text== "添加时无编号")
            {
                //添加
                if (diBll.Add(di))
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
                di.DId = Convert.ToInt32(txtId.Text);
                if (diBll.Edit(di))
                {
                    LoadList();
                }
                else
                {
                    MessageBox.Show("修改失败");
                }
            }
            txtId.Text = "添加时无编号";
            txtTitleSave.Text = "";
            ddlTypeAdd.SelectedIndex = 0;
            txtPrice.Text = "";
            txtChar.Text = "";
            btnSave.Text = "添加";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtId.Text = "添加时无编号";
            txtTitleSave.Text = "";
            ddlTypeAdd.SelectedIndex = 0;
            txtPrice.Text = "";
            txtChar.Text = "";
            btnSave.Text = "添加";
        }

        private void txtTitleSave_Leave(object sender, EventArgs e)
        {
            txtChar.Text = PinyinHelper.GetPinyin(txtTitleSave.Text);
        }

        private void dgvList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtId.Text = dgvList.SelectedCells[0].Value.ToString();
            txtTitleSave.Text = dgvList.SelectedCells[1].Value.ToString();
            ddlTypeAdd.Text = dgvList.SelectedCells[2].Value.ToString();
            txtPrice.Text = dgvList.SelectedCells[3].Value.ToString();
            txtChar.Text = dgvList.SelectedCells[4].Value.ToString();
            btnSave.Text = "修改";
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int Did = Convert.ToInt32(dgvList.SelectedCells[0].Value);
            DialogResult result= MessageBox.Show("确定要删除此条数据吗", "提示", MessageBoxButtons.OKCancel);
            if (result == DialogResult.Cancel)
            {
                return;
            }
            if (diBll.Remove(Did))
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
            DishTypeInfoFrm dtiFrm = new DishTypeInfoFrm();
            DialogResult result= dtiFrm.ShowDialog();
            if (result == DialogResult.OK)
            {
                LoadTypeList();
                LoadList();
            }
        }
    }
}
