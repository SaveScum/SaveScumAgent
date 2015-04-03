using System;
using System.IO;
using System.Threading.Tasks;
using Ionic;
using Ionic.Zip;
using SaveScumAgent.UtilityClasses;


namespace SaveScumAgent.Archiver.Formats
{
    public class ZipArchiver : ArchiverBase
    {
        private IZipFile _zipFile;
        private bool _abort;

        private readonly object _lock = new object();


        public override void Abort()
        {
            _abort = true;
        }

        public override void StartArchivingAsync()
        {
            StartArchivingAsync(new ZipFileWrapper());
        }

        public void StartArchivingAsync(IZipFile zf)
        {

            if (ArchivesLocation.IsFolderSubfolderOf(DirectoryToArchive))
            {
                throw new InvalidOperationException(string.Format("{0} cannot be a subdirectory of {1}",
                    ArchivesLocation, DirectoryToArchive));
            }

            if (IsArchiving)
                throw new InvalidOperationException("Already archiving");

            lock (_lock)
            {
                _abort = false;
                IsArchiving = true;
                ArchiveIdentifier = Utils.GenerateBackupFilename(ArchivesLocation);
                _zipFile = zf;
                _zipFile.AddDirectory(DirectoryToArchive);
                _zipFile.SaveProgress += OnSaveProgress;
                _zipFile.ZipError += OnZipError;
                Task
                    .Factory
                    .StartNew(() =>
                    {
                        _zipFile.Save(ArchiveIdentifier);
                        IsArchiving = false;
                        _zipFile.Dispose();
                        if (_abort)
                        {
                            OnArchivingError(new ArchivingInterruptedEventArgs(ArchiveIdentifier,
                                File.Exists(ArchiveIdentifier), new EventArgs(), true));
                        }
                        else
                        {
                            OnArchivingDone(new ArchivingEventArgs(DirectoryToArchive, ArchiveIdentifier));
                        }

                    });
            }
        }

        private void OnZipError(object sender, ZipErrorEventArgs e)
        {
            _zipFile.Dispose();
            IsArchiving = false;
            OnArchivingError(new ArchivingInterruptedEventArgs(ArchiveIdentifier, File.Exists(ArchiveIdentifier), e));
        }


        private void OnSaveProgress(object sender, SaveProgressEventArgs e)
        {
            e.Cancel = e.Cancel || _abort;
            switch (e.EventType)
            {
                case ZipProgressEventType.Saving_Started:
                    OnArchiveProgress(this, new ArchivingEventArgs(DirectoryToArchive, ArchiveIdentifier));
                    break;
                case ZipProgressEventType.Saving_Completed:
                    _zipFile.Dispose();
                    OnArchivingDone(new ArchivingEventArgs(DirectoryToArchive, ArchiveIdentifier, 100));
                    break;
                case ZipProgressEventType.Saving_AfterWriteEntry:
                case ZipProgressEventType.Saving_BeforeWriteEntry:
                    OnArchiveProgress(this, new ArchivingEventArgs(DirectoryToArchive, ArchiveIdentifier));
                    break;
            }
        }
    }
}