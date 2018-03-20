using BILL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BILL.Dal;

namespace BILL.Bll
{
    public class AccountLogBll : BaseBll<AccountLog>
    {
        protected static readonly AccountLogDal<AccountLog> dal = new AccountLogDal<AccountLog>(ConnectionString);

    }
}