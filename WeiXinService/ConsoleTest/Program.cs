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


namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {

            new RedPack().SendReadPack("t000t",1);

            //using (var ms = new MemoryStream(Encoding.UTF8.GetBytes("<xml><return_code><![CDATA[SUCCESS]]></return_code><return_msg><![CDATA[发放成功.]]></return_msg><result_code><![CDATA[SUCCESS]]></result_code><err_code><![CDATA[0]]></err_code><err_code_des><![CDATA[发放成功.]]></err_code_des><mch_billno><![CDATA[0010010404201411170000046545]]></mch_billno><mch_id>10010404</mch_id><wxappid><![CDATA[wx6fa7e3bab7e15415]]></wxappid><re_openid><![CDATA[onqOjjmM1tad-3ROpncN-yUfa6uI]]></re_openid><total_amount>1</total_amount></xml>")))
            //{
            //    using (var sr = new StreamReader(ms, Encoding.UTF8))
            //    {
                    

            //        Console.WriteLine(sr.ReadToEnd()); 
            //    }
            //}
            //byte[] resByte = Encoding.Default.GetBytes("<xml><return_code><![CDATA[SUCCESS]]></return_code><return_msg><![CDATA[发放成功.]]></return_msg><result_code><![CDATA[SUCCESS]]></result_code><err_code><![CDATA[0]]></err_code><err_code_des><![CDATA[发放成功.]]></err_code_des><mch_billno><![CDATA[0010010404201411170000046545]]></mch_billno><mch_id>10010404</mch_id><wxappid><![CDATA[wx6fa7e3bab7e15415]]></wxappid><re_openid><![CDATA[onqOjjmM1tad-3ROpncN-yUfa6uI]]></re_openid><total_amount>1</total_amount></xml>");
            //resByte = Encoding.Convert(Encoding.Default, Encoding.GetEncoding("GBK"), resByte);




            //var s = new RedPack().DecodeRes("<xml><return_code><![CDATA[SUCCESS]]></return_code><return_msg><![CDATA[发放成功.]]></return_msg><result_code><![CDATA[SUCCESS]]></result_code><err_code><![CDATA[0]]></err_code><err_code_des><![CDATA[发放成功.]]></err_code_des><mch_billno><![CDATA[0010010404201411170000046545]]></mch_billno><mch_id>10010404</mch_id><wxappid><![CDATA[wx6fa7e3bab7e15415]]></wxappid><re_openid><![CDATA[onqOjjmM1tad-3ROpncN-yUfa6uI]]></re_openid><total_amount>1</total_amount></xml>");
            //Console.WriteLine(s);
            //Console.WriteLine(new RedPack().SendReadPack("oK8WAt8VieVye7PJW41kU9oW_vH0",1));

retry:
            //var str = new PayBase()._Nonce();
            //Console.WriteLine(str);
          
            var y = Console.ReadKey();
            if (y.KeyChar.ToString() == "y") {
                goto retry;
            }
            //var res = new UserManage().GetUserList();
            //var t = Wx.Weixin.Api.Token;
            //new MessageManage().SendTextMsg("osObDvmhRn7lIbpRRINNZJvN-WJ4", "this is a test msg!");
            //var res = Url.Encode("");
            //Console.WriteLine(t);
            //Console.ReadKey();
            //Console.WriteLine("==============================");
            //CacheApi.Set("test01", "ttt", 1);
            ////Console.WriteLine(t);
            //Console.ReadKey();
            //Console.WriteLine("==============================");
            //Console.Write(CacheApi.Get("test"));
            Console.ReadKey();
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
