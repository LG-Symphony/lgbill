using DapperExtensions;
using BILL.Models.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BILL.Dal.Token
{
    public class FindPwdVerifyDal<T> : BaseDal<T> where T : FindPwdVerify
    {
        public FindPwdVerifyDal(string connectionString) : base(connectionString) { }
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
    }
}