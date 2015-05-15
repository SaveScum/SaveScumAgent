using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

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
            return string.IsNullOrEmpty(value);
        }

        public static string GenerateBackupFilename(string foldername = "", string extension = ".zip")
        {
            extension = "{0}" + extension;
            var dateString = string.Format(extension, DateTime.Now.ToFileTimeUtc());
            var outputFilename = Path.Combine(foldername, dateString);
            return outputFilename;
        }

        /// <summary>
        ///     Gets an attribute on an enum field value
        /// </summary>
        /// <typeparam name="T">The type of the attribute you want to retrieve</typeparam>
        /// <param name="enumVal">The enum value</param>
        /// <returns>The attribute of type T that exists on the enum value</returns>
        public static T GetAttributeOfType<T>(this Enum enumVal) where T : Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof (T), false);
            return (attributes.Length > 0) ? (T) attributes[0] : null;
        }

        public static void Times(this int count, Action action)
        {
            for (var i = 0; i < count; i++)
            {
                action();
            }
        }

        public static string FormatWith(this string format, Dictionary<string, string> valuesDictionary)
        {
            return FormatWith(format, valuesDictionary, "{0}");
        }

        public static string FormatWith(this string format, Dictionary<string, string> valuesDictionary,
            string surroundWith)
        {
            const string pattern = @"\{[a-zA-Z]+?\}";
            var matches = Regex.Matches(format, pattern).Cast<Match>()
                .Select(x => x.Value.ToUpper().Trim("{}".ToCharArray()))
                .Distinct()
                .Where(x => valuesDictionary.ContainsKey(x.Trim("{}".ToCharArray())));

            return matches.Aggregate(format,
                (current, key) =>
                    Regex.Replace(current, "\\{" + key + "\\}", string.Format(surroundWith, valuesDictionary[key]),
                        RegexOptions.IgnoreCase));
        }
    }
}