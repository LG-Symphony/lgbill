using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BILL.Dto
{
    /// <summary>
    /// 登录
    /// </summary>
    public class SignInDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Verify { get; set; }
        public string VerifyId { get; set; }
    }
}