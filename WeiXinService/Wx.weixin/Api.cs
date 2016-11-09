using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Wx.Cache;
using System.Web.Script.Serialization;
using System.Collections.Specialized;
using System.Configuration;

namespace Wx.Weixin
{
    public sealed class Api
    {
       private static  NameValueCollection appConfig = System.Configuration.ConfigurationManager.AppSettings;

        private static readonly string token_key = "_icache_token_key_x80e";
        private static readonly string appid = appConfig["_appkey"];  //"wxf07c08710f813ad4";
        private static readonly string appsec = appConfig["_appsec"];// "a2f116467862a1b5625dff0466d05275";

        /// <summary>
        /// 当前有效token
        /// <para>只读属性</para>
        /// </summary>
        public static string Token
        {
            get { return (CacheApi.Get(token_key) ?? _GetToken()).ToString(); }

        }
        /// <summary>
        /// 当前账号appid
        /// <para>  只读属性</para>
        /// </summary>
        public static string Appid
        {
            get { return appid; }
        }

        public static string AppSec
        {
            get { return appsec; }
        }

        private static void _setToken(string token, int expTime)
        {
            CacheApi.Set(token_key, token, expTime);
        }
        private static string _GetToken()
        {
            //string appid = "wx6cd10b08ec0441fb";
            //string appsec = "33c778c9272bd4aac84022a5231e41e5";
            string result = null;

            System.Net.WebRequest wReq = System.Net.WebRequest.Create("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + appid + "&secret=" + appsec);
            System.Net.WebResponse wResp = wReq.GetResponse();
            System.IO.Stream respStream = wResp.GetResponseStream();
            using (System.IO.StreamReader reader = new System.IO.StreamReader(respStream, Encoding.GetEncoding("UTF-8")))
            {
                result = reader.ReadToEnd();
            }

            JavaScriptSerializer ser = new JavaScriptSerializer();
            var res = ser.Deserialize<TokenModel>(result);
            _setToken(res.access_token, res.expires_in - 120);
            return res.access_token;

        }

        private class TokenModel
        {
            public string access_token { get; set; }
            public int expires_in { get; set; }
        }
    }
}
