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
    public partial class TableInfoFrm : Form
    {
        public TableInfoFrm()
        {
            InitializeComponent();
        }
        TableInfoBll tiBll = new TableInfoBll();
        public event Action Refresh;
        private void TableInfoFrm_Load(object sender, EventArgs e)
        {
             LoadTypeList();
            LoadList();
        }

        private void LoadTypeList()
        {
            HallInfoBll hiBll = new HallInfoBll();
            List<HallInfo> list = new List<HallInfo>();
            list= hiBll.GetList();
            list.Insert(0, new HallInfo()
            {
                HId = 0,
                HTitle = "全部"
            });
            ddlHallSearch.DataSource = list;
            ddlHallSearch.ValueMember = "HId";
            ddlHallSearch.DisplayMember = "HTitle";
            List<ddlFree> listDf = new List<ddlFree>()
            {
                new ddlFree(-1, "全部"),
                new ddlFree(1, "空闲"),
                new ddlFree(0, "使用中")
            };                ;
            ddlFreeSearch.DataSource = listDf;
            ddlFreeSearch.ValueMember = "Value";
            ddlFreeSearch.DisplayMember = "Display";
            ddlHallAdd.DataSource = hiBll.GetList();
            ddlHallAdd.ValueMember = "HId";
            ddlHallAdd.DisplayMember = "HTitle";
        }

        private void LoadList()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            if (ddlHallSearch.SelectedIndex > 0)
            {
                dic.Add("THallId", ddlHallSearch.SelectedValue.ToString());
            }
            if (ddlFreeSearch.SelectedIndex > 0)
            {
                dic.Add("TIsFree", ddlFreeSearch.SelectedValue.ToString());
            }
            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = tiBll.GetList(dic);
        }

        private void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                e.Value = Convert.ToBoolean(e.Value) ? "√" : "×";
            }
        }

        private void ddlHallSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadList();
        }

        private void ddlFreeSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadList();
        }

        private void btnSearchAll_Click(object sender, EventArgs e)
        {
            ddlHallSearch.SelectedIndex = 0;
            ddlFreeSearch.SelectedIndex = 0;
            LoadList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            TableInfo ti = new TableInfo()
            {
                TTitle = txtTitle.Text,
                THallId = Convert.ToInt32(ddlHallAdd.SelectedValue),
                TIsFree = rbFree.Checked ? true : false
            };
            if(txtId.Text== "添加时无编号")
            {
                //添加
                if (tiBll.Add(ti))
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
                ti.TId = Convert.ToInt32(txtId.Text);
                if (tiBll.Edit(ti))
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
            ddlHallAdd.SelectedIndex = 0;
            rbFree.Checked = true;
            btnSave.Text = "添加";
            Refresh();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtId.Text = "添加时无编号";
            txtTitle.Text = "";
            ddlHallAdd.SelectedIndex = 0;
            rbFree.Checked = true;
            btnSave.Text = "添加";
        }

        private void dgvList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtId.Text = dgvList.SelectedCells[0].Value.ToString();
            txtTitle.Text = dgvList.SelectedCells[1].Value.ToString();
            ddlHallAdd.Text = dgvList.SelectedCells[2].Value.ToString();
            var tt = dgvList.SelectedCells[3].Value.ToString();
            if (Convert.ToBoolean( dgvList.SelectedCells[3].Value))
            {
                rbFree.Checked = true;
            }
            else
            {
                rbUnFree.Checked = true;
            }
            btnSave.Text = "修改";
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(dgvList.SelectedCells[0].Value);
            DialogResult result = MessageBox.Show("确定要删除此条数据吗", "提示", MessageBoxButtons.OKCancel);
            if (result == DialogResult.Cancel)
            {
                return;
            }
            if (tiBll.Remove(TID))
            {
                LoadList();
            }
            else
            {
                MessageBox.Show("删除失败");
            }
            Refresh();
        }

        private void btnAddHall_Click(object sender, EventArgs e)
        {
            HallInfoFrm hiFrm = new HallInfoFrm();
            hiFrm.updateInfo += HiFrm_updateInfo;
            hiFrm.Show();
        }

        private void HiFrm_updateInfo()
        {
            LoadTypeList();
            LoadList();
        }
    }
}
