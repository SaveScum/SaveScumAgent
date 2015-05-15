using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace SaveScumAgent.UtilityClasses
{
    /// <summary>
    ///     For handling, parsing, and tokenizing environment variable special folders & strings.
    /// </summary>
    public class SpecialFolderHelper
    {
        private static Dictionary<string, string> _dictionary;

        public static readonly IList<Environment.SpecialFolder> SupportedFolders = new ReadOnlyCollection
            <Environment.SpecialFolder>(
            new List<Environment.SpecialFolder>
            {
                Environment.SpecialFolder.ApplicationData,
                Environment.SpecialFolder.CommonApplicationData,
                Environment.SpecialFolder.CommonDesktopDirectory,
                Environment.SpecialFolder.Programs,
                Environment.SpecialFolder.MyDocuments,
                Environment.SpecialFolder.Personal,
                Environment.SpecialFolder.MyMusic,
                Environment.SpecialFolder.MyVideos,
                Environment.SpecialFolder.DesktopDirectory,
                Environment.SpecialFolder.LocalApplicationData,
                Environment.SpecialFolder.Windows,
                Environment.SpecialFolder.System,
                Environment.SpecialFolder.ProgramFiles,
                Environment.SpecialFolder.UserProfile,
                Environment.SpecialFolder.SystemX86,
                Environment.SpecialFolder.ProgramFilesX86,
                Environment.SpecialFolder.CommonProgramFiles,
                Environment.SpecialFolder.CommonProgramFilesX86,
                Environment.SpecialFolder.CommonDocuments
            });

        public static object FormatWithProvider
            => new {CurrentTime = DateTime.Now, ProcessName = "Something", APPDATA = "nothing"};

        public static Dictionary<string, string> PathsDictionary => _dictionary ?? (_dictionary = SupportedFolders
            .Distinct()
            .ToDictionary(f => f.ToString().ToUpper(), Environment.GetFolderPath));

        public static List<SpecialFolderTag> FindMatchedSpecialFolders(PathString subject)
        {
            return FindMatchedSpecialFolders(subject, "{0}");
        }

        public static List<SpecialFolderTag> FindMatchedSpecialFolders(PathString subject, string surroundWith)
        {
            if (!subject.IsAbsoluteUrl())
                throw new ArgumentException("Must be an absolute path", subject);
            return
                PathsDictionary.Where(f => subject.IsFolderSubfolderOf(f.Value))
                    .OrderByDescending(f => f.Value.Length)
                    .Select(f => new SpecialFolderTag
                    {
                        Start = subject.FullPath().ToLower().IndexOf(f.Value.ToLower(), StringComparison.Ordinal),
                        End =
                            subject.FullPath().ToLower().IndexOf(f.Value.ToLower(), StringComparison.Ordinal) +
                            f.Value.Length,
                        Tag = f.Key,
                        ReplacedString =
                            Regex.Replace(subject.FullPath(), Regex.Escape(f.Value),
                                string.Format(surroundWith, $"{{{f.Key}}}"), RegexOptions.IgnoreCase),
                        RawReplacedString =
                            Regex.Replace(subject.FullPath(), Regex.Escape(f.Value), $"{{{f.Key}}}",
                                RegexOptions.IgnoreCase),
                        MatchedString =
                            Regex.Replace(subject.FullPath(), Regex.Escape(f.Value),
                                string.Format(surroundWith, f.Value), RegexOptions.IgnoreCase)
                    }).ToList();
        }
    }

    public struct SpecialFolderTag
    {
        public int End;
        public string MatchedString;
        public string RawReplacedString;
        public string ReplacedString;
        public int Start;
        public string Tag;
    }
}