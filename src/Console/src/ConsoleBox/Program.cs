// Created with JandaBox http://github.com/Jandini/JandaBox
#if (basic)
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
#if (serilog)
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
#endif

using var provider = new ServiceCollection()
#if (serilog)    
    .AddLogging(builder => builder.AddSerilog(new LoggerConfiguration()
        .Enrich.WithMachineName()
        .WriteTo.Console(
            theme: AnsiConsoleTheme.Code,
            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u4}] [{MachineName}] [{SourceContext}] {Message}{NewLine}{Exception}")
        .CreateLogger()))
#else
    .AddLogging(builder => builder.AddConsole())
#endif
    .AddTransient<Main>()
    .BuildServiceProvider();

try
{
    provider.GetRequiredService<Main>().Run();
}
catch (Exception ex)
{
    provider.GetService<ILogger<Program>>()?
        .LogCritical(ex, "Unhandled exception");
}
#else
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using CommandLine;

Parser.Default.ParseArguments<Options.Run>(args).WithParsed((parameters) =>
{
    var config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddEmbeddedJsonFile("appsettings.json")
        .Build();

    using var provider = new ServiceCollection()
        .AddConfiguration(config)
        .AddLogging(config)
        .AddServices()
        .BuildServiceProvider();

    provider.LogVersion<Program>();

    try
    {
        var main = provider.GetRequiredService<Main>();

        switch (parameters)
        {
            case Options.Run options:
                main.Run(options.Path);
                break;
        };
    }
    catch (Exception ex)
    {
        provider.GetService<ILogger<Program>>()?
            .LogCritical(ex, "Unhandled exception");
    }
});
#endif