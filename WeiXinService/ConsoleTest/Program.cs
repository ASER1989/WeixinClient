using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wx.Cache;


namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            new Wx.Weixin.Api().GetToken();
            Cache.Set("test", "ttt",10);
            Console.WriteLine(Cache.Get("test"));
            Console.ReadKey();
            Console.WriteLine("==============================");
            Cache.Set("test01", "ttt", 1);
            Console.WriteLine(Cache.Get("test"));
            Console.ReadKey();
            Console.WriteLine("==============================");
            Console.Write(Cache.Get("test"));
            Console.ReadKey();
        }
    }
}
