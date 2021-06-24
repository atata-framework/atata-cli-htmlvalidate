namespace Atata.Cli.HtmlValidate
{
    /// <summary>
    /// Represents the result of "html-validate" CLI.
    /// </summary>
    public class HtmlValidateResult
    {
        public HtmlValidateResult(bool isSuccessful, string output)
        {
            IsSuccessful = isSuccessful;
            Output = output;
        }

        /// <summary>
        /// Gets a value indicating whether this result is successful.
        /// </summary>
        public bool IsSuccessful { get; }

        /// <summary>
        /// Gets the text output of result.
        /// </summary>
        public string Output { get; }

        public override string ToString()
        {
            return $"{nameof(IsSuccessful)} = {IsSuccessful}";
        }
    }
}
