using Atata.Cli.Npm;
using NUnit.Framework;

namespace Atata.Cli.HtmlValidate.IntegrationTests
{
    [SetUpFixture]
    public class SetUpFixture
    {
        [OneTimeSetUp]
        public void GlobalSetUp()
        {
            new NpmCli()
                .InstallIfMissing(HtmlValidateCli.Name, global: true);
        }
    }
}
