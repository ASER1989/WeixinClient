using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wx.Weixin.ApiModels
{
    public class RedPackModel
    {
        /// <summary>
        /// 随机字符串，不长于32位
        /// </summary>
        public string nonce_str { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }

        /// <summary>
        /// 商户订单号(mch_id+yyyymmdd+10位一天内不能重复的数字)
        /// </summary>
        public string mch_billno { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        public string mch_id { get; set; }

        /// <summary>
        /// 公众账号appid
        /// </summary>
        public string wxappid { get; set; }

        /// <summary>
        /// 商户名称(红包发送者名称)
        /// </summary>
        public string send_name { get; set; }

        /// <summary>
        /// 用户openid(接受红包的用户)
        /// </summary>
        public string re_openid { get; set; }

        /// <summary>
        /// 付款金额(单位:分)
        /// </summary>
        public int total_amount { get; set; }

        /// <summary>
        /// 红包发放总人数(total_num=1)
        /// </summary>
        public int total_num { get; set; }

        /// <summary>
        /// 红包祝福语
        /// </summary>
        public string wishing { get; set; }

        /// <summary>
        /// 调用接口的机器Ip地址
        /// </summary>
        public string client_ip { get; set; }


        /// <summary>
        /// 活动名称	
        /// </summary>
        public string act_name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 场景id(非必填）
        /// <para>PRODUCT_1:商品促销</para>
        /// <para>PRODUCT_2:抽奖</para>
        /// <para>PRODUCT_3:虚拟物品兑奖 </para>
        /// <para>PRODUCT_4:企业内部福利</para>
        /// <para>PRODUCT_5:渠道分润</para>
        /// <para>PRODUCT_6:保险回馈</para>
        /// <para>PRODUCT_7:彩票派奖</para>
        /// <para>PRODUCT_8:税务刮奖</para>
        /// </summary>
        public string scene_id { get; set; }

        /// <summary>
        /// 活动信息	(非必填）
        /// </summary>
        public string risk_info { get; set; }

        /// <summary>
        /// 资金授权商户号(服务商替特约商户发放时使用）(非必填）
        /// </summary>
        public string consume_mch_id { get; set; }
    }
}
