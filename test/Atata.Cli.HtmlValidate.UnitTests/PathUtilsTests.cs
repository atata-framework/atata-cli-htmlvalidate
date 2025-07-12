using NUnit.Framework;

namespace Atata.Cli.HtmlValidate.UnitTests;

public static class PathUtilsTests
{
    public sealed class GetRelativePath
    {
        [TestCase(@"D:\a\b\c", @"D:\file.txt", ExpectedResult = @"..\..\..\file.txt")]
        [TestCase(@"D:\a\b\c\", @"D:\file.txt", ExpectedResult = @"..\..\..\file.txt")]
        [TestCase(@"D:\a\b\c\", @"D:\a\b\c\file.txt", ExpectedResult = @"file.txt")]
        [TestCase(@"D:\a\b\c\", @"D:\a\b\c\d\file.txt", ExpectedResult = @"d\file.txt")]
        [TestCase(@"D:\a\b\c\", @"C:\file.txt", ExpectedResult = @"C:\file.txt")]
        [Platform("Win")]
        public string WithValidValues(string relativeTo, string path)
        {
            string result = PathUtils.GetRelativePath(relativeTo, path);

            Assert.That(result, Is.EqualTo(Path.GetRelativePath(relativeTo, path)));

            return result;
        }
    }
}
