using System;
using System.ComponentModel;

namespace SaveScumAgent.Archiver
{
    public interface IArchiver
    {
        string ArchiveIdentifier { get; }
        bool Archiving { get; }
        void Abort();
        void StartArchiving();
        event EventHandler<ArchivingEventArgs> ArchiveProgress;
        event EventHandler<ArchivingEventArgs> ArchivingDone;
    }

    public enum ArchiveFormats
    {
        [ArchiveFormat(typeof (ZipArchiver))] [Description("Zip")] Zip,
        [ArchiveFormat(typeof (SevenZipArchiver))] [Description("7zip")] SevenZip,
        [ArchiveFormat(typeof (GitArchiver))] [Description("Git")] Git
    }
}