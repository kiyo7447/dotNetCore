using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace ASPNETCoreAction2
{
    public class Startup
    {
        //Build
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //Build
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Pageフォルダタイプ
            services.AddRazorPages();

            //APIコントローラータイプ
            //services.AddControllers();

            //RazorビューとMVCコントローラタイプ
            //services.AddControllersWithViews();

            services.TryAddSingleton<IMessageSender, EmailSender>();
        }

        //Run
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Debug.WriteLine($"env.ApplicationName:{env.ApplicationName}");
            Debug.WriteLine($"env.ContentRootPath:{env.ContentRootPath}");
            Debug.WriteLine($"env.EnvironmentName:{env.EnvironmentName}");
            Debug.WriteLine($"env.WebRootPath:{env.WebRootPath}");

            //app.UseStatusCodePages

            if (env.IsDevelopment())
            {
                //↓ExceptionHandlerMiddleware
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //アプリケーションがセキュア（HTTPS）要求にのみ応答するようになり、業界のベストプラクティスになります。
            app.UseHttpsRedirection();
            //このミドルウェアは、CSSファイル、JavaScriptファイル、画像などの静的ファイルのリクエストを処理します。
            app.UseStaticFiles();
            

            //??
            app.UseRouting();

            //認証ミドルウェアは、Razorページが実行される前にアクセスを許可するかどうかを決定できます。
            app.UseAuthorization();

            //
            //app.UseMiddleware<WebcomePageMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
