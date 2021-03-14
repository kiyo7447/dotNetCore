using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp
{
	class BizService : IHostedService
	{
        private readonly IHostApplicationLifetime _appLifetime;
        private readonly ILogger<BizService> _logger;
        private readonly IOptions<AppSettings> _config;
        //private readonly IOptions<CustomSettings> _customConfig;
        public BizService(IHostApplicationLifetime appLifetime, IOptions<AppSettings> config,ILogger<BizService> logger)
        //public SampleBatchService(IHostApplicationLifetime appLifetime, IOptions<AppSettings> config, IOptions<CustomSettings> customConfig, ILogger<SampleBatchService> logger)
        {
            _appLifetime = appLifetime;
            _logger = logger;
            _config = config;
            //_customConfig = customConfig;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _appLifetime.ApplicationStarted.Register(OnStarted); // 最低限これだけあればバッチ処理としては成り立つ

            _logger.LogInformation("StartAsync");

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("StopAsync");

            return Task.CompletedTask;
        }

        private void OnStarted()
        {
            _logger.LogInformation("OnStarted");

            // 実際の処理はここに書きます
            _logger.LogInformation($"appsettings.json.Sample.name={_config.Value.Name}");
            _logger.LogInformation($"appsettings.json.Sample.value={_config.Value.Value}");

            //ここの値がappsettings.drifer.jsonで上書きされます。
            _logger.LogInformation($"key1={_config.Value.Key1}");
            _logger.LogInformation($"key2={_config.Value.Key2}");

            //_logger.LogInformation($"customkey1={_customConfig.Value.Key1}");
            //_logger.LogInformation($"key2={_customConfig.Value.Key2}");

            Thread.Sleep(1000);

            //この行がない場合は、Ctro+Cで終了となります。
            _appLifetime.StopApplication(); // 自動でアプリケーションを終了させる
        }
    }
}
