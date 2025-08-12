# SSL/TLS 证书问题解决方案

## 问题描述

在使用 `mcr.microsoft.com/dotnet/framework/sdk:4.8.1` 镜像时，可能遇到以下错误：

```
fatal the underlying connection was closed could not establish trust relationship for the ssl/tsl secure channel
```

## 原因分析

1. **内网自签名证书**：内网 Nexus 服务器使用自签名证书
2. **证书不受信任**：容器内缺少根证书颁发机构
3. **TLS 版本不匹配**：服务器和客户端 TLS 版本不兼容
4. **代理设置问题**：内网环境中的代理配置

## 解决方案

### 方案一：跳过 SSL 验证（推荐用于内网环境）

#### 1. 修改 `.gitlab-ci-docker.yml`

在 `build` 和 `publish` 阶段添加 SSL 跳过配置：

```yaml
build:
  script:
    - |
      $ErrorActionPreference = 'Stop'
      # 配置 .NET Framework 跳过 SSL 验证
      [System.Net.ServicePointManager]::ServerCertificateValidationCallback = {$true}
      [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.SecurityProtocolType]::Tls12
      
      # 使用 -NoSSL 参数
      nuget restore $env:SOLUTION_FILE -NonInteractive -ConfigFile .\nuget.config -PackagesDirectory $env:NUGET_PACKAGES -NoSSL
```

#### 2. 修改 `nuget.config`

```xml
<configuration>
  <packageSources>
    <clear />
    <add key="MyNexus" value="http://your-nexus-server/repository/nuget-hosted/" />
  </packageSources>
  
  <!-- 跳过 SSL 验证 -->
  <config>
    <add key="http_proxy" value="" />
    <add key="signatureValidationMode" value="accept" />
    <add key="requireSignatureForVerification" value="false" />
  </config>
</configuration>
```

### 方案二：使用 HTTP 而非 HTTPS

如果 Nexus 支持 HTTP 访问，将 URL 改为 HTTP：

```xml
<add key="MyNexus" value="http://your-nexus-server/repository/nuget-hosted/" />
```

### 方案三：导入证书到容器

#### 1. 创建自定义 Dockerfile

```dockerfile
FROM mcr.microsoft.com/dotnet/framework/sdk:4.8.1

# 复制证书文件
COPY certificates/ /certificates/

# 导入证书
RUN certutil -addstore -f "ROOT" /certificates/your-ca-cert.cer
```

#### 2. 构建自定义镜像

```bash
docker build -t my-dotnet-framework-sdk:4.8.1 .
```

#### 3. 更新 CI 配置

```yaml
build:
  image: my-dotnet-framework-sdk:4.8.1
```

### 方案四：环境变量配置

在 GitLab CI 变量中设置：

```yaml
variables:
  NUGET_CREDENTIALPROVIDER_SESSIONTOKENCACHE_ENABLED: "true"
  NUGET_PLUGIN_PATHS: ""
  NUGET_CREDENTIALPROVIDER_SESSIONTOKENCACHE_ENABLED: "true"
```

## 验证解决方案

### 1. 测试 NuGet 连接

```powershell
# 在容器内执行
nuget sources List
nuget restore -ConfigFile .\nuget.config -Verbosity detailed
```

### 2. 检查网络连接

```powershell
# 测试到 Nexus 的连接
Test-NetConnection -ComputerName your-nexus-server -Port 443
Test-NetConnection -ComputerName your-nexus-server -Port 80
```

### 3. 查看详细错误信息

```powershell
# 启用详细日志
nuget restore -Verbosity detailed -ConfigFile .\nuget.config
```

## 最佳实践

### 1. 内网环境
- 使用方案一（跳过 SSL 验证）
- 确保 Nexus 配置正确
- 使用 HTTP 而非 HTTPS（如果可能）

### 2. 生产环境
- 使用方案三（导入证书）
- 确保证书有效性
- 定期更新证书

### 3. 安全考虑
- 仅在可信的内网环境中跳过 SSL 验证
- 定期检查证书有效性
- 监控网络连接安全性

## 常见问题

### Q: 为什么会出现这个错误？
A: 通常是因为内网 Nexus 使用自签名证书，容器内无法验证证书的有效性。

### Q: 使用 -NoSSL 是否安全？
A: 在内网环境中相对安全，但不建议在公网环境使用。

### Q: 如何获取 Nexus 的证书？
A: 联系网络管理员获取 Nexus 服务器的 CA 证书。

### Q: 是否影响其他功能？
A: 不会影响编译、测试等功能，仅影响 NuGet 包还原和发布。

## 相关资源

- [NuGet 配置参考](https://docs.microsoft.com/en-us/nuget/reference/nuget-config-file)
- [.NET Framework SSL/TLS 配置](https://docs.microsoft.com/en-us/dotnet/framework/network-programming/tls)
- [GitLab CI 变量配置](https://docs.gitlab.com/ee/ci/variables/)
