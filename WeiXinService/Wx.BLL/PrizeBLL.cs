﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 

namespace Wx.BLL
{
    public class PrizeBLL
    {
        public PrizeCode GetPrizeByCode(string code)
        {
            using (var edm = new DataModel())
            {
                var data = edm.PrizeCode.FirstOrDefault(p => p.BarCode == code);
                return data;
            }
        }

        /// <summary>
        /// 奖品查询并修改访问次数
        /// </summary>
        /// <param name="code"></param>
        public PrizeCode PrizeQuery(string code)
        {
            using (var edm  = new DataModel())
            {
                var data = edm.PrizeCode.FirstOrDefault(p => p.BarCode == code);
                if(data!=null){
                    data.QueryCount = (data.QueryCount ?? 0) + 1;
                    data.FirstQuery = data.FirstQuery == null ? DateTime.Now : data.FirstQuery;
                    edm.SaveChanges();
                }
                return data;
            }
        }

        public PrizeSettings GetPrizeSetting(string no) {
            using (var edm = new DataModel())
            {
                var data = edm.PrizeSettings.FirstOrDefault(p => p.PrizeNo == no);
                return data;
            }
        }


        /// <summary>
        /// 修改奖品领奖状态
        /// </summary>
        /// <param name="code"></param>
        public void UpdatePrizeState(string code,string openid)
        {
            using (var edm = new DataModel())
            {
                var data = edm.PrizeCode.FirstOrDefault(p => p.BarCode == code);
                if (data != null)
                {
                    data.AwardFlag = true;
                    data.openId = openid;
                    data.AwardDate = DateTime.Now;
                    var setting = edm.PrizeSettings.FirstOrDefault(p => p.PrizeNo == data.PrizeNo);
                    if (setting != null)
                    {
                        setting.WinningCount += 1;

                    }
                    edm.SaveChanges();
                }
            }
        }

        /// <summary>
        /// 奖品领奖状态回滚
        /// </summary>
        /// <param name="code"></param>
        public void PrizeStateReback(string code) {
            using (var edm = new DataModel())
            {
                var data = edm.PrizeCode.FirstOrDefault(p => p.BarCode == code);
                if (data != null)
                {
                    data.AwardFlag =false;
                    //data.openId = null;
                    //data.AwardDate = null;
                    var setting = edm.PrizeSettings.FirstOrDefault(p => p.PrizeNo == data.PrizeNo);
                    if (setting != null)
                    {
                        setting.WinningCount -= 1;
                        
                    }
                    edm.SaveChanges();
                }
            }
        }
    }
}
