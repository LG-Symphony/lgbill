using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BILL.Core
{
    public class SystemNoticeHelper
    {
        /// <summary>
        /// 写入通知表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string InsertNotice(string userId,string content)
        {
            string[] userIdArray = userId.Split(',');
            string sql = "INSERT INTO SystemNotice ([UserId],[Content]) VALUES ";
            foreach (string id in userIdArray)
            {
                sql += ("('"+ id+ "','"+ content+ "'),");
            }
            sql = sql.Substring(0, sql.Length - 1);
            return sql;
        }
    }
}