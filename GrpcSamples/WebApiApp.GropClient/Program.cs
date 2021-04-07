using Grpc.Net.Client;
using GrpcGreeter;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApiApp.GropClient
{
	class Program
	{
		static async Task Main(string[] args)
		{
			try
			{
#if true
				//元のソース
				var channel = GrpcChannel.ForAddress("https://localhost:5001");
#else
				//リクエストのタイムアウトを設定
				Console.WriteLine("Hello World!");
				var httpClient = new HttpClient();
				httpClient.Timeout = TimeSpan.FromSeconds(30);
				var grpcChannelOptions = new GrpcChannelOptions() { HttpClient = httpClient };
				var channel = GrpcChannel.ForAddress("https://localhost:5001", grpcChannelOptions);
				/*
				3秒後にエラーが発生した
				Grpc.Core.RpcException
				  HResult = 0x80131500
				  Message = Status(StatusCode = "Cancelled", Detail = "")
				  Source = System.Private.CoreLib
				  スタック トレース:
				   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
				   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
				   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
				   at System.Runtime.CompilerServices.TaskAwaiter`1.GetResult()
				   at WebApiApp.GropClient.Program.< Main > d__0.MoveNext() in C:\dev\GitHub\dotNetCore\WebApiApp.GropClient\Program.cs:line 21
				*/

#endif
				var client = new Greeter.GreeterClient(channel);
#if true
				//元のソース
				var response = await client.SayHelloAsync(
					new HelloRequest { Name = "{gRPCClient}" });
#else
			//クライアントタイムアウトを設定
			var response = await client.SayHelloAsync(
				new HelloRequest { Name = "World" }, deadline: DateTime.UtcNow.AddSeconds(30));
#endif
				if (response.ResultStatus == ResultStatus.Ok)
				{
					Console.WriteLine(response.Hello.Message);
				}
				else if (response.ResultStatus == ResultStatus.FunctionError)
				{
					Console.WriteLine(response.ErrorMessage);

				}
				else
				{
					Console.WriteLine(response.ErrorMessage);

				}


			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				throw;
			}
		}
	}
}
