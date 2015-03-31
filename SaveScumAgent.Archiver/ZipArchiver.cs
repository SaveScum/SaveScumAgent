using System;
using SevenZip;

namespace SaveScumAgent.Archiver
{
    public class ZipArchiver : IArchiver
    {
        private bool _abortArchiving;
        internal ISevenZipCompressor Compressor;

        public ZipArchiver() : this(new SevenZipCompressorWrapper())
        {
        }

        public ZipArchiver(ISevenZipCompressor compressor)
        {
            Compressor = compressor;
        }

        protected virtual string Extension => ".zip";
        protected virtual OutArchiveFormat ArchiveFormat => OutArchiveFormat.Zip;
        public string ArchivesDirectory { get; set; }
        public string Directory { get; set; }
        public CompressionMethod CompressionMethod { get; set; } = CompressionMethod.Default;
        public CompressionLevel CompressionLevel { get; set; } = CompressionLevel.Normal;

        public void Abort()
        {
            _abortArchiving = true;
        }

        public void StartArchiving()
        {
            Compressor.ArchiveFormat = ArchiveFormat;
            ArchiveIdentifier = Utils.GenerateBackupFilename(ArchivesDirectory, Extension);
            _abortArchiving = false;
            Compressor.Compressing += (sender, args) =>
            {
                var handler = ArchiveProgress;
                handler?.Invoke(this, new ArchivingEventArgs(Directory, ArchiveIdentifier, args.PercentDone));
                if (_abortArchiving)
                {
                    args.Cancel = true;
                }
            };

            Compressor.CompressionFinished += (sender, args) =>
            {
                Archiving = false;
                var handler = ArchivingDone;
                handler?.Invoke(this, new ArchivingEventArgs(Directory, ArchiveIdentifier, 100));
            };

            Compressor.BeginCompressDirectory(Directory, ArchiveIdentifier);
            Archiving = true;
        }

        public string ArchiveIdentifier { get; private set; }
        public event EventHandler<ArchivingEventArgs> ArchiveProgress;
        public event EventHandler<ArchivingEventArgs> ArchivingDone;
        public bool Archiving { get; private set; }
    }
}