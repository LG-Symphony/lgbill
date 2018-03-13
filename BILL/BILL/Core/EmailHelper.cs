using BILL.Bll.Token;
using BILL.Models.Token;
using System;
using System.Net;
using System.Net.Mail;

namespace BILL.Core
{
    public class EmailHelper
    {
        public static bool SendEmail(string Address,string Nickname)
        {
            try
            {
                //发送者
                var emailAcount = "ibh@ibhmail.lgwow.com";
                var emailPassword = "ligen930720ABC";
                //接受者
                var reciver = Address;
                //生成一个8位长的随机字符，具体长度可以自己更改
                string str = "23456789ABCDEFGHJKLMNPQRSTUVWXYZ";//75个字符
                Random r = new Random();
                string result = string.Empty;
                for (int i = 0; i < 6; i++)
                {
                    int m = r.Next(0, str.Length);//这里下界是0，随机数可以取到，上界应该是str.Length，因为随机数取不到上界，也就是最大str.Length-1，符合我们的题意
                    string s = str.Substring(m, 1);
                    result += s;
                }
                //删除老的验证码
                FindPwdVerifyBll.ExecuteSql("delete from FindPwdVerify where Email = '" + Address + "'");
                //将新验证码写到数据库
                FindPwdVerify model = new FindPwdVerify
                {
                    Email = Address,
                    StartTime = DateTime.Now,
                    Code = result.ToLower()//不区分大小写
                };
                FindPwdVerifyBll.Insert(model);
                //内容
                var content = "<!DOCTYPE html><html lang='en'><head><meta charset='UTF-8'><title>您的IBH账户：密码找回</title></head>";
                content += "<body><div style='width: 540px;;margin: 0 auto'>";
                content += "<div style='height: 55px;line-height: 55px;background-color: #171a21;color: #dbdbdb;font-size: 26px'><span style='margin-left: 20px'>IBH.Community</span></div>";
                content += "<div style='height:490px;border-top: 1px solid #4d4b48;background-color: #17212e;'>";
                content += "<div style='width: 80%;margin: 0 auto'><span style='color: #66c0f4;font-size: 24px;margin-top: 20px;display: inline-block'>敬爱的："+ Nickname + "</span>";
                content += "<span style='color: #c6d4df;font-size: 17px;margin-top: 20px;display: inline-block'>以下是您帐户 "+ Nickname + " 找回密码时所需的令牌验证码：</span>";
                content += "<span style='color: #66c0f4;font-size: 24px;margin-top: 20px;display: inline-block'>"+ result + "</span>";
                content += "<div style='height: 190px;background-color: #121a25;margin-top: 10px'>";
                content += "<span style = 'color: #c6d4df;font-size: 12px;display: inline-block;margin: 15px'> 要完成找回密码，您将需要 IBH.Community 令牌验证码。<span style = 'color: #fff;font-weight: bold'> 无人可以在不访问这封电子邮件的前提下重置您的密码。</span></span>";
                content += "<span style = 'color: #c6d4df;font-size: 12px;display: inline-block;margin: 15px'><span style = 'color: #fff;font-weight: bold'> 如果您未曾试图找回密码，</span>请更改您的 IBH.Community 密码，并考虑更改您的电子邮箱密码，确保您的帐户安全。</span>";
                content += "<span style='color: #c6d4df;font-size: 12px;display: inline-block;margin: 15px'><span style='color: #fff;font-weight: bold'>该令牌验证码30分钟内有效！</span></span></div>";
                content += "<span style = 'font-size: 12px;color: #6d7880;margin-top: 20px;display: inline-block'> IBH.Community 团队 </span>";
                content += "<span style = 'font-size: 12px;margin-top: 20px;display: block'><a style = 'color: #6d7880;' href = 'http://www.lgwow.com'> http://www.lgwow.com</a></span></div></div>";
                content += "<div style='height: 45px;line-height: 45px;background-color: #171a21;color: #dbdbdb;font-size: 9px'><span style='margin-left: 20px'>© 2017 lgwow.com </span></div></div></body></html>";
                MailMessage message = new MailMessage();
                //设置发件人,发件人需要与设置的邮件发送服务器的邮箱一致
                MailAddress fromAddr = new MailAddress("ibh@ibhmail.lgwow.com");
                message.From = fromAddr;
                //设置收件人,可添加多个,添加方法与下面的一样
                message.To.Add(reciver);
                //设置抄送人
                //message.CC.Add("izhaofu@163.com");
                //设置邮件标题
                message.Subject = "您的IBH.Community账户：密码找回";
                //设置邮件内容
                message.Body = content;
                message.IsBodyHtml = true;
                //设置邮件发送服务器,服务器根据你使用的邮箱而不同,可以到相应的 邮箱管理后台查看,下面是QQ的
                SmtpClient client = new SmtpClient("smtpdm.aliyun.com", 80);
                //设置发送人的邮箱账号和密码
                client.Credentials = new NetworkCredential(emailAcount, emailPassword);
                //启用ssl,也就是安全发送
                client.EnableSsl = false;
                //发送邮件
                client.Send(message);
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
            
        }
        
    }
}