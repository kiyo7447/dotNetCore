


Create a Windows Service App in .NET Core 3.0
https://codeburst.io/create-a-windows-service-app-in-net-core-3-0-5ecb29fb5ad0

１）プロジェクトはワーカーサービスで作成する

２）NuGetの追加
	Microsoft.Extensions.Hosting
	Microsoft.Extensions.Hosting.WindowsServices

	.csprojの中身
	  <ItemGroup>
	    <PackageReference Include="Microsoft.Extensions.Hosting" Version="3.1.1" />
	    <PackageReference Include="Microsoft.Extensions.Hosting.WindowsServices" Version="3.1.1" />

		<PackageReference Include="Serilog.Enrichers.Thread" Version="3.1.0" />
		<PackageReference Include="Serilog.Extensions.Hosting" Version="3.0.0" />
		<PackageReference Include="Serilog.Sinks.Console" Version="3.1.1" />
		<PackageReference Include="Serilog.Sinks.File" Version="4.1.0" />
	  </ItemGroup>

３）



４）出荷

Releaseビルドして管理者のDOS窓で↓を実行した。

:: Create a Windows Service
sc create WindowsService DisplayName="Windows Service Silverlight Isolated Storage Watching Service" binPath="D:\dev\sample_gomi\20200212_ワーカーサービス\WorkerService\WindowsService\bin\Release\netcoreapp3.1\WindowsService.exe"

:: Start a Windows Service
sc start WindowsService

:: Stop a Windows Service
sc stop WindowsService

:: Delete a Windows Service
sc delete WindowsService


※Power Shellの場合は、
New-Service、Start-Service、Get-Service、Stop-Service、Remove-Service
で実行しても良い。


