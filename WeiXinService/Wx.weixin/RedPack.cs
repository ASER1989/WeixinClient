using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Wx.Weixin.ApiModels;
using Wx.Extend;

namespace Wx.Weixin
{
    public class RedPack
    {
        public RedPackBackModel SendReadPack(string openid, int amount)
        {
            //请求路径
            string url = "https://api.mch.weixin.qq.com/mmpaymkttransfers/sendredpack";

            //参数配置
            var dic = new Dictionary<string, string>();
            dic.Add("re_openid", openid); //接受人openid
            dic.Add("total_amount", amount.ToString()); //红包里装的毛爷爷（单位：分）
            dic.Add("total_num", "1");//红包发放数量（默认：1，暂不支持修改）
            dic.Add("wishing", "老板发大财！");//红包祝福语（暂未自定义）
            dic.Add("act_name", "红包测试，中饱私囊"); //活动名称
            dic.Add("remark", "JSON你懂吗？");//备注
            //return new PayBase().SignTest(dic, url);

            var resStr = new PayBase().Pay(dic, url);

            return _DecodeXml(resStr);

        }

        private RedPackBackModel _DecodeXml(string xml)
        {
            xml = xml.Replace("<xml>", "<xml><root>").Replace("<xml>", "<?xml version=\"1.0\" encoding=\"UTF-8\"?>").Replace("</xml>", "</root></xml>").Replace("</xml>", "");

            return xml.DeSerialize<RedPackBackModel>();
        }



    }
}
