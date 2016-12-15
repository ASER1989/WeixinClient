using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Wx.Weixin.ApiModels
{
    /// <summary>
    /// 预下单返回Model
    /// </summary>
    [XmlRoot("root")]
    [Serializable]
    public class UnifiedorderModel
    {
        public string return_code { get; set; }
        public string return_msg { get; set; }
        public string appid { get; set; }
        public string mch_id { get; set; }
        public string nonce_str { get; set; }
        public string sign { get; set; }

        public string result_code { get; set; }

        /// <summary>
        /// 微信生成的预支付回话标识，用于后续接口调用中使用，该值有效期为2小时
        /// </summary>
        public string prepay_id { get; set; }

        public string trade_type { get; set; }

    }
}
