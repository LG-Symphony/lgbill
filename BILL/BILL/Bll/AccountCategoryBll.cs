using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BILL.Dal;
using BILL.Models;

namespace BILL.Bll
{
    public class AccountCategoryBll : BaseBll<AccountCategory>
    {
        protected static readonly AccountCategoryDal<AccountCategory> dal = new AccountCategoryDal<AccountCategory>(ConnectionString);
        /// <summary>
        /// 根据创建者返回类别列表
        /// </summary>
        /// <param name="CreateUserId"></param>
        /// <returns></returns>
        public static IList<AccountCategory> GetListByCreateUserId(string CreateUserId)
        {
            return dal.GetListByCreateUserId(CreateUserId);
        }
        /// <summary>
        /// 根据Id返回Model
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        //public static AccountCategory GetModelById(int Id)
        //{
        //    return dal.GetModelById(Id);
        //}

        
        

        /// <summary>
        /// 根据Name返回Model
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static AccountCategory GetModelByName(string Name)
        {
            return dal.GetModelByName(Name);
        }
        /// <summary>
        /// 返回所有公开展示的类别
        /// </summary>
        /// <returns></returns>
        public static IList<AccountCategory> GetListByIsShowEqTrue()
        {
            return dal.GetListByIsShowEqTrue();
        }
    }
}