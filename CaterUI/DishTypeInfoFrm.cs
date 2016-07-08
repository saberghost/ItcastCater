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
    public partial class DishTypeInfoFrm : Form
    {
        public DishTypeInfoFrm()
        {
            InitializeComponent();
        }
        DishTypeInfoBll dtiBll = new DishTypeInfoBll();
        int index = -1;
        DialogResult result = DialogResult.Cancel;
        private void DishTypeInfoFrm_Load(object sender, EventArgs e)
        {
            LoadList();
        }

        private void LoadList()
        {
            //设置列宽自适应
            dgvList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = dtiBll.GetList();
            if (index >= 0)
            {
                dgvList.Rows[index].Selected = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DishTypeInfo dti = new DishTypeInfo()
            {
                DTitle = txtTitle.Text
            };
            if(txtId.Text== "添加时无编号")
            {
                //添加
                if (dtiBll.Add(dti))
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
                index = dgvList.SelectedRows[0].Index;
                dti.DId = Convert.ToInt32(txtId.Text);
                if (dtiBll.Edit(dti))
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
            btnSave.Text = "添加";
            result = DialogResult.OK;
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
            int DId = Convert.ToInt32(dgvList.SelectedCells[0].Value);
            DialogResult result = MessageBox.Show("你确定要删除此条记录吗？", "提示", MessageBoxButtons.OKCancel);
            if (result == DialogResult.Cancel)
            {
                return;
            }
            if (dtiBll.Remove(DId))
            {
                LoadList();
            }
            else
            {
                MessageBox.Show("删除失败");
            }
            result = DialogResult.OK;
        }

        private void DishTypeInfoFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = result;
        }
    }
}
