using System;
using SaveScumAgent.Archiver.Formats;
using SaveScumAgent.UtilityClasses;

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

        public PathString DirectoryToArchive
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }


        /// <summary>
        /// Unused value for Git archiving purposes.
        /// </summary>
        public PathString ArchivesLocation
        {
            get { return null; }
            set { }
        }

        public string GameTitle
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }
}