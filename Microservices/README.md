# Microservice
## 作り方
```
#Databaseの作成（SQL Server）
docker pull mcr.microsoft.com/mssql/server
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=P@ssw0rd' -p 1433:1433 -d mcr.microsoft.com/mssql/server

docker ps

CONTAINER ID        IMAGE                            COMMAND                  CREATED             STATUS              PORTS                      NAMES
3388b710892f        mcr.microsoft.com/mssql/server   "/opt/mssql/bin/perm…"   52 minutes ago      Up 52 minutes       0.0.0.0:1433->1433/tcp     friendly_bell

select @@VERSION
-- Microsoft SQL Server 2019 (RTM-CU10) (KB5001090) - 15.0.4123.1 (X64)
 	Mar 22 2021 18:10:24 	Copyright (C) 2019 Microsoft Corporation
     	Developer Edition (64-bit) on Linux (Ubuntu 20.04.2 LTS) <X64>


# キャッシュの作成

# RabbitMQの作成


# S3の作成




```

## ServiceSetup

* 法人追加/削除
* 初期データ
  * 初期データ
  * デモデータ
  * 自動テストデータ

## メンテナンス 
* ウォーム処理
* ヘルスチュエック
  * Live
  * 
* ステータス
  * システムエラー情報（最終）
  *
* データ削除
　* データベース
  * キャッシュ
  * ファイル

## アプリケーションバージョン管理

* バージョン情報取得
* アプリケーション更新
  * スキーマ更新
  * データ更新
    * 稼働法人データの更新
  *

## ライセンス管理


