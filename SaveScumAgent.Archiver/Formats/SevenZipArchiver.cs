using System;
using System.Threading.Tasks;
using SaveScumAgent.UtilityClasses;
using SevenZip;

namespace SaveScumAgent.Archiver.Formats
{
    public class SevenZipArchiver : ArchiverBase
    {
        private const string Extension = ".7z";

        private bool _abortArchiving;
        internal ISevenZipCompressor Compressor;

        public SevenZipArchiver() : this(new SevenZipCompressorWrapper())
        {
        }

        public SevenZipArchiver(ISevenZipCompressor compressor)
        {
            Compressor = compressor;
            Compressor.ArchiveFormat = OutArchiveFormat.Zip;
            Compressor.Compressing += OnCompressing;
            Compressor.CompressionFinished += OnCompressionFinished;
        }


        public CompressionMethod CompressionMethod { get; set; } = CompressionMethod.Default;
        public CompressionLevel CompressionLevel { get; set; } = CompressionLevel.Normal;

        public override void Abort()
        {
            _abortArchiving = true;
        }

        public override void StartArchivingAsync()
        {
            if (ArchivesLocation.IsFolderSubfolderOf(DirectoryToArchive))
                throw new InvalidOperationException("Archive cannot be saved to directory to be archived");

            ArchiveIdentifier = Utils.GenerateBackupFilename(ArchivesLocation, Extension);
            _abortArchiving = false;
            Compressor.BeginCompressDirectory(DirectoryToArchive, ArchiveIdentifier);
            SevenZipCompressor s = new SevenZipCompressor();
            IsArchiving = true;
        }

        public override void Dispose()
        {
            throw new NotImplementedException();
        }

        private void OnCompressionFinished(object sender, EventArgs e)
        {
            OnArchivingDone(new ArchivingEventArgs(DirectoryToArchive, ArchiveIdentifier));
        }

        private void OnCompressing(object sender, ProgressEventArgs args)
        {
            OnArchiveProgress(this, new ArchivingEventArgs(DirectoryToArchive, ArchiveIdentifier, args.PercentDone));
            if (_abortArchiving)
            {
                args.Cancel = true;
            }
        }
    }


}