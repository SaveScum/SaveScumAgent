using System;
using SevenZip;

namespace SaveScumAgent.Archiver
{
    public class ZipArchiver : IArchiver
    {
        private readonly string _directory;
        private readonly string _archivesDirectory;
        private SevenZipCompressor _compressor;
        private bool _abortArchiving;
        private string _archiveFilename;
        private const string Extension = ".zip";

        public void Abort()
        {
            _abortArchiving = true;
        }

        public void StartArchiving()
        {
            
            _archiveFilename = Utils.GenerateBackupFilename(_archivesDirectory, Extension);
            _abortArchiving = false;
            _compressor.Compressing += (sender, args) =>
            {
                var handler = ArchiveProgress;
                handler?.Invoke(this, new ArchivingEventArgs(_directory, _archiveFilename, args.PercentDone));
                if (_abortArchiving)
                {
                    args.Cancel = true;
                }
            };

            _compressor.CompressionFinished += (sender, args) =>
            {
                Archiving = false;
                var handler = ArchivingDone;
                handler?.Invoke(this, new ArchivingEventArgs(_directory, _archiveFilename, 100));
            };

            _compressor.BeginCompressDirectory(_directory, _archiveFilename);
            Archiving = true;
        }

        public string ArchiveIdentifier => _archiveFilename;

        public event EventHandler<ArchivingEventArgs> ArchiveProgress;
        public event EventHandler<ArchivingEventArgs> ArchivingDone;

        public bool Archiving { get; private set; }


        public ZipArchiver(string directory, string archivesDirectory, CompressionLevel compressionLevel = CompressionLevel.Normal)
        {
            _directory = directory;
            _archivesDirectory = archivesDirectory;
            InitializeCompressor(compressionLevel, OutArchiveFormat.Zip);
        }

        private void InitializeCompressor(CompressionLevel compressionLevel, OutArchiveFormat format)
        {
            _compressor = new SevenZipCompressor
            {
                ArchiveFormat = format,
                CompressionLevel = compressionLevel,
                CompressionMethod = CompressionMethod.Default,
                DirectoryStructure = false,
                CompressionMode = CompressionMode.Create,
                PreserveDirectoryRoot = true
            };
        }
    }
}
