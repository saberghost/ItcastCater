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
    public partial class OrderInfoDal
    {
        #region 开单
        public int KaiDan(int TId)
        {
            string sql = @"insert into OrderInfo (ODate,IsPay,TableId) values (datetime('now','localtime'),0,@TableId);
                        update TableInfo set TIsFree=0 where TId=@TId;
                        select OId from OrderInfo order by OId desc limit 0,1";
            SQLiteParameter[] sp =
            {
                new SQLiteParameter("TableId",TId),
                new SQLiteParameter("TId",TId)
            };
            return Convert.ToInt32(SQLiteHelper.ExecuteScalar(sql, sp));
        }
        #endregion
        #region 通过餐桌ID得到订单ID
        public int GetOidByTableId(int TableId)
        {
            string sql = "select OId from OrderInfo where TableId=@TableId and IsPay=0";
            SQLiteParameter sp = new SQLiteParameter("TableId", TableId);
            return Convert.ToInt32(SQLiteHelper.ExecuteScalar(sql, sp));
        }
        #endregion
        #region 点菜
        public int DianCai(int OId, int DId)
        {
            string sql = "select count(*) from OrderDetailInfo where OrderId=@OrderId and DishId=@DishId";
            SQLiteParameter[] sp =
            {
                new SQLiteParameter("OrderId",OId),
                new SQLiteParameter("DishId",DId)
            };
            int count = Convert.ToInt32(SQLiteHelper.ExecuteScalar(sql, sp));
            if (count == 0)
            {
                sql = "insert into OrderDetailInfo (OrderId,DishId,Count) values (@OrderId,@DishId,1)";
            }
            else
            {
                sql = "update OrderDetailInfo set Count=Count+1 where OrderId=@OrderId and DishId=@DishId";
            }

            return SQLiteHelper.ExecuteNonQuery(sql, sp);
        }
        #endregion
        #region 获取订单详情列表
        public List<OrderDetailInfo> GetOdiList(int OrderId)
        {
            string sql = @"select odi.OId,di.DTitle,odi.Count,di.DPrice
                        from OrderDetailInfo odi
                        inner join DishInfo di
                        on odi.DishId=di.DId
                        where odi.OrderId=@OrderId";
            SQLiteParameter sp = new SQLiteParameter("OrderId", OrderId);
            DataTable dt = SQLiteHelper.GetDataTable(sql, sp);
            List<OrderDetailInfo> list = new List<OrderDetailInfo>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new OrderDetailInfo()
                {
                    OId = Convert.ToInt32(row["OId"]),
                    DTitle = row["DTitle"].ToString(),
                    Count = Convert.ToInt32(row["Count"]),
                    DPrice = Convert.ToDecimal(row["DPrice"])
                });
            }
            return list;
        } 
        #endregion
        public int UpdateCountByOId(int OId,int Count)
        {
            string sql = "update OrderDetailInfo set Count=@Count where OId=@OId";
            SQLiteParameter[] sp =
            {
                new SQLiteParameter("OId",OId),
                new SQLiteParameter("Count",Count)
            };
            return SQLiteHelper.ExecuteNonQuery(sql, sp);
        }
        public decimal GetTotalMoneyByOrderId(int OrderId)
        {
            string sql= @"select sum(odi.Count*di.DPrice)
                        from OrderDetailInfo odi
                        inner
                        join DishInfo di
                        on odi.DishId = di.DId
                        where odi.OrderId = @OrderId";
            SQLiteParameter sp = new SQLiteParameter("OrderId", OrderId);
            if(Convert.IsDBNull(SQLiteHelper.ExecuteScalar(sql, sp)))
            {
                return 0;
            }
            return Convert.ToDecimal(SQLiteHelper.ExecuteScalar(sql, sp));
        }
        public int UpdateOMoney(int OId, decimal OMoney)
        {
            string sql = "update OrderInfo set OMoney=@OMoney where OId=@OId";
            SQLiteParameter[] sp =
            {
                new SQLiteParameter("OId",OId),
                new SQLiteParameter("OMoney",OMoney)
            };
            return SQLiteHelper.ExecuteNonQuery(sql, sp);
        }
        public int DeleteOdiByOId(int OId)
        {
            string sql = "delete from OrderDetailInfo where OId=@OId";
            SQLiteParameter sp = new SQLiteParameter("OId", OId);
            return SQLiteHelper.ExecuteNonQuery(sql, sp);
        }
        public int Pay(bool isUseMoney, int memberId, decimal payMoney, int orderid, decimal discount)
        {
            //创建数据库的链接对象
            using (SQLiteConnection conn = new SQLiteConnection(System.Configuration.ConfigurationManager.ConnectionStrings["itcastCater"].ConnectionString))
            {
                int result = 0;
                //由数据库链接对象创建事务
                conn.Open();
                SQLiteTransaction tran = conn.BeginTransaction();

                //创建command对象
                SQLiteCommand cmd = new SQLiteCommand();
                //将命令对象启用事务
                cmd.Transaction = tran;
                //执行各命令
                string sql = "";
                SQLiteParameter[] ps;
                try
                {
                    //1、根据是否使用余额决定扣款方式
                    if (isUseMoney)
                    {
                        //使用余额
                        sql = "update MemberInfo set mMoney=mMoney-@payMoney where mid=@mid";
                        ps = new SQLiteParameter[]
                        {
                            new SQLiteParameter("@payMoney", payMoney),
                            new SQLiteParameter("@mid", memberId)
                        };
                        cmd.CommandText = sql;
                        cmd.Parameters.AddRange(ps);
                        result += cmd.ExecuteNonQuery();
                    }

                    //2、将订单状态为IsPage=1
                    sql = "update orderInfo set isPay=1,memberId=@mid,discount=@discount where oid=@oid";
                    ps = new SQLiteParameter[]
                    {
                        new SQLiteParameter("@mid", memberId),
                        new SQLiteParameter("@discount", discount),
                        new SQLiteParameter("@oid", orderid)
                    };
                    cmd.CommandText = sql;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddRange(ps);
                    result += cmd.ExecuteNonQuery();

                    //3、将餐桌状态IsFree=1
                    sql = "update tableInfo set tIsFree=1 where tid=(select tableId from orderinfo where oid=@oid)";
                    SQLiteParameter p = new SQLiteParameter("@oid", orderid);
                    cmd.CommandText = sql;
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(p);
                    result += cmd.ExecuteNonQuery();
                    //提交事务
                    tran.Commit();
                }
                catch
                {
                    result = 0;
                    //回滚事务
                    tran.Rollback();
                }
                return result;
            }
        }

    }
}
