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
    internal sealed class PayBase
    {
      
        /// <summary>
        /// 发红包
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public string RedPack(Dictionary<string, string> dic,string url)
        {
            var nonce = _Nonce();
             
            dic.Add("nonce_str", nonce);
            dic.Add("mch_billno", _OrderNo());
            dic.Add("mch_id", Api.MchId);
            dic.Add("wxappid", Api.Appid);
            dic.Add("send_name", Api.MchName);
            dic.Add("client_ip",Api.MachineIp);

            //创建签名
            string strA = _PerParam(dic) + "&key="+Api.SecretKey;
            string sign = strA.ToMd5().ToUpper();
            dic.Add("sign", sign);
            

            var postData = _DicToXmlStr(dic);
            SessionCore.Set("reqDic", strA+"  sign:"+sign);
           return new WebHttp().WebPostSSL(url, postData,Api.CertPath,Api.CertPassword);
           
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


        public string CreateOrder(string ip,List<goods_detail> detail) {
            string url = "https://api.mch.weixin.qq.com/pay/unifiedorder";
            int amt = detail.Sum(p => p.price * p.quantity);
            var dic = new Dictionary<string, string>();
            var nocestr= _Nonce();
            var orderNo = _OrderNo();
            dic.Add("appid",Api.Appid);
            dic.Add("mch_id",Api.MchId);
            dic.Add("nonce_str",nocestr);
            dic.Add("out_trade_no",orderNo);
            dic.Add("total_fee", amt.ToString());
            dic.Add("body",Api.MchName+"-购物");
            dic.Add("spbill_create_ip",ip);
            dic.Add("notify_url","");
            dic.Add("trade_type","JSAPI");
            dic.Add("openid", SessionCore.OpenId);
            dic.Add("detail", "{\"goods_detail\":"+detail.Serialize()+"}");

            string strA = _PerParam(dic) + "&key=" + Api.SecretKey;
            string sign = strA.ToMd5().ToUpper();
            dic.Add("sign", sign);
            var postData = _DicToXmlStr(dic);

            return new WebHttp().WebPost(url, postData);

        }
        public string CreateOrder(string ip,int amt,string callback)
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
            dic.Add("body", Api.MchName + "-购物");
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


        private string _PerParam(Dictionary<string, string> Param)
        {
            var list = Param.OrderBy(s => s.Key);
            StringBuilder param = new StringBuilder();
            foreach (var s in list)
               param.Append(s.Key).Append("=").Append(s.Value).Append("&");

            var resStr = param.ToString().Trim(new char[] { '&' });
            return resStr;
        }

        /// <summary>
        /// Dictionary转换为Xml格式字符串
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        private string _DicToXmlStr(Dictionary<string, string> dic) {
            StringBuilder xml = new StringBuilder();
            xml.Append("<xml>");

            foreach (var item in dic)
            {
                xml.Append("<").Append(item.Key).Append(">");
                xml.Append("<![CDATA[").Append(item.Value).Append("]]>");
                xml.Append("</").Append(item.Key).Append(">");
            }
            xml.Append("</xml>"); 

            //别问我为什么不用string+的形式，因为SB的效率高啊！

            return xml.ToString();
        }
        /// <summary>
        /// 获取随机字符串
        /// </summary>
        public string _Nonce()
        {
            int number;
            string checkCode = String.Empty;     //存放随机码的字符串   

            var random = new Random();
            int count = random.Next(23, 31);
            for (int i = 0; i < count; i++) //产生4位校验码   
            {
                number = random.Next();
                number = number % 36;
                if (number < 10)
                {
                    number += 48;    //数字0-9编码在48-57   
                }
                else
                {
                    number += 55;    //字母A-Z编码在65-90   
                }

                checkCode += ((char)number).ToString();
            }
            return checkCode.ToUpper();
        }

        /// <summary>
        /// 简易版订单号（当前时间毫秒的基础上加一位随机数）
        /// </summary>
        /// <returns></returns>
        private string _OrderNo(){
            var time = DateTime.Now.ToString("HHmmssfff");
            time += new Random().Next(10).ToString();
            return Api.MchId+DateTime.Now.ToString("yyyyMMdd")+time;
        }

       
    }
}
