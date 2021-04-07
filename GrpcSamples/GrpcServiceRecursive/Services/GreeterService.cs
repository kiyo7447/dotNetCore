using Grpc.Core;
using Grpc.Net.Client;
using GrpcHelloer;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GreetService
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

			_logger.LogInformation($"HttpContext.Request.Headers.Count:{c}");

			try
			{
				//元のソース
				var channel = GrpcChannel.ForAddress("https://localhost:5001");
				var client = new Helloer.HelloerClient(channel);
				//元のソース
				var response = client.SayHello(
					new GrpcHelloer.HelloRequest { Name = "World" });
			}
			catch(Exception ex)
			{
				throw new SystemException("", ex);
			}
				return Task.FromResult(new HelloReply
			{
				ResultStatus = ResultStatus.Ok,
				Hello = new Hello { Message = "Hello " + request.Name }
			});
		}
	}
}
