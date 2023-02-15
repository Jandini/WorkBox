# JandaBox

[![.NET](https://github.com/Jandini/JandaBox/actions/workflows/build.yml/badge.svg)](https://github.com/Jandini/JandaBox/actions/workflows/build.yml)
[![NuGet](https://github.com/Jandini/JandaBox/actions/workflows/nuget.yml/badge.svg)](https://github.com/Jandini/JandaBox/actions/workflows/nuget.yml)

.NET templates provides startup code with dependency injection, logging, configuration and more...

## Install

To install this template use `dotnet` command. It will automatically download the template NuGet package from https://www.nuget.org/packages/JandaBox

```bash
dotnet new install JandaBox
```

or in earlier versions 
```bash
dotnet new -i JandaBox
```



## Quick Start

Create default console application with `consolebox` template.

```sh
dotnet new consolebox -n MyApp
```



#### Name parameter

The name parameter `-n` is optional.  

```sh
dotnet new consolebox
```





## ConsoleBox

ConsoleBox .NET template provides startup solution for console application with dependency injection, logging, and configuration. Default logger is [Serilog](https://serilog.net/ ). Microsoft console logger is available with `--serilog false` switch. 

###### Template options

* `--basic`  Create basic console application with minimal amount startup code. Default value is `false`.
* `--serilog`  Provides Serilog as the logger. Default value is `true`. 
* `--async` Create asynchronous code.  Default value is `false`.
* `--git` Add versioning with GitVersion. Note: The code can be build only within git repository. 



### Basic console application

Create basic console application with Microsoft console logger. 

```sh
dotnet new consolebox -n Basic --simple --serilog false	
```

You can create basic console application with Serilog too.

```sh
dotnet new consolebox -n Basic --simple
```



### Default console application

Create console application with Serilog to console and file. 

```sh
dotnet new consolebox -n MyApp
```

You can create console application with Microsoft console logger only.

```sh
dotnet new consolebox -n MyApp --serilog false
```



### Help

For more information about **ConsoleBox** template run 

```
dotnet new consolebox -h  
```



## ActionBox

Provides GitHub actions templates. 

`--docker`

`--nuget`

`--build`







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
  *  `Serilog`  or `Microsoft` log providers  
  *  Unhandled exceptions logging
  *  Version logging
  *  Dynamic logger
* Configuration
  * Embedded `appsettings.json`  file
  * Override embedded `appsettings.json` with the file
  * Settings binding
  * Configuration and settings injection
* Command line parser
  * Verbs and options parser








## Useful Extensions



#### Add file logger

This extension creates an extra log file in given location. You might need to log all the actions related to processing data in selected folder. 

```c#
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





## Resources

Box icon was downloaded from [Flaticon](https://www.flaticon.com/free-icon/open-box_869027?term=box&related_id=869027).


