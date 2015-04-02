using System;
using System.ComponentModel;
using SaveScumAgent.UtilityClasses;

namespace SaveScumAgent.Archiver.Formats
{
    public interface IArchiver
    {
        string ArchiveIdentifier { get; }
        bool Archiving { get; }
        void Abort();
        void StartArchiving();
        event EventHandler<ArchivingEventArgs> ArchiveProgress;
        event EventHandler<ArchivingEventArgs> ArchivingDone;
        PathString DirectoryToArchive { get; set; }
        PathString ArchivesLocation { get; set; }
        string GameTitle { get; set; }
    }

    public enum ArchiveFormats
    {
        [ArchiveFormat(typeof (ZipArchiver))] [Description("Zip")] Zip,
        [ArchiveFormat(typeof (SevenZipArchiver))] [Description("7zip")] SevenZip,
        [ArchiveFormat(typeof (GitArchiver))] [Description("Git")] Git
    }
}