using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Session.Redis.Web.Code
{
    /// <summary>
    /// 当前上下文
    /// </summary>
    public static class HttpContext
    {
        /// <summary>
        /// IHttpContextAccessor
        /// </summary>
        private static IHttpContextAccessor _accessor;

        /// <summary>
        /// Current
        /// </summary>
        public static Microsoft.AspNetCore.Http.HttpContext Current => _accessor.HttpContext;

        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="httpContextAccessor">httpContextAccessor</param>
        internal static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _accessor = httpContextAccessor;
        }
    }
}
