using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Wx.Client.Filter;
using Wx.Client.Models;
using Wx.Extend;
using Wx.Weixin;

namespace Wx.Client.Controllers
{
    [Login]
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public string Init(string echostr = null)
        {
            return echostr;
        }

        public ActionResult MyHome() {
            var opt = new UserManage();
            var openid = SessionCore.OpenId;
            string userInfo = opt.GetUserInfo(openid);
            TempData["userInfo"] = userInfo;
            return View();
        }


        public ActionResult Index() {
            var opt = new UserManage();
            var openid = SessionCore.OpenId;
            string userInfo = opt.GetUserInfo(openid);
            TempData["userInfo"] = userInfo;
            return View();
        }

        public ActionResult UserList(string code = null)
        {
            //var data = new JavaScriptSerializer().Deserialize<UserReqModel>(resStr);
            var opt = new UserManage();

            var openid = opt.GetOpenidByCode(code);
            string userInfo = opt.GetUserInfo(openid);

            TempData["code"] = openid;
            TempData["userInfo"] = userInfo;
            return View();
        }

        public string GetUserList(string callback = null, string varname = null)
        {
            var resStr = new UserManage().GetUserList();
            if (!string.IsNullOrWhiteSpace(callback))
            {
                resStr = callback + "(" + resStr + ")";
            }
            else if (!string.IsNullOrWhiteSpace(varname))
            {
                resStr = "var " + varname + "=" + resStr;
            }
            return resStr;
        }

    }
}
