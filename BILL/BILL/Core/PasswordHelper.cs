using System;
using System.Security.Cryptography;
using System.Text;

namespace BILL.Core
{
    public class PasswordHelper
    {
        /// <summary>
        /// 密码转Hash字符串
        /// </summary>
        /// <param name="pwdNormalStr">普通密码</param>
        /// <returns></returns>
        public static string PwdStrToHashStr(string pwdNormalStr)
        {
            HashAlgorithm createHash = HashAlgorithm.Create();
            byte[] pwdByte = Encoding.Default.GetBytes(pwdNormalStr);
            byte[] pwdHash = createHash.ComputeHash(pwdByte);
            string pwdHashStr = BitConverter.ToString(pwdHash);
            createHash.Clear();
            return pwdHashStr;
        }
        /// <summary>
        /// 检查普通密码是否等于Hash密码
        /// </summary>
        /// <param name="pwdNormalStr">普通密码</param>
        /// <param name="dataBasePwdHashStr">Hash密码</param>
        /// <returns></returns>
        public static bool CheckPwd(string pwdNormalStr, string PwdHashStr)
        {
            var inputPwd = PwdStrToHashStr(pwdNormalStr);
            if (Equals(inputPwd, PwdHashStr))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}