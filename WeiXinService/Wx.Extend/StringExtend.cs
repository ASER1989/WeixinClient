﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Script.Serialization;

namespace Wx.Extend
{
    public static class StringExtend
    {
        public static string ObjToString(this object val, string defaultVal = null)
        {

            try
            {
                if (val != null)
                {
                    return val.ToString();
                }
            }
            catch (Exception e)
            {
            }
            return defaultVal;
        }

        /// <summary>
        /// 转换为32位MD5
        /// <para>
        /// 事实告诉我们，MD5很重要，因为有的时候未必是32位。
        /// </para>
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToMd5(this string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(str);
            bytes = md5.ComputeHash(bytes);
            md5.Clear();

            string ret = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                ret += Convert.ToString(bytes[i], 16).PadLeft(2, '0');
            }

            return ret.PadLeft(32, '0');

        }

        public static string SHA1(this string text)
        {
            byte[] cleanBytes = Encoding.Default.GetBytes(text);
            byte[] hashedBytes = System.Security.Cryptography.SHA1.Create().ComputeHash(cleanBytes);
            return BitConverter.ToString(hashedBytes).Replace("-", "");
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Serialize(this object obj)
        {
            return new JavaScriptSerializer().Serialize(obj);
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T DeSerialize<T>(this string str)
        {
            return new JavaScriptSerializer().Deserialize<T>(str);
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
