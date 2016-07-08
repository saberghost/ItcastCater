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
    public class DishInfoBll
    {
        DishInfoDal diDal = new DishInfoDal();
        public List<DishInfo> GetList(Dictionary<string, string> dic)
        {
            return diDal.GetList(dic);
        }
        public bool Add(DishInfo di)
        {
            return diDal.Insert(di) > 0;
        }
        public bool Edit(DishInfo di)
        {
            return diDal.Update(di) > 0;
        }
        public bool Remove(int Did)
        {
            return diDal.Delete(Did) > 0;
        }
    }
}
