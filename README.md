

# Scm.Net

## 项目介绍

Scm.Net 是一个基于 .NET 平台的软件项目，结合 Vue.js 前端框架，提供了一套完整的前后端架构解决方案。该项目主要用于构建企业级应用程序，涵盖了从数据访问层到用户界面层的完整结构，支持多种功能模块，包括数据管理、文件上传、缓存服务、定时任务等。

## 软件架构

该项目后端使用 .NET 8.0 构建，包含多个模块化的类库，采用分层架构设计，包括：

- **DAO (Data Access Object)**: 负责与数据库交互，如 `Samples.Server.Dao` 模块。
- **DTO (Data Transfer Object)**: 数据传输对象，用于封装和传递数据。
- **DVO (Domain View Object)**: 用于前端展示的数据对象。
- **Service**: 提供业务逻辑处理，例如 `SamplesDemoService` 和 `SamplesBookService`。
- **Controllers**: 提供 RESTful API 接口，支持前端交互。

前端使用 Vue.js 框架，结合 Material Symbols 和其他公共资源，构建现代用户界面。

## 设计原则

- **模块化**: 各个功能模块独立，便于维护和扩展。
- **分层架构**: 清晰的分层结构，分离数据访问、业务逻辑和用户界面。
- **可扩展性**: 支持通过插件和模块扩展功能。
- **前后端分离**: � - **缓存**：使用 `Scm.Cache` 系列 DLL 提供缓存支持。
- **定时任务**：通过 `Scm.Server.Quartz` 支持定时任务管理。
- **文件处理**：支持文件上传、下载、Excel 导入导出等功能。
- **Redis 缓存**：提供 Redis 缓存集成支持。
- **代码生成**：`Scm.Generator` 支持代码模板生成。

## 项目特色

- **多数据库支持**：通过 `SqlSugar` 支持多种数据库操作。
- **文件操作模块**：支持多种文件格式和文件存储管理。
- **缓存机制**：支持内存缓存和 Redis 缓存。
- **前后端分离架构**：后端提供 API，前端使用 Vue.js 独立开发。
- **模块化设计**：易于扩展和维护。
- **企业级功能**：包括定时任务、审批流程、日志记录等。

## 开发环境搭建

1. **后端开发环境**:
   - 安装 .NET 8.0 SDK
   - 安装 Visual Studio 或 Rider
   - 恢复项目依赖项

2. **前端开发环境**:
   - 安装 Node.js 和 npm
   - 使用 `package.json` 安装前端依赖
   - 使用 Vue CLI 构建和运行项目

3. **数据库配置**:
   - 使用 `SqlSugar` 配置数据库连接
   - 初始化数据库结构

## 常见问题

### 如何运行后端服务？

使用 Visual Studio 或 Rider 打开 `.sln` 文件，运行 `Scm.Api` 项目。

### 如何运行前端？

进入 `vue` 目录，执行：

```bash
npm install
npm run serve
```

### 如何上传文件？

使用 `UploadRequest` 和 `UploadAsync` 方法进行文件上传，支持多模块文件处理。

### 如何实现 Excel 导入？

通过 `ImportRequest` 和 `ImportAsync` 方法实现 Excel 数据导入功能。

## 开源协议

该项目采用开源协议（请查看 LICENSE 文件）进行分发。

## 项目截图

- `pc-login.png`: 登录界面截图
- `pc-home.png`: 主界面截图
- `pc-task.png`: 任务模块界面
- `pc-sql.png`: 数据库操作界面

## 特别鸣谢

感谢 .NET 社区、Vue.js 开发团队以及所有为该项目提供支持的开源项目。

## 支持作者

如需支持项目开发，欢迎加入我们的 QQ 交流群（群号请查看 `README.md` 文件）。

## 联系方式

如需进一步帮助，请联系作者或提交 issue 到本项目的 Gitee 页面。