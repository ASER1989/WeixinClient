using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Wx.Weixin.ApiModels
{
    /// <summary>
    /// 微信红包接口返回结果
    /// </summary>
    [XmlRoot("root")]  
    [Serializable]
    public class RedPackBackModel
    {
        public string return_code { get; set; }

        public string return_msg { get; set; }

        public string result_code { get; set; }

        public string err_code { get; set; }

        public string err_code_des { get; set; }

        public string mch_billno { get; set; }

        public string mch_id { get; set; }

        public string wxappid { get; set; }

        public string re_openid { get; set; }

        public string total_amount { get; set; }

        //转账部分
        public string mch_appid { get; set; }

        public string mchid { get; set; }

        public string device_info { get; set; }

        public string partner_trade_no { get; set; }

        public string payment_no { get; set; }

        public string payment_time { get; set; }
        

    }
}
