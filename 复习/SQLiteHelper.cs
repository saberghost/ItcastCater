using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Configuration;
using System.Data;

namespace CaterDal
{
    public class SQLiteHelper
    {
        public static string connStr = ConfigurationManager.ConnectionStrings["ItcastCater"].ConnectionString;
        public static int ExecuteNonQuery(string sql,params SQLiteParameter[] sp)
        {
            using(SQLiteConnection conn=new SQLiteConnection(connStr))
            {
                using (SQLiteCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(sp);
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        public static object ExecuteScalar(string sql,params SQLiteParameter[] sp)
        {
            using(SQLiteConnection conn=new SQLiteConnection(connStr))
            {
                using(SQLiteCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(sp);
                    return cmd.ExecuteScalar();
                }
            }
        }
        /// <summary>
        /// 通过SQLiteParameter获取DataTable
        /// </summary>
        /// <param name="sql">查询字符串</param>
        /// <param name="sp">参数列表</param>
        /// <returns></returns>
        public static DataTable GetDataTable(string sql,params SQLiteParameter[] sp)
        {
            DataTable dt = new DataTable();
            using(SQLiteDataAdapter sda=new SQLiteDataAdapter(sql, connStr))
            {
                sda.SelectCommand.Parameters.AddRange(sp);
                sda.Fill(dt);
            }
            return dt;
        }
    }
}
