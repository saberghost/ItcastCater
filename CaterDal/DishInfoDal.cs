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
    public class DishInfoDal
    {
        public List<DishInfo> GetList(Dictionary<string, string> dic)
        {
            string sql = @"select a.*,b.DTitle as DTypeTitle from DishInfo a
                        inner join DishTypeInfo b
                        on a.DTypeId=b.DId
                        where a.DIsDelete=0 and b.DIsDelete=0";
            List<SQLiteParameter> listSp = new List<SQLiteParameter>();
            if (dic.Count > 0)
            {
                foreach (var pair in dic)
                {
                    sql += $" and a.{pair.Key} like @{pair.Key}";
                    listSp.Add(new SQLiteParameter(pair.Key, $"%{pair.Value}%"));
                }
            }

            List<DishInfo> list = new List<DishInfo>();
            DataTable dt = SQLiteHelper.GetDataTable(sql,listSp.ToArray());
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new DishInfo()
                {
                    DId = Convert.ToInt32(row["DId"]),
                    DTitle = row["DTitle"].ToString(),
                    DPrice = Convert.ToDecimal(row["DPrice"]),
                    DChar = row["DChar"].ToString(),
                    DTypeTitle = row["DTypeTitle"].ToString()
                });
            }
            return list;
        }
        public int Insert(DishInfo di)
        {
            string sql = "insert into DishInfo (DTitle,DTypeId,DPrice,DChar,DIsDelete) values (@DTitle,@DTypeId,@DPrice,@DChar,0)";
            SQLiteParameter[] sp =
            {
                new SQLiteParameter("DTitle",di.DTitle),
                new SQLiteParameter("DTypeId",di.DTypeId),
                new SQLiteParameter("DPrice",di.DPrice),
                new SQLiteParameter("DChar",di.DChar)
            };
            return SQLiteHelper.ExecuteNonQuery(sql, sp);
        }
        public int Update(DishInfo di)
        {
            string sql = "update DishInfo set DTitle=@DTitle,DTypeId=@DTypeId,DPrice=@DPrice,DChar=@DChar where DId=@DId";
            SQLiteParameter[] sp =
            {
                new SQLiteParameter("DTitle",di.DTitle),
                new SQLiteParameter("DTypeId",di.DTypeId),
                new SQLiteParameter("DPrice",di.DPrice),
                new SQLiteParameter("DChar",di.DChar),
                new SQLiteParameter("Did",di.DId)
            };
            return SQLiteHelper.ExecuteNonQuery(sql, sp);
        }
        public int Delete(int Did)
        {
            string sql = "update DishInfo set DIsDelete=1 where DId=@DId";
            SQLiteParameter sp = new SQLiteParameter("Did", Did);
            return SQLiteHelper.ExecuteNonQuery(sql, sp);
        }
    }
}
