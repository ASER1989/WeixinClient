using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using ThoughtWorks.QRCode.Codec;

namespace Wx.Extend
{
    public class Qrcode
    {
        public Bitmap GetQrcode(string link)
        {
            Bitmap bmp = null;
            try
            {
                QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                qrCodeEncoder.QRCodeScale = 4;
                //int version = Convert.ToInt16(cboVersion.Text);
                qrCodeEncoder.QRCodeVersion = 7;
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                bmp = qrCodeEncoder.Encode(link);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Invalid version !");
            }
            return bmp;
        }
        /// <summary>
        /// 生成二维码并保存为图片
        /// </summary>
        /// <param name="link"></param>
        /// <param name="savepath"></param>
        public void SaveQrcode(string link, string savepath)
        {
            Bitmap bmp = GetQrcode(link);
            if (bmp != null) {
                bmp.Save(savepath, System.Drawing.Imaging.ImageFormat.Jpeg);
            } 

        }

    }
}
