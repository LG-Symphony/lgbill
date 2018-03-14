using BILL.Bll.Token;
using BILL.Models.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BILL.Core
{
    public class TokenHelper
    {
        /// <summary>
        /// 刷新用户登录状态
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static void WriteLoginStateTokenByUserId(string UserId)
        {
            //若已登录，则刷新时间
            if (!CheckLoginStateByUserId(UserId))
            {
                LoginState loginState = new LoginState
                {
                    UserId = UserId,
                    StartTime = DateTime.Now
                };
                LoginStateBll.Insert(loginState);
            }
        }
        /// <summary>
        /// 注销用户登陆状态
        /// </summary>
        /// <param name="Id"></param>
        public static void ClearLoginStateByUserId(string UserId)
        {
            LoginStateBll.ExecuteSql("delete from LoginState where UserId ='" + UserId + "'");
        }
        /// <summary>
        /// 检查用户登陆状态
        /// </summary>
        /// <param name="Email">用户邮箱</param>
        /// <returns></returns>
        public static bool CheckLoginStateByUserId(string UserId)
        {
            LoginState loginToken = LoginStateBll.GetModelByUserId(UserId);
            //若已登录，则刷新时间
            if (loginToken != null && (DateTime.Now - loginToken.StartTime).Minutes < 30)
            {
                loginToken.StartTime = DateTime.Now;
                LoginStateBll.Update(loginToken);
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 校验验证码
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Verify"></param>
        /// <returns></returns>
        public static bool CheckVerify(string Id, string Verify)
        {
            Verify model = VerifyBll.GetModelById(Id);
            var verify = Verify.ToLower();
            if((DateTime.Now - model.StartTime).Minutes > 10)
            {
                return false;
            }
            if (model.Code == verify)
            {
                // VerifyBll.ExecuteSql("delete from Verify where Id = " + model.Id);
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 写入验证码
        /// </summary>
        /// <param name="OldId"></param>
        /// <param name="NewId"></param>
        /// <param name="Code"></param>
        public static void WriteVerifyToken(string OldId, string NewId,string Code)
        {
            VerifyBll.ExecuteSql("delete from Verify where Id = '" + OldId +"'");
            Verify model = new Verify()
            {
                Id = NewId,
                Code = Code.ToLower(),
                StartTime = DateTime.Now
            };
            try
            {
                VerifyBll.Insert(model);
            }
            catch(Exception ex)
            {
                var good = ex;
            }
           
        }
    }
}