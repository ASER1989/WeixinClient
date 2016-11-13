using System;
using System.Collections.Generic;
using System.IO;
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

        public ActionResult MyHome()
        {
            var opt = new UserManage();
            var openid = SessionCore.OpenId;
            string userInfo = opt.GetUserInfo(openid);
            TempData["userInfo"] = userInfo;
            return View();
        }


        public ActionResult Index()
        {
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


        public ActionResult RedPackTest()
        {
            string packRes = "哈哈~，你不在白名单内！";
            var whitelist = GetWhiteList();
            if (whitelist.Contains(SessionCore.OpenId))
            {
                if (DataTest(SessionCore.OpenId))
                {
                    var res = new RedPack().SendReadPack(SessionCore.OpenId, 100);
                    if (res.return_code == "SUCCESS" && res.result_code == "SUCCESS")
                    {
                        packRes = "老板很大方，给你发了一个红包！想要更多更大的红包？请贿赂作者.";
                        AddRedPackLog(SessionCore.OpenId);
                        new MessageManage().SendTextMsg("oK8WAt8VieVye7PJW41kU9oW_vH0", SessionCore.OpenId+":领钱成功。");
                    }
                    else if (res.result_code == "NOTENOUGH")
                    {
                        packRes = "活动已结束！";
                    }
                    else
                    {
                        new MessageManage().SendTextMsg("oK8WAt8VieVye7PJW41kU9oW_vH0", new JavaScriptSerializer().Serialize(res) + SessionCore.OpenId);
                        packRes = "你没收到老板发的红包，因为作者写的代码有点bug! bug已发送给作者，他会改好的。";
                    }
                }
                else
                {
                    packRes = "没有行贿只能领一次哦！";
                }
            }

            TempData["res"] = packRes;
            return View();
        }

        private List<string> GetWhiteList()
        {

            string filepath = Server.MapPath("/Res/Data/whitelist.txt");
            string txt = System.IO.File.ReadAllText(filepath);
             ///   Cache.CacheApi.Get("_white_list").ObjToString() ?? 
            //Cache.CacheApi.Set("_white_list", txt);

            return new JavaScriptSerializer().Deserialize<List<string>>(txt);
        }
        private bool DataTest(string openid)
        {
            string filepath = Server.MapPath("/Res/Data/json.txt");
            var js = new JavaScriptSerializer();
            if (!System.IO.File.Exists(filepath))
            {
                return true;
            }
            else
            {
                string json = System.IO.File.ReadAllText(filepath);
                var dt = js.Deserialize<List<DataTestModel>>(json);
                if (dt.Any(p => p.Openid == openid))
                {
                    return false;
                }

            }
            return true;

        }
        private void AddRedPackLog(string openid)
        {
            string filepath = Server.MapPath("/Res/Data/json.txt");
            var js = new JavaScriptSerializer();
            if (!System.IO.File.Exists(filepath))
            {
                List<DataTestModel> dt = new List<DataTestModel>();
                dt.Add(new DataTestModel() { Openid = openid , CreateTime = DateTime.Now});
                var txt = js.Serialize(dt);
                System.IO.File.WriteAllText(filepath, txt);
            }
            else
            {
                string json = System.IO.File.ReadAllText(filepath);
                var dt = js.Deserialize<List<DataTestModel>>(json);
                dt.Add(new DataTestModel() { Openid = openid, CreateTime = DateTime.Now });
                var txt = js.Serialize(dt);
                System.IO.File.WriteAllText(filepath, txt);
            }
        }

        private class DataTestModel
        {
            public string Openid { get; set; }
            public string Key { get; set; }
            public DateTime CreateTime { get; set; }
        }

    }
}
