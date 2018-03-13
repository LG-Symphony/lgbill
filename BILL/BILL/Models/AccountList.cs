using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DapperExtensions.Mapper;

namespace BILL.Models
{
    public class AccountList
    {
        public AccountList() { }
        /// <summary>
        /// 账单Id-->此为表名（用户ID_5位随机数）
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 创建者
        /// </summary>
        public int CreateUserId { get; set; }
        /// <summary>
        /// 所有使用者
        /// </summary>
        public string AllUserId { get; set; } = "";
        /// <summary>
        /// 账单名
        /// </summary>
        public string Name { get; set; } = "";
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public int Member { get; set; }
    }
    public class AccountListMap : ClassMapper<AccountList>
    {
        public AccountListMap()
        {
            Table("AccountList");
            Map(c => c.Code).Key(KeyType.Assigned);
            AutoMap();
        }
    }
}