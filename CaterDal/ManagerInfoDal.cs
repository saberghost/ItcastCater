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
    public class ManagerInfoDal
    {
        /// <summary>
        /// 获取对象的数据集
        /// </summary>
        /// <returns></returns>
        public List<ManagerInfo> GetList()
        {
            DataTable dt = new DataTable();
            string sql = "select * from ManagerInfo";
            dt = SQLiteHelper.GetDataTable(sql);
            List<ManagerInfo> list = new List<ManagerInfo>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new ManagerInfo()
                {
                    Mid = Convert.ToInt32(row["Mid"]),
                    Mname = row["Mname"].ToString(),
                    Mpwd = row["Mpwd"].ToString(),
                    Mtype = Convert.ToInt32(row["Mtype"])
                });
            }
            return list;
        }
        public int Insert(ManagerInfo mi)
        {
            string sql = "insert into ManagerInfo(Mname,Mpwd,Mtype) values(@Mname,@Mpwd,@Mtype)";
            SQLiteParameter[] sp =
            {
                new SQLiteParameter("Mname",mi.Mname),
                new SQLiteParameter("Mpwd",Md5Helper.GetMd5(mi.Mpwd)),
                new SQLiteParameter("Mtype",mi.Mtype)
            };
            return SQLiteHelper.ExecuteNonQuery(sql, sp);
        }
        public int Update(ManagerInfo mi)
        {
            string temp = "";
            List<SQLiteParameter> spList = new List<SQLiteParameter>()
            {
                new SQLiteParameter("Mname",mi.Mname),
                new SQLiteParameter("Mtype",mi.Mtype),
                new SQLiteParameter("Mid",mi.Mid)
            };
            if (mi.Mpwd != "7ACCA3D3-FE55-475A-B7F7-11948B6CA476")
            {
                temp = ",Mpwd=@Mpwd";
                spList.Add(new SQLiteParameter("Mpwd", Md5Helper.GetMd5(mi.Mpwd)));
            }
            string sql = $"update ManagerInfo set Mname=@Mname,Mtype=@Mtype{temp} where Mid=@Mid";
            SQLiteParameter[] sp = spList.ToArray();
            return SQLiteHelper.ExecuteNonQuery(sql, sp);
        }
        public int Delete(int Mid)
        {
            string sql = "delete from ManagerInfo where Mid=@Mid";
            SQLiteParameter sp = new SQLiteParameter("Mid", Mid);
            return SQLiteHelper.ExecuteNonQuery(sql, sp);
        }
        public ManagerInfo GetByName(string name)
        {
            ManagerInfo mi = null;
            string sql = "select Mid,Mname,Mpwd,Mtype from ManagerInfo where Mname=@Mname";
            SQLiteParameter sp = new SQLiteParameter("Mname", name);
            DataTable dt = SQLiteHelper.GetDataTable(sql, sp);
            if (dt.Rows.Count > 0)
            {
                mi = new ManagerInfo()
                {
                    Mid = Convert.ToInt32(dt.Rows[0][0]),
                    Mname = dt.Rows[0][1].ToString(),
                    Mpwd = dt.Rows[0][2].ToString(),
                    Mtype = Convert.ToInt32(dt.Rows[0][3])
                };
            }
            return mi;
        }
               
                   
    }
}
