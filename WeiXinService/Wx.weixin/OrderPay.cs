using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Wx.Weixin.ApiModels;
using Wx.Extend;

namespace Wx.Weixin
{
    public class OrderPay
    {
        public void Pay(string ip) {
            var str = new PayBase().CreateOrder(ip, 10, "http://aser.src.demo.ahyunhe.com/demo/paycall");
            var a = 1;
        }
    }
}
