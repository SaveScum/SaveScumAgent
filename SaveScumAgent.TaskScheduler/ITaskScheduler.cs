using System;

namespace SaveScumAgent.TaskScheduler
{
    public interface ITaskScheduler
    {

        double Interval { get; set; }

        double MinimumInterval { get; }

        bool IsWaiting { get; }

        void Start();

        void Stop();

        bool ReStart();
        double ReStart(double timerReduction);

        event EventHandler Elapsed;
    }
}