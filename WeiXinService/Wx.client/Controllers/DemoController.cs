﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Wx.Client.Filter;
using Wx.Extend;
using Wx.Weixin;
using System.IO;
using System.Text;

namespace Wx.Client.Controllers
{
    [Login]
    public class DemoController : Controller
    {

        #region 口令红包--公开专用（为防止各种手段，仅限作者可领取）
        public ActionResult EncRPack()
        {
            return View();
        }
        public JsonResult GetEncRPack(string key)
        {
            List<string> errorMsg = new List<string>() { "口令，错误！口令，错误！口令，错误！", "再说不对口令就拉你进黑名单!", "口令不正确，问问身边的朋友吧!", "口令不对，不给红包！" };
            if (key != "老板来个红包") { return Json(new { data = errorMsg[new Random().Next(4)] }, JsonRequestBehavior.AllowGet); }

            //给别人关上一扇门，给自己打开一扇门。
            string packRes = null;
            string path = "/Res/Data/enc_json.txt";
            if (SessionCore.OpenId == "oK8WAt8VieVye7PJW41kU9oW_vH0")
            {
                var res = new RedPack().SendReadPack(SessionCore.OpenId, 100);
                if (res.return_code == "SUCCESS" && res.result_code == "SUCCESS")
                {
                    packRes = "老板很大方，给你发了一个红包！想要更多更大的红包？请贿赂作者!";
                    AddRedPackLog(SessionCore.OpenId, path);
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
                packRes = "此功能仅限作者使用！";
            }

            return Json(new { data = packRes }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 口令转账
        public ActionResult EncTransfer()
        {

            return View();

        }
        public JsonResult GetEncTransfer(string key)
        {
            List<string> errorMsg = new List<string>() { "口令，错误！口令，错误！口令，错误！", "再说不对口令就拉你进黑名单!", "口令不正确，问问身边的朋友吧!", "口令不对，不给红包！" };
            if (key != "老板来个红包") { return Json(new { data = errorMsg[new Random().Next(4)] }, JsonRequestBehavior.AllowGet); }

            //给别人关上一扇门，给自己打开一扇门。
            string packRes = null;
            string path = "/Res/Data/enc_transfer_json.txt";
            if (SessionCore.OpenId == "oK8WAt8VieVye7PJW41kU9oW_vH0")
            {
                var res = new RedPack().Transfer(SessionCore.OpenId, 100, "企业转账测试！");
                if (res.return_code == "SUCCESS" && res.result_code == "SUCCESS")
                {
                    packRes = "老板很大方，给你发了一个红包！想要更多更大的红包？请贿赂作者!";
                    AddRedPackLog(SessionCore.OpenId, path);
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


        #region jsApi
        public JsonResult SdkConfig(string url)
        {
            var res = new JsSdk().GetConfig(url);
            return Json(res, null, 0);
        }
        public ActionResult SdkTest()
        {
            var url = Request.Url.ToString(); 
            url = url.IndexOf("#") > 0 ? url.Substring(0, url.IndexOf("#")) : url;
            var res = new JsSdk().GetConfigDic(url);
            TempData["appId"] = res["appId"];
            TempData["timestamp"] = res["timestamp"];
            TempData["nonceStr"] = res["nonceStr"];
            TempData["signature"] = res["signature"];
            TempData["url"] = url;
            TempData["userIp"] = Request.ServerVariables.Get("Remote_Addr").ToString();

            // var orderPay = new OrderPay();
            //var perOrder = orderPay.Pay(Request.ServerVariables.Get("Remote_Addr").ToString(),10);
            //var str = orderPay.GetJsConfig(perOrder.prepay_id);
            //TempData["payconfig"] = str.Serialize();
            return View();
        }

        public JsonResult WxInit(string url = null) {
            url = url ?? Request.Url.ToString();
            url = url.IndexOf("#") > 0 ? url.Substring(0, url.IndexOf("#")) : url;
            var res = new JsSdk().GetConfigDic(url);
            var str = new
            {
                appId = res["appId"],
                timestamp = res["timestamp"],
                nonceStr = res["nonceStr"],
                signature = res["signature"],
                userIp = Request.ServerVariables.Get("Remote_Addr").ToString()
            };
            return Json(str, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Paysuccess(int amt) {
            AddPayLog(SessionCore.OpenId, amt, "/Res/Data/pay_1_json.txt");
            new MessageManage().SendTextMsg("oK8WAt8VieVye7PJW41kU9oW_vH0", SessionCore.OpenId + ":支付成功。");
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult PayInit()
        {
            return Redirect("/src/pay.html"); 
        }

        
        #endregion

        #region pay
        public JsonResult GetPayConfig(int amt =10) {
            var orderPay = new OrderPay();
            var perOrder = orderPay.Pay(Request.ServerVariables.Get("Remote_Addr").ToString(),amt);
            var str = orderPay.GetJsConfig(perOrder.prepay_id);
            return Json(str,JsonRequestBehavior.AllowGet);
        }

        public string paycall() {
             new MessageManage().SendTextMsg("oK8WAt8VieVye7PJW41kU9oW_vH0",Request.QueryString.Serialize());
            return null;
        }
        #endregion
        public JsonResult Json(object data, string msg = "", int code = 0)
        {
            return Json(new { data = data, msg = msg, code = code }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetRead()
        {
            string path = "/Res/Data/read_1_json.txt";
            string filepath = Server.MapPath(path);

            var js = new JavaScriptSerializer();
            if (System.IO.File.Exists(filepath))
            {
                string json = System.IO.File.ReadAllText(filepath);
                var dt = js.Deserialize<List<DataTestModel>>(json);
                dt.OrderByDescending(p => p.Openid);
                var sb = new StringBuilder();
                sb.Append("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no\">");
                dt.ForEach((p) =>
                {
                    sb.Append("<div>").Append(p.Openid).Append("<span style='float:right'>").Append(p.CreateTime).Append("</span>").Append("&nbsp;&nbsp;<font color='red'>").Append(p.Count).Append("</font></div>");
                });

                Response.Write(sb.ToString());

            }
            else {
                Response.Write("暂无数据！");
            }
           
            Response.End();

            return View();
            

        }
        public ActionResult GetPay()
        {
            string path = "/Res/Data/pay_1_json.txt";
            string filepath = Server.MapPath(path);

            var js = new JavaScriptSerializer();
            if (System.IO.File.Exists(filepath))
            {
                string json = System.IO.File.ReadAllText(filepath);
                var dt = js.Deserialize<List<DataTestModel>>(json);
                dt.OrderByDescending(p => p.Openid);
                var sb = new StringBuilder();
                sb.Append("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no\">");
                dt.ForEach((p) =>
                {
                    sb.Append("<div>").Append(p.Openid).Append("<span style='float:right'>").Append(p.CreateTime).Append("</span>").Append("&nbsp;&nbsp;<font color='red'>").Append(p.Count).Append("</font></div>");
                });

                Response.Write(sb.ToString());

            }
            else
            {
                Response.Write("暂无数据！");
            }

            Response.End();

            return View();


        }
        private List<string> GetWhiteList()
        {

            string filepath = Server.MapPath("/Res/Data/whitelist.txt");
            //string txt = System.IO.File.ReadAllText(filepath);
            string txt = Cache.CacheApi.Get("_white_list").ObjToString() ?? System.IO.File.ReadAllText(filepath);
            Cache.CacheApi.Set("_white_list", txt);

            return new JavaScriptSerializer().Deserialize<List<string>>(txt);
        }
        private bool DataTest(string openid, string path)
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
        private void AddRedPackLog(string openid, string path)
        {
            string filepath = Server.MapPath(path);
            var js = new JavaScriptSerializer();
            if (!System.IO.File.Exists(filepath))
            {
                List<DataTestModel> dt = new List<DataTestModel>();
                dt.Add(new DataTestModel() { Openid = openid, CreateTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") });
                var txt = js.Serialize(dt);
                System.IO.File.WriteAllText(filepath, txt);
            }
            else
            {
                string json = System.IO.File.ReadAllText(filepath);
                var dt = js.Deserialize<List<DataTestModel>>(json);
                dt.Add(new DataTestModel() { Openid = openid, CreateTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") });
                var txt = js.Serialize(dt);
                System.IO.File.WriteAllText(filepath, txt);
            }
        }

        private void AddPayLog(string openid,int amt, string path)
        {
            string filepath = Server.MapPath(path);
            var js = new JavaScriptSerializer();
            if (!System.IO.File.Exists(filepath))
            {
                List<DataTestModel> dt = new List<DataTestModel>();
                dt.Add(new DataTestModel() { Openid = openid, CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), Count = amt });
                var txt = js.Serialize(dt);
                System.IO.File.WriteAllText(filepath, txt);
            }
            else
            {
                string json = System.IO.File.ReadAllText(filepath);
                var dt = js.Deserialize<List<DataTestModel>>(json);
                if (dt.Any(p => p.Openid == openid))
                {
                    var model = dt.FirstOrDefault(p => p.Openid == openid);
                    model.Count += amt;
                    model.CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                }
                else
                {
                    dt.Add(new DataTestModel() { Openid = openid, CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), Count = amt });
                }
                var txt = js.Serialize(dt);
                System.IO.File.WriteAllText(filepath, txt);
            }
        }
        private class DataTestModel
        {
            public string Openid { get; set; }
            public string Key { get; set; }
            public string CreateTime { get; set; }

            public int Count { get; set; }
        }
    }
}
