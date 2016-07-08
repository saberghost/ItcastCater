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
    public partial class MemberTypeInfoDal
    {
        public List<MemberTypeInfo> GetList()
        {
            string sql = "select * from MemberTypeInfo where MIsDelete=0";
            DataTable dt = SQLiteHelper.GetDataTable(sql);
            List<MemberTypeInfo> list = new List<MemberTypeInfo>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new MemberTypeInfo
                {
                    MId = Convert.ToInt32(row["MId"]),
                    MTitle = row["MTitle"].ToString(),
                    MDiscount = Convert.ToDecimal(row["MDiscount"]),
                    MIsDelete = Convert.ToBoolean(row["MIsDelete"])
                });
            }
            return list;
        }
        public int Insert(MemberTypeInfo mti)
        {
            string sql = "insert into MemberTypeInfo (MTitle,MDiscount,MIsDelete) values (@MTitle,@MDiscount,0)";
            SQLiteParameter[] sp = 
            {
                new SQLiteParameter("MTitle",mti.MTitle),
                new SQLiteParameter("MDiscount",mti.MDiscount)
            };
            return SQLiteHelper.ExecuteNonQuery(sql, sp);
        }
        public int Update(MemberTypeInfo mti)
        {
            string sql = "update MemberTypeInfo set MTitle=@MTitle,MDiscount=@MDiscount where Mid=@Mid";
            SQLiteParameter[] sp =
            {
                new SQLiteParameter("MTitle",mti.MTitle),
                new SQLiteParameter("MDiscount",mti.MDiscount),
                new SQLiteParameter("Mid",mti.MId)
            };
            return SQLiteHelper.ExecuteNonQuery(sql, sp);
        }
        public int Delete(int Mid)
        {
            string sql = "update MemberTypeInfo set MIsDelete=1 where Mid=@Mid";
            SQLiteParameter sp = new SQLiteParameter("Mid", Mid);
            return SQLiteHelper.ExecuteNonQuery(sql, sp);
        }
    }
}
