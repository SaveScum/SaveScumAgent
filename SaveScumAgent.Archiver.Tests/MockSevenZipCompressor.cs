using System;
using SaveScumAgent.Archiver.Formats;
using SevenZip;

namespace SaveScumAgent.Archiver.Tests
{
    internal class MockSevenZipCompressor : ISevenZipCompressor
    {
        public CompressionLevel CompressionLevel { get; set; }
        public OutArchiveFormat ArchiveFormat { get; set; }
        public CompressionMode CompressionMode { get; set; }
        public bool DirectoryStructure { get; set; }
        public bool PreserveDirectoryRoot { get; set; }
        public CompressionMethod CompressionMethod { get; set; }
        public event EventHandler<ProgressEventArgs> Compressing;
        public event EventHandler<EventArgs> CompressionFinished;

        public void BeginCompressDirectory(string directory, string archiveName)
        {
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public virtual void OnCompressing(ProgressEventArgs e)
        {
            Compressing?.Invoke(this, e);
        }

        public virtual void OnCompressionFinished()
        {
            CompressionFinished?.Invoke(this, EventArgs.Empty);
        }
    }
}