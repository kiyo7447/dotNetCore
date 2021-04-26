using Amazon;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using System;
using System.IO;
using System.Threading.Tasks;

namespace S3
{
	class Program
	{
		/*
		 * このページでリストはとれた
		 * https://sig9.hatenablog.com/entry/2020/01/25/000000
		 * 
		 * 
		 * 
		 * 
		*/
		static async Task Main(string[] args)
		{
			Console.WriteLine("Hello World!");

			var ret = await new Program().Run(args);


		}

		private IAmazonS3 m_client;

		async Task<(bool x, bool y)> Run(string[] args)
		{
			Initialized();

			//バケットリストを表示する
			ListBucketsResponse res1 = await m_client.ListBucketsAsync();
			foreach (S3Bucket bucket in res1.Buckets)
			{ Console.WriteLine("{0}", bucket.BucketName); }




			//バケットを作成する
			var bucketName = @"bucket03";
			bool isExisted = await AmazonS3Util.DoesS3BucketExistV2Async(m_client, bucketName);
			if (isExisted == false)
			{
				PutBucketRequest req2 = new PutBucketRequest { BucketName = bucketName, UseClientRegion = true };
				PutBucketResponse res2 = await m_client.PutBucketAsync(req2);
				if (res2 != null)
				{
					Console.Write($"バケットの作成に失敗しました。{bucketName}");
				}
			}
			else
			{
				Console.Write($"既にバケットはある。BucketName={bucketName}");

			}




			//ファイルをアップロードする
			//二度目は更新されます。
			var req3 = new PutObjectRequest
			{
				BucketName = "bucket01",
				Key = "TextFile.txt",
				FilePath = @"TextFile.txt",

			};
			var res3 = await m_client.PutObjectAsync(req3);
			if (res3?.HttpStatusCode == System.Net.HttpStatusCode.OK)
			{
				Console.WriteLine($"ファイルのアップロードは成功した。BucketName={bucketName}");
			}

			//ファイルをダウンロードする
			//https://dev.classmethod.jp/articles/use-dot-net-core-and-aws-sdk-for-dot-net/
			var req4 = new GetObjectRequest { BucketName = "bucket01", Key = "abe.txt" };
			using (var res4 = await m_client.GetObjectAsync(req4))
			using (var streamReader = new StreamReader(res4.ResponseStream))
			{
				if (res4.HttpStatusCode == System.Net.HttpStatusCode.OK)
				{
					Console.WriteLine($"ファイルのダウンロードに成功した。");
				}
				Console.WriteLine(streamReader.ReadToEnd());
			}

			return (x: true, y: true);
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public void Initialized()
		{
			AmazonS3Config config = new AmazonS3Config() {
				ServiceURL = "http://127.0.0.1:9000/",
				//MINIOを動かすときはこれが必要
				//http://sheepdogjam.cocolog-nifty.com/blog/2020/03/post-c36bac.html
				ForcePathStyle = true,
			};
			/*			config.RegionEndpoint = RegionEndpoint.APNortheast1;

						// プロキシの設定を行う場合は、下記を設定
						config.ProxyHost = "127.0.0.1";
						config.ProxyPort = 9000;
			*/
			//SharedCredentialsFile credentialsFile = new SharedCredentialsFile();
			//CredentialProfile profile = null;

			//if (credentialsFile.TryGetProfile("minio", out profile) == false)
			//{
			//	Console.WriteLine("プロファイルの取得に失敗しました。");
			//	return;
			//}

			//AWSCredentials awsCredentials = null;
			//if (AWSCredentialsFactory.TryGetAWSCredentials(profile, credentialsFile, out awsCredentials) == false)
			//{
			//	Console.WriteLine("認証情報の生成に失敗しました。");
			//}

			m_client = new AmazonS3Client("MINIOACCESSKEY", "wJalrXUtnFEMI/K7MDENG/bPxRfiCYEXAMPLEKEY", config);

		}
	}
}
