using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Enrichers;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace WindowsService
{
	public class Program
	{
		public static void Main(string[] args)
		{
			const string loggerTemplate = @"{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u4}]<{ThreadId}> [{SourceContext:l}] {Message:lj}{NewLine}{Exception}";
			var baseDir = AppDomain.CurrentDomain.BaseDirectory;
			//C:\Windows\system32が現在のカレントディレクトリとなるので、AppDomain.CurrentDomain.BaseDirectoryへ変更
			//var logfile = Path.Combine(baseDir, "App_Data", "logs", "log.txt");
			var logfile = Path.Combine(baseDir, "logs", "log.txt");
			Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
				.Enrich.With(new ThreadIdEnricher())
				.Enrich.FromLogContext()
				//コンソールログを追加
				.WriteTo.Console(LogEventLevel.Information, loggerTemplate, theme: AnsiConsoleTheme.Literate)
				//ファイルログを追加（毎日ローリングするプレーンテキストファイル）
				.WriteTo.File(logfile, LogEventLevel.Information, loggerTemplate,
					rollingInterval: RollingInterval.Day, retainedFileCountLimit: 90)
				.CreateLogger();

			try
			{
				Log.Information("====================================================================");
				Log.Information($"Application Starts. Version: {System.Reflection.Assembly.GetEntryAssembly()?.GetName().Version}");
				Log.Information($"Application Directory: {AppDomain.CurrentDomain.BaseDirectory}");
				
				//本来のコードはこの一行のみ
				CreateHostBuilder(args).Build().Run();
			}
			catch (Exception e)
			{
				Log.Fatal(e, "Application terminated unexpectedly");
			}
			finally
			{
				Log.Information("====================================================================\r\n");
				Log.CloseAndFlush();
			}
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.UseWindowsService()
				.ConfigureAppConfiguration((hostContext, config) =>
				{
					//
				})
				.ConfigureServices((hostContext, services) =>
				{
					services.AddHostedService<Worker>();
					services.Configure<AppSettings>(hostContext.Configuration.GetSection("AppSettings"));
				})
				//ログプロバイダを追加
				.UseSerilog();
	}
}
