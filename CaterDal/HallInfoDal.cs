using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaterModel;
using System.Data;
using System.Data.SQLite;
using CaterCommon;

namespace CaterDal
{
    public partial class HallInfoDal
    {
        public List<HallInfo> GetList()
        {
            string sql = "select * from HallInfo where HIsDelete=0";
            List<HallInfo> list = new List<HallInfo>();
            DataTable dt = SQLiteHelper.GetDataTable(sql);
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new HallInfo()
                {
                    HId = Convert.ToInt32(row["HId"]),
                    HTitle = row["HTitle"].ToString()
                });
            }
            return list;
        }
        public int Insert(HallInfo hi)
        {
            string sql = "insert into HallInfo (HTitle,HIsDelete) values (@HTitle,0)";
            SQLiteParameter sp = new SQLiteParameter("HTitle", hi.HTitle);
            return SQLiteHelper.ExecuteNonQuery(sql, sp);
        }
        public int Update(HallInfo hi)
        {
            string sql = "update HallInfo set HTitle=@HTitle where HId=@HId";
            SQLiteParameter[] sp =
            {
                new SQLiteParameter("HTitle",hi.HTitle),
                new SQLiteParameter("HId",hi.HId)
            };
            return SQLiteHelper.ExecuteNonQuery(sql, sp);
        }
        public int Delete(int Hid)
        {
            string sql = "update HallInfo set HIsDelete=1 where HId=@HId";
            SQLiteParameter sp = new SQLiteParameter("Hid", Hid);
            return SQLiteHelper.ExecuteNonQuery(sql, sp);
        }
    }
}
