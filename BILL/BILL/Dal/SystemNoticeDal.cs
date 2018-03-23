using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DapperExtensions;
using BILL.Models;

namespace BILL.Dal
{
    public class SystemNoticeDal<T> : BaseDal<T> where T : SystemNotice
    {
        public SystemNoticeDal(string connectionString) : base(connectionString) { }
    }
}