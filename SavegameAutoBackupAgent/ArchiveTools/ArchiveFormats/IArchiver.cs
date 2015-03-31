using System;

namespace SaveScumAgent.ArchiveTools.ArchiveFormats
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
