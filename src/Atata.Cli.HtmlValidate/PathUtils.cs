namespace Atata.Cli.HtmlValidate;

internal static class PathUtils
{
    // TODO: Remove GetRelativePath method when .NET 8+ is the minimum version.
    internal static string GetRelativePath(string relativeTo, string path)
    {
        Guard.ThrowIfNullOrWhitespace(relativeTo);
        Guard.ThrowIfNullOrWhitespace(path);

        if (relativeTo[^1] != Path.DirectorySeparatorChar && relativeTo[^1] != Path.AltDirectorySeparatorChar)
            relativeTo = relativeTo + Path.DirectorySeparatorChar;

        Uri fromUri = new(relativeTo);
        Uri toUri = new(path);

        if (fromUri.Scheme != toUri.Scheme)
            return path;

        Uri relativeUri = fromUri.MakeRelativeUri(toUri);
        string relativePath = Uri.UnescapeDataString(relativeUri.ToString());

        if (toUri.Scheme.Equals("file", StringComparison.OrdinalIgnoreCase))
            relativePath = relativePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);

        return relativePath;
    }
}
