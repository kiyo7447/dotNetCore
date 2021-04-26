using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetailService.Services.ShoppingCart
{
	public class ServiceSetup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddScoped<ITenantEvent, TenantEvent>();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			//app.UseEndpoints(endpoints =>
			//{
			//	endpoints.MapAreaControllerRoute(
			//		name: nameof(ShoppingCart),
			//		areaName: nameof(ShoppingCart),
			//		pattern: "{controller}/{action?}/{id?}");

			//});
		}
	}
}