using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Wx.Extend
{
    public static class StringExtend
    {
        public static string ObjToString(this object val, string defaultVal = null) {

            try
            {
                if (val != null) {
                    return val.ToString();
                }
            }
            catch (Exception e)
            {
            }
            return defaultVal;
        }

        public static string ToMd5(this string str)
        { 
            MD5 md5 = MD5.Create();
            byte[] s = md5.ComputeHash(Encoding.ASCII.GetBytes(str));
            return BitConverter.ToString(s).Replace("-", ""); 
            
        }

        public static string ToString(this DateTime? date, string part)
        {
            try
            {
                if (date == null)
                {
                    return null;
                }

                DateTime time = date ?? DateTime.Now;
                return time.ToString(part);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
