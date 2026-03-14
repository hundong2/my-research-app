using ModelContextProtocol.Server;
using McpSdkV10.DemoServer.Models;

namespace McpSdkV10.DemoServer.Tools;

[McpServerToolType(
    Title = "Research Automation Toolkit",
    ReadOnly = true,
    Idempotent = true,
    OpenWorld = false,
    IconSource = "https://raw.githubusercontent.com/modelcontextprotocol/csharp-sdk/main/images/icon.png")]
public static class ResearchTools
{
    [McpServerTool(
        Name = "sdk_release_summary",
        Title = "MCP C# SDK v1.0 요약",
        ReadOnly = true,
        Idempotent = true,
        Destructive = false)]
    public static string GetSdkReleaseSummary()
    {
        return "MCP C# SDK v1.0은 URL 입력 elicitation, Tool/ToolType icon, long-running polling, task store, 인증/consent 확장 지점을 제공합니다.";
    }

    [McpServerTool(
        Name = "crawl_papers",
        Title = "논문 URL 크롤링",
        ReadOnly = true,
        Idempotent = true,
        Destructive = false)]
    public static async Task<CrawlResult> CrawlPapersAsync(
        string query,
        int maxDepth,
        McpRequestContext requestContext,
        CancellationToken cancellationToken)
    {
        if (maxDepth > 2)
        {
            await requestContext.EnablePollingAsync(
                message: "작업 시간이 길어질 수 있어 polling 모드로 전환합니다.",
                cancellationToken: cancellationToken);
        }

        var hops = Math.Max(1, maxDepth);
        var urls = new List<string>(hops);
        var summaries = new List<string>(hops);

        for (var i = 1; i <= hops; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);

            urls.Add($"https://example.org/{Uri.EscapeDataString(query)}/paper-{i}");
            summaries.Add($"{query} 관련 요약 {i}: 핵심 키워드/인용 후보 정리 완료");
        }

        return new CrawlResult(query, maxDepth, urls, summaries);
    }
}
