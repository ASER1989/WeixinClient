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
            TempData["openid"] = SessionCore.OpenId;

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

        #region 普通红包
        public ActionResult RedPackTest()
        {
            string packRes = "哈哈~，你不在白名单内！";
            var whitelist = GetWhiteList();
            if (whitelist.Contains(SessionCore.OpenId))
            {
                //给别人关上一扇门，给自己打开一扇门。
                string path = "/Res/Data/json.txt";
                if (SessionCore.OpenId == "oK8WAt8VieVye7PJW41kU9oW_vH0" || DataTest(SessionCore.OpenId,path))
                {
                    var res = new RedPack().SendReadPack(SessionCore.OpenId, 100);
                    if (res.return_code == "SUCCESS" && res.result_code == "SUCCESS")
                    {
                        packRes = "老板很大方，给你发了一个红包！想要更多更大的红包？请贿赂作者.";
                        AddRedPackLog(SessionCore.OpenId,path);
                        new MessageManage().SendTextMsg("oK8WAt8VieVye7PJW41kU9oW_vH0", SessionCore.OpenId + ":领钱成功。" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff"));
                    }
                    else if (res.result_code == "NOTENOUGH")
                    {
                        packRes = "活动已结束！";
                    }
                    else
                    {
                        new MessageManage().SendTextMsg("oK8WAt8VieVye7PJW41kU9oW_vH0", new JavaScriptSerializer().Serialize(res) + "      " + SessionCore.Get("reqDic").ToString() + " At:" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff"));
                        packRes = "网络故障，请稍后再试！";
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
        #endregion

        #region 口令红包
        public ActionResult EncRedPack() {

            return View();
        
        }
        public JsonResult GetEncReadPack(string key) {
            List<string> errorMsg = new List<string>() {"口令，错误！口令，错误！口令，错误！","再说不对口令就拉你进黑名单!","口令不正确，问问身边的朋友吧!","口令不对，不给红包！" };
            if (key != "老板来个红包") { return Json(new { data = errorMsg[new Random().Next(4)] }, JsonRequestBehavior.AllowGet); }

            //给别人关上一扇门，给自己打开一扇门。
            string packRes = null;
            string path = "/Res/Data/enc_json.txt";
            if (SessionCore.OpenId == "oK8WAt8VieVye7PJW41kU9oW_vH0" || DataTest(SessionCore.OpenId, path))
            {
                var res = new RedPack().SendReadPack(SessionCore.OpenId, 100);
                if (res.return_code == "SUCCESS" && res.result_code == "SUCCESS")
                {
                    packRes = "老板很大方，给你发了一个红包！想要更多更大的红包？请贿赂作者!";
                    AddRedPackLog(SessionCore.OpenId,path);
                    new MessageManage().SendTextMsg("oK8WAt8VieVye7PJW41kU9oW_vH0", SessionCore.OpenId + ":领钱成功。" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff"));
                }
                else if (res.result_code == "NOTENOUGH")
                {
                    packRes = "活动已结束！";
                }
                else
                {
                    new MessageManage().SendTextMsg("oK8WAt8VieVye7PJW41kU9oW_vH0", new JavaScriptSerializer().Serialize(res) + "      " + SessionCore.Get("reqDic").ToString() + " At:" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff"));
                    packRes = "网络故障，请稍后再试！";
                }
            }
            else
            {
                packRes = "没有行贿只能领一次哦！";
            }

            return Json(new { data = packRes }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        private List<string> GetWhiteList()
        {

            string filepath = Server.MapPath("/Res/Data/whitelist.txt");
            //string txt = System.IO.File.ReadAllText(filepath);
            string txt = Cache.CacheApi.Get("_white_list").ObjToString() ?? System.IO.File.ReadAllText(filepath); 
            Cache.CacheApi.Set("_white_list", txt);

            return new JavaScriptSerializer().Deserialize<List<string>>(txt);
        }
        private bool DataTest(string openid,string path)
        {
            //string filepath = Server.MapPath("/Res/Data/json.txt");
            string filepath = Server.MapPath(path);

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
        private void AddRedPackLog(string openid,string path)
        {
            string filepath = Server.MapPath(path);
            var js = new JavaScriptSerializer();
            if (!System.IO.File.Exists(filepath))
            {
                List<DataTestModel> dt = new List<DataTestModel>();
                dt.Add(new DataTestModel() { Openid = openid, CreateTime = DateTime.Now });
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
