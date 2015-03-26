using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AutoBackup
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

        public static bool IsFolderSubfolderOf(DirectoryInfo possibleSubDir, DirectoryInfo possibleParentDir)
        {
            if (possibleSubDir != null)
            {
                return possibleParentDir.FullName.Equals(possibleSubDir.FullName) ||
                       IsFolderSubfolderOf(possibleSubDir.Parent, possibleParentDir);
            }
            else
            {
                return false;
            }
        }

        public static bool IsFolderSubfolderOf(string possibleSubDir, string possibleParentDir)
        {
            return IsFolderSubfolderOf(new DirectoryInfo(possibleSubDir), new DirectoryInfo(possibleParentDir));
        }

        public static string GenerateBackupFilename(string foldername = "", string extension = ".7z")
        {
            extension = "{0}" + extension;
            var dateString = String.Format(extension, DateTime.Now.ToFileTimeUtc());
            var outputFilename = Path.Combine(foldername, dateString);
            return outputFilename;
        }


        public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }
    }
}
