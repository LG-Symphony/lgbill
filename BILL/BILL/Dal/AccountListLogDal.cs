using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BILL.Models;
using DapperExtensions;
namespace BILL.Dal
{
    public class AccountListLogDal<T> : BaseDal<T> where T : AccountListLog
    {
        public AccountListLogDal(string connectionString) : base(connectionString) { }
    }
}