using System;
using System.ComponentModel;
using System.IO;
using System.IO.Abstractions;

namespace SaveScumAgent.DirectoryWatcher.Test
{
    internal class MockFileSystemWatcher : FileSystemWatcherBase
    {
        public MockFileSystemWatcher(string baseDir)
        {
            Path = baseDir;
            EnableRaisingEvents = false;
        }

        public override bool IncludeSubdirectories { get; set; }
        public override bool EnableRaisingEvents { get; set; }
        public override string Filter { get; set; }

        public override int InternalBufferSize
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public override NotifyFilters NotifyFilter
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public override string Path { get; set; }

        public override ISite Site
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public override ISynchronizeInvoke SynchronizingObject
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public override void BeginInit()
        {
            throw new NotImplementedException();
        }

        public override void Dispose()
        {
            throw new NotImplementedException();
        }

        public override void EndInit()
        {
            throw new NotImplementedException();
        }

        public override WaitForChangedResult WaitForChanged(WatcherChangeTypes changeType)
        {
            throw new NotImplementedException();
        }

        public override WaitForChangedResult WaitForChanged(WatcherChangeTypes changeType, int timeout)
        {
            throw new NotImplementedException();
        }

        public void RaiseErrorEvent(ErrorEventArgs e)
        {
            if (EnableRaisingEvents) OnError(this, e);
        }

        public void RaiseChangeEvent(FileSystemEventArgs fileSystemEventArgs)
        {
            if (EnableRaisingEvents) OnChanged(this, fileSystemEventArgs);
        }
    }
}