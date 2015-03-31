using System.Timers;

namespace SaveScumAgent.TaskScheduler
{
    public interface ITimer
    {
        double Interval { get; set; }
        bool Enabled { get; set; }
        bool AutoReset { get; set; }
        event ElapsedEventHandler Elapsed;
        void Start();
        void Stop();
    }
}