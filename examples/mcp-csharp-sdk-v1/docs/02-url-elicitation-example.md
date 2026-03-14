# URL Elicitation Example (v1.0)

이 문서는 v1.0에서 소개된 URL 입력 기반 elicitation 흐름을 Tool 내부에 적용하는 예시를 보여줍니다.

## 샘플 코드

```csharp
using ModelContextProtocol.Protocol.Types;
using ModelContextProtocol.Server;

[McpServerTool(Name = "request_seed_url", Title = "Seed URL 입력 요청", ReadOnly = true, Idempotent = true)]
public static async Task<string> RequestSeedUrlAsync(
    McpRequestContext requestContext,
    CancellationToken cancellationToken)
{
    var response = await requestContext.ElicitAsync(
        title: "크롤링 시작 URL이 필요합니다",
        message: "유효한 HTTPS URL을 입력해 주세요.",
        requestedSchema: new
        {
            type = "object",
            properties = new
            {
                seedUrl = new
                {
                    type = "string",
                    format = "uri",
                    inputType = "url"
                }
            },
            required = new[] { "seedUrl" }
        },
        cancellationToken: cancellationToken);

    return response?.Data?.ToString() ?? "no url";
}
```

## 설명

1. `inputType = "url"`을 명시해 클라이언트에서 URL 친화 입력 UI를 유도합니다.
2. `format = "uri"`로 값 검증 힌트를 제공합니다.
3. 수집된 URL은 후속 Tool (`crawl_papers`)의 입력으로 연결할 수 있습니다.
