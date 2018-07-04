using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Session.Redis.Web.Code
{
    public class Utility
    {

        /// <summary>
        /// 验证码
        /// </summary>
        public static string GetIpAddress()
        {
            var ipAddress = HttpContext.Current.Request.Host.Host;
            return ipAddress;
        }
    }
}
