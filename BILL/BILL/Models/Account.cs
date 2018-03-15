using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BILL.Models
{
    public class Account
    {
        public int Id { get; set; }
        /// <summary>
        /// 记账人Id
        /// </summary>
        public string RecorderId { get; set; }
        /// <summary>
        /// 使用人Id（用，分开）
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 记账日期
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public float Money { get; set; }
        /// <summary>
        /// 消费类别Name
        /// </summary>
        public int Category { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }
    }
}