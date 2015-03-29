
using System;
using System.ComponentModel;
using System.Timers;
using SavegameAutoBackupAgent.FolderWatcherAgent;

namespace SaveScumTests.Fakes
{
    public class FakeTimer : ITimer
    {
        public bool AutoReset { get; set; }

        public bool Enabled { get; set; }

        public double Interval { get; set; }

        public ISynchronizeInvoke SynchronizingObject { get; set; }

        public event EventHandler<ElapsedEventArgs> Elapsed;

        public void BeginInit()
        {
            //throw new System.NotImplementedException();
        }

        public void Close()
        {
            //throw new System.NotImplementedException();
        }

        public void EndInit()
        {
            //throw new System.NotImplementedException();
        }

        public void Start()
        {
            //throw new System.NotImplementedException();
        }

        public void Stop()
        {
            //throw new System.NotImplementedException();
        }

        public void OnIntervalElapsed()
        {
            var e = Elapsed;
            e?.Invoke(this, (ElapsedEventArgs) new EventArgs());
        }
    }
}