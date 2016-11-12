using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Wx.Cache;
using Wx.Weixin;
using Wx.Extend;
using System.Net;


namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine(GetAddressIP());

retry:
            var str = new PayBase()._Nonce();
            Console.WriteLine(str);
          
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
