using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Wx.Cache;
using Wx.Weixin;
using Wx.Extend;
using System.Net;
using System.Web.Script;
using System.Web.Script.Serialization;
using System.Security.Cryptography;


namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {



            var dic = new Dictionary<string, string>();
            dic.Add("re_openid", "oK8WAt8VieVye7PJW41kU9oW_vH0"); //接受人openid
            dic.Add("total_amount", "100"); //红包里装的毛爷爷（单位：分）
            dic.Add("total_num", "1");//红包发放数量（默认：1，暂不支持修改）

            dic.Add("wishing", "老板发大财！");//红包祝福语（暂未自定义）
            dic.Add("act_name", "红包测试行动"); //活动名称
            dic.Add("remark", "JSON你懂吗？");//备注

            dic.Add("nonce_str", "PYDX4JFQ4W9JPF7YAAMPIT9Q77");
            dic.Add("mch_billno", "1405750202201611151415475244");
            dic.Add("mch_id", Api.MchId);
            dic.Add("wxappid", Api.Appid);
            dic.Add("send_name", Api.MchName);
            dic.Add("client_ip", "169.254.197.92");
            string strA = _PerParam(dic) + "&key=" + Api.SecretKey;
            string sign = strA.ToMd5().ToUpper();

            var d = md5(strA);
            Console.WriteLine(sign);

            Console.ReadKey();
        }

        public static String md5(String s)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(s);
            bytes = md5.ComputeHash(bytes);
            md5.Clear();

            string ret = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                ret += Convert.ToString(bytes[i], 16).PadLeft(2, '0');
            }

            return ret.PadLeft(32, '0');
        }
        static string _PerParam(Dictionary<string, string> Param)
        {
            var list = Param.OrderBy(s => s.Key);
            StringBuilder param = new StringBuilder();
            foreach (var s in list)
               param.Append(s.Key).Append("=").Append(s.Value).Append("&");

            var resStr = param.ToString().Trim(new char[] { '&' });
            return resStr;
        }

        static string GetAddressIP()
        {
            ///获取本地的IP地址
            string AddressIP = string.Empty;
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    AddressIP = _IPAddress.ToString();
                }
            }
            return AddressIP;
        }
    }

    public class Test {

        public T eq<T>(bool istrue,T v,T fv) {
            return istrue ? v : fv;
        }
    }
}
