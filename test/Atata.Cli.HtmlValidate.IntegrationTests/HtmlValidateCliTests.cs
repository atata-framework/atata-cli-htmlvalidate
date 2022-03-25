using System;
using System.IO;
using Atata.Cli.Npm;
using NUnit.Framework;

namespace Atata.Cli.HtmlValidate.IntegrationTests
{
    [TestFixture]
    public class HtmlValidateCliTests
    {
        private Subject<HtmlValidateCli> _sut;

        [OneTimeSetUp]
        public void SetUpFixture()
        {
            _sut = HtmlValidateCli.InDirectory("TestPages").ToSutSubject();
        }

        [Test]
        public void Validate_InvalidFile()
        {
            ResultOfValidate("missing.html")
                .Should.Throw<CliCommandException>();
        }

        [Test]
        public void Validate_NoErrors()
        {
            ResultOfValidate("Errors0.html")
                .ValueOf(x => x.IsSuccessful).Should.BeTrue()
                .ValueOf(x => x.Output).Should.BeEmpty();
        }

        [Test]
        public void Validate_1Error()
        {
            ResultOfValidate("Errors1.html")
                .ValueOf(x => x.IsSuccessful).Should.BeFalse()
                .ValueOf(x => x.Output).Should.Contain("1 error, 0 warnings");
        }

        [Test]
        public void Validate_2Errors()
        {
            ResultOfValidate("Errors2.html")
                .ValueOf(x => x.IsSuccessful).Should.BeFalse()
                .ValueOf(x => x.Output).Should.Contain("2 errors, 0 warnings");
        }

        [Test]
        public void Validate_2Errors_WithConfig_Where1ErrorIsOff()
        {
            var options = new HtmlValidateOptions
            {
                Config = Configs.EmptyTitleOff
            };

            ResultOfValidate("Errors2.html", options)
                .ValueOf(x => x.IsSuccessful).Should.BeFalse()
                .ValueOf(x => x.Output).Should.Contain("1 error, 0 warnings");
        }

        [Test]
        public void Validate_2Errors_WithConfig_Where1ErrorIsWarn()
        {
            var options = new HtmlValidateOptions
            {
                Config = Configs.EmptyTitleWarn
            };

            ResultOfValidate("Errors2.html", options)
                .ValueOf(x => x.IsSuccessful).Should.BeFalse()
                .ValueOf(x => x.Output).Should.Contain("1 error, 1 warning");
        }

        [Test]
        public void Validate_1Error_WithConfig_WhereThisErrorIsWarn_AndMaxWarningsIs0()
        {
            var options = new HtmlValidateOptions
            {
                Config = Configs.EmptyTitleWarn,
                MaxWarnings = 0
            };

            ResultOfValidate("Errors1.html", options)
                .ValueOf(x => x.IsSuccessful).Should.BeFalse()
                .ValueOf(x => x.Output).Should.Contain("0 errors, 1 warning");
        }

        [Test]
        public void Validate_1Error_WithConfig_WhereThisErrorIsWarn_AndMaxWarningsIs1()
        {
            var options = new HtmlValidateOptions
            {
                Config = Configs.EmptyTitleWarn,
                MaxWarnings = 1
            };

            ResultOfValidate("Errors1.html", options)
                .ValueOf(x => x.IsSuccessful).Should.BeTrue()
                .ValueOf(x => x.Output).Should.Contain("0 errors, 1 warning");
        }

        [Test]
        public void Validate_1Error_WithFullPathConfig_WhereThisErrorIsOff()
        {
            var options = new HtmlValidateOptions
            {
                Config = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configs", "empty-title-off.json"),
            };

            ResultOfValidate("Errors1.html", options)
                .ValueOf(x => x.IsSuccessful).Should.BeTrue()
                .ValueOf(x => x.Output).Should.BeEmpty();
        }

        [Test]
        public void Validate_WithCodeframeFormatter()
        {
            var options = new HtmlValidateOptions
            {
                Formatter = HtmlValidateFormatter.Codeframe()
            };

            ResultOfValidate("Errors1.html", options)
                .ValueOf(x => x.IsSuccessful).Should.BeFalse()
                .ValueOf(x => x.Output).Should.Contain("1 error found");
        }

