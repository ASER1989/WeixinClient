using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wx.Weixin
{
    public class ToolBase
    {
        internal string _PerParam(Dictionary<string, string> Param)
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
        internal string _DicToXmlStr(Dictionary<string, string> dic)
        {
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
        internal string _Nonce()
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
        internal string _OrderNo()
        {
            var time = DateTime.Now.ToString("HHmmssfff");
            time += new Random().Next(10).ToString();
            return Api.MchId + DateTime.Now.ToString("yyyyMMdd") + time;
        }

        internal string _GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
    }
}
