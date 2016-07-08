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
    public partial class ManagerInfoFrm : Form
    {
        private ManagerInfoFrm()
        {
            InitializeComponent();
        }
        #region 创当前窗口的单例
        private static ManagerInfoFrm _frm = null;
        public static ManagerInfoFrm Create()
        {
            if (_frm == null)
            {
                _frm = new ManagerInfoFrm();
            }
            return _frm;
        } 
        #endregion
        ManagerInfoBll miBll = new ManagerInfoBll();
        private void ManagerInfoFrm_Load(object sender, EventArgs e)
        {
            LoadList();
            dgvList.ClearSelection();
        }
        /// <summary>
        /// 加载管理员列表
        /// </summary>
        void LoadList()
        {
            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = miBll.GetList();
        }
        /// <summary>
        /// 添加或修改管理员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            ManagerInfo mi = new ManagerInfo()
            {
                Mname = txtName.Text,
                Mpwd = txtPwd.Text,
                Mtype = rb1.Checked ? 1 : 0,
            };
            if (btnSave.Text == "添加")
            {
                if (miBll.Add(mi))
                {
                    LoadList();
                    txtName.Text = "";
                    txtPwd.Text = "";
                    rb2.Checked = true;
                }
                else
                {
                    MessageBox.Show("添加失败");
                }
            }
            else if (btnSave.Text == "修改")
            {
                mi.Mid = Convert.ToInt32(txtId.Text);
                if (miBll.Edit(mi))
                {
                    LoadList();
                    txtName.Text = "";
                    txtPwd.Text = "";
                    rb2.Checked = true;
                    txtId.Text = "添加时无编号";
                    btnSave.Text = "添加";
                }
                else
                {
                    MessageBox.Show("修改失败");
                }
            }

        }
        /// <summary>
        /// 格式化单元格显示内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                e.Value = Convert.ToInt32(e.Value) == 1 ? "经理" : "店员";
            }
        }
        /// <summary>
        /// 选择要修改的管理员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtId.Text = dgvList.SelectedCells[0].Value.ToString();
            txtName.Text = dgvList.SelectedCells[1].Value.ToString();
            txtPwd.Text = "7ACCA3D3-FE55-475A-B7F7-11948B6CA476";
            if (Convert.ToUInt32(dgvList.SelectedCells[2].Value) == 1)
            {
                rb1.Checked = true;
            }
            else
            {
                rb2.Checked = true;
            }
            btnSave.Text = "修改";
        }
        /// <summary>
        /// 取消操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtPwd.Text = "";
            rb2.Checked = true;
            txtId.Text = "添加时无编号";
            btnSave.Text = "添加";
        }
        /// <summary>
        /// 删除管理员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgvList.SelectedCells.Count == 0)
            {
                MessageBox.Show("请选择一行删除！");
                return;
            }
            DialogResult= MessageBox.Show("确定删除所选行吗？", "提示", MessageBoxButtons.OKCancel);
            if (DialogResult == DialogResult.Cancel)
            {
                return;
            }
            int mid = Convert.ToInt32(dgvList.SelectedCells[0].Value);
            if (miBll.Remove(mid))
            {
                LoadList();
            }
            else
            {
                MessageBox.Show("删除失败");
            }
        }

        private void ManagerInfoFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _frm = null;
        }
    }
}
