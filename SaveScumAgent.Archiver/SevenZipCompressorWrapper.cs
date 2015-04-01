using System;
using System.IO;
using SevenZip;

namespace SaveScumAgent.Archiver
{
    public class SevenZipCompressorWrapper : ISevenZipCompressor
    {
        private readonly SevenZipCompressor _compressor;

        public SevenZipCompressorWrapper()
        {
            _compressor = new SevenZipCompressor
            {
                CompressionMethod = CompressionMethod.Default,
                CompressionMode = CompressionMode.Create,
                DirectoryStructure = false,
                PreserveDirectoryRoot = true
            };
            _compressor.Compressing += OnCompressing;
            _compressor.CompressionFinished += OnCompressionFinished;
        }

        public CompressionLevel CompressionLevel
        {
            get { return _compressor.CompressionLevel; }
            set { _compressor.CompressionLevel = value; }
        }

        public OutArchiveFormat ArchiveFormat
        {
            get { return _compressor.ArchiveFormat; }
            set { _compressor.ArchiveFormat = value; }
        }

        public CompressionMode CompressionMode
        {
            get { return _compressor.CompressionMode; }
            set { _compressor.CompressionMode = value; }
        }

        public bool DirectoryStructure
        {
            get { return _compressor.DirectoryStructure; }
            set { _compressor.DirectoryStructure = value; }
        }

        public bool PreserveDirectoryRoot
        {
            get { return _compressor.PreserveDirectoryRoot; }
            set { _compressor.PreserveDirectoryRoot = value; }
        }

        public CompressionMethod CompressionMethod
        {
            get { return _compressor.CompressionMethod; }
            set { _compressor.CompressionMethod = value; }
        }

        public event EventHandler<ProgressEventArgs> Compressing;
        public event EventHandler<EventArgs> CompressionFinished;

        public void BeginCompressDirectory(string directory, string archiveName)
        {
            if (!Directory.Exists(directory))
                throw new ArgumentException(String.Format("{0} must exist", directory), "directory");
           _compressor.BeginCompressDirectory(directory, archiveName);

        }

        protected virtual void OnCompressing(object sender, ProgressEventArgs progressEventArgs)
        {
            Compressing?.Invoke(this, progressEventArgs);
        }

        protected virtual void OnCompressionFinished(object sender, EventArgs eventArgs)
        {
            CompressionFinished?.Invoke(this, eventArgs);
        }
    }
}