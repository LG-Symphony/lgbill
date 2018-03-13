using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BILL.Dto
{
    public class UserDto
    {
        public class ModifyPasswordDto
        {
            public string Email { get; set; }
            public string OldPassword { get; set; }
            public string NewPassword { get; set; }
            public string Verify { get; set; }
            public string VerifyId { get; set; }
        }
        public class SignInDto
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public string Verify { get; set; }
            public string VerifyId { get; set; }
        }
    }
}