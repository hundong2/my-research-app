# Polling + Task Store Pattern

장시간 도구 실행을 다루는 최소 패턴입니다.

## 서버 등록

```csharp
builder.Services
    .AddMcpServer(options =>
    {
        options.ServerInfo = new() { Name = "research-mcp-server", Version = "1.0.0" };
        options.TaskStore = new InMemoryMcpTaskStore();
    })
    .WithHttpTransport()
    .WithDistributedCacheEventStore()
    .WithTools<ResearchTools>();
```

## Tool 내부 polling 전환

```csharp
if (maxDepth > 2)
{
    await requestContext.EnablePollingAsync(
        message: "작업 시간이 길어질 수 있어 polling 모드로 전환합니다.",
        cancellationToken: cancellationToken);
}
```

## 운영 고려사항

1. `InMemoryMcpTaskStore`는 개발용으로만 사용합니다.
2. 멀티 인스턴스 환경은 공유 가능한 task/event 저장소를 사용합니다.
3. polling 전환 조건(`maxDepth > 2`)은 실제 처리 시간 지표로 조정합니다.
