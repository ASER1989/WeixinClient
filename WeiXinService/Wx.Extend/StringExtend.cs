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
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            var result = "";
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符

                result = result + s[i].ToString("X");

            }
            return result;
            //return BitConverter.ToString(s).Replace("-", ""); 
            
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
