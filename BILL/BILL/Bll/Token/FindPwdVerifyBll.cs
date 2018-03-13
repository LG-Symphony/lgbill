using BILL.Dal.Token;
using BILL.Models.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BILL.Bll.Token
{
    /// <summary>
    /// 找回密码验证码
    /// </summary>
    public class FindPwdVerifyBll : BaseBll<FindPwdVerify>
    {
        protected static readonly FindPwdVerifyDal<FindPwdVerify> dal = new FindPwdVerifyDal<FindPwdVerify>(ConnectionString);
        public static FindPwdVerify GetModelByEmail(string Email)
        {
            return dal.GetModelByEmail(Email);
        }
    }
}