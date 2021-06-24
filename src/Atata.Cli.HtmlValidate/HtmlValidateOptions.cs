namespace Atata.Cli.HtmlValidate
{
    /// <summary>
    /// Represents the set of options for "html-validate" CLI.
    /// </summary>
    public class HtmlValidateOptions
    {
        /// <summary>
        /// Gets or sets the formatter.
        /// </summary>
        public HtmlValidateFormatter Formatter { get; set; }

        /// <summary>
        /// Gets or sets the maximum allowed warnings count.
        /// The default value is <see langword="null"/>, which means that warnings are allowed.
        /// Use <c>0</c> to disallow warnings.
        /// </summary>
        public int? MaxWarnings { get; set; }

        /// <summary>
        /// Gets or sets the configuration file path (full or relative to CLI <c>WorkingDirectory</c>).
        /// </summary>
        public string Config { get; set; }

        /// <summary>
        /// Gets or sets the file extensions to use when searching for files in directories.
        /// For example: <c>"html"</c>, <c>"vue"</c>, etc.
        /// </summary>
        public string[] Extensions { get; set; } = new string[0];
    }
}
