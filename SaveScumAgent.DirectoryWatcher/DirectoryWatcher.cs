using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using SaveScumAgent.TaskScheduler;
using SaveScumAgent.UtilityClasses;

namespace SaveScumAgent.DirectoryWatcher
{
    public class DirectoryWatcher : IDisposable, IDirectoryWatcher
    {
        private readonly ITaskScheduler _backupDelayTimer;
        private readonly List<FilesystemChangeRecord> _changedFilesList = new List<FilesystemChangeRecord>();
        private readonly FileSystemWatcherBase _fsWatcher;

        public DirectoryWatcher(FileSystemWatcherBase watcher, ITaskScheduler delayTimer)
        {
            if (delayTimer == null) throw new ArgumentNullException("delayTimer");
            if (watcher == null) throw new ArgumentNullException("watcher");

            _fsWatcher = watcher;
            _backupDelayTimer = delayTimer;
            InitializeTimer();
            InitializeFilesystemWatcher();
        }

        public DirectoryWatcher(string watchDirectory, double delayInMilliseconds) :
            this(new FileSystemWatcherWrapper(watchDirectory), new TaskScheduler.TaskScheduler(delayInMilliseconds))
        {
        }

        public bool Enabled
        {
            get { return _fsWatcher.EnableRaisingEvents; }

            set { _fsWatcher.EnableRaisingEvents = value; }
        }

        private void InitializeFilesystemWatcher()
        {
            _fsWatcher.IncludeSubdirectories = true;
            _fsWatcher.Created += _fsWatcher_Changed;
            _fsWatcher.Renamed += _fsWatcher_Changed;
            _fsWatcher.Changed += _fsWatcher_Changed;
            _fsWatcher.Deleted += _fsWatcher_Changed;
            _fsWatcher.Error += _fsWatcher_Error;
        }

        private void InitializeTimer()
        {
            _backupDelayTimer.Elapsed += BackupDelayTimerElapsed;
        }

        private void _fsWatcher_Error(object sender, ErrorEventArgs e)
        {
            var handler = DirectoryWatchError;
            handler?.Invoke(this, e);
        }

        private void BackupDelayTimerElapsed(object sender, EventArgs eventArgs)
        {
            OnDirectoryChangeDetected();
        }

        private void _fsWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            //Change has been detected, restart the delay delayTimer
            _backupDelayTimer.Start();
            var relPath = new PathString(e.FullPath).GetRelativePathFromAbsolute(_fsWatcher.Path);
            _changedFilesList.Add(new FilesystemChangeRecord(relPath, e.ChangeType));
        }

        #region Event implementations

        protected virtual void OnDirectoryChangeDetected()
        {
            var handler = DirectoryChangeDetected;
            handler?.Invoke(this, new DirectoryWatcherEventArgs(_changedFilesList));
            _changedFilesList.Clear();
        }

        #endregion

        #region Events

        public event EventHandler<DirectoryWatcherEventArgs> DirectoryChangeDetected;
        public event EventHandler<ErrorEventArgs> DirectoryWatchError;

        #endregion

        #region Idispose members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            _fsWatcher.EnableRaisingEvents = false;
        }

        #endregion
    }
}