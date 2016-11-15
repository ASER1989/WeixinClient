using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Wx.Extend
{
    public class SessionCore
    {
        /// <summary>
        /// 获取或保存OpenId
        /// </summary>
        public static string OpenId
        {
            set { HttpContext.Current.Session["_open_id"] = value; }
            get { return HttpContext.Current.Session["_open_id"].ObjToString(); }
        }

        public static void Set(string key, object value) {
            HttpContext.Current.Session[key] = value;
        }

        public static object Get(string key) {
            return HttpContext.Current.Session[key];
        }

    }
}
