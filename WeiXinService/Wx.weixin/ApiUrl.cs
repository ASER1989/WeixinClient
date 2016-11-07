using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wx.Weixin
{
    public class ApiUrl
    {
        private static string _SendMsg = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=";
        
        /// <summary>
        /// 发送消息（文本，图片，图文，视频，音乐）
        /// <para>需要带Token</para>
        /// </summary>
        public static string SendMsg { get { return _SendMsg; } }

        
    }
}
