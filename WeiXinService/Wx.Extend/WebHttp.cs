using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Wx.Extend
{
    public class WebHttp
    {

        public static string WebReq(string uri)
        {

            string result = null;
            System.Net.WebRequest wReq = System.Net.WebRequest.Create(uri);
            System.Net.WebResponse wResp = wReq.GetResponse();
            System.IO.Stream respStream = wResp.GetResponseStream();
            using (System.IO.StreamReader reader = new System.IO.StreamReader(respStream, Encoding.GetEncoding("UTF-8")))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }
        public static string WebPost(string uri, string data)
        {

            string result = null;
            System.Net.WebRequest wReq = System.Net.WebRequest.Create(uri);
            var byteArray = Encoding.UTF8.GetBytes(data);
            wReq.Method = "POST";
            wReq.ContentType = "application/x-www-form-urlencoded";
            wReq.ContentLength = byteArray.Length;
            Stream dataStream = wReq.GetRequestStream();

            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();
            // Get the response.

            System.Net.WebResponse wResp = wReq.GetResponse();
            System.IO.Stream respStream = wResp.GetResponseStream();
            using (System.IO.StreamReader reader = new System.IO.StreamReader(respStream, Encoding.GetEncoding("UTF-8")))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }
    }
}
