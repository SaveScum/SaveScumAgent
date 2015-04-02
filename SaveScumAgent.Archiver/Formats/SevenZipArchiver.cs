using System;
using SaveScumAgent.UtilityClasses;
using SevenZip;

namespace SaveScumAgent.Archiver.Formats
{
    public class SevenZipArchiver : IArchiver
    {
        private const string Extension = ".7z";
        protected OutArchiveFormat ArchiveFormat => OutArchiveFormat.Zip;


        private bool _abortArchiving;
        internal ISevenZipCompressor Compressor;

        public SevenZipArchiver() : this(new SevenZipCompressorWrapper())
        {
        }

        public SevenZipArchiver(ISevenZipCompressor compressor)
        {
            Compressor = compressor;
            Compressor.ArchiveFormat = ArchiveFormat;
            Compressor.Compressing += OnCompressing;
            Compressor.CompressionFinished += OnCompressionFinished;
        }

        public PathString ArchivesLocation { get; set; }
        public PathString DirectoryToArchive { get; set; }


        public string GameTitle
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public CompressionMethod CompressionMethod { get; set; } = CompressionMethod.Default;
        public CompressionLevel CompressionLevel { get; set; } = CompressionLevel.Normal;

        public void Abort()
        {
            _abortArchiving = true;
        }

        public void StartArchiving()
        {
            if (ArchivesLocation.IsFolderSubfolderOf(DirectoryToArchive))
                throw new InvalidOperationException("Archive cannot be saved to directory to be archived");

            ArchiveIdentifier = Utils.GenerateBackupFilename(ArchivesLocation, Extension);
            _abortArchiving = false;
            Compressor.BeginCompressDirectory(DirectoryToArchive, ArchiveIdentifier);
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
            handler?.Invoke(this, new ArchivingEventArgs(DirectoryToArchive, ArchiveIdentifier, 100));
        }

        private void OnCompressing(object sender, ProgressEventArgs args)
        {
            var handler = ArchiveProgress;
            handler?.Invoke(this, new ArchivingEventArgs(DirectoryToArchive, ArchiveIdentifier, args.PercentDone));
            if (_abortArchiving)
            {
                args.Cancel = true;
            }
        }
    }


}