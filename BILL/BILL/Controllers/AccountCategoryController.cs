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
    public class AccountCategoryController : BaseController
    {
        /// <summary>
        /// 添加分类
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResponse AddCategory([FromBody] AccountCategoryDto dto)
        {
            //查询有没有
            var model = AccountCategoryBll.GetModelByName(dto.Name);
            //有的话则UserNum+1
            if (model != null)
            {
                model.UserNum = model.UserNum + 1;
                if (!AccountCategoryBll.Update(model))
                {
                    return BadResponse("网络错误，请重试！");
                }
            }
            //没有则添加
            else
            {
                model = new AccountCategory
                {
                    Name = dto.Name,
                    CreateUserId = dto.UserId
                };
                if (!AccountCategoryBll.Insert(model))
                {
                    return BadResponse("网络错误，请重试！");
                }
            }
            //将Category的Id添加到UserInfo的Category字段中
            model = AccountCategoryBll.GetModelByName(dto.Name);
            var user = UserInfoBll.GetModelById(dto.UserId);
            if (user == null)
            {
                return BadResponse("用户信息出错，请重试！");
            }
            user.Category += (model.Name + ",");
            if (!UserInfoBll.Update(user))
            {
                return BadResponse("网络错误，请重试！");
            }
            return OkResponse(null, "添加成功！");
        }
        /// <summary>
        /// 从个人偏好中删除分类
        /// </summary>
        [HttpPost]
        public JsonResponse DeleteUserCategory([FromBody] AccountCategoryDto dto)
        {
            //查询该类别的Id
            var categoryModel = AccountCategoryBll.GetModelByName(dto.Name);
            //从UserInfo中的Category字段里删除该类别
            var userModel = UserInfoBll.GetModelById(dto.UserId);
            if (userModel == null)
            {
                return BadResponse("用户信息出错，请重试！");
            }
            userModel.Category = userModel.Category.Replace((categoryModel.Name + ","), "");
            if (!UserInfoBll.Update(userModel))
            {
                return BadResponse("网络错误，请重试！");
            }
            if (categoryModel == null)
            {
                //此时代表数据库出问题了，应作出处理-----------------------------------------------------------------
                return OkResponse(null, "删除成功！");
            }
            //类别表中该类别使用数-1,若使用数为0则删除该类别
            if (categoryModel.UserNum == 1)
            {
                AccountCategoryBll.ExecuteSql("delete from AccountCategory where Name='" + categoryModel.Name+"'");
            }
            else
            {
                categoryModel.UserNum--;
                if (AccountCategoryBll.Update(categoryModel))
                {
                    return BadResponse("网络错误，请重试！");
                }
            }

            return OkResponse(null, "删除成功！");
        }

        /// <summary>
        /// 修改分类名
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResponse ChangeCategoryName([FromBody] ChangeCategoryNameDto dto)
        {
            //获取更改者信息
            var userModel = UserInfoBll.GetModelById(dto.UserId);
            if (userModel == null)
            {
                return BadResponse("用户信息出错，请重试！");
            }
            //查询使用人数
            var categoryModel = AccountCategoryBll.GetModelByName(dto.OldName);
            if(categoryModel == null)
            {
                //此时代表数据库出问题了，应作出处理-----------------------------------------------------------------
                return BadResponse("分类信息出错，请重试！");
            }
            //若使用人数为1则直接更新名字
            if (categoryModel.UserNum <= 1)
            {
                categoryModel.Name = dto.NewName;
                if (!AccountCategoryBll.Update(categoryModel))
                {
                    return BadResponse("网络错误，请重试！");
                }
            }
            //若有他人使用则新建一条，原条目使用人数-1，将UserInfo中的Category字段更换为新Id
            else
            {
                categoryModel.UserNum--;
                if (!AccountCategoryBll.Update(categoryModel))
                {
                    return BadResponse("网络错误，请重试！");
                }
                categoryModel = new AccountCategory
                {
                    Name = dto.NewName,
                    CreateUserId = dto.UserId
                };
                if (!AccountCategoryBll.Insert(categoryModel))
                {
                    return BadResponse("网络错误，请重试！");
                }
            }
            userModel.Category = userModel.Category.Replace(dto.OldName, dto.NewName);
            if (!UserInfoBll.Update(userModel))
            {
                return BadResponse("网络错误，请重试！");
            }
            return OkResponse(null, "修改成功！");
        }

        /// <summary>
        /// 修改类别是否展示为公共类别
        /// </summary>
        /// <param name="Names"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResponse ChangeCategoryShow([FromBody] ChangeCategoryShowDto dto)
        {
            //将所有dto.ShowName的IsShow改为true
            //将所有dto.HideName的IsShow改为false
            //dto.ShowName->"'1','2'"
            var sql = "update AccountCategory set IsShow = 1 where Name in("+ dto.ShowName + ");update AccountCategory set IsShow = 0 where UserId in(" + dto.HideName + ")";
            if (!AccountCategoryBll.ExecuteSql(sql))
            {
                return BadResponse("网络错误，请重试！");
            }
            return OkResponse(null, "修改成功！");
        }
    }
}
