using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Environment;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace SaveScumAgent.UtilityClasses
{
    /// <summary>
    ///     For handling, parsing, and tokenizing environment variable special folders & strings.
    /// </summary>
    public class SpecialFolderHelper
    {
        private static Dictionary<string, string> _dictionary;

        public static readonly IList<SpecialFolder> SupportedFolders = new ReadOnlyCollection<SpecialFolder>(
            new List<SpecialFolder>
            {
                SpecialFolder.ApplicationData,
                SpecialFolder.CommonApplicationData,
                SpecialFolder.CommonDesktopDirectory,
                SpecialFolder.Programs,
                SpecialFolder.MyDocuments,
                SpecialFolder.Personal,
                SpecialFolder.MyMusic,
                SpecialFolder.MyVideos,
                SpecialFolder.DesktopDirectory,
                SpecialFolder.LocalApplicationData,
                SpecialFolder.Windows,
                SpecialFolder.System,
                SpecialFolder.ProgramFiles,
                SpecialFolder.UserProfile,
                SpecialFolder.SystemX86,
                SpecialFolder.ProgramFilesX86,
                SpecialFolder.CommonProgramFiles,
                SpecialFolder.CommonProgramFilesX86,
                SpecialFolder.CommonDocuments
            });

        
        public static object FormatWithProvider
            => new {CurrentTime = DateTime.Now, ProcessName = "Something", APPDATA = "nothing"};

        public static Dictionary<string, string> PathsDictionary => _dictionary ?? (_dictionary = SupportedFolders
            .Distinct()
            .ToDictionary(f => f.ToString().ToUpper(), GetFolderPath));


        public static List<SpecialFolderTag> FindMatchedSpecialFolders(PathString subject)
        {
            return FindMatchedSpecialFolders(subject, "{0}");
        }

        public static List<SpecialFolderTag> FindMatchedSpecialFolders(PathString subject, string surroundWith)
        {
            if (!subject.IsAbsoluteUrl())
                throw new ArgumentException("Must be an absolute path", subject);
            return PathsDictionary.Where(f => subject.IsFolderSubfolderOf(f.Value)).OrderByDescending(f => f.Value.Length).Select(f => new SpecialFolderTag()
            {
                Start = subject.FullPath().ToLower().IndexOf(f.Value.ToLower(), StringComparison.Ordinal),
                End = subject.FullPath().ToLower().IndexOf(f.Value.ToLower(), StringComparison.Ordinal) + f.Value.Length,
                Tag = f.Key,
                ReplacedString = Regex.Replace(subject.FullPath(), Regex.Escape(f.Value), String.Format(surroundWith, String.Format("{{{0}}}", f.Key)), RegexOptions.IgnoreCase),
                RawReplacedString = Regex.Replace(subject.FullPath(), Regex.Escape(f.Value), String.Format("{{{0}}}", f.Key), RegexOptions.IgnoreCase),
                MatchedString = Regex.Replace(subject.FullPath(), Regex.Escape(f.Value), String.Format(surroundWith, f.Value), RegexOptions.IgnoreCase)
            }).ToList();
        }
    }

    public struct SpecialFolderTag
    {
        public string MatchedString;
        public int Start;
        public int End;
        public string ReplacedString;
        public string Tag;
        public string RawReplacedString;
    }
}