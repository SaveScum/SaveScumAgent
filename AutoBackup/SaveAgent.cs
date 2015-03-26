using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoBackup.ArchiveTools;
using AutoBackup.FilesystemWatcher;
using SevenZip;

namespace AutoBackup
{
    public class SaveAgent
    {
        public string ArchiveFolder { get; set; }

        private FolderWatcher _folderWatcher;

        public OutArchiveFormat Format { get; set; }

        public CompressionLevel Level { get; set; }

        public int WatchEventDelay
        {
            get { return _folderWatcher.TimerDelay; }
            set { _folderWatcher.TimerDelay = value; }
        }

        public string WatchFolder
        {
            get { return _watchFolder; }
            set
            {
                _watchFolder = value;
                InitializeFilesystemWatcher(WatchEventDelay);
            }
        }

        private Archiver _archiver;
        private string _watchFolder;

        public SaveAgent(string watchFolder, string archiveFolder, int watchEventDelay, OutArchiveFormat format,
            CompressionLevel level)
        {


            _watchFolder = watchFolder;
            ArchiveFolder = archiveFolder;
            Format = format;
            Level = level;

            InitializeFilesystemWatcher(watchEventDelay);
        }

        private void InitializeFilesystemWatcher(int watchEventDelay)
        {
            if (Utils.IsFolderSubfolderOf(ArchiveFolder, WatchFolder))
                throw new ArgumentException(string.Format("<{0}> cannot be a subfolder of <{1}>", ArchiveFolder, WatchFolder));

            _folderWatcher?.Dispose();

            _folderWatcher = new FolderWatcher(WatchFolder, watchEventDelay);
            _folderWatcher.DirectoryChangeDetected += _folderWatcher_DirectoryChangeDetected;
            _folderWatcher.Enabled = true;
        }

        public bool Enabled
        {
            get { return _folderWatcher.Enabled; }
            set { _folderWatcher.Enabled = value; }
        }


        private void _folderWatcher_DirectoryChangeDetected(object sender, FilesystemWatcherEventArgs e)
        {
            if (_archiver != null)
            {
                var archFile = _archiver.ArchiveFile;
                _archiver.Abort();
                if (File.Exists(archFile))
                {
                    try
                    {
                        File.Delete(archFile);
                        OnArchivingInterrupted(new ArchivingInterruptedEventArgs(archFile, File.Exists(archFile)));
                    }
                    catch (Exception ex)
                    {
                        OnArchivingInterrupted(new ArchivingInterruptedEventArgs(archFile, File.Exists(archFile), ex));
                    }
                }
                _archiver = null;
            }
            InitializeArchiver(e.ChangedFiles);
        }

        private void InitializeArchiver(List<string> changedFileList)
        {
            var archFile = Utils.GenerateBackupFilename(ArchiveFolder, Archiver.SuggestedExtension(Format));
            _archiver = new Archiver(WatchFolder, archFile, Format, Level);
            _archiver.ArchiveProgress += (o, args) => _archiver_ArchiveProgress(args, changedFileList);
            _archiver.ArchivingDone += (o, args) => _archiver_ArchivingDone(args, changedFileList);
            _archiver.Archive();
            var handler = ArchivingStarted;
            handler?.Invoke(this, new ArchivingEventArgs(WatchFolder, archFile, 0, changedFileList));
        }

        public event EventHandler<ArchivingEventArgs> ArchivingStarted;
        public event EventHandler<ArchivingEventArgs> ArchivingProgress;
        public event EventHandler<ArchivingEventArgs> ArchivingDone;
        public event EventHandler<ArchivingInterruptedEventArgs> ArchivingInterrupted;

        private void _archiver_ArchivingDone(ArchivingEventArgs e, List<string> changedFileList)
        {
            e.ChangedFileList = changedFileList;
            ArchivingDone?.Invoke(this, e);
        }

        private void _archiver_ArchiveProgress(ArchivingEventArgs e, List<string> changedFileList)
        {
            e.ChangedFileList = changedFileList;
            ArchivingProgress?.Invoke(this, e);
        }

        protected virtual void OnArchivingInterrupted(ArchivingInterruptedEventArgs e)
        {
            ArchivingInterrupted?.Invoke(this, e);
        }
    }
}