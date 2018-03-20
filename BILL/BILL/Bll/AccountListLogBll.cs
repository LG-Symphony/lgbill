using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BILL.Dal;
using BILL.Models;

namespace BILL.Bll
{
    public class AccountListLogBll : BaseBll<AccountListLog>
    {
        protected static readonly AccountListLogDal<AccountListLog> dal = new AccountListLogDal<AccountListLog>(ConnectionString);

    }
}