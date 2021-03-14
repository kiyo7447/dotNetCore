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

			//2hÇÃé¿çsÇééÇ∑
			//Thread.Sleep(2 * 3600 * 1000);

			_logger.LogInformation($"Begin End");

			return Task.FromResult(new HelloReply
			{
				Message = "Hello " + request.Name
			});
		}
	}
}
