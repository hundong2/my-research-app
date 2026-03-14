# MCP C# SDK v1.0 상세 예제 (Issue #1)

Microsoft .NET 블로그 글을 기반으로, MCP C# SDK v1.0 핵심 기능을 로컬에서 바로 확인할 수 있게 예제를 구성했습니다.

- Reference: https://devblogs.microsoft.com/dotnet/release-v10-of-the-official-mcp-csharp-sdk/

## 폴더 구조

```text
examples/mcp-csharp-sdk-v1/
  McpCSharpSdkV10Demo.sln
  README.md
  docs/
    01-feature-map.md
    02-url-elicitation-example.md
    03-polling-and-task-store.md
  src/
    McpSdkV10.DemoServer/
      Program.cs
      Tools/ResearchTools.cs
      Models/CrawlResult.cs
    McpSdkV10.DemoClient/
      Program.cs
```

## 이 예제에서 다루는 v1.0 포인트

1. Tool/ToolType 메타데이터 (`Title`, `ReadOnly`, `Idempotent`, `IconSource`)
2. Long-running request polling (`McpRequestContext.EnablePollingAsync`)
3. Task store (`InMemoryMcpTaskStore`)
4. HTTP transport 기반 MCP 엔드포인트 (`MapMcp("/mcp")`)
5. 클라이언트 Tool 목록 조회 + Tool 호출

## 빠른 실행

```bash
cd examples/mcp-csharp-sdk-v1
dotnet restore
dotnet build
dotnet run --project src/McpSdkV10.DemoServer
```

다른 터미널에서:

```bash
cd examples/mcp-csharp-sdk-v1
dotnet run --project src/McpSdkV10.DemoClient -- http://localhost:5000/mcp
```

## 동작 시나리오

1. 클라이언트가 `ListToolsAsync()`로 서버 Tool 목록을 조회합니다.
2. `sdk_release_summary` Tool을 호출해 v1.0 요약 텍스트를 받습니다.
3. `crawl_papers` Tool을 `maxDepth=4`로 호출합니다.
4. 서버는 장기 작업으로 판단해 polling 모드로 전환 후 결과를 순차 생성합니다.

## 파일별 핵심 설명

- `src/McpSdkV10.DemoServer/Program.cs`
  - 서버 메타정보, TaskStore, HTTP transport, event store, Tool 등록
- `src/McpSdkV10.DemoServer/Tools/ResearchTools.cs`
  - ToolType 및 Tool annotation
  - `EnablePollingAsync` 사용 예제
- `src/McpSdkV10.DemoClient/Program.cs`
  - SSE transport 연결
  - Tool 목록 조회/출력
  - Tool 호출과 JSON 결과 확인

## 상세 문서

- `docs/01-feature-map.md`: 블로그 기능과 예제 매핑
- `docs/02-url-elicitation-example.md`: URL 입력 elicitation 패턴 예시
- `docs/03-polling-and-task-store.md`: polling/task store 확장 패턴

## 운영 전환 시 권장 사항

1. `InMemoryMcpTaskStore` 대신 DB/Redis 기반 영속 스토어 사용
2. distributed cache를 실제 shared cache로 교체
3. 인증/권한/consent 흐름을 HTTP 계층에 추가
