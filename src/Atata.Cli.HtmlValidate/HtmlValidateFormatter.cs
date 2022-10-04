namespace Atata.Cli.HtmlValidate
{
    /// <summary>
    /// Represents the formatter.
    /// </summary>
    public class HtmlValidateFormatter
    {
        public HtmlValidateFormatter(string name, string filePath = null)
        {
            Name = name.CheckNotNullOrWhitespace(nameof(name));
            FilePath = filePath;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the output file path.
        /// </summary>
        public string FilePath { get; }

        public static HtmlValidateFormatter Checkstyle(string filePath = null) =>
            new HtmlValidateFormatter(Names.Checkstyle, filePath);

        public static HtmlValidateFormatter Codeframe(string filePath = null) =>
            new HtmlValidateFormatter(Names.Codeframe, filePath);

        public static HtmlValidateFormatter Json(string filePath = null) =>
            new HtmlValidateFormatter(Names.Json, filePath);

        public static HtmlValidateFormatter Stylish(string filePath = null) =>
            new HtmlValidateFormatter(Names.Stylish, filePath);

        public static HtmlValidateFormatter Text(string filePath = null) =>
            new HtmlValidateFormatter(Names.Text, filePath);

        public static class Names
        {
#pragma warning disable S3218 // Inner class members should not shadow outer class "static" or type members
            public const string Checkstyle = "checkstyle";

            public const string Codeframe = "codeframe";

            public const string Json = "json";

            public const string Stylish = "stylish";

            public const string Text = "text";
#pragma warning restore S3218 // Inner class members should not shadow outer class "static" or type members
        }
    }
}
