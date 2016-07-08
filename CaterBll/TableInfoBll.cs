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
    public class TableInfoBll
    {
        TableInfoDal tiDal = new TableInfoDal();
        public List<TableInfo> GetList(Dictionary<string,string> dic)
        {
            return tiDal.GetList(dic);
        }
        public bool Add(TableInfo ti)
        {
            return tiDal.Insert(ti) > 0;
        }
        public bool Edit(TableInfo ti)
        {
            return tiDal.Update(ti) > 0;
        }
        public bool Remove(int TId)
        {
            return tiDal.Delete(TId)>0;
        }
    }
}
