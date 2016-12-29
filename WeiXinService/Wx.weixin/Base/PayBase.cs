using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Wx.Extend;
using System.Web.Script.Serialization;
using Wx.Weixin.ApiModels;


namespace Wx.Weixin
{
    internal sealed class PayBase : ToolBase
    {

        /// <summary>
        /// 发红包
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public string RedPack(Dictionary<string, string> dic, string url)
        {
            var nonce = _Nonce();

            dic.Add("nonce_str", nonce);
            dic.Add("mch_billno", _OrderNo());
            dic.Add("mch_id", Api.MchId);
            dic.Add("wxappid", Api.Appid);
            dic.Add("send_name", Api.MchName);
            dic.Add("client_ip", Api.MachineIp);

            //创建签名
            string strA = _PerParam(dic) + "&key=" + Api.SecretKey;
            string sign = strA.ToMd5().ToUpper();
            dic.Add("sign", sign);


            var postData = _DicToXmlStr(dic);
            SessionCore.Set("reqDic", strA + "  sign:" + sign);
            return new WebHttp().WebPostSSL(url, postData, Api.CertPath, Api.CertPassword);

        }

        /// <summary>
        /// 转账
        /// </summary>
        /// <returns></returns>
        public string Transfer(Dictionary<string, string> dic)
        {
            //请求路径
            string url = "https://api.mch.weixin.qq.com/mmpaymkttransfers/promotion/transfers";
            var nonce = _Nonce();

            dic.Add("nonce_str", nonce);
            dic.Add("partner_trade_no", _OrderNo());
            dic.Add("mchid", Api.MchId);
            dic.Add("mch_appid", Api.Appid);

            //dic.Add("send_name", Api.MchName);
            dic.Add("spbill_create_ip", Api.MachineIp);

            //创建签名
            string strA = _PerParam(dic) + "&key=" + Api.SecretKey;
            string sign = strA.ToMd5().ToUpper();
            dic.Add("sign", sign);

            var postData = _DicToXmlStr(dic);
            SessionCore.Set("reqDic", strA + "  sign:" + sign);
            return new WebHttp().WebPostSSL(url, postData, Api.CertPath, Api.CertPassword);

        }

        #region 订单支付
        public string CreateOrder(string ip, List<goods_detail> detail)
        {
            string url = "https://api.mch.weixin.qq.com/pay/unifiedorder";
            int amt = detail.Sum(p => p.price * p.quantity);
            var dic = new Dictionary<string, string>();
            var nocestr = _Nonce();
            var orderNo = _OrderNo();
            dic.Add("appid", Api.Appid);
            dic.Add("mch_id", Api.MchId);
            dic.Add("nonce_str", nocestr);
            dic.Add("out_trade_no", orderNo);
            dic.Add("total_fee", amt.ToString());
            dic.Add("body", Api.MchName + "-购物");
            dic.Add("spbill_create_ip", ip);
            dic.Add("notify_url", "");
            dic.Add("trade_type", "JSAPI");
            dic.Add("openid", SessionCore.OpenId);
            dic.Add("detail", "{\"goods_detail\":" + detail.Serialize() + "}");

            string strA = _PerParam(dic) + "&key=" + Api.SecretKey;
            string sign = strA.ToMd5().ToUpper();
            dic.Add("sign", sign);
            var postData = _DicToXmlStr(dic);

            return new WebHttp().WebPost(url, postData);

        }
        public string CreateOrder(string ip, int amt, string callback)
        {
            string url = "https://api.mch.weixin.qq.com/pay/unifiedorder";
            var dic = new Dictionary<string, string>();
            var nocestr = _Nonce();
            var orderNo = _OrderNo();
            dic.Add("appid", Api.Appid);
            dic.Add("mch_id", Api.MchId);
            dic.Add("nonce_str", nocestr);
            dic.Add("out_trade_no", orderNo);
            dic.Add("total_fee", amt.ToString());
            dic.Add("body", Api.MchName + "-馈赠");
            dic.Add("spbill_create_ip", ip);
            dic.Add("notify_url", callback);
            dic.Add("trade_type", "JSAPI");
            dic.Add("openid", SessionCore.OpenId);

            string strA = _PerParam(dic) + "&key=" + Api.SecretKey;
            string sign = strA.ToMd5().ToUpper();
            dic.Add("sign", sign);
            var postData = _DicToXmlStr(dic);

            return new WebHttp().WebPost(url, postData);

        }

        /// <summary>
        /// 支付JsApi参数
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public object GetPayConfig(string prepay_id)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string nonce = _Nonce().ToLower();
            string time = _GetTimeStamp();
            dic.Add("nonceStr", nonce);
            dic.Add("timeStamp", time);
            dic.Add("signType", "MD5");
            dic.Add("appId", Api.Appid);
            dic.Add("package", "prepay_id=" + prepay_id);
            //string sign = _PerParam(dic).ToMd5().ToUpper();
            string strA = _PerParam(dic) + "&key=" + Api.SecretKey;
            string sign = strA.ToMd5().ToUpper();
            var ret = new
            {
                appId = Api.Appid,
                timeStamp = time,
                nonceStr = nonce,
                package = "prepay_id=" + prepay_id,
                signType = "MD5",
                paySign = sign
            };
            return ret;

        }
        #endregion




    }
}
