using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaterDal;
using CaterModel;
using CaterCommon;

namespace CaterBll
{
    public class OrderInfoBll
    {
        OrderInfoDal oiDal = new OrderInfoDal();
        public int KaiDan(int TId)
        {
            return oiDal.KaiDan(TId);
        }
        public int GetOidByTableId(int TableId)
        {
            return oiDal.GetOidByTableId(TableId);
        }
        public bool DianCai(int OId, int DId)
        {
            return oiDal.DianCai(OId, DId) > 0;
        }
        public List<OrderDetailInfo> GetOdiList(int OrderId)
        {
            return oiDal.GetOdiList(OrderId);
        }
        public bool UpdateCountByOId(int OId,int Count)
        {
            return oiDal.UpdateCountByOId(OId, Count) > 0;
        }
        public decimal GetTotalMoneyByOrderId(int OrderId)
        {
            return oiDal.GetTotalMoneyByOrderId(OrderId);
        }
        public bool UpdateOMoney(int OId, decimal OMoney)
        {
            return oiDal.UpdateOMoney(OId, OMoney) > 0;
        }
        public bool DeleteOdiByOId(int OId)
        {
            return oiDal.DeleteOdiByOId(OId) > 0;
        }
        public bool Pay(bool isUseMoney, int memberId, decimal payMoney, int orderid, decimal discount)
        {
            return oiDal.Pay(isUseMoney, memberId, payMoney, orderid, discount) > 0;
        }

    }
}
