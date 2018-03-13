using BILL.Dal;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace BILL.Bll
{
    public class BaseBll<T> where T : class
    {
        protected static readonly BaseDal<T> baseDal = new BaseDal<T>(ConnectionString);

        protected static string ConnectionString
        {
            get
            {
                string connectionString = ConfigurationManager.ConnectionStrings["BILL"].ConnectionString;

                return connectionString;

            }
        }

        public static bool Insert(T model)
        {
            return baseDal.Insert(model);
        }

        public static bool Update(T model)
        {
            return baseDal.Update(model);
        }



        public static IList<T> GetList()
        {
            return baseDal.GetList();
        }

        public static bool ExecuteSql(string Sql)
        {
            return baseDal.Execute(Sql);
        }


    }
}