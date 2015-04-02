using System;
using System.IO;

namespace SaveScumAgent.DirectoryWatcher
{
    public interface IDirectoryWatcher : IDisposable
    {
        bool Enabled { get; set; }
        event EventHandler<DirectoryWatcherEventArgs> DirectoryChangeDetected;
        event EventHandler<ErrorEventArgs> DirectoryWatchError;
    }
}