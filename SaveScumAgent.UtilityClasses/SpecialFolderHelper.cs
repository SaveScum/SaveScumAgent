using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Environment;
using System.Linq;

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
                SpecialFolder.Desktop,
                SpecialFolder.Programs,
                SpecialFolder.MyDocuments,
                SpecialFolder.Personal,
                SpecialFolder.MyMusic,
                SpecialFolder.MyVideos,
                SpecialFolder.DesktopDirectory,
                SpecialFolder.CommonDesktopDirectory,
                SpecialFolder.ApplicationData,
                SpecialFolder.LocalApplicationData,
                SpecialFolder.CommonApplicationData,
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

        public SpecialFolderHelper(PathString path)
        {
            Path = path;
        }

        public SpecialFolderHelper(string path) : this((PathString) path)
        {
        }

        public PathString Path { get; set; }

        public static object FormatWithProvider
            => new {CurrentTime = DateTime.Now, ProcessName = "Something", APPDATA = "nothing"};

        public static Dictionary<string, string> PathsDictionary => _dictionary ?? (_dictionary = SupportedFolders
            .Distinct()
            .ToDictionary(f => f.ToString().ToUpper(), GetFolderPath));
    }
}