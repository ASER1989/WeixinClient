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
           var t =  Wx.Weixin.Api.Token;
           
            Console.WriteLine(t);
            Console.ReadKey();
            Console.WriteLine("==============================");
            CacheApi.Set("test01", "ttt", 1);
            Console.WriteLine(t);
            Console.ReadKey();
            Console.WriteLine("==============================");
            Console.Write(CacheApi.Get("test"));
            Console.ReadKey();
        }
    }
}
