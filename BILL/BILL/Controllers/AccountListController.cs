using BILL.Bll;
using BILL.Core;
using BILL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BILL.Controllers
{
    public class AccountListController : BaseController
    {
        /// <summary>
        /// 返回账单所有使用者
        /// </summary>
        /// <param name="Code">账单Code</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResponse AccountUserList([FromUri] string Code)
        {
            AccountList model = new AccountList();
            model = AccountListBll.GetModelByCode(Code);
            string[] sArray = model.AllUserId.Split(',');
            var good = UserInfoBll.GetListByIdList(sArray);
            
            return OkResponse(null, "添加成功！");
        }
    }
}
