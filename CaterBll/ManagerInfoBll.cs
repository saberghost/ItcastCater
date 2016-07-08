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
    public class ManagerInfoBll
    {
        ManagerInfoDal miDal = new ManagerInfoDal();
        public List<ManagerInfo> GetList()
        {
            return miDal.GetList();
        }
        public bool Add(ManagerInfo mi)
        {
            return miDal.Insert(mi)>0;
        }
        public bool Edit(ManagerInfo mi)
        {
            return miDal.Update(mi)>0;
        }
        public bool Remove(int Mid)
        {
            return miDal.Delete(Mid)>0;
        }
        public LoginState Login(string name,string pwd,out int type)
        {
            ManagerInfo mi= miDal.GetByName(name);
            type = 0;
            if (mi == null)
            {
                return LoginState.NameError;
            }
            else
            {
                if (mi.Mpwd == Md5Helper.GetMd5(pwd))
                {
                    type = mi.Mtype;
                    return LoginState.Ok;
                }
                else
                {
                    return LoginState.PwdError;
                }
            }
        }
    }
}
