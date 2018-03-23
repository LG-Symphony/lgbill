using BILL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BILL.Dal;

namespace BILL.Bll
{
    public class SystemNoticeBll : BaseBll<SystemNotice>
    {
        protected static readonly SystemNoticeDal<SystemNotice> dal = new SystemNoticeDal<SystemNotice>(ConnectionString);

    }
}