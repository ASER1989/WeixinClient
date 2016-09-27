using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wx.Weixin
{
    public abstract  class BaseApi
    {
        public  void GetToken(){
            GetAccToken();
        }

       public  abstract string GetAccToken();
        
      

    }
}
