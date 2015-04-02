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


        public override void Abort()
        {
            _abort = true;
        }

        public override void StartArchiving()
        {
            StartArchiving(new ZipFileWrapper());
        }

        public async void StartArchiving(IZipFile zf)
        {

            ArchiveIdentifier = Utils.GenerateBackupFilename(ArchivesLocation);
            _zipFile = zf;
            _zipFile.AddDirectory(DirectoryToArchive);
            _zipFile.SaveProgress += OnSaveProgress;
            _zipFile.ZipError += OnZipError;
            await Task.Run(() =>
            {
                _zipFile.Save(ArchiveIdentifier);
            });
        }

        private void OnZipError(object sender, ZipErrorEventArgs e)
        {
            bool deleted = false;
            try
            {
                if (File.Exists(ArchiveIdentifier))
                    File.Delete(ArchiveIdentifier);
                deleted = true;
            }
            finally
            {
                _zipFile.Dispose();
                OnArchivingError(new ArchivingInterruptedEventArgs(ArchiveIdentifier, deleted, e));
            }
        }


        private void OnSaveProgress(object sender, SaveProgressEventArgs e)
        {
            switch (e.EventType)
            {
                case ZipProgressEventType.Saving_Completed:
                    _zipFile.Dispose();
                    OnArchivingDone(new ArchivingEventArgs(DirectoryToArchive, ArchiveIdentifier, 100));
                    break;
                case ZipProgressEventType.Saving_AfterWriteEntry:
                case ZipProgressEventType.Saving_BeforeWriteEntry:
                    e.Cancel = _abort;
                    break;
            }
            throw new NotImplementedException();
        }
    }
}