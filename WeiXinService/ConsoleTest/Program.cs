using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using Wx.Cache;
using Wx.Weixin;


namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //var res = new UserManage().GetUserList();
            var t = Wx.Weixin.Api.Token;
            new MessageManage().SendTextMsg("osObDvmhRn7lIbpRRINNZJvN-WJ4", "this is a test msg!");
            //var res = Url.Encode("");
            Console.WriteLine(t);
            Console.ReadKey();
            Console.WriteLine("==============================");
            CacheApi.Set("test01", "ttt", 1);
            //Console.WriteLine(t);
            Console.ReadKey();
            Console.WriteLine("==============================");
            Console.Write(CacheApi.Get("test"));
            Console.ReadKey();
        }
    }
}
