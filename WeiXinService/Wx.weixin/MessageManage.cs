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
           
            var postObj = new
            {
                touser = openid,
                msgtype = "text",
                text = new { content = content }
            };

            var postJson = new JavaScriptSerializer().Serialize(postObj);

            return _Send(postJson);
          
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="postJson">string：消息Json</param>
        /// <returns>code</returns>
        private int _Send(string postJson) {
            string uri = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + Api.Token;

            //{"errcode":0,"errmsg":"ok"}
            var postStr = WebHttp.WebPost(uri, postJson);
            var res = new JavaScriptSerializer().Deserialize<SendMsgModel>(postStr);

            return res.errcode;
        }
    }
}
