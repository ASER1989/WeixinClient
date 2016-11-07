using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Wx.Client.Models;
using Wx.Extend;
using Wx.Weixin;

namespace Wx.Client.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public string Init(string echostr=null) {
            return echostr;
        }
        public ActionResult Index()
        {
            //HttpRuntime.Cache
            return OAuth("http://sq.023qx.net/home/myhome");
            //return View();
        }

        public ActionResult MyHome(string code=null) {
            var opt = new UserManage();

            var openid = opt.GetOpenidByCode(code);
            string userInfo = opt.GetUserInfo(openid);

            SessionCore.OpenId = openid;

            TempData["code"] = openid;
            TempData["userInfo"] = userInfo;
            return View();
        }

        public RedirectResult OAuth(string uri)
        {
            string url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + Api.Appid + "&redirect_uri=" + Url.Encode(uri) + "&response_type=code&scope=snsapi_base&state=dev#wechat_redirect";
            
          return  Redirect(url);
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
