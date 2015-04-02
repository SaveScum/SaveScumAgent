using System;
using System.IO;
using System.Reflection;

namespace SaveScumAgent.UtilityClasses
{
    public static class Utils
    {
        /// <summary>
        ///     Syntactic sugar for "String.IsNullOrEmpty(x)"
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsBlank(this string value)
        {
            return String.IsNullOrEmpty(value);
        }

        /// <summary>
        ///     Get the list of all emdedded resources in the assembly.
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

        /// <summary>
        /// Gets an attribute on an enum field value
        /// </summary>
        /// <typeparam name="T">The type of the attribute you want to retrieve</typeparam>
        /// <param name="enumVal">The enum value</param>
        /// <returns>The attribute of type T that exists on the enum value</returns>
        public static T GetAttributeOfType<T>(this Enum enumVal) where T : System.Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return (attributes.Length > 0) ? (T)attributes[0] : null;
        }
    }
}