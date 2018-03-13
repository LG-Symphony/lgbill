using BILL.Bll;
using BILL.Bll.Token;
using BILL.Core;
using BILL.Models;
using BILL.Models.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static BILL.Dto.UserDto;

namespace BILL.Controllers
{
    public class UserController : BaseController
    {
        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResponse SingUp([FromBody] UserInfo model)
        {
            if (model.Email == null || model.Password == null || model.Nickname == null )
            {
                return BadResponse("参数提供不完整");
            }
            //检查邮箱是否可用
            JsonResponse emailModel = CheckUser(model.Email);
            if (!emailModel.Success)
            {
                return BadResponse(emailModel.Message);
            }
            //检查昵称是否可用
            JsonResponse nicknameModel = CheckUser(model.Email);
            if (!nicknameModel.Success)
            {
                return BadResponse(nicknameModel.Message);
            }
            //密码加密
            model.Password = PasswordHelper.PwdStrToHashStr(model.Password);
            //写入数据库
            if (UserInfoBll.Insert(model))
            {
                return OkResponse(null);
            }
            else
            {
                return BadResponse("注册失败，请重试");
            }
        }
        /// <summary>
        /// 检查邮箱是否注册
        /// </summary>
        /// <param name="Email">邮箱</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResponse CheckUser([FromUri] string Email)
        {
            if (UserInfoBll.CheckUser(Email))
            {
                return BadResponse("邮箱已被注册");
            }
            else
            {
                return OkResponse(null,"邮箱可用");
            }
        }
        /// <summary>
        /// 检查昵称是否注册
        /// </summary>
        /// <param name="Nickname">昵称</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResponse CheckNickname([FromUri] string Nickname)
        {
            if (UserInfoBll.CheckNickname(Nickname))
            {
                return BadResponse("昵称已被注册");
            }
            else
            {
                return OkResponse(null,"昵称可用");
            }
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResponse ModifyPassword([FromBody] ModifyPasswordDto dto)
        {
            if (dto.Email == null|| dto.OldPassword ==null|| dto.NewPassword ==null|| dto.Verify ==null|| dto.VerifyId ==null)
            {
                return BadResponse("参数提供不完整");
            }
            //判断用户是否登录
            if (!CheckLogin(dto.Email).Success)
            {
                return BadResponse("用户未登录");
            }
            //判断验证码是否输入正确
            if (!CheckVerify(dto.VerifyId, dto.Verify).Success)
            {
                return BadResponse("验证码错误");
            }
            //判断用户是否存在
            UserInfo model = new UserInfo();
            model = UserInfoBll.GetModelByEmail(dto.Email);
            if (model == null)
            {
                return BadResponse("用户不存在");
            }
            //新密码加密
            dto.NewPassword = PasswordHelper.PwdStrToHashStr(dto.NewPassword);
            //判断新旧密码是否相同
            if (dto.NewPassword == model.Password)
            {
                return BadResponse("旧密码与新密码相同");
            }
            //若不相同新密码写入数据库
            model.Password = dto.NewPassword;
            if (!UserInfoBll.Update(model))
            {
                return BadResponse("网络错误，请重试");
            }
            return OkResponse(null,"密码修改成功");
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResponse SignIn([FromBody] SignInDto dto)
        {
            if (dto.Email == null ||  dto.Password == null ||  dto.Verify == null || dto.VerifyId == null)
            {
                return BadResponse("参数提供不完整");
            }
            //检查验证码是否正确
            if (!CheckVerify(dto.VerifyId, dto.Verify).Success)
            {
                return BadResponse(null, "验证码错误");
            }
            //检查用户名密码是否正确
            UserInfo model = new UserInfo();
            model = UserInfoBll.GetModelByEmail(dto.Email);
            if (model == null)
            {
                return BadResponse(null, "用户不存在");
            }
            //检查用户是否登录，若有登录信息则刷新时间
            //判断用户是否登录
            if (!CheckLogin(dto.Email).Success)
            {
                LoginState loginState = new LoginState
                {
                    Email = model.Email,
                    StartTime = DateTime.Now
                };
                LoginStateBll.Insert(loginState);
            }
            return OkResponse(null);
        }
        /// <summary>
        /// 登出
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResponse SignOut([FromUri] string Email)
        {
            //注销用户登陆状态
            TokenHelper.ClearLoginStateByEmail(Email);
            return OkResponse(null);
        }
    }
}
