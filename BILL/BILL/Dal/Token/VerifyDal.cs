using DapperExtensions;
using BILL.Models.Token;
using System;
using System.Collections.Generic;

namespace BILL.Dal.Token
{
    public class VerifyDal<T> : BaseDal<T> where T : Verify
    {
        public VerifyDal(string connectionString) : base(connectionString) { }
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
    }
}