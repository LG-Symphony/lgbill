using BILL.Core;
using BILL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BILL.Controllers
{
    public class AccountCategoryController : BaseController
    {
        [HttpPost]
        public JsonResponse AddCategory([FromBody] AccountCategoryDto dto)
        {
            //dto.
            return OkResponse(null);
        }
    }
}
