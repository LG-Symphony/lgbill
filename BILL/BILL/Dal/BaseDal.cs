using BILL.Core;
using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BILL.Dal
{
    public class BaseDal<T> where T : class
    {
        public BaseDal(string connectionString)
        {
            ConnectionString = connectionString;
        }
        //private string _connectionString = Global.ApplicationParms.ConnectionString;
        public string ConnectionString { get; set; }
        public virtual bool Insert(T model)
        {
            return SqlHelper.Insert<T>(model, ConnectionString);
        }

        public int InsertWithReturnId(T model)
        {
            return SqlHelper.InsertWithReturnId<T>(model, ConnectionString);
        }
        public virtual bool Update(T model)
        {
            return SqlHelper.Update<T>(model, ConnectionString);
        }

        public virtual bool Delete(T model)
        {
            return false;
        }

        public bool Delete(PredicateGroup predicate)
        {
            return SqlHelper.Delete<T>(predicate, ConnectionString);
        }



        public IList<T> GetList()
        {
            return SqlHelper.GetList<T>(ConnectionString);
        }
        public IList<T> GetList(PredicateGroup pdg)
        {
            try
            {
                return SqlHelper.GetList<T>(pdg, ConnectionString);
            }
            catch (Exception ex)
            {
                int a;
                //Logger.LogError4Exception(ex, "AppLogger");
            }
            return new List<T>();
        }
        public IList<T> GetList(string storedProcedure, dynamic param = null)
        {
            try
            {
                return SqlHelper.QuerySp<T>(storedProcedure, param, null, null, true, null, ConnectionString);
            }
            catch (Exception ex)
            {
                int a;
                //Logger.LogError4Exception(ex, "AppLogger");
            }
            return new List<T>();
        }
        public T GetModel(PredicateGroup pdg)
        {
            T model = null;
            try
            {
                model = SqlHelper.Find<T>(pdg, ConnectionString);
            }
            catch (Exception ex)
            {
                int a;
                //Logger.LogError4Exception(ex, "AppLogger");
            }
            return model;
        }

        public int ExecuteScalar2Int(string sql)
        {
            int? ret = null;

            try
            {
                ret = SqlHelper.ExecuteScalar2Int(sql, ConnectionString);
            }
            catch (Exception ex)
            {
                int a;
                //Logger.LogError4Exception(ex, "AppLogger");
            }
            if (ret.HasValue)
            {
                return ret.Value;
            }
            else
            {
                return 0;
            }
        }

        public bool Execute(string sql)
        {
            try
            {
                return SqlHelper.Execute(sql, ConnectionString);
            }
            catch (Exception ex)
            {
                int a;
                //Logger.LogError4Exception(ex, "AppLogger");
            }
            return false;
        }
    }
}