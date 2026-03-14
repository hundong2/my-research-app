using ModelContextProtocol;
using McpSdkV10.DemoServer.Tools;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();

builder.Services
    .AddMcpServer(options =>
    {
        options.ServerInfo = new()
        {
            Name = "research-mcp-server",
            Version = "1.0.0"
        };

        options.TaskStore = new InMemoryMcpTaskStore();
    })
    .WithHttpTransport()
    .WithDistributedCacheEventStore()
    .WithTools<ResearchTools>();

var app = builder.Build();

app.MapGet("/", () => Results.Ok(new
{
    message = "MCP C# SDK v1.0 demo server",
    mcp = "/mcp"
}));

app.MapMcp("/mcp");
app.Run();
