using System.ComponentModel;
using System.IO;
using System.IO.Fakes;
using System.Reflection;

namespace SaveScumTests.Fakes
{
    public class EventedStubFileSystemWatcher : StubFileSystemWatcher
    {
        public EventedStubFileSystemWatcher(string path)
        {
        }

        public void RaiseChangeEvent(FileSystemEventArgs fileSystemEventArgs)
        {
            var events = Events;

            OnChanged(fileSystemEventArgs);
        }
    }
}