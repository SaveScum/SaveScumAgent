using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using SevenZip;

namespace AutoBackup.ArchiveTools
{
    public class Archiver
    {
        public static readonly Dictionary<OutArchiveFormat, string> FileExtensions = new Dictionary
            <OutArchiveFormat, string>
        {
            {OutArchiveFormat.BZip2, @".bz2"},
            {OutArchiveFormat.SevenZip, @".7z"},
            {OutArchiveFormat.XZ, @".xz"},
            {OutArchiveFormat.Zip, @".zip"}
        };

        private readonly SevenZipCompressor _compressor;
        private bool _abortArchiving;

        public Archiver(string archiveFile, string directory, OutArchiveFormat format = OutArchiveFormat.Zip,
            CompressionLevel compressionLevel = CompressionLevel.Normal)
        {
            if (!Directory.Exists(directory))
                throw new ArgumentException(string.Format("{0} does not exist", directory), "directory");


            if (!FileExtensions.ContainsKey(format))
                throw new InvalidEnumArgumentException(String.Format("{0} not supported", format));

            #region Set up compressor

            _compressor = new SevenZipCompressor
            {
                ArchiveFormat = format,
                CompressionMode = CompressionMode.Create,
                CompressionLevel = compressionLevel
            };


            ArchiveFile = archiveFile;
            DirectoryToArchive = directory;

            #endregion

            #region Set up compressor events

            #endregion
        }

        public bool Archiving { get; private set; }
        public string ArchiveFile { get; set; }
        public string DirectoryToArchive { get; }

        public static string SuggestedExtension(OutArchiveFormat format)
        {
            return FileExtensions[format];
        }

        private static void IsArchiveFileWritable(string archiveFile)
        {
            var arch = File.Open(archiveFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);

            if (!arch.CanWrite)
            {
                throw new ArgumentException(string.Format("Cannot write to {0}", archiveFile), "archiveFile");
            }
            arch.Close();
        }

        public void Abort()
        {
            _abortArchiving = true;
        }

        public event EventHandler<ArchivingEventArgs> ArchiveProgress;
        public event EventHandler<ArchivingEventArgs> ArchivingDone;

        public void Archive()
        {
            IsArchiveFileWritable(ArchiveFile);

            _compressor.Compressing += (sender, args) =>
            {
                var handler = ArchiveProgress;
                handler?.Invoke(this, new ArchivingEventArgs(DirectoryToArchive, ArchiveFile, args.PercentDone));
                if (_abortArchiving)
                {
                    args.Cancel = true;
                }
            };

            _compressor.CompressionFinished += (sender, args) =>
            {
                Archiving = false;
                var handler = ArchivingDone;
                handler?.Invoke(this, new ArchivingEventArgs(DirectoryToArchive, ArchiveFile, 100));
            };

            _compressor.BeginCompressDirectory(DirectoryToArchive, ArchiveFile);
            Archiving = true;
        }
    }
}