using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BILL.Core
{
    public static class JsonHelper
    {
        public static string ToJson(this object obj)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            return JsonConvert.SerializeObject(obj, settings);
        }
    }
}