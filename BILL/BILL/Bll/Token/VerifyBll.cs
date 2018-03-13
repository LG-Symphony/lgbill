using BILL.Dal.Token;
using BILL.Models.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BILL.Bll.Token
{
    /// <summary>
    /// 验证码Session
    /// </summary>
    public class VerifyBll : BaseBll<Verify>
    {
        protected static readonly VerifyDal<Verify> dal = new VerifyDal<Verify>(ConnectionString);
        public static Verify GetModelById(string Id)
        {
            return dal.GetModelById(Id);
        }
    }
}