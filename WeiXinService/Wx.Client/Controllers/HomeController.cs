using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Wx.Client.Models;
using Wx.Weixin;

namespace Wx.Client.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            //HttpRuntime.Cache
            
            return View();
        }

        public ActionResult UserList() {
            //var data = new JavaScriptSerializer().Deserialize<UserReqModel>(resStr);

            return View();
        }

        public string GetUserList(string callback=null,string varname=null) {
            var resStr = new UserManage().GetUserList();
            if (!string.IsNullOrWhiteSpace(callback))
            {
                resStr = callback + "(" + resStr+")";
            }
            else if(!string.IsNullOrWhiteSpace(varname)) {
                resStr = "var " + varname + "=" + resStr;
            }
            return resStr;
        }

    }
}
