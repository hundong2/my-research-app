namespace McpSdkV10.DemoServer.Models;

public sealed record CrawlResult(
    string Query,
    int MaxDepth,
    IReadOnlyList<string> CollectedUrls,
    IReadOnlyList<string> Summary);
