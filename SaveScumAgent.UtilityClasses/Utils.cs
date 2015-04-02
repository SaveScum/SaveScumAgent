using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.UI;

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

        /// <summary>
        ///     Courtesy of James Newton-King
        ///     http://james.newtonking.com/archive/2008/03/29/formatwith-2-0-string-formatting-with-named-variables
        /// </summary>
        /// <param name="format"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string FormatWith(this string format, object source)

        {
            return FormatWith(format, null, source);
        }

        /// <summary>
        ///     Courtesy of James Newton-King
        ///     http://james.newtonking.com/archive/2008/03/29/formatwith-2-0-string-formatting-with-named-variables
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string FormatWith(this string format, IFormatProvider provider, object source)

        {
            if (format == null)

                throw new ArgumentNullException("format");


            var r = new Regex(@"(?<start>\{)+(?<property>[\w\.\[\]]+)(?<format>:[^}]+)?(?<end>\})+",
                RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);


            var values = new List<object>();

            var rewrittenFormat = r.Replace(format, delegate(Match m)

            {
                var startGroup = m.Groups["start"];

                var propertyGroup = m.Groups["property"];

                var formatGroup = m.Groups["format"];

                var endGroup = m.Groups["end"];


                values.Add((propertyGroup.Value == "0")
                    ? source
                    : DataBinder.Eval(source, propertyGroup.Value));


                return new string('{', startGroup.Captures.Count) + (values.Count - 1) + formatGroup.Value
                       + new string('}', endGroup.Captures.Count);
            });


            return string.Format(provider, rewrittenFormat, values.ToArray());
        }
    }
}