using BILL.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace BILL.Controllers
{
    public class BaseController : ApiController
    {
        
        /// <summary>
        /// 响应成功
        /// </summary>
        /// <param name="data"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        protected JsonResponse OkResponse(object data, string msg = "请求成功执行")
        {
            JsonResponse response = new JsonResponse();
            response.Message = msg;
            response.Data = data.ToJson();

            return response;
        }

        /// <summary>
        /// 响应失败
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        protected JsonResponse BadResponse(string msg, object data = null,bool loginState = true)
        {

            JsonResponse response = new JsonResponse();
            response.Success = false;
            response.Message = msg;
            response.LoginState = loginState;
            response.Data = data.ToJson();
            return response;
        }
        /// <summary>
        /// 获取访问者IP地址
        /// </summary>
        /// <returns></returns>
        protected string GetClientAddress()
        {
            string userIP = "127.0.0.1";

            try
            {
                if (System.Web.HttpContext.Current == null)
                    return "";

                string CustomerIP = "";

                //CDN加速后取到的IP 
                CustomerIP = System.Web.HttpContext.Current.Request.Headers["Cdn-Src-Ip"];
                if (!string.IsNullOrEmpty(CustomerIP))
                {
                    return CustomerIP;
                }

                CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];


                if (!string.IsNullOrEmpty(CustomerIP))
                    return CustomerIP;

                if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                {
                    CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (CustomerIP == null)
                        CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                else
                {
                    CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

                }

                if (String.Compare(CustomerIP, "unknown", StringComparison.OrdinalIgnoreCase) == 0)
                    return System.Web.HttpContext.Current.Request.UserHostAddress;
                return CustomerIP;
            }
            catch
            {
                //Logger.LogDebug("获取访问ip失败");
            }

            return userIP;
        }
        /// <summary>
        /// 判断验证码是否输入正确
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Verify"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResponse CheckVerify(string Id, string Verify)
        {
            if (TokenHelper.CheckVerify(Id, Verify))
            {
                return OkResponse(null);
            }
            else
            {
                return BadResponse("验证码错误",null);
            }
        }
        /// <summary>
        /// 判断用户是否登录
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResponse CheckLogin(string UserId)
        {
            if(!TokenHelper.CheckLoginStateByUserId(UserId))
            {
                return BadResponse("用户未登录",null, false);
            }
            else
            {
                return OkResponse(null);
            }
        }
        /// <summary>
        /// 获取验证码图片
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage Verify(string OldId, string Id)
        {
            string code;
            Bitmap bmp = VerifyCodeHelper.CreateVerifyCodeBmp(out code);
            TokenHelper.WriteVerifyToken(OldId, Id, code);
            Bitmap newbmp = new Bitmap(bmp, 108, 36);
            MemoryStream ms = new MemoryStream();
            newbmp.Save(ms, ImageFormat.Gif);
            byte[] data = new byte[ms.Length];
            ms.Seek(0, SeekOrigin.Begin);
            ms.Read(data, 0, Convert.ToInt32(ms.Length));
            var resp = new HttpResponseMessage(HttpStatusCode.OK)
            { Content = new ByteArrayContent(data) };
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("image/gif");
            return resp;
        }
    }
}