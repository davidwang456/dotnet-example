# MyWebApi (.NET Framework 4.8)

这是一个基于 .NET Framework 4.8 的多模块解决方案：OWIN 自托管 Web API + 业务服务 + WPF 客户端 + NUnit 单元测试；配套 GitLab Windows Runner CI（镜像 `mcr.microsoft.com/dotnet/framework/sdk:4.8.1`）。

## 项目结构

```
dotnet/
├── MyWebApi/             # OWIN 自托管 Web API（net48）
│   ├── Program.cs        # WebApp.Start<Startup>()，默认 http://*:5000
│   ├── Startup.cs        # OWIN 启动，Web API 2 注册
│   └── Controllers/      # Web API 2 控制器
├── MyWebApi.Core/        # 核心实体与基础逻辑（net48）
├── MyWebApi.Service/     # 服务层（net48，示例使用 Newtonsoft.Json）
├── MyWebApi.Wpf/         # WPF 客户端（net48，MVVM 简化示例）
├── MyWebApi.Tests/       # NUnit 测试（net48）
├── MyWebApi.sln          # 解决方案文件
├── .gitlab-ci-docker.yml # GitLab CI（Windows Runner）
├── nuget.config          # 内网 NuGet 源配置（Nexus）
└── README.md
```

## 模块说明
- **MyWebApi.Core**：`WeatherForecast` 等实体与基础逻辑。
- **MyWebApi.Service**：`IWeatherForecastService` 与实现，演示三方包使用（`Newtonsoft.Json`）。
- **MyWebApi（Web API）**：OWIN 自托管 Web API 2，`SimpleResolver` 演示最小化 DI。
- **MyWebApi.Wpf**：WPF 客户端，`WeatherForecastViewModel` 调用服务展示列表。
- **MyWebApi.Tests**：NUnit 用例覆盖 Controller、Service、WPF ViewModel。

## 外部依赖引用
以 `MyWebApi` 为例，使用 `packages.config` 管理包，并在 `*.csproj` 中通过 `<Reference>` + `<HintPath>` 引用已还原的程序集（CI 在构建阶段通过 `nuget restore` 自动还原）。

## 本地开发
- 还原与构建：
  - `nuget restore MyWebApi.sln -PackagesDirectory packages -ConfigFile nuget.config`
  - `msbuild MyWebApi.sln /p:Configuration=Release`
- 运行 Web API：`MyWebApi\bin\Release\MyWebApi.exe`（默认 `http://localhost:5000/`）
- 运行 WPF：`MyWebApi.Wpf\bin\Release\MyWebApi.Wpf.exe`
- 运行测试：使用 `tools/NUnit.ConsoleRunner/tools/nunit3-console.exe` 执行测试 DLL 或在 VS 中运行

## 依赖注入
示例采用简易 `IDependencyResolver`（`SimpleResolver`）。生产环境建议引入 Autofac/Unity 等容器。

## CI/CD（Windows Runner）
- 镜像：`mcr.microsoft.com/dotnet/framework/sdk:4.8.1`（含 NuGet CLI）
- 阶段：`build` → `test` → `package` → `package-wpf` → `publish`
- 覆盖率：OpenCover + ReportGenerator（依赖仓库内 `tools/`，见下）

## CI 所需本地工具（tools/）
测试依赖以下可执行文件（置于仓库根 `tools/`）：
- `tools/OpenCover/tools/OpenCover.Console.exe`
- `tools/NUnit.ConsoleRunner/tools/nunit3-console.exe`
- `tools/ReportGenerator/tools/<任一子目录>/ReportGenerator.exe`

## NuGet（内网）
`nuget.config` 已清除外网源，仅保留内网 Nexus。请根据环境更新仓库 URL 与凭据。