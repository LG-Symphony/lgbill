using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BILL.Models;
using DapperExtensions;

namespace BILL.Dal
{
    public class AccountCategoryDal<T> : BaseDal<T> where T : AccountCategory
    {
        public AccountCategoryDal(string connectionString) : base(connectionString) { }
        public IList<T> GetListByCreateUserId(string CreateUserId)
        {
            PredicateGroup pdg = new PredicateGroup();
            pdg.Predicates = new List<IPredicate>();
            pdg.Predicates.Add(Predicates.Field<T>(a => a.CreateUserId, Operator.Eq, CreateUserId));
            return GetList(pdg);
        }
        //public T GetModelById(int Id)
        //{
        //    T model = null;
        //    try
        //    {
        //        PredicateGroup pdg = new PredicateGroup();
        //        pdg.Predicates = new List<IPredicate>();
        //        pdg.Predicates.Add(Predicates.Field<T>(a => a.Id, Operator.Eq, Id));
        //        model = GetModel(pdg);
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return model;
        //}
        public T GetModelByName(string Name)
        {
            T model = null;
            try
            {
                PredicateGroup pdg = new PredicateGroup();
                pdg.Predicates = new List<IPredicate>();
                pdg.Predicates.Add(Predicates.Field<T>(a => a.Name, Operator.Eq, Name));
                model = GetModel(pdg);
            }
            catch (Exception ex)
            {
            }
            return model;
        }

        public IList<T> GetListByIsShowEqTrue()
        {
            PredicateGroup pdg = new PredicateGroup();
            pdg.Predicates = new List<IPredicate>();
            pdg.Predicates.Add(Predicates.Field<T>(a => a.IsShow, Operator.Eq, true));
            return GetList(pdg);
        }
    }
}