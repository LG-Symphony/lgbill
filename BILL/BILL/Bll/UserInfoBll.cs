using BILL.Dal;
using BILL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BILL.Bll
{
    public class UserInfoBll : BaseBll<UserInfo>
    {
        protected static readonly UserInfoDal<UserInfo> dal = new UserInfoDal<UserInfo>(ConnectionString);
        /// <summary>
        /// 根据用户ID返回用户信息
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public static UserInfo GetModelByEmail(string Email)
        {
            return dal.GetModelByEmail(Email);
        }
        public static UserInfo GetModelById(string Id)
        {
            return dal.GetModelById(Id);
        }
        /// <summary>
        /// 检查邮箱是否被注册
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        public static bool CheckUser(string Email)
        {
            //不存在该用户
            if (GetModelByEmail(Email) == null)
            {
                return false;
            }
            //存在该用户
            return true;
        }
        /// <summary>
        /// 检查改昵称是否被注册
        /// </summary>
        /// <param name="Nickname"></param>
        /// <returns></returns>
        public static bool CheckNickname(string Nickname)
        {
            //不存在该昵称
            if (dal.GetModelByNickname(Nickname) == null)
            {
                return false;
            }
            //存在该昵称
            return true;
        }
    }

}