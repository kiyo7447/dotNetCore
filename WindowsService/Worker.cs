using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace WindowsService
{
	public class Worker : BackgroundService
	{
		private readonly ILogger<Worker> _logger;
		private FileSystemWatcher _folderWatcher;
		private readonly AppSettings _appSettings;

		public Worker(ILogger<Worker> logger, IOptions<AppSettings> appSettings)
		{
			_logger = logger;
			//マジックストリングはappsettings.jsonへ移動
			//_appSettings.WatchFolder = @"D:\temp";
			//↓
			_appSettings = appSettings.Value;
		}
		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			await Task.CompletedTask;
		}
		//protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		//{
		//    while (!stoppingToken.IsCancellationRequested)
		//    {
		//        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
		//        await Task.Delay(1000, stoppingToken);
		//    }
		//}

		public override Task StartAsync(CancellationToken cancellationToken)
		{
			_logger.LogInformation("Service Starting");
			if (!Directory.Exists(_appSettings.WatchFolder))
			{
				_logger.LogWarning($"Please make sure the InputFolder [{_appSettings.WatchFolder}] exists, then restart the service.");
				return Task.CompletedTask;
			}

			_logger.LogInformation($"Binding Events from Input Folder: {_appSettings.WatchFolder}");
			//*.TXTを監視します。⇒すべてのファイルを監視します
			_folderWatcher = new FileSystemWatcher(_appSettings.WatchFolder, _appSettings.RepresentsTest)
			{
				NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.LastWrite | NotifyFilters.FileName |
								  NotifyFilters.DirectoryName,
				IncludeSubdirectories = _appSettings.IncludeSubdirectories
			};
			_folderWatcher.Created += Input_OnChanged;
			_folderWatcher.Changed += Input_OnChanged;
			_folderWatcher.Deleted += Input_OnChanged;
			_folderWatcher.Renamed += Input_OnChanged;
			_folderWatcher.Error += _folderWatcher_Error;

			_folderWatcher.EnableRaisingEvents = true;

			return base.StartAsync(cancellationToken);
		}

		private void _folderWatcher_Error(object sender, ErrorEventArgs e)
		{
			_logger.LogInformation($"Exception={e.GetException().ToString()}");
		}

		protected void Input_OnChanged(object source, FileSystemEventArgs e)
		{
			Action<string> file2Log = (file) =>
			{
				try
				{
					_logger.LogInformation(File.ReadAllText(file));
				}
				catch (Exception ex)
				{
					_logger.LogInformation($"{ex.ToString()}");
				}
			};

			switch (e.ChangeType)
			{

				case WatcherChangeTypes.Created:
					_logger.LogInformation($"Created: [{e.FullPath.Replace(_appSettings.WatchFolder,"")}]");

					// do some work
					//file2Log(e.FullPath);

					//_logger.LogInformation("Done with Inbound Change Event");
					break;
				case WatcherChangeTypes.Changed:
					_logger.LogInformation($"Changed: [{e.FullPath.Replace(_appSettings.WatchFolder, "")}]");

					// do some work
					//file2Log(e.FullPath);

					//_logger.LogInformation("Done with Inbound Change Event");

					break;

				case WatcherChangeTypes.Deleted:
					_logger.LogInformation($"Deleted: [{e.FullPath.Replace(_appSettings.WatchFolder, "")}]");

					// do some work

					//_logger.LogInformation("Done with Inbound Change Event");
					break;

				case WatcherChangeTypes.Renamed:

					_logger.LogInformation($"Renamed: [{e.FullPath.Replace(_appSettings.WatchFolder, "")}]");

					// do some work

					//_logger.LogInformation("Done with Inbound Change Event");
					break;

			}
		}

		public override async Task StopAsync(CancellationToken cancellationToken)
		{
			_logger.LogInformation("Stopping Service");
			_folderWatcher.EnableRaisingEvents = false;
			await base.StopAsync(cancellationToken);
		}

		public override void Dispose()
		{
			_logger.LogInformation("Disposing Service");
			_folderWatcher.Dispose();
			base.Dispose();
		}
	}
}
