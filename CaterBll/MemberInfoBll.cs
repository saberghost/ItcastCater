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
    public class MemberInfoBll
    {
        MemberInfoDal miDal;
        public MemberInfoBll()
        {
            miDal = new MemberInfoDal();
        }
        public List<MemberInfo> GetList(Dictionary<string, string> dic)
        {
            return miDal.GetList(dic);
        }
        public bool Add(MemberInfo mi)
        {
            return miDal.Insert(mi) > 0;
        }
        public bool Edit(MemberInfo mi)
        {
            return miDal.Update(mi) > 0;
        }
        public bool Remove(int Mid)
        {
            return miDal.Delete(Mid) > 0;
        }
    }
}
