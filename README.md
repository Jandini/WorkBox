# JandaBox

[![.NET](https://github.com/Jandini/JandaBox/actions/workflows/build.yml/badge.svg)](https://github.com/Jandini/JandaBox/actions/workflows/build.yml)
[![NuGet](https://github.com/Jandini/JandaBox/actions/workflows/nuget.yml/badge.svg)](https://github.com/Jandini/JandaBox/actions/workflows/nuget.yml)

.NET template provides console application startup code with dependency injection, logging, configuration and more...



## Parameters

`--simple`

`--serilog`

`--async`

`--git`



#### ActionBox

`--docker`

`--nuget`

`--build`





## Install

To install this template use `dotnet` command. It will automatically download template nuget package from https://www.nuget.org/packages/JandaBox

```bash
dotnet new install JandaBox
```

in earlier dotnet versions it was 
```bash
dotnet new -i JandaBox
```



## Start

Once the template is installed you can create new application from **JandaBox Console (C#)** template. 

```bash
dotnet new consolebox 
```

or 

```bash
dotnet new consolebox -n MyApp
```

The console main code is ready to "Run" with dependency injection and logging.

```c#
public void Run()
{
    _logger.LogInformation("Hello, World");
    _logger.LogWarning("No implementation");
    throw new NotImplementedException("Fix it");
}
```

![image](https://user-images.githubusercontent.com/19593367/152032611-382ae24e-23f2-4117-ae6b-cdf358ac3e00.png)

The `Program.cs` code is going to look like this

```C#
// Created with JandaBox http://github.com/Jandini/JandaBox
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace MyApp
{
    class Program
    {
        static void Main()
        {
            try
            {
                using var provider = new ServiceCollection()
                    .AddTransient<IMain, Main>()
                    .AddLogging(builder => builder.AddConsole())
                    .BuildServiceProvider();

                try
                {
                    provider
                        .GetRequiredService<IMain>()
                        .Run();
                }
                catch (Exception ex)
                {
                    provider.GetRequiredService<ILogger<Program>>()
                        .LogCritical(ex, ex.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
```

### Serilog

Serilog is the default logger in ConsoleBox template. If you like to use Microsoft logger use `--serilog false`.

```
dotnet new consolebox -n MyApp --serilog false
```

![image](https://user-images.githubusercontent.com/19593367/152033659-27d21c1a-293e-4e97-8282-2747f07f804f.png)




## Help

More information about **Console Box** template 

```
dotnet new consolebox -h  
```





## Features

* .NET6
* Repository Layout
  * The `src` and `bin` folders 
  * Default `README.md` file 
  * Default `.gitignore` file
  * Default `launchSettings.json` file
* GitHub Actions
  * `Build` and `Test` workflow file for .NET6
* Dependency Injection
  * Main service with logging
  * Service provider disposal
* Logging
  * `Microsoft` or `Serilog` log providers  
  * Unhandled exceptions logging
  * Version logging
  * Dynamic logger
* Configuration
  * Embedded `appsettings.json`  file
  * Override embedded `appsettings.json` with the file
  * Settings binding
  * Configuration and settings injection
* Command line parser
  * Verbs and options parser








## Extensions

#### Add file logger

This extension creates an extra log file in given location. You might need to log all the actions related to processing data in selected folder. 

```c#
internal static void AddFileLogger(this ILoggerFactory factory, IConfiguration config, FileInfo logFile)
{        
    factory
        .AddSerilog(new LoggerConfiguration()
        .Enrich.WithMachineName()
        .WriteTo.File(logFile.FullName, outputTemplate: config
            .GetSection("Serilog:WriteTo")
            .GetChildren().First(a => a.GetValue<string>("Name") == "File")
            .GetValue<string>("Args:outputTemplate"))
        .CreateLogger(), dispose: true);
}
```

or

```
internal static void AddFileLogger(this ILoggerFactory factory, FileInfo logFile)
{        
    factory
        .AddSerilog(new LoggerConfiguration()
        .Enrich.WithMachineName()
        .WriteTo.File(
            logFile.FullName, 
            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u4}] [{MachineName}] [{SourceContext}] {Message}{NewLine}{Exception}")
        .CreateLogger(), dispose: true);
}
```





### Resources

Box icon was downloaded from [Flaticon](https://www.flaticon.com/free-icon/open-box_869027?term=box&related_id=869027).


