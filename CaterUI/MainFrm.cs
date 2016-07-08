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
    public partial class MainFrm : Form
    {
        public MainFrm()
        {
            InitializeComponent();
        }

        private void MainFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void menuQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            int type = Convert.ToInt32(this.Tag);
            if (type == 0)
            {
                menuManager.Visible = false;
            }
            LoadHallInfo();
        }
        private void LoadHallInfo()
        {
            HallInfoBll hiBll = new HallInfoBll();
            var listHi = hiBll.GetList();
            TableInfoBll tiBll = new TableInfoBll();
            tabHallInfo.TabPages.Clear();
            foreach (var hi in listHi)
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("THallId", hi.HId.ToString());
                var listTi = tiBll.GetList(dic);
                TabPage tp = new TabPage(hi.HTitle);
                ListView lv = new ListView();
                lv.LargeImageList = ilTableInfo;
                foreach (var ti in listTi)
                {
                    ListViewItem lvi = new ListViewItem(ti.TTitle, ti.TIsFree ? 0 : 1);
                    lvi.Tag = ti.TId;
                    lv.Items.Add(lvi);
                }
                lv.Dock = DockStyle.Fill;
                lv.DoubleClick += Lv_DoubleClick;
                tp.Controls.Add(lv);
                tabHallInfo.TabPages.Add(tp);
            }
        }

        private void Lv_DoubleClick(object sender, EventArgs e)
        {
            ListView lv = sender as ListView;
            ListViewItem lvi = lv.SelectedItems[0];
            //获取餐桌ID
            int TId = Convert.ToInt32(lvi.Tag);
            OrderInfoBll oiBll = new OrderInfoBll();
            OrderInfoFrm oiFrm = new OrderInfoFrm();
            if (lvi.ImageIndex==0)
            {
                //添加订单数据并获取订单ID
                int OId = oiBll.KaiDan(TId);
                oiFrm.Tag = OId;
                lvi.ImageIndex = 1;
            }
            else
            {
                oiFrm.Tag = oiBll.GetOidByTableId(TId);
            }
            oiFrm.Show();
        }

        private void menuManager_Click(object sender, EventArgs e)
        {
            ManagerInfoFrm miFrm = ManagerInfoFrm.Create();
            miFrm.Show();
            miFrm.Focus();
        }

        private void menuMember_Click(object sender, EventArgs e)
        {
            MemberInfoFrm miFrm = MemberInfoFrm.Create();
            miFrm.Show();
        }

        private void menuTable_Click(object sender, EventArgs e)
        {
            TableInfoFrm tiFrm = new TableInfoFrm();
            tiFrm.Refresh += LoadHallInfo;
            tiFrm.Show();
        }

        private void menuDish_Click(object sender, EventArgs e)
        {
            DishInfoFrm diFrm = new DishInfoFrm();
            diFrm.Show();
        }

        private void menuOrder_Click(object sender, EventArgs e)
        {
            OrderInfoBll oiBll = new OrderInfoBll();
            //先找到选中的标签页，再找到listView,再找到选中的项，项中存储了餐桌编号，由餐桌编号查到订单编号
            var listView = tabHallInfo.SelectedTab.Controls[0] as ListView;
            var lvTable = listView.SelectedItems[0];
            if (lvTable.ImageIndex == 0)
            {
                MessageBox.Show("餐桌还未使用，无法结账");
                return;
            }
            int tableId = Convert.ToInt32(lvTable.Tag);
            int orderId = oiBll.GetOidByTableId(tableId);

            //打开付款窗体
            OrderPayFrm formOrderPay = new OrderPayFrm();
            formOrderPay.Tag = orderId;
            formOrderPay.Refresh += LoadHallInfo;
            formOrderPay.Show();
        }
    }
}
