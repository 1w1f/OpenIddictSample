using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace server.Tools
{
    public static class Md5Helper
    {
        public static string GetMd5(string originStr)
        {
            try
            {
                if (originStr==null) throw new ArgumentException("参数不可为空");
                var md5Instance = MD5.Create();
                var originStrBytes = Encoding.UTF8.GetBytes(originStr);
                var encodedBytes = md5Instance!.ComputeHash(originStrBytes);
                var encodedStr = BitConverter.ToString(encodedBytes);
                return encodedStr;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
