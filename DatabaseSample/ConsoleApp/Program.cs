using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System;
using System.Diagnostics;

namespace ConsoleApp
{
	class Program
	{
		static int Main(string[] args)
		{
			Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Debug()
				.MinimumLevel.Override("Microsoft", LogEventLevel.Information)
				.Enrich.FromLogContext()
				.WriteTo.Console()
				.WriteTo.File("logfile.log", rollingInterval: RollingInterval.Day)
				.CreateLogger();

			var conputerName = Environment.GetEnvironmentVariable("COMPUTERNAME"); // 追加
			Log.Information($"conputerName={conputerName}");

			var host = Host.CreateDefaultBuilder(args)
//			var host = new HostBuilder()
				.ConfigureHostConfiguration(config =>
				{
					config.AddJsonFile("appsettings.json", optional: true);
					config.AddJsonFile($"appsettings.{conputerName}.json", optional: true);
					config.AddEnvironmentVariables(prefix: "KIYO_");
					config.AddCommandLine(args);
				})
				.ConfigureServices((hostContext, servieces) =>
				{
					servieces.Configure<AppSettings>(hostContext.Configuration.GetSection("Sample"));
					servieces.Configure<AppSettings>(hostContext.Configuration.GetSection("Section1"));
					//Configは一つが合理的
					//↓これは↑にまとめて削除しました。
					//servieces.Configure<CustomSettings>(hostContext.Configuration.GetSection("Section1"));

					servieces.AddHostedService<BizService>();
				})
				.UseSerilog()
				.ConfigureLogging((hostContext, config) =>
				{
					config.AddConsole();
					config.AddDebug();
				})
				//
				.Build();

			try
			{
				Log.Information("Starting host");
				host.Run();
				
				return 0;
			}
			catch (Exception ex)
			{
				Log.Fatal(ex, "Host terminated unexpectedly");

				return -1;
			}
			finally
			{
				Log.CloseAndFlush();
			}
			//Console.ReadKey();
		}
	}
}
