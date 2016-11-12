using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wx.Extend;

namespace Wx.Client.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/

        public ActionResult QrTest()
        {
            string fileName = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:fff").ToMd5()+".jpg";
            new Qrcode().SaveQrcode("http://wx.ahyunhe.com/home/RedPackTest", Server.MapPath("/Res/Links") + "/"+fileName);
            TempData["img"] = "/Res/Links/" + fileName;
            return View();
        }

    }
}
