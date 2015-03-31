using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SaveScumAgent.DirectoryWatcher
{
    public class DirectoryWatcherEventArgs : EventArgs
    {
        public bool WatchTimerInterrupted;

        public DirectoryWatcherEventArgs(IEnumerable<FilesystemChangeRecord> changedFileList, bool interrupted = false)
        {
            WatchTimerInterrupted = interrupted;
            ChangedFiles = changedFileList.ToList();
        }

        public List<FilesystemChangeRecord> ChangedFiles { get; private set; }
    }

    public struct FilesystemChangeRecord
    {
        public FilesystemChangeRecord(string filename, WatcherChangeTypes changeType)
        {
            Filename = filename;
            ChangeType = changeType;
        }

        public WatcherChangeTypes ChangeType { get; }
        public string Filename { get; }

        public override string ToString()
        {
            return string.Format("{0}:{1}", ChangeType, Filename);
        }
    }
}