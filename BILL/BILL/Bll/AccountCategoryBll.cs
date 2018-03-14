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
        public static AccountCategory GetModelById(int Id)
        {
            return dal.GetModelById(Id);
        }
    }
}