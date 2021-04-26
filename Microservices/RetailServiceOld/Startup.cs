using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RetailService
{
	public class Startup
	{
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			//REST Controller��ǉ�
			services.AddControllers();

//			services.AddControllers(opt => { opt. });

			//
			services.Scan(selector =>
				selector
				  .FromAssemblyOf<Startup>()
				  .AddClasses()
				  .AsImplementedInterfaces());


			//�p�����[�^�������ꍇ�͑S�ẴT�[�r�X���N������B


		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			//if (env.IsDevelopment())
			//{
			//    app.UseDeveloperExceptionPage();
			//}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();


			app.UseEndpoints(endpoints =>
			{
				//endpoints.MapAreaControllerRoute();
				endpoints.MapControllers();
				//endpoints.MapGet("/", async context =>
				//{
				//    await context.Response.WriteAsync("Hello World!");
				//});
			});
		}
	}

}
