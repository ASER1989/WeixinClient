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
       private static readonly string ip_key = "_icache_Ip_key_xE0A";

        private static readonly string appid = appConfig["_appkey"];  //"wxf07c08710f813ad4";
        private static readonly string appsec = appConfig["_appsec"];// "a2f116467862a1b5625dff0466d05275";

        private static readonly string certPath = appConfig["_cret_path"];//证书安装路径
        private static readonly string certPassword = appConfig["_cret_password"];//证书密码
        private static readonly string mchId = appConfig["_mch_id"];//商户号
        private static readonly string mchName = appConfig["_mch_name"];//商户名称
        private static readonly string secretKey = appConfig["_secret_key"];//api密钥

        

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
        /// <summary>
        /// 证书路径
        /// </summary>
        public static string CertPath
        {
            get { return certPath; }
        }
        /// <summary>
        /// 证书密码
        /// </summary>
        public static string CertPassword {
            get { return certPassword; }
        }

        /// <summary>
        /// 商户号
        /// </summary>
        public static string MchId {
            get { return mchId; }
        }

        /// <summary>
        /// 商户名称
        /// </summary>
        public static string MchName {
            get { return mchName; }
        }
        /// <summary>
        /// 本机IP地址
        /// </summary>
        public static string MachineIp {
            get { return (CacheApi.Get(ip_key) ?? _GetAddressIP()).ToString(); }
        }

        /// <summary>
        /// API密钥
        /// </summary>
        public static string SecretKey {
            get { return secretKey; }
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

        private static string _GetAddressIP()
        {
            ///获取本地的IP地址
            string AddressIP = string.Empty;
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    AddressIP = _IPAddress.ToString();
                }
            }
            CacheApi.Set(ip_key, AddressIP);
            return AddressIP;
        }

        private class TokenModel
        {
            public string access_token { get; set; }
            public int expires_in { get; set; }
        }
    }
}
