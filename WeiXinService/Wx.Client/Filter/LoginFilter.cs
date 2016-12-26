using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wx.Extend;

namespace Wx.Client.Filter
{

    public class LoginAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (filterContext.HttpContext.Request.Url.Host.ToString().ToLower().Equals("aser.src.demo.ahyunhe.com"))
            {
                filterContext.HttpContext.Response.Redirect("http://wx.ahyunhe.com/");
                filterContext.Result = new ContentResult();
            }
            else {
                var openid = SessionCore.OpenId;
                if (string.IsNullOrWhiteSpace(openid))
                {

                    filterContext.HttpContext.Response.Redirect("/Auth/index?callback=" + System.Web.HttpContext.Current.Request.RawUrl);
                    filterContext.Result = new ContentResult();

                    //System.Web.HttpContext.Current.Request.RawUrl
                }
            }

           
        }
    }

}