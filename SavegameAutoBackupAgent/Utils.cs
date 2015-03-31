using System;
using System.IO;
using System.Reflection;

namespace SaveScumAgent
{
    public static class Utils
    {
        /// <summary>
        /// Syntactic sugar for "String.IsNullOrEmpty(x)"
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsBlank(this string value)
        {
            return String.IsNullOrEmpty(value);
        }

        /// <summary>
        /// Get the list of all emdedded resources in the assembly.
        /// </summary>
        /// <returns>An array of fully qualified resource names</returns>
        public static string[] GetEmbeddedResourceNames()
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceNames();
        }

        public static string GenerateBackupFilename(string foldername = "", string extension = ".7z")
        {
            extension = "{0}" + extension;
            var dateString = String.Format(extension, DateTime.Now.ToFileTimeUtc());
            var outputFilename = Path.Combine(foldername, dateString);
            return outputFilename;
        }

    }
}