<p align="left">
  <a href="https://iotsharp.io/">
    <img src="docs/static/img/logo_white.svg" width="360px" alt="IoTSharp logo" />
  </a>
</p>

[![Build status](https://ci.appveyor.com/api/projects/status/5o23f5vss89ct2lw/branch/master?svg=true)](https://ci.appveyor.com/project/MaiKeBing/iotsharp/branch/master)
![GitHub](https://img.shields.io/github/license/iotsharp/iotsharp.svg)
![.NET Core](https://github.com/IoTSharp/IoTSharp/workflows/.NET%20Core/badge.svg?branch=master)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=IoTSharp_IoTSharp&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=IoTSharp_IoTSharp)
[![Technical Debt](https://sonarcloud.io/api/project_badges/measure?project=IoTSharp_IoTSharp&metric=sqale_index)](https://sonarcloud.io/summary/new_code?id=IoTSharp_IoTSharp)
[![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=IoTSharp_IoTSharp&metric=reliability_rating)](https://sonarcloud.io/summary/new_code?id=IoTSharp_IoTSharp)
[![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=IoTSharp_IoTSharp&metric=code_smells)](https://sonarcloud.io/summary/new_code?id=IoTSharp_IoTSharp)


IoTSharp is an open-source IoT platform for data collection, processing, visualization, and device management.

## Here is a blessing for all users of this project
 * May you do good and not evil.
 * May you find forgiveness for yourself and forgive others.
 * May you share freely, never taking more than you give.

## How to  install  IoTSharp using docker-compose  ?

 * [ZPT](Deployments/zeromq_taos) Using ZeroMQ as EventBus, PostgreSQL as message storage, telemetry data stored through TDengine  

 * [ZPS](Deployments/zeromq_sharding) The default deployment configuration, IoTSharp and PostgreSql, telemetry data is stored through a single table or shading. 

 * [RMI](Deployments/rabbit_mongo_influx) Using Rabbitmq as EventBus, mongodb as message storage, telemetry data stored through influx  

 more [Deployments](https://github.com/IoTSharp/IoTSharp/tree/master/Deployments)

## What databases are supported?

 *  [PostgreSql](IoTSharp/appsettings.PostgreSql.json) The test environment is  PostgreSQL 11.3,Support for  sharding.
 *  [MySql](IoTSharp/appsettings.MySql.json) The test environment is MySQL 8.0.17,Support for  sharding.
 *  [Oracle](IoTSharp/appsettings.Oracle.json)  The test environment is  Oracle Standard Edition 12c Release 2 on CentOS , Support for  sharding.  See also: https://github.com/MaksymBilenko/docker-oracle-12c
 *  [SQLServer](IoTSharp/appsettings.SQLServer.json)  Microsoft SQL Server 2016 (RTM-GDR) (KB4019088) - 13.0.1742.0 (X64)   ,Support for  sharding
 *  [Sqlite](IoTSharp/appsettings.Sqlite.json) Support for  sharding
 *  [Cassandra](IoTSharp/appsettings.Cassandra.json)  

## What EventBus Message Queue  are supported?

 *  RabbitMQ
 *  Kafka 
 *	InMemory 
 *	ZeroMQ 
 *	NATS 
 *	Pulsar 
 *	RedisStreams 
 *	AmazonSQS 
 *	AzureServiceBus 

## What EventBus Store are supported?
* PostgreSql,
* MongoDB,
* InMemory,
* LiteDB,
* MySql,
* SqlServer

## IoTShar Demo 
    http://139.9.232.10:2927    
##  IoTSharp online  
  https://cloud.iotsharp.net/

## doc
  https://docs.iotsharp.net/

## How to install IoTSharp using docker ?

  -  docker pull iotsharp/iotsharp


## How to install  using Linux daemon ?

 -  mkdir  /var/lib/iotsharp/
 -	cp ./*  /var/lib/iotsharp/
 -	chmod 777 /var/lib/iotsharp/IoTSharp
 -	cp  iotsharp.service   /etc/systemd/system/iotsharp.service
 -	sudo systemctl enable  /etc/systemd/system/iotsharp.service 
 -	sudo systemctl start  iotsharp.service 
 -	sudo journalctl -fu  iotsharp.service 



##  IoTSharp.SDKs

- IoTSharp.Sdk.Http   [![IoTSharp.Sdk.Http](https://img.shields.io/nuget/v/IoTSharp.Sdk.Http.svg)](https://www.nuget.org/packages/IoTSharp.Sdk.Http/)
- IoTSharp.Sdk.MQTT   [![IoTSharp.Sdk.MQTT](https://img.shields.io/nuget/v/IoTSharp.Sdk.MQTT.svg)](https://www.nuget.org/packages/IoTSharp.Sdk.MQTT/)

 

## IoTSharp-C-Client-Sdk

IoTSharp-C-client-Sdk is mqttt client, write by   c;

 [https://github.com/IoTSharp/IoTSharp.Sdks.MQTT-C](https://github.com/IoTSharp/IoTSharp.Sdks.MQTT-C)

## paho.mqtt.c's demo 

It' like IoTSharp-C-Client-Sdk, but is use paho.mqtt.c
 https://github.com/IoTSharp/IoTSharp.Edge.paho.mqtt.c

## IoTSharp for nanoFramework

IoTSharp.Edge.nanoFramework is a nanoFramework's mqtt client , it run on STM32 ！

  https://github.com/IoTSharp/IoTSharp.Edge.nanoFramework

more info read https://www.cnblogs.com/MysticBoy/p/13159648.html
or click  https://www.nanoframework.net/

# IoTSharp for RTthread Package

https://github.com/IoTSharp/iotsharp-rtthread-package



IoTSharp's ecosystem

- MaiKeBing.CAP.ZeroMQ [![MaiKeBing.CAP.ZeroMQ](https://img.shields.io/nuget/v/MaiKeBing.CAP.ZeroMQ.svg)](https://www.nuget.org/packages/MaiKeBing.CAP.ZeroMQ/)
- MaiKeBing.CAP.LiteDB  [![MaiKeBing.CAP.LiteDB](https://img.shields.io/nuget/v/MaiKeBing.CAP.LiteDB.svg)](https://www.nuget.org/packages/MaiKeBing.CAP.LiteDB/)
- MaiKeBing.HostedService.ZeroMQ  [![MaiKeBing.HostedService.ZeroMQ](https://img.shields.io/nuget/v/MaiKeBing.HostedService.ZeroMQ.svg)](https://www.nuget.org/packages/MaiKeBing.HostedService.ZeroMQ/)
- IoTSharp.X509Extensions  [![IoTSharp.X509Extensions](https://img.shields.io/nuget/v/IoTSharp.X509Extensions.svg)](https://www.nuget.org/packages/IoTSharp.X509Extensions/)
- Silkier    [![Silkier](https://img.shields.io/nuget/v/Silkier.svg)](https://www.nuget.org/packages/Silkier/) 
- Silkier.EFCore   [![Silkier.EFCore](https://img.shields.io/nuget/v/Silkier.EFCore.svg)](https://www.nuget.org/packages/Silkier.EFCore/)
- Silkier.AspNetCore  [![Silkier.AspNetCore](https://img.shields.io/nuget/v/Silkier.AspNetCore.svg)](https://www.nuget.org/packages/Silkier.AspNetCore/)
- SilkierQuartz   [![SilkierQuartz](https://img.shields.io/nuget/v/SilkierQuartz.svg)](https://www.nuget.org/packages/SilkierQuartz/)
- IoTSharp.EntityFrameworkCore.Taos   [![IoTSharp.EntityFrameworkCore.Taos](https://img.shields.io/nuget/v/IoTSharp.EntityFrameworkCore.Taos.svg)](https://www.nuget.org/packages/IoTSharp.EntityFrameworkCore.Taos/)
- IoTSharp.Sdk.Http   [![IoTSharp.Sdk.Http](https://img.shields.io/nuget/v/IoTSharp.Sdk.Http.svg)](https://www.nuget.org/packages/IoTSharp.Sdk.Http/)
- IoTSharp.Sdk.MQTT   [![IoTSharp.Sdk.MQTT](https://img.shields.io/nuget/v/IoTSharp.Sdk.MQTT.svg)](https://www.nuget.org/packages/IoTSharp.Sdk.MQTT/)



## Contributing

[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg?style=flat-square)](https://github.com/IoTSharp/IoTSharp/pulls)

If you would like to contribute, feel free to create a [Pull Request](https://github.com/IoTSharp/IoTSharp/pulls), or give us [Bug Report](https://github.com/IoTSharp/IoTSharp/issues/new).

### Contributors

This project exists thanks to all the people who contribute.

<a href="https://github.com/IoTSharp/IoTSharp/graphs/contributors">
  <img src="https://contrib.rocks/image?repo=IoTSharp/IoTSharp" />
</a>

## Donation

This project is an  Apache 2.0 licensed open source project. In order to achieve better and sustainable development of the project, we expect to gain more backers. We will use the proceeds for community operations and promotion. You can support us in any of the following ways:

- [OpenCollective](https://opencollective.com/IoTSharp)
- 微信![二维码](docs/static/img/maikebing_wxpay.png)

We will put the detailed donation records on the below!


|                                                        | Name                                  | Stars | Donations | Message |
| ------------------------------------------------------------ | ------------------ | -------- | -------- | -------- |
| [![@iioter](https://avatars.githubusercontent.com/u/29589505?s=80&v=4)](https://github.com/iioter) | whd | ![GitHub User's stars](https://img.shields.io/github/stars/iioter?affiliations=OWNER%2CCOLLABORATOR%2CORGANIZATION_MEMBER&style=for-the-badge) | ￥1024 =120+100+292+512(码云共计四次) |  |
| [![@nnhy](https://avatars.githubusercontent.com/u/506367?s=80&v=4)](https://github.com/nnhy) | 大石头 | ![GitHub User's stars](https://img.shields.io/github/stars/nnhy?affiliations=OWNER%2CCOLLABORATOR%2CORGANIZATION_MEMBER&style=for-the-badge) |     ￥672=666+5（码云+公众号）     |            |
|  | 无敌飞行家 | ![GitHub User's stars](https://img.shields.io/github/stars/hehaoyu_2014?affiliations=OWNER%2CCOLLABORATOR%2CORGANIZATION_MEMBER&style=for-the-badge) | ￥5=5(公众号) |  |
|  | 匿名公司 |  | ￥1000=1000(微信转账) |  |
| [![@davidzhu001](https://avatars.githubusercontent.com/u/9436230?s=80&v=4)](https://github.com/davidzhu001)   | 农民也很疯狂 |  ![GitHub User's stars](https://img.shields.io/github/stars/davidzhu001?affiliations=OWNER%2CCOLLABORATOR%2CORGANIZATION_MEMBER&style=for-the-badge)| ￥400=200+200 微信转账 | |
| [![@280780363](https://avatars.githubusercontent.com/u/20083278?s=80&v=4)](https://github.com/280780363)   | 谷草 |  ![GitHub User's stars](https://img.shields.io/github/stars/280780363?affiliations=OWNER%2CCOLLABORATOR%2CORGANIZATION_MEMBER&style=for-the-badge)| ￥88 微信转账 | |


## Community Support

If you encounter any problems in the process, feel free to ask for help via following channels. We also encourage experienced users to help newcomers.

- [![Discord Server](https://img.shields.io/discord/895689311612178442?color=%237289DA&label=IoTSharp&logo=discord&logoColor=white&style=flat-square)](https://discord.gg/My6PaTmUvu)

| 公众号 |    [QQ群63631741](https://jq.qq.com/?_wv=1027&k=HJ7h3gbO)  |  微信群  |
| ------ | ---- | ---- |
| ![](docs/static/img/qrcode.jpg) | ![](docs/static/img/IoTSharpQQGruop.png) | ![企业微信群](docs/static/img/qyqun.jpg) |

## dotNET China

[![DotNetChina](https://images.gitee.com/uploads/images/2021/0309/134044_9c191d7b_974299.png)](https://gitee.com/dotnetchina/IoTSharp)

