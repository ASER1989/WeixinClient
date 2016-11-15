using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Wx.Extend;

namespace Wx.Client.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/

        public ActionResult QrTest()
        {
            string fileName = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:fff").ToMd5() + ".jpg";
            new Qrcode().SaveQrcode("http://wx.ahyunhe.com/home/RedPackTest", Server.MapPath("/Res/Links") + "/" + fileName);
            TempData["img"] = "/Res/Links/" + fileName;
            return View();
        }

        public ActionResult index(string pwd)
        {
            if (pwd == "今天天气不错！")
            {
                SessionCore.Set("_key_Password",pwd);
                return Redirect("/test/config");
            }
            return View();
        }
        public ActionResult Config()
        {
            if (SessionCore.Get("_key_Password").ObjToString() != "今天天气不错！") {
                return Redirect("/test/index");
            }

            string filepath = Server.MapPath("/Res/Data/whitelist.txt");
            string txt = Cache.CacheApi.Get("_white_list").ObjToString() ?? System.IO.File.ReadAllText(filepath);
            TempData["data"] = txt;
            return View();
        }
        public ActionResult SetConfig(string openid, string sub)
        {
            if (SessionCore.Get("_key_Password").ObjToString() != "今天天气不错！")
            {
                return Redirect("/test/index");
            }

            string filepath = Server.MapPath("/Res/Data/whitelist.txt");
            var txt = System.IO.File.ReadAllText(filepath);
            var js = new JavaScriptSerializer();
            var data = js.Deserialize<List<string>>(txt);

            if (sub == "添加")
            {
                data.Add(openid);
            }
            else
            {
                data.Remove(openid);
            }

            txt = js.Serialize(data);
            System.IO.File.WriteAllText(filepath, txt);
            Cache.CacheApi.Remove("_white_list");
            return Redirect("/test/config");
        }
        public string CleanJson() { 
            if (SessionCore.Get("_key_Password").ObjToString() != "今天天气不错！")
            {
                 Redirect("/test/index");
                 return null;
            }
            string filepath = Server.MapPath("/Res/Data/json.txt");
            string bakpath = Server.MapPath("/Res/Data/json_"+DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss")+"-bak.txt");
            var txt =  System.IO.File.Exists(filepath)? System.IO.File.ReadAllText(filepath):"";
            System.IO.File.WriteAllText(bakpath, txt);
            System.IO.File.WriteAllText(filepath, "");
            return "success!";
        }
        public string Clean()
        {
            Cache.CacheApi.Remove("_white_list");

            string filepath = Server.MapPath("/Res/Data/whitelist.txt");
            string txt = System.IO.File.ReadAllText(filepath);
            Cache.CacheApi.Set("_white_list", txt);
            return Cache.CacheApi.Get("_white_list").ObjToString();
        }

    }
}
