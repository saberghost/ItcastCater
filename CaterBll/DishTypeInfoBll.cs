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
    public class DishTypeInfoBll
    {
        DishTypeInfoDal dtiDal = new DishTypeInfoDal();
        public List<DishTypeInfo> GetList()
        {
            return dtiDal.GetList();
        }
        public bool Add(DishTypeInfo dti)
        {
            return dtiDal.Insert(dti) > 0;
        }
        public bool Edit(DishTypeInfo dti)
        {
            return dtiDal.Update(dti) > 0;
        }
        public bool Remove(int DId)
        {
            return dtiDal.Delete(DId) > 0;
        }
    }
}
