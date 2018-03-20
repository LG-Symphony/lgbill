using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DapperExtensions.Mapper;


namespace BILL.Models
{
    public class AccountListLog
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public string OldInfo { get; set; } = "";
        public string NewInfo { get; set; }
        /// <summary>
        /// 确认过的UserId
        /// </summary>
        public string ConfirmUser { get; set; } = "";
        public LogType Type { get; set; } = LogType.Modify;
        public string Note { get; set; } = "";
    }
    public class AccountListLogMap : ClassMapper<AccountListLog>
    {
        public AccountListLogMap()
        {
            Table("AccountList");
            Map(c => c.Id).Key(KeyType.Identity);
            AutoMap();
        }
    }
    public enum LogType
    {
        Modify = 1,
        Delete = 2
    }
}