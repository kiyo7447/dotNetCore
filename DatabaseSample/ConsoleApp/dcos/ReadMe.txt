■汎用ホストでのコンソールアプリケーションの作成




■構成情報の管理
https://docs.microsoft.com/ja-jp/aspnet/core/fundamentals/configuration/?view=aspnetcore-3.1
一般的な一連の構成プロバイダーは次のとおりです。
・ファイル (appsettings.json、appsettings.{Environment}.json。{Environment} はアプリの現在のホスト環境です)
	→OK
・Azure Key Vault
・ユーザー シークレット (Secret Manager) (開発環境のみ)
・環境変数
・コマンド ライン引数
→Azure Key VaultやUser Secretって面白そう

■.NETロギングフレームワーク

log4net		2001のJavaから以降
NLog		2006にバージョン1.0がリリース
Serilog		2013にリリースされた
				ベンチマークでは最強
				https://www.darylcumbo.net/serilog-vs-nlog-benchmarks/




■Entity Framework Core
https://thinkami.hatenablog.com/entry/2019/12/17/224720
１）Code First
	１．環境作成

		ライブラリのパスで
		dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 3.1.1
		dotnet tool install --global dotnet-ef --version 3.1.1
		⇒すでにインストール済み

	２．NuGetの追加
		Microsoft.EntityFrameworkCore.Design
		をNuGetで追加する。

	３．コードを変更する都度に実施すること

		変更内容をコード化
		dotnet ef migrations add InitialCreate

		それをデータベースに反映
		dotnet ef database update  

	メモ
		・途中でデータを追加したらMigrationsのソースに追加データが入る。
		・最初からやり直して、Migrationsを作りたい場合は、Migrationフォルダを削除して、データベースを削除するとよい。



