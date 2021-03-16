using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GrpcService
{
	public class GreeterService : Greeter.GreeterBase
	{
		private readonly ILogger<GreeterService> _logger;
		public GreeterService(ILogger<GreeterService> logger)
		{
			_logger = logger;
		}

		public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
		{
			var httpContext = context.GetHttpContext();
			var c = httpContext.Request.Headers.Count;
			//				var clientCertificate = httpContext.Connection.ClientCertificate;

			_logger.LogInformation($"HttpContext.Request.Headers.Count:{c}");

			_logger.LogInformation($"Begin Start");

			//2hの実行を試す
			//Thread.Sleep(2 * 3600 * 1000);

			_logger.LogInformation($"Begin End");

/*
			Metadata trailers = new Metadata();
			trailers.Add("error_code", "01001");
			trailers.Add("error_message", "エラーメッセージです。");

			throw new RpcException(
				new Status(Grpc.Core.StatusCode.OK, "業務エラーです。")
				, trailers
			);
*/
/*
			throw new SystemException("システムエラーが発生しました。");

*/			return Task.FromResult(new HelloReply
			{
				ResultStatus = ResultStatus.Ok,
				Hello = new Hello { Message = "Hello " + request.Name }
			});
		}
	}
}
