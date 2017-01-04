using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wx.BLL;
using Wx.Client.Filter;
using Wx.Client.Models;
using Wx.Extend;
using Wx.Weixin;

namespace Wx.Client.Controllers
{
    public class PrizeController : Controller
    {
        //
        // GET: /Prize/
        [Login]
        public ActionResult Index(string code = null)
        {
            var prbll = new PrizeBLL();
            var data = prbll.PrizeQuery(code);

            TempData["isRedPack"] = "0";
            if (data!=null)
            { 
                 var prize = prbll.GetPrizeSetting(data.PrizeNo);
                 //prizeType：虚拟奖品1，实物奖品2 && prize.PrizeType == 1
                 if (prize != null)
                 {
                     TempData["isRedPack"] = "1";
                 }
            }
            return View(data);
        }

        [Login]
        public ActionResult Detail(string code = null)
        {
            var prbll = new PrizeBLL();
            var data = prbll.GetPrizeByCode(code);
            TempData["isWin"] = "-1";
            //发红包
            if (data != null  && data.AwardFlag == false)
            {
                TempData["isWin"] = "0";
                var prize = prbll.GetPrizeSetting(data.PrizeNo);
                //prizeType：虚拟奖品1，实物奖品2
                if (prize != null && prize.PrizeType==1) {
                    //修改奖品状态
                    prbll.UpdatePrizeState(data.BarCode,SessionCore.OpenId);
                    try
                    {
                        var res = new RedPack().SendReadPack(SessionCore.OpenId, prize.Money ?? 0);
                        if (res.return_code == "SUCCESS" && res.result_code == "SUCCESS")
                        {
                            return View(data);
                        }
                        else
                        {
                            //奖品状态回滚
                            prbll.PrizeStateReback(data.BarCode);
                            return Redirect("/res/error/Error.html");
                        }
                    }
                    catch (Exception ex)
                    {
                        //奖品状态回滚
                        prbll.PrizeStateReback(data.BarCode);
                        return Redirect("/res/error/Error.html");
                    }
                   
                }
                
            }
            return View(data);
        }

    }
}
