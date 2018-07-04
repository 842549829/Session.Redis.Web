using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Session.Redis.Web.Code;

namespace Session.Redis.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region 注册HttpContext
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            #endregion

            #region 使用Redis保存Session
            /*
             *  Microsoft.AspNetCore.Session;
             *  Microsoft.AspNetCore.DataProtection.Redis;
             *  Microsoft.Extensions.Caching.Redis.Core;
             *  Microsoft.Extensions.Caching.Redis
             *  Microsoft.AspNetCore.Http;     //使用Session时有扩展方法
             */

            var redisConn = Configuration["WebConfig:Redis:Connection"];
            var redisInstanceName = Configuration["WebConfig:Redis:InstanceName"];
            //Session 过期时长分钟
            var sessionOutTime = Configuration.GetValue<int>("WebConfig:SessionTimeOut", 30);

            //var redis = StackExchange.Redis.ConnectionMultiplexer.Connect(redisConn);
            //services.AddDataProtection().PersistKeysToRedis(redis, "DataProtection-Test-Keys");
            services.AddDistributedRedisCache(option =>
                {
                    //redis 连接字符串
                    option.Configuration = redisConn;
                    //redis 实例名
                    option.InstanceName = redisInstanceName;
                }
            );

            //添加Session并设置过期时长
            services.AddSession(options => { options.IdleTimeout = TimeSpan.FromMinutes(sessionOutTime); });
            #endregion

            #region 注册视图规则
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new CustomViewLocationExpander());
            });
            #endregion

            #region 注册MVC
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            #region 注册HttpContext
            // 注册HttpContext
            app.UseStaticHttpContext();
            #endregion

            #region Session
            // 注册Session 必须在 UseMvc 之前调用
            app.UseSession();
            #endregion

            #region 路由
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            #endregion
        }
    }
}