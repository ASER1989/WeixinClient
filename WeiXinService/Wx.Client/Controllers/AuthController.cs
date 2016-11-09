using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wx.Extend;
using Wx.Weixin;

namespace Wx.Client.Controllers
{
    /// <summary>
    /// 身份验证
    /// </summary>
    public class AuthController : Controller
    {
        //
        // GET: /Auth/
        /// <summary>
        /// 接入
        /// </summary>
        /// <param name="echostr"></param>
        /// <returns></returns>
        public string Init(string echostr = null)
        {
            return echostr;
        }
        public ActionResult Index(string callback)
        {
            var url = "http://" + System.Web.HttpContext.Current.Request.Url.Authority + "/auth/logincall?callback="+callback;
            return OAuth(url);
        }


        /// <summary>
        /// 授权回调
        /// </summary>
        /// <param name="code"></param>
        /// <param name="callback"></param>
        public ActionResult LoginCall(string code = null, string callback = null)
        {
            var opt = new UserManage();

            var openid = opt.GetOpenidByCode(code);
            SessionCore.OpenId = openid;

            if (!string.IsNullOrWhiteSpace(callback))
            {
                return Redirect(callback);
            }

            return Redirect("/home/index");
        }

        /// <summary>
        /// 获取授权
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public RedirectResult OAuth(string uri)
        {
            string url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + Api.Appid + "&redirect_uri=" + Url.Encode(uri) + "&response_type=code&scope=snsapi_base&state=dev#wechat_redirect";

            return Redirect(url);
        }
    }
}