        [Test]
        public void Validate_WithJsonFormatter()
        {
            var options = new HtmlValidateOptions
            {
                Formatter = HtmlValidateFormatter.Json()
            };

            ResultOfValidate("Errors1.html", options)
                .ValueOf(x => x.IsSuccessful).Should.BeFalse()
                .ValueOf(x => x.Output).Should.Contain("\"errorCount\":1,");
        }

        [Test]
        public void Validate_WithTextFormatterTargetingRelativeFile()
        {
            string filePath = $"{TestContext.CurrentContext.Test.Name}.txt";

            ValidateWithTextFormatterTargetingFile(filePath);
        }

        [Test]
        public void Validate_WithTextFormatterTargetingFullPathFile()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestFiles", $"{TestContext.CurrentContext.Test.Name}.txt");

            ValidateWithTextFormatterTargetingFile(filePath);
        }

        private void ValidateWithTextFormatterTargetingFile(string filePath)
        {
            var options = new HtmlValidateOptions
            {
                Formatter = HtmlValidateFormatter.Text(filePath)
            };

            ResultOfValidate("Errors1.html", options)
                .ValueOf(x => x.IsSuccessful).Should.BeFalse()
                .ValueOf(x => x.Output).Should.Contain("error [empty-title]");

            AssertFileExistsAndDelete(filePath);
        }

        private void AssertFileExistsAndDelete(string filePath)
        {
            string outputFilePath = Path.IsPathRooted(filePath)
                ? filePath
                : Path.Combine(_sut.Object.WorkingDirectory, filePath);

            new FileSubject(outputFilePath)
                .Should.Exist();

            File.Delete(outputFilePath);
        }

        [Test]
        public void Validate_0Errors_WithTextFormatterTargetingRelativeFile()
        {
            string filePath = $"{TestContext.CurrentContext.Test.Name}.txt";

            var options = new HtmlValidateOptions
            {
                Formatter = HtmlValidateFormatter.Text(filePath)
            };

            ResultOfValidate("Errors0.html", options)
                .ValueOf(x => x.IsSuccessful).Should.BeTrue()
                .ValueOf(x => x.Output).Should.BeEmpty();

            AssertFileExistsAndDelete(filePath);
        }

        [Test]
        public void Validate_CurrentDirectory_WithValidExtensions()
        {
            var options = new HtmlValidateOptions
            {
                Extensions = new[] { "html" }
            };

            ResultOfValidate(".", options)
                .ValueOf(x => x.IsSuccessful).Should.BeFalse()
                .ValueOf(x => x.Output).Should.Contain("3 errors, 0 warnings)");
        }

        [Test]
        public void Validate_CurrentDirectory_WithInvalidExtensions()
        {
            var options = new HtmlValidateOptions
            {
                Extensions = new[] { "htm" }
            };

            ResultOfValidate(".", options)
                .Should.Throw<CliCommandException>();
        }

        [Test]
        public void GetInstalledVersion()
        {
            string expectedVersion = new NpmCli()
                .GetInstalledVersion(HtmlValidateCli.Name, global: true);

            _sut.ResultOf(x => x.GetInstalledVersion())
                .Should.Not.BeNullOrEmpty()
                .Should.Equal(expectedVersion);
        }

        [Test]
        [Platform(Exclude = "Linux", Reason = "No permissions.")]
        public void RequireVersion_Then_GetInstalledVersion()
        {
            string version = "5.0.0";

            _sut.Act(x => x.RequireVersion(version))
                .ResultOf(x => x.GetInstalledVersion())
                .Should.Equal(version);
        }

        private Subject<HtmlValidateResult> ResultOfValidate(string path, HtmlValidateOptions options = null) =>
            _sut.ResultOf(x => x.Validate(path, options));

        private static class Configs
        {
            public const string EmptyTitleOff = "../Configs/empty-title-off.json";

            public const string EmptyTitleWarn = "../Configs/empty-title-warn.json";
        }
    }
}
