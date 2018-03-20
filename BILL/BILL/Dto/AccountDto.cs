using BILL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BILL.Dto
{
    public class AccountDto : BaseDto
    {
        public List<Account> List { get; set; }
        public string TableName { get; set; }
    }
    public class AccountListAllUserDto
    {
        public string UserId { get; set; }
        public string NickName { get; set; }
       
    }
    public class AccountListDto : BaseDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 所有人员id以逗号分割，最后一个逗号不省略
        /// </summary>
        public string AllUserId { get; set; }
    }
}