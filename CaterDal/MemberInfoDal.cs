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
    public class MemberInfoDal
    {
        public List<MemberInfo> GetList(Dictionary<string, string> dic)
        {
            string sql = "select a.*,b.MTitle,b.mDiscount " +
                        "from MemberInfo a inner " +
                        "join MemberTypeInfo b " +
                        "on a.MTypeId = b.MId " +
                        "where a.MIsDelete = 0";
            List<SQLiteParameter> sp = new List<SQLiteParameter>();
            if (dic.Count > 0)
            {
                foreach (KeyValuePair<string, string> pair in dic)
                {
                    sql += $" and a.{pair.Key} like @{pair.Key}";
                    sp.Add(new SQLiteParameter(pair.Key, $"%{pair.Value}%"));
                }
            }
            DataTable dt = SQLiteHelper.GetDataTable(sql, sp.ToArray());
            List<MemberInfo> list = new List<MemberInfo>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new MemberInfo()
                {
                    MId = Convert.ToInt32(row["Mid"]),
                    MIsDelete = Convert.ToBoolean(row["MIsDelete"]),
                    MMoney = Convert.ToDecimal(row["MMoney"]),
                    MName = row["MName"].ToString(),
                    MPhone = row["MPhone"].ToString(),
                    MTitle = row["MTitle"].ToString(),
                    MTypeId = Convert.ToInt32(row["MTypeId"]),
                    MTypeTitle = row["MTitle"].ToString(),
                    MDiscount = Convert.ToDecimal(row["mDiscount"])
                });
            }
            return list;
        }
        public int Insert(MemberInfo mi)
        {
            string sql = "insert into MemberInfo (MName,MTypeId,MPhone,MMoney,MIsDelete) values (@MName,@MTypeId,@MPhone,@MMoney,0)";
            SQLiteParameter[] sp =
            {
                new SQLiteParameter("MName",mi.MName),
                new SQLiteParameter("MTypeId",mi.MTypeId),
                new SQLiteParameter("MPhone",mi.MPhone),
                new SQLiteParameter("MMoney",mi.MMoney)
            };
            return SQLiteHelper.ExecuteNonQuery(sql, sp);
        }
        public int Update(MemberInfo mi)
        {
            string sql = "update MemberInfo set MName=@MName,MTypeId=@MTypeId,MPhone=@MPhone,MMoney=@MMoney where Mid=@Mid";
            SQLiteParameter[] sp =
            {
                new SQLiteParameter("MName",mi.MName),
                new SQLiteParameter("MTypeId",mi.MTypeId),
                new SQLiteParameter("MPhone",mi.MPhone),
                new SQLiteParameter("MMoney",mi.MMoney),
                new SQLiteParameter("Mid",mi.MId)
            };
            return SQLiteHelper.ExecuteNonQuery(sql, sp);
        }
        public int Delete(int Mid)
        {
            string sql = "update MemberInfo set MIsDelete=1 where Mid=@Mid";
            SQLiteParameter sp = new SQLiteParameter("Mid", Mid);
            return SQLiteHelper.ExecuteNonQuery(sql, sp);
        }
    }
}
