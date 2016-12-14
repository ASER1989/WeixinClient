using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wx.Weixin.ApiModels
{
    public class goods_detail
    {
        /// <summary>
        /// 商品的编号
        /// </summary>
        public string goods_id { get; set; }
        /// <summary>
        /// 微信支付定义的统一商品编号(可选）
        /// </summary>
        public string wxpay_goods_id { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string goods_name { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        public int quantity { get; set; }
        /// <summary>
        /// 商品单价，单位为分
        /// </summary>
        public int price { get; set; }

    }
}
