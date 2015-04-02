using System;
using Ionic;
using Ionic.Zip;
using SaveScumAgent.UtilityClasses;


namespace SaveScumAgent.Archiver.Formats
{
    public class ZipArchiver : IArchiver
    {
        private ZipFile _zipFile;
        private readonly string _archiveIdentifier;


        public string ArchiveIdentifier
        {
            get { return _archiveIdentifier; }
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

        public PathString ArchivesLocation
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string GameTitle
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }
}