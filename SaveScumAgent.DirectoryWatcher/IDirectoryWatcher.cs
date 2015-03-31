using System;
using System.IO;

namespace SaveScumAgent.DirectoryWatcher
{
    public interface IDirectoryWatcher
    {
        bool Enabled { get; set; }
        event EventHandler<DirectoryWatcherEventArgs> DirectoryChangeDetected;
        event EventHandler<ErrorEventArgs> DirectoryWatchError;
    }
}