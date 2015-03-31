using System;
using SevenZip;

namespace SaveScumAgent.Archiver
{
    public class SevenZipCompressorWrapper : ISevenZipCompressor
    {
        private SevenZipCompressor _compressor;

        public SevenZipCompressorWrapper()
        {
            _compressor = new SevenZipCompressor();
        }

        public CompressionLevel CompressionLevel
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public OutArchiveFormat ArchiveFormat
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public CompressionMode CompressionMode
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public bool DirectoryStructure
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public bool PreserveDirectoryRoot
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public CompressionMethod CompressionMethod
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public event EventHandler<ProgressEventArgs> Compressing;
        public event EventHandler<EventArgs> CompressionFinished;

        public void BeginCompressDirectory(string directory, string archiveName)
        {
            throw new NotImplementedException();
        }

        protected virtual void OnCompressing(ProgressEventArgs e)
        {
            Compressing?.Invoke(this, e);
        }

        protected virtual void OnCompressionFinished()
        {
            CompressionFinished?.Invoke(this, EventArgs.Empty);

        }
    }
}