using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wx.Weixin.ApiModels
{
    public class SendMsgModel
    {
        /// <summary>
        /// 返回代码
        /// <para>0--正常</para>
        /// </summary>
        public int errcode{get;set;}

        /// <summary>
        /// 错误详细信息
        /// </summary>
        public string errmsg { get; set; }
    }
}
