using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DapperExtensions.Mapper;

namespace BILL.Models
{
    public class SystemNotice
    {
        public SystemNotice() { }
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public bool Confirm { get; set; } = false;

    }
    public class SystemNoticeMap : ClassMapper<SystemNotice>
    {
        public SystemNoticeMap()
        {
            Table("UserInfo");
            Map(c => c.Id).Key(KeyType.Identity);
            AutoMap();
        }

    }
}