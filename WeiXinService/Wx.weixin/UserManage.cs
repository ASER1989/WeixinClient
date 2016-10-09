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
            //https://api.weixin.qq.com/cgi-bin/user/get?access_token=ACCESS_TOKEN&next_openid=NEXT_OPENID
            string uri = "https://api.weixin.qq.com/cgi-bin/user/get?access_token=" + Api.Token;
            var resStr = WebHttp.WebReq(uri);

            JavaScriptSerializer ser = new JavaScriptSerializer();
            var res = ser.Deserialize<UserModel>(resStr);
            //res.count = 10;
            var rsb = new StringBuilder();
            for (int i = 0; i < (res.count + 99) / 100; i++) {

                var reqData = res.data.openid.Skip(i * 100).Take(100).ToList();
               
                var reqStr = GetUserInfo(reqData).Replace("{\"user_info_list\":[", "");
                reqStr = reqStr.Substring(0,reqStr.Length - 2);

                if(rsb.Length > 0 )rsb.Append(","); 

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
            //https://api.weixin.qq.com/cgi-bin/user/info/batchget?access_token=ACCESS_TOKEN
            List<infoOpenid> dataModel = new List<infoOpenid>();
            openidList = openidList.Take(100).ToList();
            openidList.ForEach((p) =>
            {
                dataModel.Add(new infoOpenid() { openid = p });
            });

            var dataJson ="{\"user_list\":"+ new JavaScriptSerializer().Serialize(dataModel)+"}";
            var postData = new System.Collections.Specialized.NameValueCollection();
            postData.Add("",dataJson);


            var postStr = WebHttp.WebPost("https://api.weixin.qq.com/cgi-bin/user/info/batchget?access_token=" + Api.Token, dataJson);
           return postStr;


        }
        /// <summary>
        /// 通过openid获取用户信息
        /// </summary>
        /// <param name="openid">openid</param>
        /// <returns>string，用户信息json字符串</returns>
        public string GetUserInfo(string openid) {
            //https://api.weixin.qq.com/cgi-bin/user/info?access_token=ACCESS_TOKEN&openid=OPENID&lang=zh_CN

            if (string.IsNullOrWhiteSpace(openid)) return null;

            var uri = "https://api.weixin.qq.com/cgi-bin/user/info?access_token=" + Api.Token + "&openid=" + openid + "&lang=zh_CN";
            return WebHttp.WebReq(uri);

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
    }
}
