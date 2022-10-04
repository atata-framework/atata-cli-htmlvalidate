using System;
using System.IO;
using Atata.Cli.Npm;
using NUnit.Framework;

namespace Atata.Cli.HtmlValidate.IntegrationTests
{
    public abstract class HtmlValidateCliTests
    {
        private const string TestVersion = "7.5.0";

        protected Subject<HtmlValidateCli> Sut { get; private set; }

        [OneTimeSetUp]
        public void SetUpFixture()
        {
            new NpmCli()
                .InstallIfMissing(HtmlValidateCli.Name, TestVersion, global: true);

            Sut = HtmlValidateCli.InDirectory("TestPages").ToSutSubject();
        }

        public class Validate : HtmlValidateCliTests
        {
            [Test]
            public void InvalidFile()
            {
                ResultOfValidate("missing.html")
                    .Should.Throw<CliCommandException>();
            }

            [Test]
            public void When0Errors()
            {
                ResultOfValidate("Errors0.html")
                    .ValueOf(x => x.IsSuccessful).Should.BeTrue()
                    .ValueOf(x => x.Output).Should.BeEmpty();
            }

            [Test]
            public void When1Error()
            {
                ResultOfValidate("Errors1.html")
                    .ValueOf(x => x.IsSuccessful).Should.BeFalse()
                    .ValueOf(x => x.Output).Should.Contain("1 error, 0 warnings");
            }

            [Test]
            public void When2Errors()
            {
                ResultOfValidate("Errors2.html")
                    .ValueOf(x => x.IsSuccessful).Should.BeFalse()
                    .ValueOf(x => x.Output).Should.Contain("2 errors, 0 warnings");
            }

            [Test]
            public void When2Errors_WithConfig_Where1ErrorIsOff()
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
            public void When2Errors_WithConfig_Where1ErrorIsWarn()
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
            public void When1Error_WithConfig_WhereThisErrorIsWarn_AndMaxWarningsIs0()
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
            public void When1Error_WithConfig_WhereThisErrorIsWarn_AndMaxWarningsIs1()
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
            public void When1Error_WithFullPathConfig_WhereThisErrorIsOff()
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
            public void WithCodeframeFormatter()
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
            public void WithJsonFormatter()
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
            public void WithTextFormatterTargetingRelativeFile()
            {
                string filePath = $"{TestContext.CurrentContext.Test.Name}.txt";

                ValidateWithTextFormatterTargetingFile(filePath);
            }

            [Test]
            public void WithTextFormatterTargetingFullPathFile()
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
                    : Path.Combine(Sut.Object.WorkingDirectory, filePath);

                new FileSubject(outputFilePath)
                    .Should.Exist();

                File.Delete(outputFilePath);
            }

            [Test]
            public void When0Errors_WithTextFormatterTargetingRelativeFile()
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
            public void CurrentDirectory_WithValidExtensions()
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
            public void CurrentDirectory_WithInvalidExtensions()
            {
                var options = new HtmlValidateOptions
                {
                    Extensions = new[] { "htm" }
                };

                ResultOfValidate(".", options)
                    .Should.Throw<CliCommandException>();
            }

            private Subject<HtmlValidateResult> ResultOfValidate(string path, HtmlValidateOptions options = null) =>
                Sut.ResultOf(x => x.Validate(path, options));
        }

        public class GetInstalledVersion : HtmlValidateCliTests
        {
            [Test]
            public void Ok()
            {
                string expectedVersion = new NpmCli()
                    .GetInstalledVersion(HtmlValidateCli.Name, global: true);

                Sut.ResultOf(x => x.GetInstalledVersion())
                    .Should.Not.BeNullOrEmpty()
                    .Should.Equal(expectedVersion);
            }
        }

        public class RequireVersion : HtmlValidateCliTests
        {
            [Test]
            [Platform(Exclude = "Linux", Reason = "No permissions.")]
            public void Then_GetInstalledVersion()
            {
                string version = "5.0.0";

                Sut.Act(x => x.RequireVersion(version))
                    .ResultOf(x => x.GetInstalledVersion())
                    .Should.Equal(version);
            }
        }

        private static class Configs
        {
            public const string EmptyTitleOff = "../Configs/empty-title-off.json";

            public const string EmptyTitleWarn = "../Configs/empty-title-warn.json";
        }
    }
}
