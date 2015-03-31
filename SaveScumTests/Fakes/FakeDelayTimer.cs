
using System;
using System.ComponentModel;
using System.Timers;
using SavegameAutoBackupAgent.DirectoryWatcherAgent;

namespace SaveScumTests.Fakes
{
    public class FakeDelayTimer : IDelayTimer
    {
        public bool Enabled { get; set; }

        public double Interval { get; set; }

        public event EventHandler Elapsed;
        public void Start()
        {
            //throw new NotImplementedException();
        }

        public void Stop()
        {
            //throw new NotImplementedException();
        }

        public void OnElapsed() => Elapsed?.Invoke(this, new EventArgs());
    }
}