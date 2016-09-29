using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Wx.Weixin
{
    public class UserManage
    {
        public string GetUserList()
        {
            //https://api.weixin.qq.com/cgi-bin/user/get?access_token=ACCESS_TOKEN&next_openid=NEXT_OPENID
            string uri = "https://api.weixin.qq.com/cgi-bin/user/get?access_token=" + Api.Token;
            var resStr = _webReq(uri);

            JavaScriptSerializer ser = new JavaScriptSerializer();
            var res = ser.Deserialize<UserModel>(resStr);

            var rsb = new StringBuilder();
            for (int i = 0; i < (res.count + 99) / 100; i++) {
                var reqData = res.data.openid.Skip(i * 100).Take(100).ToList();
              rsb.Append(GetUserInfo(reqData));

            }
            return rsb.ToString();
            
        }

        public string GetUserInfo(List<string> data)
        {
            //https://api.weixin.qq.com/cgi-bin/user/info/batchget?access_token=ACCESS_TOKEN
            List<infoOpenid> dataModel = new List<infoOpenid>();
            data = data.Take(100).ToList();
            data.ForEach((p) =>
            {
                dataModel.Add(new infoOpenid() { openid = p });
            });

            var dataJson ="{\"user_list\":"+ new JavaScriptSerializer().Serialize(dataModel)+"}";
            var postData = new System.Collections.Specialized.NameValueCollection();
            postData.Add("",dataJson);


           var postStr = _webPost("https://api.weixin.qq.com/cgi-bin/user/info/batchget?access_token=" + Api.Token, dataJson);
           return postStr;


        }

        private string _webReq(string uri)
        {

            string result = null;
            System.Net.WebRequest wReq = System.Net.WebRequest.Create(uri);
            System.Net.WebResponse wResp = wReq.GetResponse();
            System.IO.Stream respStream = wResp.GetResponseStream();
            using (System.IO.StreamReader reader = new System.IO.StreamReader(respStream, Encoding.GetEncoding("UTF-8")))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }
        private string _webPost(string uri, string data)
        {

            string result = null;
            System.Net.WebRequest wReq = System.Net.WebRequest.Create(uri);
            var byteArray =  Encoding.UTF8.GetBytes(data);
            wReq.Method = "POST";
            wReq.ContentType = "application/x-www-form-urlencoded";
            wReq.ContentLength = byteArray.Length;
            Stream dataStream = wReq.GetRequestStream();

            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();
            // Get the response.

            System.Net.WebResponse wResp = wReq.GetResponse();
            System.IO.Stream respStream = wResp.GetResponseStream();
            using (System.IO.StreamReader reader = new System.IO.StreamReader(respStream, Encoding.GetEncoding("UTF-8")))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }
        //private string _webPost(string uri, System.Collections.Specialized.NameValueCollection data)
        //{

        //    string result = null;
        //    System.Net.WebClient wReq = new System.Net.WebClient();
        //    byte[] wResp = wReq.UploadValues(uri, "POST", data);


        //    result = System.Text.Encoding.UTF8.GetString(wResp);
        //    return result;
        //}

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
