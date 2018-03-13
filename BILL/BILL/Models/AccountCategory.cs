using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DapperExtensions.Mapper;

namespace BILL.Models
{
    public class AccountCategory
    {
        public AccountCategory() { }
        public int Id { get; set; }
        /// <summary>
        /// 类别名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 创建者Id
        /// </summary>
        public int CreateUserId { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
    public class AccountCategoryMap : ClassMapper<AccountCategory>
    {
        public AccountCategoryMap()
        {
            Table("AccountList");
            Map(c => c.Id).Key(KeyType.Identity);
            AutoMap();
        }
    }
}