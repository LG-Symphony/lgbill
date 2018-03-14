using BILL.Dal.Token;
using BILL.Models.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BILL.Bll.Token
{
    /// <summary>
    /// 用户登录信息的Session
    /// </summary>
    public class LoginStateBll : BaseBll<LoginState>
    {
        protected static readonly LoginStateDal<LoginState> dal = new LoginStateDal<LoginState>(ConnectionString);
        public static LoginState GetModelByUserId(string UserId)
        {
            return dal.GetModelByUserId(UserId);
        }
    }
}