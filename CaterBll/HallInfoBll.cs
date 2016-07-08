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
    public partial class HallInfoBll
    {
        HallInfoDal HiDal = new HallInfoDal();
        public List<HallInfo> GetList()
        {
            return HiDal.GetList();
        }
        public bool Add(HallInfo hi)
        {
            return HiDal.Insert(hi) > 0;
        }
        public bool Edit(HallInfo hi)
        {
            return HiDal.Update(hi) > 0;
        }
        public bool Remove(int Hid)
        {
            return HiDal.Delete(Hid) > 0;
        }
    }
}
