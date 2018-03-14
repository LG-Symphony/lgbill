using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BILL.Dto
{
    /// <summary>
    /// 修改密码
    /// </summary>
    public class ModifyPasswordDto : BaseDto
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string Verify { get; set; }
        public string VerifyId { get; set; }
    }
}