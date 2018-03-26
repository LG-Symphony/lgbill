using BILL.Bll;
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
        public JsonResponse AddAccount([FromBody] AccountDto dto)
        {
            //判断用户是否登录
            if (!TokenHelper.CheckLoginStateByUserId(dto.UserId))
            {
                return BadResponse("用户未登录", null, false);
            }
            string sql = "INSERT INTO " + dto.TableName + " VALUES ";
            foreach (Account model in dto.List)
            {
                //(
                sql += "(";
                //RecorderId
                sql += ("'" + model.RecorderId + "',");
                //UserId
                sql += ("'" + model.UserId + "',");
                //CreateDate
                sql += ("'" + DateTime.Now + "',");
                //Money
                sql += ("" + model.Money + ",");
                //Category
                sql += ("'" + model.Category + "',");
                //Note
                sql += ("'" + model.Note + "'");
                //)
                sql += "),";
            }
            sql = sql.Substring(0, sql.Length - 1);
            if (!BaseBll<Account>.ExecuteSql(sql))
            {
                BadResponse("添加失败！");
            }
            return OkResponse(null, "添加成功！");
        }
        //
        //修改账目，新建一张修改记录表
        [HttpPost]
        public JsonResponse ModifyAccount([FromBody] AccountDto dto)
        {

        }
    }
}
