using BILL.Models;
using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BILL.Dal
{
    public class UserInfoDal<T> : BaseDal<T> where T : UserInfo
    {
        public UserInfoDal(string connectionString) : base(connectionString) { }

        /// <summary>
        /// 获取指定用户信息
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        public T GetModelByEmail(string Email)
        {
            T model = null;
            try
            {
                PredicateGroup pdg = new PredicateGroup();
                pdg.Predicates = new List<IPredicate>();
                pdg.Predicates.Add(Predicates.Field<T>(a => a.Email, Operator.Eq, Email));
                model = GetModel(pdg);
            }
            catch (Exception ex)
            {
            }
            return model;
        }
        public T GetModelById(string Id)
        {
            T model = null;
            try
            {
                PredicateGroup pdg = new PredicateGroup();
                pdg.Predicates = new List<IPredicate>();
                pdg.Predicates.Add(Predicates.Field<T>(a => a.Id, Operator.Eq, Id));
                model = GetModel(pdg);
            }
            catch (Exception ex)
            {
            }
            return model;
        }
        /// <summary>
        /// 获取指定昵称的用户
        /// </summary>
        /// <param name="Nickname"></param>
        /// <returns></returns>
        public T GetModelByNickname(string Nickname)
        {
            T model = null;
            try
            {
                PredicateGroup pdg = new PredicateGroup();
                pdg.Predicates = new List<IPredicate>();
                pdg.Predicates.Add(Predicates.Field<T>(a => a.Nickname, Operator.Eq, Nickname));
                model = GetModel(pdg);
            }
            catch (Exception ex)
            {
            }
            return model;
        }
    }
}