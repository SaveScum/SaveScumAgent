using System;
using SevenZip;

namespace AutoBackup.ArchiveTools
{
    internal class ZipArchiver : IArchiver
    {

        private bool _archiving = false;
        private bool _initialized;

        public void Abort()
        {
            throw new NotImplementedException();
        }

        public void Archive()
        {
            throw new NotImplementedException();
        }

        public event EventHandler<ArchivingEventArgs> ArchiveProgress;
        public event EventHandler<ArchivingEventArgs> ArchivingDone;

        public void Initialize(string targetDirectory, string gameTitle, CompressionLevel compressionLevel)
        {
            _initialized = true;
        }

        public void Initialize(string targetDirectory, string gameTitle)
        {
            Initialize(targetDirectory, gameTitle, CompressionLevel.Normal);
        }

        public bool Archiving
        {
            get { return _archiving; }
        }

        public bool Initialized
        {
            get { return _initialized; }
        }
    }
}
