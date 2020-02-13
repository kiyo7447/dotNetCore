using Serilog;
using System;

namespace ConsoleApp_Serilog
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
			Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Debug()
				.WriteTo.Console()
				.WriteTo.File("logfile.log", rollingInterval: RollingInterval.Day)
				.CreateLogger();

			Log.Debug("Starting up");
			Log.Debug("Shutting down");

			Console.ReadLine();
		}
	}
}
