using System;

namespace SaveScumAgent.Archiver
{
    public interface IArchiver
    {
        void Abort();

        void StartArchiving();

        string ArchiveIdentifier { get; }

        event EventHandler<ArchivingEventArgs> ArchiveProgress;
        event EventHandler<ArchivingEventArgs> ArchivingDone;


        bool Archiving { get; }

    }
}
