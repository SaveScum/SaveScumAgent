using System.Timers;

namespace SaveScumAgent.TaskScheduler.Tests
{
    internal class MockTimer : ITimer
    {
        public event ElapsedEventHandler Elapsed;
        public double Interval { get; set; }
        public bool Enabled { get; set; }
        public bool AutoReset { get; set; }

        public void Start()
        {
            Enabled = true;
        }

        public void Stop()
        {
            Enabled = false;
        }

        public void TriggerElapsed()
        {
            OnElapsed(null);
        }

        protected virtual void OnElapsed(ElapsedEventArgs e)
        {
            if (!AutoReset) Stop();
            Elapsed?.Invoke(this, e);
        }
    }
}