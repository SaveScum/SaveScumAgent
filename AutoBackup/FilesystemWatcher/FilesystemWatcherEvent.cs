using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoBackup.FilesystemWatcher
{
    internal class FilesystemWatcherEventArgs : EventArgs
    {

        public List<string> ChangedFiles { get; private set; }

        public FilesystemWatcherEventArgs(IEnumerable<string> changedFileList)
        {
            ChangedFiles = changedFileList.ToList();
        }

    }
}
