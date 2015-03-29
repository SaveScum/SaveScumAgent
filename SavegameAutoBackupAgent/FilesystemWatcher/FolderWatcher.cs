using System;
using System.Collections.Generic;
using System.IO;
using System.Timers;

namespace AutoBackup.FilesystemWatcher
{
    internal class FolderWatcher :IDisposable
    {
        private Timer _backupTimer;
        private FileSystemWatcher _fsWatcher;
        private readonly List<string> _changedFilesList = new List<string>();
        private int _timerDelay;

        public bool Enabled
        {
            get { return _fsWatcher.EnableRaisingEvents; }

            set { _fsWatcher.EnableRaisingEvents = value; }
        }

        public int TimerDelay
        {
            get { return _timerDelay; }
            set
            {
                _timerDelay = value;
                InitializeTimer(value);
            }
        }

        //private Archiver _arch;

        public FolderWatcher(string folderToWatch, int delayInSeconds)
        {
            InitializeTimer(delayInSeconds);
            InitializeFilesystemWatcher(folderToWatch);
        }

        private void InitializeFilesystemWatcher(string folderToWatch)
        {
            _fsWatcher = new FileSystemWatcher(folderToWatch)
            {
                IncludeSubdirectories = true,
                EnableRaisingEvents = true
            };
            _fsWatcher.Created += _fsWatcher_Changed;
            _fsWatcher.Renamed += _fsWatcher_Changed;
            _fsWatcher.Changed += _fsWatcher_Changed;
            _fsWatcher.Deleted += _fsWatcher_Changed;
            _fsWatcher.Error += _fsWatcher_Error;
        }

        private void InitializeTimer(int delayInSeconds)
        {
            _backupTimer?.Dispose();
            _timerDelay = delayInSeconds;
            _backupTimer = new Timer {AutoReset = false, Interval = _timerDelay * 1000};
            _backupTimer.Elapsed += backupTimer_Elapsed;
        }

        private void _fsWatcher_Error(object sender, ErrorEventArgs e)
        {
            var handler = DirectoryWatchError;
            handler?.Invoke(this, e);
        }

        #region Events
        public event EventHandler<FilesystemWatcherEventArgs> DirectoryChangeDetected;
        public event EventHandler<ErrorEventArgs> DirectoryWatchError;
        #endregion

        private void backupTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            OnDirectoryChangeDetected();
        }




        private void _fsWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            //Change has been detected, restart the delay timer
            _backupTimer.Stop();
            _backupTimer.Start();
            _changedFilesList.Add(string.Format("{0}:{1}", e.ChangeType, e.FullPath));
        }



        #region Event implementations
        protected virtual void OnDirectoryChangeDetected()
        {
            var handler = DirectoryChangeDetected;
            handler?.Invoke(this, new FilesystemWatcherEventArgs(_changedFilesList));
            _changedFilesList.Clear();
        }

        #endregion

        #region Idispose members
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _fsWatcher.EnableRaisingEvents = false;
                _fsWatcher.Dispose();
            }
        } 
        #endregion
    }
}