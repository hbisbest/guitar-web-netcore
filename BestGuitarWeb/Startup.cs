using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using BestGuitarWeb.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;

namespace BestGuitarWeb
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILoggerFactory _loggerFactory;

        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment, ILoggerFactory loggerFactory)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
            _loggerFactory = loggerFactory;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region 配置Cookie服务策略

            services.Configure<CookiePolicyOptions>(options =>
                {
                //是否需要用户确认才能使用core的cookie策略
                options.CheckConsentNeeded = context => false;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                });

            #endregion

            #region 添加session服务

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                options.Cookie.HttpOnly = true;
            });

            #endregion

            #region 添加MVC服务 + 添加全局异常处理

            services.AddMvc(o =>
                {
                    o.Filters.Add(typeof(GlobalExceptionFilter)); // 全局异常过滤
                })
                // 取消默认驼峰
                .AddJsonOptions(options => { options.SerializerSettings.ContractResolver = new DefaultContractResolver(); });

            //加入mvc的所有默认功能
            //如果不加会导致控制器中的依赖注入失败
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            #endregion

            #region 添加Http上下文服务

            //注入Http上下文服务
            //相当于services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
            services.AddHttpContextAccessor();

            #endregion

            #region 内置DI注册



            #endregion

            #region 添加Swagger服务

            string applicationBasePath = AppContext.BaseDirectory;
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("V1", new Info() {Title = "DemoAPI", Version = "V1"});
                c.IncludeXmlComments(Path.Combine(applicationBasePath, "WebApi.xml"));
            });

            #endregion

            //避免传给视图的中文出现乱码
            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            #region 异常错误处理中间件

            if (_hostingEnvironment.IsDevelopment())
            {
                //在开发环境下使用开发人员异常页面，也就是显示异常信息和堆栈调用的页面（不能给用户看）
                app.UseDeveloperExceptionPage();

                //报告数据库运行时错误
                app.UseDatabaseErrorPage();
            }
            else
            {
                //在生产环境下发生异常时给用户看为用户服务的错误页
                app.UseExceptionHandler("~/Views/Shared/Error.cshtml");

                //为了web安全，HTTP严格传输安全协议HSTS中间件添加Strict-Transport-Security标头
                app.UseHsts();
            }

            #endregion

            #region 配置HTTP错误代码页

            //使用HTTP错误代码页（例如访问不存在的页面时）
            app.UseStatusCodePages();

            //该方法还支持lambda表达式
            //app.UseStatusCodePages(async context =>
            //{
            //    context.HttpContext.Response.ContentType = "text/plain";
            //    await context.HttpContext.Response.WriteAsync("Status Code Page，Status Code:" +
            //                                                  context.HttpContext.Response.StatusCode);
            //});

            //还可以跳转到指定页面，并附加Response.StatusCode
            //app.UseStatusCodePagesWithReExecute("/Home/Error/{0}"); 

            #endregion

            #region HTTPS重定向中间件

            app.UseHttpsRedirection();

            #endregion

            #region 可请求静态文件

            app.UseStaticFiles();

            #endregion

            #region 使用Cookie

            app.UseCookiePolicy();

            #endregion

            #region 身份验证中间件

            app.UseAuthentication();

            #endregion

            #region 使用session

            app.UseSession();

            #endregion

            //#region 使用MVC

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(name: "default", template: "{controller}/{action}/{id?}",
            //        defaults: new { controller = "Login", action = "Index" });
            //});

            //#endregion

            #region 配置Swagger中间件

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DemoAPI V1");
                //加载汉化的js文件，注意 swagger.js文件属性必须设置为“嵌入的资源”
                c.InjectJavascript("/Content/js/swagger_cn.js");
            });

            #endregion

            #region 配置Http请求上下文中间件

            var httpContextAccessor = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
            Utility.HttpContext.Configure(httpContextAccessor);

            #endregion

            //第一个Run委托终止了管道（如果把Run放在最前面，后面添加的中间件都没用了）
            //调用单个匿名函数以响应每个HTTP请求
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
