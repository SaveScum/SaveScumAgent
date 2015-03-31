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
            Compressor.ArchiveFormat = ArchiveFormat;
            Compressor.Compressing += OnCompressing;
            Compressor.CompressionFinished += OnCompressionFinished;
        }

        public ZipArchiver(ISevenZipCompressor compressor)
        {
            Compressor = compressor;
        }

        protected virtual string Extension => ".zip";
        protected virtual OutArchiveFormat ArchiveFormat => OutArchiveFormat.Zip;
        public PathString ArchivesDirectory { get; set; }
        public PathString Directory { get; set; }
        public CompressionMethod CompressionMethod { get; set; } = CompressionMethod.Default;
        public CompressionLevel CompressionLevel { get; set; } = CompressionLevel.Normal;

        public void Abort()
        {
            _abortArchiving = true;
        }

        public void StartArchiving()
        {
            if (ArchivesDirectory.IsFolderSubfolderOf(Directory))
                throw new InvalidOperationException("Archive cannot be saved to directory to be archived");
            
            ArchiveIdentifier = Utils.GenerateBackupFilename(ArchivesDirectory, Extension);
            _abortArchiving = false;
            Compressor.BeginCompressDirectory(Directory, ArchiveIdentifier);
            Archiving = true;
        }

        public string ArchiveIdentifier { get; set; }
        public event EventHandler<ArchivingEventArgs> ArchiveProgress;
        public event EventHandler<ArchivingEventArgs> ArchivingDone;
        public bool Archiving { get; private set; }

        private void OnCompressionFinished(object sender, EventArgs args)
        {
            Archiving = false;
            var handler = ArchivingDone;
            handler?.Invoke(this, new ArchivingEventArgs(Directory, ArchiveIdentifier, 100));
        }

        private void OnCompressing(object sender, ProgressEventArgs args)
        {
            var handler = ArchiveProgress;
            handler?.Invoke(this, new ArchivingEventArgs(Directory, ArchiveIdentifier, args.PercentDone));
            if (_abortArchiving)
            {
                args.Cancel = true;
            }
        }
    }
}