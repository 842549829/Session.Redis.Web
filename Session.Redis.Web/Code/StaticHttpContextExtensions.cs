﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Session.Redis.Web.Code
{
    /// <summary>
    /// HttpContext扩展类型
    /// </summary>
    public static class StaticHttpContextExtensions
    {
        /// <summary>
        /// UseStaticHttpContext
        /// </summary>
        /// <param name="app">app</param>
        /// <returns>IApplicationBuilder</returns>
        public static IApplicationBuilder UseStaticHttpContext(this IApplicationBuilder app)
        {
            var httpContextAccessor = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
            HttpContext.Configure(httpContextAccessor);
            return app;
        }
    }
}
