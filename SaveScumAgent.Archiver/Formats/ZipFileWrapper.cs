using System;
using Ionic.Zip;

namespace SaveScumAgent.Archiver.Formats
{
    internal class ZipFileWrapper : IZipFile
    {
        private readonly ZipFile _zipFile;

        public ZipFileWrapper()
        {
            _zipFile = new ZipFile();
            _zipFile.SaveProgress += OnSaveProgress;
            _zipFile.ZipError += OnZipError;
        }

        public ZipEntry AddDirectory(string directoryName)
        {
            return _zipFile.AddDirectory(directoryName);
        }

        public ZipEntry AddDirectory(string directoryName, string directoryPathInArchive)
        {
            return _zipFile.AddDirectory(directoryName, directoryPathInArchive);
        }

        public void Dispose()
        {
            _zipFile.Dispose();
        }

        public void Save(string fileName)
        {
            _zipFile.Save(fileName);
        }

        public event EventHandler<SaveProgressEventArgs> SaveProgress;
        public event EventHandler<ZipErrorEventArgs> ZipError;

        private void OnZipError(object sender, ZipErrorEventArgs e)
        {
            ZipError?.Invoke(this, e);
        }

        private void OnSaveProgress(object sender, SaveProgressEventArgs e)
        {
            SaveProgress?.Invoke(this, e);
        }
    }
}