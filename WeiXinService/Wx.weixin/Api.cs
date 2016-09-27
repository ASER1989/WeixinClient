using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace Wx.Weixin
{
    public class Api 
    {
        public string GetToken()
        {
            string appid = "wxa8d4e8cabb20e0d8";
            string appsec = "77f944c9ca7964485253564ac7b38d80";
            string result = null;

            System.Net.WebRequest wReq = System.Net.WebRequest.Create("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + appid + "&secret=" + appsec);
            System.Net.WebResponse wResp = wReq.GetResponse();
            System.IO.Stream respStream = wResp.GetResponseStream();
            using (System.IO.StreamReader reader = new System.IO.StreamReader(respStream, Encoding.GetEncoding("UTF-8")))
            {
                result= reader.ReadToEnd();
            }

            return result;

        }
    }
}
