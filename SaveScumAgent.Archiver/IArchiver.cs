using System;

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
}