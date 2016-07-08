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
    public partial class TableInfoDal
    {
        public List<TableInfo> GetList(Dictionary<string,string> dic)
        {
            string sql = @"select ti.*,hi.HTitle from TableInfo as ti
                        inner join HallInfo as hi
                        on ti.THallId=hi.HId
                        where ti.TIsDelete=0 and hi.HIsDelete=0";
            List<SQLiteParameter> listSp = new List<SQLiteParameter>();
            if (dic.Count>0)
            {
                foreach (var pair in dic)
                {
                    sql += $" and {pair.Key} = @{pair.Key}";
                    listSp.Add(new SQLiteParameter(pair.Key, pair.Value));
                } 
            }
            List<TableInfo> list = new List<TableInfo>();
            DataTable dt = SQLiteHelper.GetDataTable(sql,listSp.ToArray());
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new TableInfo()
                {
                    TId = Convert.ToInt32(row["TId"]),
                    TTitle = row["TTitle"].ToString(),
                    HTitle = row["HTitle"].ToString(),
                    TIsFree = Convert.ToBoolean(row["TIsFree"])
                });
            }
            return list;
        }
        public int Insert(TableInfo ti)
        {
            string sql = "insert into TableInfo (TTitle,THallId,TIsFree,TIsDelete) values (@TTitle,@THallId,@TIsFree,0)";
            SQLiteParameter[] sp =
            {
                new SQLiteParameter("TTitle",ti.TTitle),
                new SQLiteParameter("THallId",ti.THallId),
                new SQLiteParameter("TIsFree",ti.TIsFree)
            };
            return SQLiteHelper.ExecuteNonQuery(sql, sp);
        }
        public int Update(TableInfo ti)
        {
            string sql = "update TableInfo set TTitle=@TTitle,THallId=@THallId,TIsFree=@TIsFree where TId=@TId";
            SQLiteParameter[] sp =
            {
                new SQLiteParameter("TTitle",ti.TTitle),
                new SQLiteParameter("THallId",ti.THallId),
                new SQLiteParameter("TIsFree",ti.TIsFree),
                new SQLiteParameter("TId",ti.TId)
            };
            return SQLiteHelper.ExecuteNonQuery(sql, sp);
        }
        public int Delete(int TId)
        {
            string sql = "update TableInfo set TIsDelete=1 where TId=@TId";
            SQLiteParameter sp = new SQLiteParameter("TId", TId);
            return SQLiteHelper.ExecuteNonQuery(sql, sp);
        }
    }
}
