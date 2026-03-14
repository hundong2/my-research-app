using System.Text.Json;
using ModelContextProtocol.Client;
using ModelContextProtocol.Protocol.Transport;

var endpoint = args.Length > 0
    ? args[0]
    : "http://localhost:5000/mcp";

await using var client = await McpClientFactory.CreateAsync(
    new SseClientTransport(new SseClientTransportOptions
    {
        Endpoint = new Uri(endpoint)
    }));

var tools = await client.ListToolsAsync();
Console.WriteLine("== Registered tools ==");
foreach (var tool in tools)
{
    Console.WriteLine($"- {tool.Name} ({tool.Title})");
}

Console.WriteLine();
Console.WriteLine("== Calling sdk_release_summary ==");
var summary = await client.CallToolAsync("sdk_release_summary", new Dictionary<string, object?>());
Console.WriteLine(JsonSerializer.Serialize(summary, new JsonSerializerOptions
{
    WriteIndented = true
}));

Console.WriteLine();
Console.WriteLine("== Calling crawl_papers ==");
var response = await client.CallToolAsync(
    "crawl_papers",
    new Dictionary<string, object?>
    {
        ["query"] = "mcp csharp sdk",
        ["maxDepth"] = 4
    });

Console.WriteLine(JsonSerializer.Serialize(response, new JsonSerializerOptions
{
    WriteIndented = true
}));
