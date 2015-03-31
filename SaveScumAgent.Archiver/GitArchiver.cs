using System;

namespace SaveScumAgent.Archiver
{
    internal class GitArchiver : IArchiver
    {
        public string ArchiveIdentifier
        {
            get { throw new NotImplementedException(); }
        }

        public bool Archiving
        {
            get { throw new NotImplementedException(); }
        }

        public void Abort()
        {
            throw new NotImplementedException();
        }

        public void StartArchiving()
        {
            throw new NotImplementedException();
        }

        public event EventHandler<ArchivingEventArgs> ArchiveProgress;
        public event EventHandler<ArchivingEventArgs> ArchivingDone;
    }
}