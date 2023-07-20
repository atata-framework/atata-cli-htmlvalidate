using Atata.Cli.Npm;

namespace Atata.Cli.HtmlValidate;

/// <summary>
/// Represents the CLI of "html-validate" NPM package.
/// </summary>
public class HtmlValidateCli : GlobalNpmPackageCli<HtmlValidateCli>
{
    /// <summary>
    /// The name of the program.
    /// </summary>
    public const string Name = "html-validate";

    /// <summary>
    /// Initializes a new instance of the <see cref="HtmlValidateCli"/> class.
    /// </summary>
    public HtmlValidateCli()
        : base(Name) =>
        ResultValidationRules = CliCommandResultValidationRules.NoError;

    /// <summary>
    /// Validates the file or all files in the directory by the specified path.
    /// </summary>
    /// <param name="path">The file or directory path relative to <see cref="ProgramCli.WorkingDirectory"/> property value of this instance.</param>
    /// <param name="options">The options.</param>
    /// <returns>The result of validation.</returns>
    public HtmlValidateResult Validate(string path, HtmlValidateOptions options = null)
    {
        StringBuilder commandText = new StringBuilder();

        if (!string.IsNullOrEmpty(path))
            commandText.Append($"\"{path}\"");

        if (options != null)
            AddOptions(commandText, options);

        var cliResult = Execute(commandText.ToString());

        string output = options?.Formatter?.FilePath != null
            ? ReadOutputFromFile(options.Formatter.FilePath)
            : cliResult.Output;

        output = PostProcessOutput(output);

        return new HtmlValidateResult(cliResult.ExitCode == 0, output);
    }

    private static string PostProcessOutput(string output) =>
        output?.Replace("тЬЦ", "\u2716");

    /// <inheritdoc cref="Validate(string, HtmlValidateOptions)"/>
    public async Task<HtmlValidateResult> ValidateAsync(string path, HtmlValidateOptions options = null) =>
        await Task.Run(() => Validate(path, options));

    private static void AddOptions(StringBuilder commandText, HtmlValidateOptions options)
    {
        if (options.Formatter != null)
        {
            commandText.Append($" -f {options.Formatter.Name}");

            if (options.Formatter.FilePath != null)
                commandText.Append($"={options.Formatter.FilePath}");
        }

        if (options.MaxWarnings != null)
            commandText.Append($" --max-warnings {options.MaxWarnings}");

        if (!string.IsNullOrEmpty(options.Config))
            commandText.Append($" -c \"{options.Config}\"");

        if (options.Extensions?.Any() ?? false)
            commandText.Append(" --ext ").Append(string.Join(",", options.Extensions));
    }

    private string ReadOutputFromFile(string filePath)
    {
        string fullFilePath = Path.IsPathRooted(filePath)
            ? filePath
            : Path.Combine(WorkingDirectory, filePath);

        return File.ReadAllText(fullFilePath);
    }

    public override string GetInstalledVersion()
    {
        CliCommandResult versionCommandResult = ExecuteRaw("--version");

        return versionCommandResult.HasError
            ? null
            : versionCommandResult.Output.Split('-').Last();
    }
}
