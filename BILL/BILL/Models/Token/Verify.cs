using DapperExtensions.Mapper;
using System;

namespace BILL.Models.Token
{
    public class Verify
    {
        /// <summary>
        /// 随机生成的Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 最后一次验证时间
        /// </summary>
        public DateTime StartTime { get; set; }
        
    }
    public class VerifyMap : ClassMapper<Verify>
    {
        public VerifyMap()
        {
            Table("Verify");
            Map(c => c.Id).Key(KeyType.Assigned);
            AutoMap();
        }

    }
}