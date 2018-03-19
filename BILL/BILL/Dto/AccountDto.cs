using BILL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BILL.Dto
{
    public class AccountListDto
    {
        public List<Account> List { get; set; }
        public string TableName { get; set; }
    }
    public class AccountListAllUserDto
    {
        public string UserId { get; set; }
        public string NickName { get; set; }
       
    }
}