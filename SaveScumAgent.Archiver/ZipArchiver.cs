using System;
using SevenZip;

namespace SaveScumAgent.Archiver
{
    public class ZipArchiver : IArchiver
    {
        private readonly string _extension = ".zip";
        private readonly string _archivesDirectory;
        private readonly string _directory;
        private bool _abortArchiving;
        private ISevenZipCompressor _compressor;

        public ZipArchiver(string directory, string archivesDirectory,
            CompressionLevel compressionLevel = CompressionLevel.Normal)
        {
            _directory = directory;
            _archivesDirectory = archivesDirectory;
            InitializeCompressor(compressionLevel, OutArchiveFormat.Zip);
        }

        public ZipArchiver()
        {
            
        }

        public void Abort()
        {
            _abortArchiving = true;
        }

        public void StartArchiving()
        {
            ArchiveIdentifier = Utils.GenerateBackupFilename(_archivesDirectory, _extension);
            _abortArchiving = false;
            _compressor.Compressing += (sender, args) =>
            {
                var handler = ArchiveProgress;
                handler?.Invoke(this, new ArchivingEventArgs(_directory, ArchiveIdentifier, args.PercentDone));
                if (_abortArchiving)
                {
                    args.Cancel = true;
                }
            };

            _compressor.CompressionFinished += (sender, args) =>
            {
                Archiving = false;
                var handler = ArchivingDone;
                handler?.Invoke(this, new ArchivingEventArgs(_directory, ArchiveIdentifier, 100));
            };

            _compressor.BeginCompressDirectory(_directory, ArchiveIdentifier);
            Archiving = true;
        }

        public string ArchiveIdentifier { get; private set; }
        public event EventHandler<ArchivingEventArgs> ArchiveProgress;
        public event EventHandler<ArchivingEventArgs> ArchivingDone;
        public bool Archiving { get; private set; }

        private void InitializeCompressor(CompressionLevel compressionLevel, OutArchiveFormat format)
        {
            _compressor = new SevenZipCompressorWrapper
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