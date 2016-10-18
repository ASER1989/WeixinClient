using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Wx.Extend;

namespace Wx.Weixin
{
    public class UserManage
    {
        public string GetUserList()
        {

            string uri = "https://api.weixin.qq.com/cgi-bin/user/get?access_token=" + Api.Token;
            var resStr = WebHttp.WebReq(uri);

            JavaScriptSerializer ser = new JavaScriptSerializer();
            var res = ser.Deserialize<UserModel>(resStr);
            //res.count = 10;
            var rsb = new StringBuilder();
            for (int i = 0; i < (res.count + 99) / 100; i++)
            {

                var reqData = res.data.openid.Skip(i * 100).Take(100).ToList();

                var reqStr = GetUserInfo(reqData).Replace("{\"user_info_list\":[", "");
                reqStr = reqStr.Substring(0, reqStr.Length - 2);

                if (rsb.Length > 0) rsb.Append(",");

                rsb.Append(reqStr);
            }
            rsb.Insert(0, "{\"user_info_list\":[");
            rsb.Append("]}");
            return rsb.ToString();
        }

        /// <summary>
        /// 摘要：
        ///     通过openid获取用户信息
        /// </summary>
        /// <param name="openidList">openid列表</param>
        /// <returns>string，用户信息json字符串</returns>
        public string GetUserInfo(List<string> openidList)
        {

            List<infoOpenid> dataModel = new List<infoOpenid>();
            openidList = openidList.Take(100).ToList();
            openidList.ForEach((p) =>
            {
                dataModel.Add(new infoOpenid() { openid = p });
            });

            var dataJson = "{\"user_list\":" + new JavaScriptSerializer().Serialize(dataModel) + "}";
            var postData = new System.Collections.Specialized.NameValueCollection();
            postData.Add("", dataJson);


            var postStr = WebHttp.WebPost("https://api.weixin.qq.com/cgi-bin/user/info/batchget?access_token=" + Api.Token, dataJson);
            return postStr;


        }
        /// <summary>
        /// 通过openid获取用户信息
        /// </summary>
        /// <param name="openid">openid</param>
        /// <returns>string，用户信息json字符串</returns>
        public string GetUserInfo(string openid)
        {

            if (string.IsNullOrWhiteSpace(openid)) return null;

            var uri = "https://api.weixin.qq.com/cgi-bin/user/info?access_token=" + Api.Token + "&openid=" + openid + "&lang=zh_CN";
            return WebHttp.WebReq(uri);

        }

        /// <summary>
        /// 通过code获取用户token
        /// <para>通常用于OAuth2接口之后</para>
        /// </summary>
        /// <param name="code">OAuth2返回的code</param>
        /// <returns>JSON
        /// {
        /// "access_token":"ACCESS_TOKEN",
        /// "expires_in":7200,
        ///"refresh_token":"REFRESH_TOKEN",
        ///"openid":"OPENID",
        ///"scope":"SCOPE"
        ///}
        /// </returns>
        public string GetUserToken(string code)
        {
            if (string.IsNullOrWhiteSpace(code)) return null;

            var uri = "https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + Api.Appid + "&secret=" + Api.AppSec + "&code=" + code + "&grant_type=authorization_code";
            return WebHttp.WebReq(uri);
        }

        /// <summary>
        /// 通过code获取用户openid
        /// <para>通常用于OAuth2接口之后</para>
        /// </summary>
        /// <param name="code">OAuth2返回的code</param>
        /// <returns>string:openid</returns>
        public string GetOpenidByCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code)) return null;

            var uri = "https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + Api.Appid + "&secret=" + Api.AppSec + "&code=" + code + "&grant_type=authorization_code";
            var res = WebHttp.WebReq(uri);
            var tokenInfo = new JavaScriptSerializer().Deserialize<TokenModel>(res);
            return tokenInfo == null ? null : tokenInfo.openid;
        }



        class UserModel
        {
            public int total { get; set; }
            public int count { get; set; }

            public openidModel data { get; set; }

            public string next_openid { get; set; }
        }
        class openidModel
        {
            public List<string> openid { get; set; }
        }

        class infoOpenid
        {
            public string openid { get; set; }
        }

        /// <summary>
        /// 通过code换取网页授权access_token 返回结果
        /// <see cref="https://mp.weixin.qq.com/wiki/4/9ac2e7b1f1d22e9e57260f6553822520.html"/>
        /// </summary>
        class TokenModel
        {
            /// <summary>
            /// 网页授权接口调用凭证,注意：此access_token与基础支持的access_token不同
            /// </summary>
            public string access_token { get; set; }

            /// <summary>
            /// 	access_token接口调用凭证超时时间，单位（秒）
            /// </summary>
            public int expires_in { get; set; }

            /// <summary>
            /// 	用户刷新access_token
            /// </summary>
            public string refresh_token { get; set; }

            /// <summary>
            /// 用户唯一标识，请注意，在未关注公众号时，用户访问公众号的网页，也会产生一个用户和公众号唯一的OpenID
            /// </summary>
            public string openid { get; set; }

            /// <summary>
            /// 用户授权的作用域，使用逗号（,）分隔
            /// </summary>
            public string scope { get; set; }
        }
    }
}
