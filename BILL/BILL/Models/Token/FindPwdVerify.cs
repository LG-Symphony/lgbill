using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BILL.Models.Token
{
    public class FindPwdVerify
    {
        /// <summary>
        /// 用户Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 最后一次验证时间
        /// </summary>
        public DateTime StartTime { get; set; }

    }
    public class FindPwdVerifyMap : ClassMapper<FindPwdVerify>
    {
        public FindPwdVerifyMap()
        {
            Table("FindPwdVerify");
            Map(c => c.Email).Key(KeyType.Assigned);
            AutoMap();
        }

    }
}