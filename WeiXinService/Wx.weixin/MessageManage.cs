using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Wx.Extend;
using Wx.Weixin.ApiModels;
namespace Wx.Weixin
{
    public class MessageManage
    {
        /// <summary>
        /// 发送文本消息
        /// </summary>
        public int SendTextMsg(string openid, string content)
        {
            string uri = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + Api.Token;
            var serializer = new JavaScriptSerializer();
            var postObj = new
            {
                touser = openid,
                msgtype = "text",
                text = new { content = content }
            };

            var postJson = serializer.Serialize(postObj);

            //{"errcode":0,"errmsg":"ok"}
            var postStr = WebHttp.WebPost(uri, postJson);
            var res = serializer.Deserialize<SendMsgModel>(postStr);
            
            return res.errcode;
        }
    }
}
