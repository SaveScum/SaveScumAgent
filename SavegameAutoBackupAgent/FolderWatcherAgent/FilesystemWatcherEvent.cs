using System;
using System.Collections.Generic;
using System.Linq;

namespace SavegameAutoBackupAgent.FolderWatcherAgent
{
    public class FilesystemWatcherEventArgs : EventArgs
    {

        public List<string> ChangedFiles { get; private set; }

        public FilesystemWatcherEventArgs(IEnumerable<string> changedFileList)
        {
            ChangedFiles = changedFileList.ToList();
        }

    }
}
