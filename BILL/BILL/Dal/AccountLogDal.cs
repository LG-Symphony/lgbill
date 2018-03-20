using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BILL.Models;
using DapperExtensions;

namespace BILL.Dal
{
    public class AccountLogDal<T> : BaseDal<T> where T : AccountLog
    {
        public AccountLogDal(string connectionString) : base(connectionString) { }

    }
}