using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Wx.Extend;
using System.Web.Script.Serialization;

namespace Wx.Weixin
{
    public sealed class JsSdk:ToolBase
    {
        public string GetConfig(string url)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string nonce = _Nonce().ToLower();
            string time = _GetTimeStamp();
            dic.Add("noncestr", nonce);
            dic.Add("jsapi_ticket", Api.Ticket);
            dic.Add("timestamp", time);
            dic.Add("url", url);
            string sign = _PerParam(dic).SHA1().ToLower();
            var ret = new
            {
                appId = Api.Appid,
                timestamp = time,
                nonceStr = nonce,
                signature = sign
            };
            return ret.Serialize();

        }
        public Dictionary<string, string> GetConfigDic(string url)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string nonce = _Nonce().ToLower();
            string time = _GetTimeStamp();
            dic.Add("noncestr", nonce);
            dic.Add("jsapi_ticket", Api.Ticket);
            dic.Add("timestamp", time);
            dic.Add("url", url);
            string sign = _PerParam(dic).SHA1().ToLower();
            var ret = new Dictionary<string, string>();

            ret.Add("appId", Api.Appid);
            ret.Add("timestamp", time);
            ret.Add("nonceStr", nonce);
            ret.Add("signature", sign);

            return ret;

        }




    }
}
