# Feature Map (MCP C# SDK v1.0)

아래는 Microsoft 블로그의 v1.0 주요 항목과 본 예제 매핑입니다.

## 1) Tool annotations + icon

- 예제 위치: `src/McpSdkV10.DemoServer/Tools/ResearchTools.cs`
- 사용 포인트:
  - `McpServerToolType(Title, ReadOnly, Idempotent, IconSource)`
  - `McpServerTool(Name, Title, ReadOnly, Idempotent, Destructive)`

## 2) Long-running request polling

- 예제 위치: `src/McpSdkV10.DemoServer/Tools/ResearchTools.cs`
- 사용 포인트:
  - `McpRequestContext.EnablePollingAsync(...)`
  - `maxDepth > 2` 조건에서 polling 모드 전환

## 3) Task store

- 예제 위치: `src/McpSdkV10.DemoServer/Program.cs`
- 사용 포인트:
  - `options.TaskStore = new InMemoryMcpTaskStore();`

## 4) HTTP transport + event store

- 예제 위치: `src/McpSdkV10.DemoServer/Program.cs`
- 사용 포인트:
  - `WithHttpTransport()`
  - `WithDistributedCacheEventStore()`
  - `MapMcp("/mcp")`

## 5) Client side tool discovery/call

- 예제 위치: `src/McpSdkV10.DemoClient/Program.cs`
- 사용 포인트:
  - `McpClientFactory.CreateAsync(...)`
  - `ListToolsAsync()`
  - `CallToolAsync(...)`
