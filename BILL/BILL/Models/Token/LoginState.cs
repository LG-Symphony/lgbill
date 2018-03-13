using DapperExtensions.Mapper;
using System;


namespace BILL.Models.Token
{
    public class LoginState
    {
        /// <summary>
        /// 用户Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 最后一次验证时间
        /// </summary>
        public DateTime StartTime { get; set; }
    }
    public class LoginStateMap : ClassMapper<LoginState>
    {
        public LoginStateMap()
        {
            Table("LoginState");
            Map(c => c.Email).Key(KeyType.Assigned);
            AutoMap();
        }

    }
}