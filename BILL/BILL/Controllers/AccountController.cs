using BILL.Core;
using BILL.Dto;
using BILL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BILL.Controllers
{
    public class AccountController : BaseController
    {
        /// <summary>
        /// 添加账目--->计划用存储过程
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResponse AddAccount([FromBody] AccountListDto dto)
        {
            string sql = "INSERT INTO " + dto.TableName + " VALUES ";
            foreach (Account model in dto.List)
            {
                sql += "(";
                //foreach ()
                //{

                //}
                sql += ("'" + model.RecorderId + "'," + "'" + model.RecorderId + "'," );

                sql += ")";
            }
            return OkResponse(null, "添加成功！");
        }
    }
}
