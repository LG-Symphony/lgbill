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
<<<<<<< HEAD
                        //查询出添加人Id和使用人Id
=======

>>>>>>> c7efe56b143be71103f37232293d56930b80b5d6
                //}
                sql += ("'" + model.RecorderId + "'," + "'" + model.RecorderId + "'," );

                sql += ")";
            }
            return OkResponse(null, "添加成功！");
        }
    }
}
