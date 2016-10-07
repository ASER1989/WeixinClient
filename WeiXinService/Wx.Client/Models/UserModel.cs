using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wx.Client.Models
{
    public class UserModel
    {
        //
        // 是否已关注公众号
        // 1: 已关注
        // 0：未关注
        public int subscribe { get; set; }

        public string openid { get; set; }

        public string nickname { set; get; }

        public int sex { get; set; }

        public string language { get; set; }

        public string city { get; set; }

        public string province { get; set; }

        public string country { get; set; }

        public string headimgurl { get; set; }

        //关注时间
        public string subscribe_time { get; set; }

        public string unionid { get; set; }

        public string remark { get; set; }
        //组id
        public int groupid { get; set; }

        public string tagid_list { get; set; }
    }

    public class UserReqModel
    {
        public List<UserModel> user_info_list { get; set; }
    }
}