using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SaveScumAgent.TaskScheduler;

namespace SaveScumAgent.DirectoryWatcher.Test
{
    class MockTaskScheduler : ITaskScheduler
    {
        public double Interval { get; set; }

        public double MinimumInterval => 100;

        public bool IsWaiting { get; set; }

        public void Start()
        {
            IsWaiting = true;
        }

        public void Stop()
        {
            IsWaiting = false;
        }

        public bool ReStart()
        {
            return IsWaiting;
        }

        public double ReStart(double timerReduction)
        {
            return Interval;
        }

        public event EventHandler Elapsed;

        public void OnElapsed()
        {
            IsWaiting = false;
            Elapsed?.Invoke(this, EventArgs.Empty);
        }
    }
}
