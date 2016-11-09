using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Wx.Extend
{
    public class WebHttp
    {

        public  string WebReq(string uri)
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
        public  string WebPost(string uri, string data)
        {

            string result = null;

            System.Net.WebRequest wReq = System.Net.WebRequest.Create(uri);
            var byteArray = Encoding.UTF8.GetBytes(data);
            wReq.Method = "POST";
            wReq.ContentType = "application/x-www-form-urlencoded";
            wReq.ContentLength = byteArray.Length;
            Stream dataStream = wReq.GetRequestStream();
             
            dataStream.Write(byteArray, 0, byteArray.Length); 
            dataStream.Close(); 

            System.Net.WebResponse wResp = wReq.GetResponse();
            System.IO.Stream respStream = wResp.GetResponseStream();
            using (System.IO.StreamReader reader = new System.IO.StreamReader(respStream, Encoding.GetEncoding("UTF-8")))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }

        public string WebPostSSL(string uri, string data, string certPath,string password)
        {

            string result = null;

            //CerPath证书路径
            //string certPath = ConfigurationManager.AppSettings["certPath"].ToString();
            ////证书密码
            //string password = ConfigurationManager.AppSettings["password"].ToString();
            X509Certificate2 cert = new System.Security.Cryptography.X509Certificates.X509Certificate2(certPath, password, X509KeyStorageFlags.MachineKeySet);

            System.Net.WebRequest wReq = System.Net.WebRequest.Create(uri);
            var byteArray = Encoding.UTF8.GetBytes(data);
            wReq.Method = "POST";
            wReq.ContentType = "application/x-www-form-urlencoded";
            wReq.ContentLength = byteArray.Length;
            Stream dataStream = wReq.GetRequestStream();
             
            dataStream.Write(byteArray, 0, byteArray.Length); 
            dataStream.Close(); 

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
