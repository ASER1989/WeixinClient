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
        private PayBase payBase;
        public OrderPay() {
            payBase = new PayBase();
        }
        public UnifiedorderModel Pay(string ip,int amt)
        {
            var str = payBase.CreateOrder(ip, amt, "http://aser.src.demo.ahyunhe.com/demo/paycall");
            return _DecodeXml(str);
        }

        public object GetJsConfig(string perpay_id) {
            return payBase.GetPayConfig(perpay_id);
        }

        private UnifiedorderModel _DecodeXml(string xml)
        {
            xml = xml.Replace("<xml>", "<xml><root>").Replace("<xml>", "<?xml version=\"1.0\" encoding=\"UTF-8\"?>").Replace("</xml>", "</root></xml>").Replace("</xml>", "");

            return xml.XMLDeSerialize<UnifiedorderModel>();
        }
    }
}
