using System;
using System.Timers;

namespace SaveScumAgent.TaskScheduler
{
    public class TaskScheduler : ITaskScheduler
    {
        private const double _minimumInterval = 100;
        private readonly ITimer _timer;
        private readonly Object timerLock = new object();
        private double _delayInMilliseconds;

        public TaskScheduler(double delayInMilliseconds) : this(delayInMilliseconds, new TimerWrapper())
        {
        }

        public TaskScheduler(double delayInMilliseconds, ITimer timer)
        {
            _delayInMilliseconds = delayInMilliseconds;
            _timer = timer;
            _timer.Interval = _delayInMilliseconds;
            _timer.AutoReset = false;
            _timer.Elapsed += _timer_Elapsed;
        }

        public double Interval
        {
            get { return _delayInMilliseconds; }
            set
            {
                _delayInMilliseconds = (value > MinimumInterval) ? value : MinimumInterval;
                _timer.Interval = _delayInMilliseconds;
            }
        }

        public double MinimumInterval => _minimumInterval;
        public bool IsWaiting => _timer.Enabled;

        public void Start()
        {
            lock (timerLock)
            {
                _timer.Start();
                _timer.Interval = _delayInMilliseconds;
            }
        }

        public void Stop()
        {
            _timer.Stop();
        }

        public bool ReStart()
        {
            var b = IsWaiting;
            _timer.Start();
            return b;
        }

        public double ReStart(double timerReduction)
        {
            lock (timerLock)
            {
                var newInterval = IsWaiting ? _timer.Interval - timerReduction : _timer.Interval;

                if (newInterval < MinimumInterval)
                    newInterval = MinimumInterval;

                _timer.Interval = newInterval;
                return _timer.Interval;
            }
        }

        public event EventHandler Elapsed;

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            lock (timerLock)
            {
                OnElapsed();
            }
        }

        protected virtual void OnElapsed()
        {
            Elapsed?.Invoke(this, EventArgs.Empty);
        }
    }
}