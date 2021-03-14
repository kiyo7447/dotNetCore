using NLog;
using System;

namespace ConsoleApp_NLog
{
	class Program
	{
		static void Main(string[] args)
		{
#if false
			OutputTypeConfig();
#else
			OutputTypeCode();

#endif
			Console.ReadLine();
		}

		static void OutputTypeConfig()
		{
			//nlog.configファイルを使用してログの設定を行う。
			LogManager.LoadConfiguration("nlog.config");
			var log = LogManager.GetCurrentClassLogger();

			log.Info("Hello World!");
			log.Debug("Starting up");
			log.Debug("Shutting down");
		}

		static void OutputTypeCode()
		{
			//コードを使ってログマネージャーを初期化
			var config = new NLog.Config.LoggingConfiguration();
			var logfile = new NLog.Targets.FileTarget("logfile") { FileName = "logfile.txt" };
			var logconsole = new NLog.Targets.ConsoleTarget("logconsole");
			config.AddRule(LogLevel.Debug, LogLevel.Fatal, logconsole);
			config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);
			NLog.LogManager.Configuration = config;

			var log = LogManager.GetCurrentClassLogger();

			log.Info("Hello World!");
			log.Debug("Starting up");
			log.Debug("Shutting down");
		}
	}
}
