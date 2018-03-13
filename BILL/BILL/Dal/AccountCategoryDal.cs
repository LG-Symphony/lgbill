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
        public IList<T> GetListByCreateUserId(int CreateUserId)
        {
            PredicateGroup pdg = new PredicateGroup();
            pdg.Predicates = new List<IPredicate>();
            pdg.Predicates.Add(Predicates.Field<T>(a => a.CreateUserId, Operator.Eq, CreateUserId));
            
            return GetList(pdg);
        }
    }
}