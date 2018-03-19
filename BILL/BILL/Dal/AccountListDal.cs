using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BILL.Models;
using DapperExtensions;

namespace BILL.Dal
{
    public class AccountListDal<T> : BaseDal<T> where T : AccountList
    {
        public AccountListDal(string connectionString) : base(connectionString) { }

        public T GetModelByCode(string Code)
        {
            T model = null;
            try
            {
                PredicateGroup pdg = new PredicateGroup();
                pdg.Predicates = new List<IPredicate>();
                pdg.Predicates.Add(Predicates.Field<T>(a => a.Code, Operator.Eq, Code));
                model = GetModel(pdg);
            }
            catch (Exception ex)
            {
            }
            return model;
        }
        public IList<T> GetListByCreateUserId(string CreateUserId)
        {
            PredicateGroup pdg = new PredicateGroup();
            pdg.Predicates = new List<IPredicate>();
            pdg.Predicates.Add(Predicates.Field<T>(a => a.CreateUserId, Operator.Eq, CreateUserId));
            return GetList(pdg);
        }
    }
}