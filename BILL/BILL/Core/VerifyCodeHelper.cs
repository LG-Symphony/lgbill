using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BILL.Core
{
    public class VerifyCodeHelper
    {
        #region 变量
        /// <summary>
        /// 颜色表
        /// </summary>
        private static Color[] colors = new Color[]{
      Color.FromArgb(220,20,60),
      Color.FromArgb(128,0,128),
      Color.FromArgb(65,105,225),
      Color.FromArgb(70,130,180),
      Color.FromArgb(46,139,87),
      Color.FromArgb(184,134,11),
      Color.FromArgb(255,140,0),
      Color.FromArgb(139,69,19),
      Color.FromArgb(0,191,255),
      Color.FromArgb(95,158,160),
      Color.FromArgb(255,20,147),
      Color.FromArgb(255,165,0)};

        /// <summary>
        /// 字体表
        /// </summary>
        private static string[] fonts = new string[] {
      "Arial",
      "Verdana",
      "Georgia",
      "黑体" };

        /// <summary>
        /// 字体大小
        /// </summary>
        private static int fontSize = 22;
        #endregion

        #region 生成验证码图片
        /// <summary>
        /// 生成验证码图片
        /// </summary>
        public static Bitmap CreateVerifyCodeBmp(out string code)
        {
            int width = 120;
            int height = 40;
            Bitmap bmp = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bmp);
            Random rnd = new Random();

            //背景色
            g.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, width, height));

            //文字
            StringBuilder sbCode = new StringBuilder();
            //var returnStr = "";
            for (int i = 0; i < 4; i++)
            {
                string str = GetChar(rnd);
                Font font = GetFont(rnd);
                Color color = GetColor(rnd);
                g.DrawString(str, font, new SolidBrush(color), new PointF((float)(i * width / 4.0), 0));
                sbCode.Append(str);
                //if (i<3)
                //{
                //    returnStr += (str.ToLower() + ",");
                //}
                //else
                //{
                //    returnStr += str.ToLower();
                //}
                
            }
            code = sbCode.ToString() ;
            //code = returnStr;

            //噪音线
            for (int i = 0; i < 5; i++)
            {
                int x1 = rnd.Next(bmp.Width);
                int x2 = rnd.Next(bmp.Width);
                int y1 = rnd.Next(bmp.Height);
                int y2 = rnd.Next(bmp.Height);

                Pen p = new Pen(GetColor(rnd), 1);
                g.DrawLine(p, x1, y1, x2, y2);
            }

            //扭曲
            bmp = TwistImage(bmp, true, 3, rnd.NextDouble() * Math.PI * 2);
            g = Graphics.FromImage(bmp);

            //噪点
            for (int i = 0; i < 50; i++)
            {
                int x1 = rnd.Next(bmp.Width);
                int y1 = rnd.Next(bmp.Height);

                Pen p = new Pen(GetColor(rnd), 1);
                g.DrawRectangle(p, x1, y1, 1, 1);
            }

            //边框
            g.DrawRectangle(new Pen(new SolidBrush(Color.FromArgb(153, 153, 153))), new Rectangle(0, 0, width - 1, height - 1));

            return bmp;
        }
        #endregion

        #region 获取随机字符
        /// <summary>
        /// 获取随机字符
        /// </summary>
        private static string GetChar(Random rnd)
        {
            int n = rnd.Next(0, 61);
            if (n <= 9)
            {
                return ((char)(48 + n)).ToString();
            }
            else if (n <= 35)
            {
                return ((char)(65 + n - 10)).ToString();
            }
            else
            {
                return ((char)(97 + n - 36)).ToString();
            }
        }
        #endregion

        #region 获取随机字体
        /// <summary>
        /// 获取随机字体
        /// </summary>
        private static Font GetFont(Random rnd)
        {
            return new Font(fonts[rnd.Next(0, fonts.Length)], fontSize, FontStyle.Bold);
        }
        #endregion

        #region 获取随机颜色
        /// <summary>
        /// 获取随机颜色
        /// </summary>
        private static Color GetColor(Random rnd)
        {
            return colors[rnd.Next(0, colors.Length)];
        }
        #endregion

        #region 正弦曲线Wave扭曲图片
        /// <summary>  
        /// 正弦曲线Wave扭曲图片（Edit By 51aspx.com）  
        /// </summary>  
        /// <param name="srcBmp">图片路径</param>  
        /// <param name="bXDir">如果扭曲则选择为True</param>  
        /// <param name="nMultValue">波形的幅度倍数，越大扭曲的程度越高，一般为3</param>  
        /// <param name="dPhase">波形的起始相位，取值区间[0-2*PI)</param>  
        private static System.Drawing.Bitmap TwistImage(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
        {
            System.Drawing.Bitmap destBmp = new Bitmap(srcBmp.Width, srcBmp.Height);

            // 将位图背景填充为白色  
            System.Drawing.Graphics graph = System.Drawing.Graphics.FromImage(destBmp);
            graph.FillRectangle(new SolidBrush(System.Drawing.Color.White), 0, 0, destBmp.Width, destBmp.Height);
            graph.Dispose();

            double dBaseAxisLen = bXDir ? (double)destBmp.Height : (double)destBmp.Width;

            for (int i = 0; i < destBmp.Width; i++)
            {
                for (int j = 0; j < destBmp.Height; j++)
                {
                    double dx = 0;
                    dx = bXDir ? (Math.PI * 2 * (double)j) / dBaseAxisLen : (Math.PI * 2 * (double)i) / dBaseAxisLen;
                    dx += dPhase;
                    double dy = Math.Sin(dx);

                    // 取得当前点的颜色  
                    int nOldX = 0, nOldY = 0;
                    nOldX = bXDir ? i + (int)(dy * dMultValue) : i;
                    nOldY = bXDir ? j : j + (int)(dy * dMultValue);

                    System.Drawing.Color color = srcBmp.GetPixel(i, j);
                    if (nOldX >= 0 && nOldX < destBmp.Width
                     && nOldY >= 0 && nOldY < destBmp.Height)
                    {
                        destBmp.SetPixel(nOldX, nOldY, color);
                    }
                }
            }

            return destBmp;
        }
        #endregion
    }
}