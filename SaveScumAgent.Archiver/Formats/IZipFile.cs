using System;
using Ionic.Zip;

namespace SaveScumAgent.Archiver.Formats
{
    public interface IZipFile
    {
        ZipEntry AddDirectory(string directoryName);
        ZipEntry AddDirectory(string directoryName, string directoryPathInArchive);
        void Dispose();
        void Save(string fileName);
        event EventHandler<SaveProgressEventArgs> SaveProgress;
        event EventHandler<ZipErrorEventArgs> ZipError;
    }
}