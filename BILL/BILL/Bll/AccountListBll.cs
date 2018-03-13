using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BILL.Dal;
using BILL.Models;

namespace BILL.Bll
{
    public class AccountListBll : BaseBll<AccountList>
    {
        protected static readonly AccountListDal<AccountList> dal = new AccountListDal<AccountList>(ConnectionString);
        public static AccountList GetModelByCode(string Code)
        {
            return dal.GetModelByCode(Code);
        }
    }
}