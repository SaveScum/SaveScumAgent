using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Environment;

namespace SaveScumAgent.UtilityClasses
{
    /// <summary>
    /// For handling, parsing, and tokenizing environment variable special folders & strings.
    /// </summary>
    internal class SpecialFolders
    {
        public static readonly IList<SpecialFolder> SupportedFolders = new ReadOnlyCollection<SpecialFolder>
            (new List<SpecialFolder>
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
    }
}