using BILL.Bll;
using BILL.Core;
using BILL.Dto;
using BILL.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
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
        public JsonResponse AddAccountList([FromBody] AccountListDto dto)
        {
            //判断用户是否登录
            if (!TokenHelper.CheckLoginStateByUserId(dto.UserId))
            {
                return BadResponse("用户未登录", null, false);
            }
            string accountName = "我的手账";
            //若用户没有写账单名则默认为“我的手账”；若有“我的手账”则命名为“我的手账1”
            if (dto.Name != null && dto.Name != "")
            {
                List<AccountList> accountList = AccountListBll.GetListByCreateUserId(dto.UserId).ToList();
                int flag = 1;
                for (int i = 0; i < accountList.Count; i++)
                {
                    if (accountList[i].Name == accountName)
                    {
                        accountName = (accountName + flag.ToString());
                        flag++;
                    }
                }
            }
            else
            {
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
        public JsonResponse AccountUserList([FromUri] string Code, string UserId)
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

        /// <summary>
        /// 修改账单基本信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResponse ModifyAccountListInfo([FromBody] AccountListDto dto)
        {
            //判断用户是否登录
            if (!TokenHelper.CheckLoginStateByUserId(dto.UserId))
            {
                return BadResponse("用户未登录", null, false);
            }
            AccountList model = new AccountList();
            model = AccountListBll.GetModelByCode(dto.Code);
            if (model == null)
            {
                return BadResponse("该账单不存在", null);
            }
            if (dto.Name != null && dto.Name != "" && dto.Name != null && dto.Name != "")
            {
                return BadResponse("参数提供不完整", null);
            }
            //修改名字
            if (dto.Name != null && dto.Name != "")
            {
                AccountListLog log = new AccountListLog
                {
                    Code = dto.Code,
                    OldInfo = "账单名：" + model.Name,
                    NewInfo = "账单名：" + dto.Name
                };
                AccountListLogBll.Insert(log);
                model.Name = dto.Name;
            }
            if (dto.AllUserId != null && dto.AllUserId != "")
            {
                //根据AllUserId返回昵称
                AccountListLog log = new AccountListLog();
                log.Code = dto.Code;
                string sql = "proc_GetNicknameByUserId";
                //Old
                DynamicParameters p = new DynamicParameters();
                p.Add("@UserId", model.AllUserId, DbType.String);
                List<UserInfo> oldList = new List<UserInfo>();
                oldList.AddRange(SqlHelper.QuerySP<UserInfo>(sql, p).ToList());
                log.OldInfo = "账单使用人：";
                foreach (UserInfo user in oldList)
                {
                    log.OldInfo += (user.Nickname + ";");
                }
                //New
                p = new DynamicParameters();
                p.Add("@UserId", dto.AllUserId, DbType.String);
                List<UserInfo> newList = new List<UserInfo>();
                newList.AddRange(SqlHelper.QuerySP<UserInfo>(sql, p).ToList());
                log.OldInfo = "账单使用人：";
                foreach (UserInfo user in newList)
                {
                    log.NewInfo += (user.Nickname + ";");
                }
                //Note
                //del
                string delName = "";
                bool have = false;
                for (var i = 0; i < oldList.Count; i++)
                {
                    have = false;
                    for (var j = 0; j < newList.Count; j++)
                    {
                        if (oldList[i] == newList[j])
                        {
                            have = true;
                            break;
                        }
                    }
                    if (!have)
                    {
                        delName += oldList[i].Nickname;
                    }
                }
                if (delName != "")
                {
                    log.Note += ("移除成员：" + delName);
                }
                //add
                string addName = "";
                for (var i = 0; i < newList.Count; i++)
                {
                    have = false;
                    for (var j = 0; j < oldList.Count; j++)
                    {
                        if (newList[i] == oldList[j])
                        {
                            have = true;
                            break;
                        }
                    }
                    if (!have)
                    {
                        addName += oldList[i].Nickname;
                    }
                }
                if (addName != "")
                {
                    log.Note += ("新增成员：" + addName);
                }
                AccountListLogBll.Insert(log);
                model.AllUserId = dto.AllUserId;
            }
            if (AccountListBll.Update(model))
            {
                return OkResponse(null, "修改成功!");
            }
            else
            {
                return BadResponse("修改失败!", null);
            }
        }
        /// <summary>
        /// 删除账单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public JsonResponse DeleteAccountList([FromBody] AccountListDto dto)
        {
            //判断用户是否登录
            if (!TokenHelper.CheckLoginStateByUserId(dto.UserId))
            {
                return BadResponse("用户未登录", null, false);
            }
            //获取账单基本信息
            //通知账单成员
            //写入操作记录
            //（若都确认后、自动删除该表，每一个人确认时都查看此人是否为最后确认的人、若是、则直接删除账单
            //否则十五天后数据库定时作业会删除该表）
            return OkResponse(null);
        }
    }
}
