using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HelloService
{
	public class HelloerService : Helloer.HelloerBase
	{
		private readonly ILogger<HelloerService> _logger;
		public HelloerService(ILogger<HelloerService> logger)
		{
			_logger = logger;
		}

		public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
		{
			var httpContext = context.GetHttpContext();
			var c = httpContext.Request.Headers.Count;

			_logger.LogInformation($"HttpContext.Request.Headers.Count:{c}");

			return Task.FromResult(new HelloReply
			{
				ResultStatus = ResultStatus.Ok,
				Hello = new Hello { Message = "Helloer.SayHello:" + request.Name }
			});
		}
	}
}
