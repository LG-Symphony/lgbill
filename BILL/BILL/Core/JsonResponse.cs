using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BILL.Core
{
    public class JsonResponse
    {
        public JsonResponse()
        {
            Success = true;
            LoginState = true;
            Message = "";
        }
        /// <summary>
        /// 是否成功响应
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 服务器处理消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        ///JsonString  返回的数据结果
        /// </summary>
        public string Data { get; set; }
        public bool LoginState { get; set; }
    }
}