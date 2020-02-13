

https://stackify.com/nlog-vs-log4net-vs-serilog/

Serilogは性能がよい！
https://www.darylcumbo.net/serilog-vs-nlog-benchmarks/

現在のプロジェクトの設定

  <ItemGroup>
    <PackageReference Include="Serilog" Version="2.9.0" />
    <PackageReference Include="Serilog.Sinks.Console" Version="3.1.1" />
    <PackageReference Include="Serilog.Sinks.File" Version="4.1.0" />
  </ItemGroup>



.csprojの中身を↓こうしたプロジェクトもある。
	<ItemGroup>
		<PackageReference Include="Serilog.Enrichers.Thread" Version="3.1.0" />
		<PackageReference Include="Serilog.Extensions.Hosting" Version="3.0.0" />
		<PackageReference Include="Serilog.Sinks.Console" Version="3.1.1" />
		<PackageReference Include="Serilog.Sinks.File" Version="4.1.0" />
	</ItemGroup>



