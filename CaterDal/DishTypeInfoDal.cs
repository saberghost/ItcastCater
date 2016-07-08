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
    public class DishTypeInfoDal
    {
        public List<DishTypeInfo> GetList()
        {
            string sql = "select * from DishTypeInfo where DIsDelete=0";
            DataTable dt = SQLiteHelper.GetDataTable(sql);
            List<DishTypeInfo> list = new List<DishTypeInfo>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new DishTypeInfo()
                {
                    DId = Convert.ToInt32(row["DId"]),
                    DTitle=row["DTitle"].ToString()
                });
            }
            return list;
        }
        public int Insert(DishTypeInfo dti)
        {
            string sql = "insert into DishTypeInfo (DTitle,DIsDelete) values (@DTitle,0)";
            SQLiteParameter sp = new SQLiteParameter("DTitle", dti.DTitle);
            return SQLiteHelper.ExecuteNonQuery(sql, sp);
        }
        public int Update(DishTypeInfo dti)
        {
            string sql = "update DishTypeInfo set DTitle=@DTitle where DId=@DId";
            SQLiteParameter[] sp =
            {
                new SQLiteParameter("DTitle",dti.DTitle),
                new SQLiteParameter("Did",dti.DId)
            };
            return SQLiteHelper.ExecuteNonQuery(sql, sp);
        }
        public int Delete(int DId)
        {
            string sql = "update DishTypeInfo set DIsDelete=1 where DId=@DId";
            SQLiteParameter sp = new SQLiteParameter("DId", DId);
            return SQLiteHelper.ExecuteNonQuery(sql, sp);
        }
    }
}
