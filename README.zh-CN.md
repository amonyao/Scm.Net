# BootstrapAdmin

<a href="README.md">English</a> | <span>中文</span>

---

## 项目介绍
异构应用。
一款基于.Net6/.Net7的后台管理系统框架，基于此项目的产品有：C-OMS（订单管理系统）、C-WMS（仓储管理系统）、C-TMS（运输管理系统）、C-DMS（配送管理系统）、Filexp.NET（图库管理系统）等。

## 项目特色
1. 采用前后端分离模式；
2. 后端基于.Net6开发，可直接升级到.Net7；
3. 后端仅依赖几个常用的三方类库：**[SqlSugarCore](https://www.donet5.com/Home/Doc)**(ORM工具）、**[Newtonsoft.Json](https://www.newtonsoft.com/json)**（JSON工具）、**[ImageSharp](https://github.com/SixLabors/ImageSharp)**（跨平台图像工具）；
4. 前端基于 **[Vue 3](https://vuejs.org)** 及 **[Element Plus](https://element-plus.gitee.io)** 开发；
5. 前端仅依赖几个常用的三方组件：**[Axios](https://axios-http.com/)**（HTTP工具），良好支持i18n（多语言）；
6. 系统无平台依赖，可直接在多平台（**Windows**、**MacOS**、**Linux**等）开发与运行。
7. 响应式布局，支持多种设备终端（**电脑**、**平板**、**手机**）等；

## 设计原则
1. 数据库仅用于存储数据，除CRUD以外不使用任何依赖特定数据库的特性；
2. 数据库原则上仅允许单表操作，最多不能同时操作两张表，可以一定程度上进行数据冗余设计；
3. 基于Json格式的多端数据交互，为了支持异构应用交互，DTO变更统一使用**蛇形命名法**；

## 特别说明

## 主要功能  
1. 首页自定义风格；
2. 支持多种登录方式（账户、手机、三方等）；
3. 支持代码自动生成，支持自定义代码模板；
4. 集成ID生成器，支持雪花ID、序列ID、格式ID等多种生成方式；
5. 支持多级权限管理：公司管理、部门管理、岗位管理、分组管理、用户管理、角色管理等。
6. 支持全局数据字典；
7. 支持全局配置参数；
8. 支持多种数据引擎（**MySQL**、**SQL Server**、**Oracle**、**SQLite**、**MariaDB**、**Postgresql**等）；
9. 支持多种缓存机制（**MemoryCache**、**Map**、**Redis**等）；
10. 支持**登录日志**与**操作日志**，并记录用户跟进信息（**登录主机**、**操作系统**、**浏览器**等）；

## 更新日志：
[更新日志](https://gitee.com/LongbowEnterprise/BootstrapAdmin/wikis/更新日志)

## 优势
1. 系统提供完善的示例与操作说明；
2. 系统将不同的功能进行模块化拆分，可以根据需要引入使用；
3. 后台系统无需任何二次开发，直接发布即可使用；
4. 前台与后台系统分离，分别为不同的系统（域名可独立）
5. 可扩展为多租户应用

[查看文档](https://gitee.com/LongbowEnterprise/BootstrapAdmin/wikis/%E9%A1%B9%E7%9B%AE%E4%BB%8B%E7%BB%8D)

## 数据库
数据库支持列表如下：  
**MSSQL/Oracle/SQLite/MySql/MariaDB/Postgresql/Firebird/MongoDB**  

## 浏览器支持

![chrome](https://img.shields.io/badge/chrome->%3D4.5-success.svg?logo=google%20chrome&logoColor=red)
![firefox](https://img.shields.io/badge/firefox->38-success.svg?logo=mozilla%20firefox&logoColor=red)
![edge](https://img.shields.io/badge/edge->%3D12-success.svg?logo=microsoft%20edge&logoColor=blue)
![ie](https://img.shields.io/badge/ie->%3D11-success.svg?logo=internet%20explorer&logoColor=blue)
![Safari](https://img.shields.io/badge/safari->%3D9-success.svg?logo=safari&logoColor=blue)
![Andriod](https://img.shields.io/badge/andriod->%3D4.4-success.svg?logo=android)
![oper](https://img.shields.io/badge/opera->%3D3.0-success.svg?logo=opera&logoColor=red)

```json
"browserslist": [
  "Chrome >= 45",
  "Firefox >= 38",
  "Edge >= 12",
  "Explorer >= 11",
  "iOS >= 9",
  "Safari >= 9",
  "Android >= 4.4",
  "Opera >= 30"
]
```  

## 移动端支持  
![ios](https://img.shields.io/badge/ios-supported-success.svg?logo=apple&logoColor=white)
![Andriod](https://img.shields.io/badge/andriod-suported-success.svg?logo=android)

|                        |  **Chrome**  |  **Firefox**  |  **Safari**  |  **Android Browser & WebView**  |  **Microsoft Edge**  |
| -------                | ---------    | ---------     | ------       | -------------------------       | --------------       |
|  **iOS**               | Supported    | Supported     | Supported    | N/A                             | Supported            |
|  **Android**           | Supported    | Supported     | N/A          | Android v5.0+ supported         | Supported            |

## 桌面端支持
![windows](https://img.shields.io/badge/windows-suported-success.svg?logo=windows)
![macOS](https://img.shields.io/badge/macOS-supported-success.svg?logo=apple&logoColor=white)
![linux](https://img.shields.io/badge/linux-suported-success.svg?logo=linux&logoColor=white)

|             | **Chrome**    | **Firefox**   | **Internet Explorer** | **Microsoft Edge** | **Opera**     | **Safari**    |
| -------     | ---------     | ---------     | -----------------     | --------------     | ---------     | ------------- |
| **Windows** | Supported     | Supported     | Supported, IE10+      | Supported          | Supported     | supported |
| **MacOS**   | Supported     | Supported     | N/A                   | N/A                | Supported     | Supported     |
| **Linux**   | Supported     | Supported     | N/A                   | N/A                | N/A           | N/A           |

## QQ交流群
[![QQ](https://img.shields.io/badge/QQ-795206915-green.svg?logo=tencent%20qq&logoColor=red)](https://shang.qq.com/wpa/qunwpa?idkey=d381355e50ff91db410c3da3eadb081ba859f64c2877e86343f4709b171f28b8)

## 开发环境搭建
1. 安装 .net sdk（.Net6或以上）[官方网址](https://dotnet.microsoft.com)
2. 安装 Visual Studio（2022或以上）[官方网址](https://visualstudio.microsoft.com)
3. 安装 MariaDB（10.3或上）[官方网址](https://mariadb.org)
4. 获取项目代码 [Scm.NET](https://gitee.com/LongbowEnterprise/BootstrapAdmin)  

环境搭建教程 [详细说明](https://gitee.com/LongbowEnterprise/BootstrapAdmin/wikis/%E5%AE%89%E8%A3%85%E6%95%99%E7%A8%8B?sort_id=1333477) 

数据库配置 [详细说明](https://gitee.com/LongbowEnterprise/BootstrapAdmin/wikis/数据库连接配置?sort_id=1333482)   

## 演示地址  
[![website1](https://img.shields.io/badge/linux-http://ba.zylweb.cn-success.svg?logo=buzzfeed&logoColor=green)](http://ba.zylweb.cn)
[![website2](https://img.shields.io/badge/linux-http://admin.blazor.zone-success.svg?logo=buzzfeed&logoColor=green)](http://admin.blazor.zone)  

## 登录用户名与密码  
管理账号：Admin/123789  
普通账号：User/123789

## 配置说明
详细配置说明请点击 [查看文档](https://gitee.com/LongbowEnterprise/BootstrapAdmin/wikis) 查看配置说明小节  

## 常见问题Q&A
请点击 [查看文档](https://gitee.com/LongbowEnterprise/BootstrapAdmin/wikis/%E5%B8%B8%E8%A7%81%E9%97%AE%E9%A2%98Q&A) 查看常见问题小节  

## 开源协议
[![Gitee license](https://img.shields.io/github/license/argozhang/bootstrapadmin.svg?logo=git&logoColor=red)](https://gitee.com/LongbowEnterprise/BootstrapAdmin/blob/master/LICENSE)

## 项目截图

后台首页

![后台首页](https://gitee.com/LongbowEnterprise/Pictures/raw/master/BootstrapAdmin/BA02-01.png "BAHome-01.png")

更多截图请点击 [查看文档](https://gitee.com/LongbowEnterprise/BootstrapAdmin/wikis) 查看项目截图小节  

## 特别鸣谢

1. <a href="https://gitee.com/571183806" target="_blank">**云龙**</a> 提供云服务器搭建在线演示系统
2. <a href="https://gitee.com/Ysmc" target="_blank">**一事冇诚**</a> 对 MongoDB 数据库提供了详细测试
3. <a href="https://gitee.com/Axxbis" target="_blank">**爱吃油麦菜**</a> 提供云服务器与二级域名搭建备份演示系统、测试环境以及图床
4. <a href="https://gitee.com/kasenhoo" target="_blank">**kasenhoo**</a> 对 CentOS + MySql 环境提供详细测试
5. <a href="https://gitee.com/finally44177" target="_blank">**finally44177**</a> 提供 AdminLTE UI 样式 PR 对 MongoDB 数据库提供了详细测试

## 捐助

如果这个项目对您有所帮助，并希望能够给更多的提供方便，请给予我们支持，深表感谢。

<img src="https://gitee.com/LongbowEnterprise/Pictures/raw/master/WeChat/BarCode@2x.png" width="382px;" />
