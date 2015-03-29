using System;
using System.ComponentModel;
using System.Timers;

namespace SavegameAutoBackupAgent.FolderWatcherAgent
{
    public interface ITimer
    {
        bool AutoReset { get; set; }
        bool Enabled { get; set; }
        double Interval { get; set; }
        ISynchronizeInvoke SynchronizingObject { get; set; }

        event ElapsedEventHandler Elapsed;

        void BeginInit();
        void Close();
        void EndInit();
        void Start();
        void Stop();
    }
}