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
    public class AccountListController : BaseController
    {
        /// <summary>
        /// 新增账单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResponse AddAccount([FromBody] AccountListDto dto)
        {
            //判断用户是否登录
            if (!TokenHelper.CheckLoginStateByUserId(dto.UserId))
            {
                return BadResponse("用户未登录",null,false);
            }
            string accountName = "我的手账";
            //若用户没有写账单名则默认为“我的手账”；若有“我的手账”则命名为“我的手账1”
            if (dto.Name != null && dto.Name != "")
            {
                List<AccountList> accountList = AccountListBll.GetListByCreateUserId(dto.UserId).ToList();
                int flag = 1;
                for (int i = 0; i < accountList.Count; i++)
                {
                    if(accountList[i].Name == accountName)
                    {
                        accountName = accountName + flag.ToString();
                        flag++;
                    }
                }
            }
            else{
                accountName = dto.Name;
            }
            //往AccountList写账单基本信息
            DateTime time = DateTime.Now;
            AccountList model = new AccountList
            {
                AllUserId = dto.AllUserId,
                Code = "Z" + (time.Year - 2000).ToString() + time.Month.ToString() + time.Day.ToString()
                + TokenHelper.GetRandomString(3, false, true, true, false, "") + TokenHelper.GetRandomString(5, true, true, true, false, ""),
                CreateDate = time,
                CreateUserId = dto.UserId,
                Member = dto.AllUserId.Count(o => o.Equals(",")),
                Name = accountName
            };
            //新建账单表
            if (BaseBll<AccountList>.ExecuteSql("exec proc_CreateAccountTable '" + model.Code + "'") 
                && AccountListBll.Insert(model))
            {
                return OkResponse(null, "添加成功！");
            }
            else
            {
                return BadResponse("添加失败！", null);
            }
            
        }
        /// <summary>
        /// 返回账单所有使用者
        /// </summary>
        /// <param name="Code">账单Code</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResponse AccountUserList([FromUri] string Code,string UserId)
        {
            //判断用户是否登录
            if (!TokenHelper.CheckLoginStateByUserId(UserId))
            {
                return BadResponse("用户未登录", null, false);
            }
            AccountList model = new AccountList();
            model = AccountListBll.GetModelByCode(Code);
            string[] allUserIdArray = model.AllUserId.Split(',');
            List<UserInfo> AllUserList = UserInfoBll.GetListByIdList(allUserIdArray).ToList();

            List<AccountListAllUserDto> returnList = new List<AccountListAllUserDto>();
            foreach (var user in AllUserList)
            {
                AccountListAllUserDto userModel = new AccountListAllUserDto
                {
                    NickName = user.Nickname,
                    UserId = user.Id
                };
                returnList.Add(userModel);
            }
            return OkResponse(returnList, "添加成功！");
        }
    }
}
