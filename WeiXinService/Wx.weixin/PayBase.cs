using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Wx.Extend;

namespace Wx.Weixin
{
    private sealed class PayBase
    {
      
        public string Pay(Dictionary<string, string> dic,string url)
        {
            var nonce = _Nonce();
             
            dic.Add("nonce_str", nonce);
            dic.Add("mch_billno", _OrderNo());
            dic.Add("mch_id", Api.MchId);
            dic.Add("wxappid", Api.Appid);
            dic.Add("send_name", Api.MchName);
            dic.Add("client_ip",Api.MachineIp);

            //创建签名
            string strA = _PerParam(dic) + "&key=";
            string sign = strA.ToMd5().ToUpper();
            dic.Add("sign", sign);
            

            var postData = _DicToXmlStr(dic);
           return new WebHttp().WebPostSSL(url, postData,Api.CertPath,Api.CertPassword);
           
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
            return xml.ToString();

            //别问我为什么不用string+的形式，因为SB的效率高啊！
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
        private static string _OrderNo(){
            var time = DateTime.Now.ToString("HHmmssfff");
            time += new Random().Next(10).ToString();
            return Api.MchId+DateTime.Now.ToString("yyyyMMdd")+time;
        }

       
    }
}
