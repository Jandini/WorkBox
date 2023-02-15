#if (simple)
using Microsoft.Extensions.Logging;

internal class Main
{
    private readonly ILogger<Main> _logger;

    public Main(ILogger<Main> logger)
    {
        _logger = logger;
    }

    public void Run()
    {
        _logger.LogInformation("Hello, World!");
    }
}
#else
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

internal class Main : IMain
{
    private readonly ILogger<Main> _logger;
    private readonly IConfiguration _config;

    public Main(ILogger<Main> logger, IConfiguration config)
    {
        _logger = logger;
        _config = config;
    }
    public void Run(string path)
    {
        _logger.LogInformation(_config.GetRequiredSection("ConsoleBox").Get<Settings>().Message);

        var dir = new DirectoryInfo(path);
        _logger.LogInformation("Directory {path:l} contains {file} files.", dir.Name, dir.GetFiles().Length);
    }
}
#endif