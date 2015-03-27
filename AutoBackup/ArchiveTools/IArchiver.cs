using System;

namespace AutoBackup.ArchiveTools
{
    interface IArchiver
    {
        void Abort();

        void Archive();

        event EventHandler<ArchivingEventArgs> ArchiveProgress;
        event EventHandler<ArchivingEventArgs> ArchivingDone;

        void Initialize(string targetDirectory, string gameTitle);

        bool Archiving { get; }

        bool Initialized { get; }
    }
}
