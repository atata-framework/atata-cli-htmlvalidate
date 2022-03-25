using System.Runtime.InteropServices;
using Atata.Cli.Npm;
using NUnit.Framework;

namespace Atata.Cli.HtmlValidate.IntegrationTests
{
    [SetUpFixture]
    public class SetUpFixture
    {
        [OneTimeSetUp]
        public void GlobalSetUp() =>
            new NpmCli()
                .WithCliCommandFactory(OSDependentShellCliCommandFactory
                    .UseCmdForWindows()
                    .UseForOS(OSPlatform.OSX, new ShShellCliCommandFactory())
                    .UseForOtherOS(new SudoShellCliCommandFactory()))
                .InstallIfMissing(HtmlValidateCli.Name, global: true);
    }
}
