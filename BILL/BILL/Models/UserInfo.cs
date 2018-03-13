using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BILL.Models
{
    public class UserInfo
    {
        public UserInfo() { }
        public int Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string Nickname { get; set; }
        /// <summary>
        /// 账单组
        /// </summary>
        public string AccountId { get; set; } = "";
        /// <summary>
        /// 创建账单数量
        /// </summary>
        public int CreateAccountNum { get; set; } = 0;
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateDate { get; set; } = DateTime.Now;
        /// <summary>
        /// 微信号
        /// </summary>
        public string WeChat { get; set; } = "";
        /// <summary>
        /// 手机号
        /// </summary>
        public string PhoneNumber { get; set; } = "";
        


    }
    public class UserInofoMap : ClassMapper<UserInfo>
    {
        public UserInofoMap()
        {
            Table("UserInfo");
            Map(c => c.Id).Key(KeyType.Identity);
            AutoMap();
        }

    }
}