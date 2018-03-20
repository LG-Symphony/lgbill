using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DapperExtensions.Mapper;

namespace BILL.Models
{
    public class AccountLog
    {
        public int Id { get; set; }
        public string AccountListCode { get; set; }
        public int AccountId { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public string ModifyUserId { get; set; }
        public string OldInfo { get; set; } = "";
        public string NewInfo { get; set; }
        public string Note { get; set; } = "";
    }
    public class AccountLogMap : ClassMapper<AccountLog>
    {
        public AccountLogMap()
        {
            Table("AccountList");
            Map(c => c.Id).Key(KeyType.Identity);
            AutoMap();
        }
    }
}