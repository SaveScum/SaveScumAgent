using System.IO;
using System.Text.RegularExpressions;

namespace AutoBackup
{
    public static class ExtensionMethods
    {
        public static string SanitizeForFilename(this string name)
        {
            var invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
            var invalidReStr = string.Format(@"[{0}]+", invalidChars);
            return Regex.Replace(name, invalidReStr, "_");
        }
    }
}