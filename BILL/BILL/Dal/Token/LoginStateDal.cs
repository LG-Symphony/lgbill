using DapperExtensions;
using BILL.Models.Token;
using System;
using System.Collections.Generic;

namespace BILL.Dal.Token
{
    public class LoginStateDal<T> : BaseDal<T> where T : LoginState
    {
        public LoginStateDal(string connectionString) : base(connectionString) { }
        public T GetModelByUserId(string UserId)
        {
            T model = null;
            try
            {
                PredicateGroup pdg = new PredicateGroup();
                pdg.Predicates = new List<IPredicate>();
                pdg.Predicates.Add(Predicates.Field<T>(a => a.UserId, Operator.Eq, UserId));
                model = GetModel(pdg);
            }
            catch (Exception ex)
            {
            }
            return model;
        }
    }
}