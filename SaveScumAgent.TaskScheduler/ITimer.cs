using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SaveScumAgent.TaskScheduler
{
    public interface ITimer
    {
        event ElapsedEventHandler Elapsed;
        double Interval { get; set; }
        bool Enabled { get; set; }
        bool AutoReset { get; set; }
        void Start();
        void Stop();
    }
}
