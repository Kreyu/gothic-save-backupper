namespace GothicSaveBackupper
{
    internal static class PathHelper
    {
        public static string? GetFullDirectoryPath(string path)
        {
            if (Directory.Exists(path))
            {
                return path;
            }

            return Path.GetDirectoryName(path);
        }

        public static string NormalizePath(string path)
        {
            return Path.GetFullPath(new Uri(path).LocalPath)
                       .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        }
    }
}
