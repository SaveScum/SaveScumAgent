using System;
using SevenZip;

namespace SaveScumAgent.Archiver
{
    public interface ISevenZipCompressor
    {
        CompressionLevel CompressionLevel { get; set; }
        OutArchiveFormat ArchiveFormat { get; set; }
        CompressionMode CompressionMode { get; set; }
        bool DirectoryStructure { get; set; }
        bool PreserveDirectoryRoot { get; set; }
        CompressionMethod CompressionMethod { get; set; }
        event EventHandler<ProgressEventArgs> Compressing;
        event EventHandler<EventArgs> CompressionFinished;
        void BeginCompressDirectory(string directory, string archiveName);
    }
}