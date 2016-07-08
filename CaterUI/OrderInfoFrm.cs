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
    public partial class OrderInfoFrm : Form
    {
        public OrderInfoFrm()
        {
            InitializeComponent();
        }
        OrderInfoBll oiBll = new OrderInfoBll();
        private void OrderInfoFrm_Load(object sender, EventArgs e)
        {
            LoadTypeList();
            LoadList();
            LoadOdiList();
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
            ddlType.DataSource = list;
            ddlType.ValueMember = "DId";
            ddlType.DisplayMember = "DTitle";
        }

        private void LoadList()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            if (txtTitle.Text != "")
            {
                dic.Add("DChar", txtTitle.Text);
            }
            if (ddlType.SelectedIndex != 0)
            {
                dic.Add("DTypeId", ddlType.SelectedValue.ToString());
            }
            DishInfoBll diBll = new DishInfoBll();
            dgvAllDish.AutoGenerateColumns = false;
            dgvAllDish.DataSource = diBll.GetList(dic);
        }
        private void txtTitle_TextChanged(object sender, EventArgs e)
        {
            txtTitle.Text = txtTitle.Text.ToUpper();
            txtTitle.SelectionStart = txtTitle.Text.Length;
            LoadList();
        }

        private void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadList();
        }

        private void dgvAllDish_DoubleClick(object sender, EventArgs e)
        {
            //获取订单ID
            int OId = Convert.ToInt32(this.Tag);
            //获取菜品ID
            int DId = Convert.ToInt32(dgvAllDish.SelectedCells[0].Value);
            if (oiBll.DianCai(OId, DId))
            {
                LoadOdiList();
            }
            else
            {
                MessageBox.Show("添加失败");
            }
        }
        private void LoadOdiList()
        {
            dgvOrderDetail.AutoGenerateColumns = false;
            dgvOrderDetail.DataSource = oiBll.GetOdiList(Convert.ToInt32(this.Tag));
            GetTotalMoneyByOrderId();
        }

        private void dgvOrderDetail_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var row = dgvOrderDetail.Rows[e.RowIndex];
            int OId = Convert.ToInt32(row.Cells[0].Value);
            int Count = Convert.ToInt32(row.Cells[2].Value);
            oiBll.UpdateCountByOId(OId, Count);
            GetTotalMoneyByOrderId();
        }
        private void GetTotalMoneyByOrderId()
        {
            lblMoney.Text = oiBll.GetTotalMoneyByOrderId(Convert.ToInt32(this.Tag)).ToString();
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            if (oiBll.UpdateOMoney(Convert.ToInt32(this.Tag), Convert.ToDecimal(lblMoney.Text)))
            {
                MessageBox.Show("下单成功");
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int OId = Convert.ToInt32(dgvOrderDetail.Rows[dgvOrderDetail.SelectedCells[0].RowIndex].Cells[0].Value);
            if (oiBll.DeleteOdiByOId(OId))
            {
                LoadOdiList();
            }
        }
    }
}
