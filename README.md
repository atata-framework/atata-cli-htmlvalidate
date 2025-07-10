# Atata.Cli.HtmlValidate

[![NuGet](http://img.shields.io/nuget/v/Atata.Cli.HtmlValidate.svg?style=flat)](https://www.nuget.org/packages/Atata.Cli.HtmlValidate/)
[![GitHub release](https://img.shields.io/github/release/atata-framework/atata-cli-htmlvalidate.svg)](https://github.com/atata-framework/atata-cli-htmlvalidate/releases)
[![Build status](https://dev.azure.com/atata-framework/atata-cli-htmlvalidate/_apis/build/status/atata-cli-htmlvalidate-ci?branchName=main)](https://dev.azure.com/atata-framework/atata-cli-htmlvalidate/_build/latest?definitionId=43&branchName=main)
[![Slack](https://img.shields.io/badge/join-Slack-green.svg?colorB=4EB898)](https://join.slack.com/t/atata-framework/shared_invite/zt-5j3lyln7-WD1ZtMDzXBhPm0yXLDBzbA)
[![Atata docs](https://img.shields.io/badge/docs-Atata_Framework-orange.svg)](https://atata.io)
[![X](https://img.shields.io/badge/follow-@AtataFramework-blue.svg)](https://x.com/AtataFramework)

**Atata.Cli.HtmlValidate** is a C#/.NET library that provides an API for [html-validate](https://www.npmjs.com/package/html-validate) NPM package.

*The package targets .NET Standard 2.0, which supports .NET 5+, .NET Framework 4.6.1+ and .NET Core/Standard 2.0+.*

## Table of Contents

- [Features](#features)
- [Installation](#installation)
- [Usage](#usage)
- [HtmlValidateOptions Properties](#htmlvalidateoptions-properties)
- [HtmlValidateResult Properties](#htmlvalidateresult-properties)
- [Feedback](#feedback)
- [Thanks](#thanks)
- [SemVer](#semver)
- [License](#license)

## Features

Provides C#/.NET API for [html-validate CLI](https://html-validate.org/usage/cli.html).
Check out <https://html-validate.org> documentation for more info.

## Installation

### NuGet Package

Install [`Atata.Cli.HtmlValidate`](https://www.nuget.org/packages/Atata.Cli.HtmlValidate/) NuGet package.

- Package Manager:
  ```
  Install-Package Atata.Cli.HtmlValidate
  ```

- .NET CLI:
  ```
  dotnet add package Atata.Cli.HtmlValidate
  ```

### NPM Package

Requires [html-validate](https://www.npmjs.com/package/html-validate) NPM package to be installed.

- Using NPM Command:
  ```
  npm install -g html-validate
  ```
- Using [Atata.Cli.Npm](https://www.nuget.org/packages/Atata.Cli.Npm/) .NET library:
  ```cs
  new NpmCli()
      .InstallIfMissing(HtmlValidateCli.Name, global: true);
  ```
- Using its own `HtmlValidateCli` class:
  ```cs
  new HtmlValidateCli()
      .RequireVersion("5.1.1");
  ```

## Usage

The main class is `HtmlValidateCli` located in `Atata.Cli.HtmlValidate` namespace.

### Validate HTML File

```cs
HtmlValidateResult result1 = HtmlValidateCli.InDirectory("some/dir")
    .Validate("testme.html");
```

### Validate HTML File With Options

```cs
var options = new HtmlValidateOptions
{
    Config = "someconfig.json",
    Formatter = HtmlValidateFormatter.Codeframe("output.txt")
};

HtmlValidateResult result3 = HtmlValidateCli.InDirectory("some/dir")
    .Validate("testme.html", options);
```

### Validate HTML File Asynchronously

```cs
HtmlValidateResult result2 = await HtmlValidateCli.InDirectory("some/dir")
    .ValidateAsync("testme.html");
```

## HtmlValidateOptions Properties

- **`HtmlValidateFormatter Formatter { get; set; }`**\
  Gets or sets the formatter.
- **`int? MaxWarnings { get; set; }`**\
  Gets or sets the maximum allowed warnings count.
  The default value is `null`, which means that warnings are allowed.
  Use `0` to disallow warnings.
- **`string Config { get; set; }`**\
  Gets or sets the configuration file path (full or relative to CLI `WorkingDirectory`).
- **`string[] Extensions { get; set; }`**\
  Gets or sets the file extensions to use when searching for files in directories.
  For example: `"html"`, `"vue"`, etc.

## HtmlValidateResult Properties

- **`bool IsSuccessful { get; }`**\
  Gets a value indicating whether this result is successful.
- **`string Output { get; }`**\
  Gets the text output of result.

## Community

- Slack: [https://atata-framework.slack.com](https://join.slack.com/t/atata-framework/shared_invite/zt-5j3lyln7-WD1ZtMDzXBhPm0yXLDBzbA)
- X: https://x.com/AtataFramework
- Stack Overflow: https://stackoverflow.com/questions/tagged/atata

## Feedback

Any feedback, issues and feature requests are welcome.

If you faced an issue please report it to [Atata.Cli.HtmlValidate Issues](https://github.com/atata-framework/atata-cli-htmlvalidate/issues),
[ask a question on Stack Overflow](https://stackoverflow.com/questions/ask?tags=atata+csharp) using [atata](https://stackoverflow.com/questions/tagged/atata) tag
or use another [Atata Contact](https://atata.io/contact/) way.

## Contributing

Check out [Contributing Guidelines](CONTRIBUTING.md) for details.

## Thanks

The library is implemented thanks to the sponsorship of **[Lombiq Technologies](https://lombiq.com/)**.

## SemVer

Atata Framework follows [Semantic Versioning 2.0](https://semver.org/).
Thus backward compatibility is followed and updates within the same major version
(e.g. from 1.3 to 1.4) should not require code changes.

## License

Atata is an open source software, licensed under the Apache License 2.0.
See [LICENSE](LICENSE) for details.
