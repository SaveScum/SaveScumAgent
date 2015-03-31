using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SaveScumAgent.TaskScheduler.Tests
{
    /// <summary>
    ///     Summary description for TaskSchedulerTests
    /// </summary>
    [TestClass]
    public class TaskSchedulerTests
    {
        private const double DefaultInterval = 1000;
        private ITaskScheduler _scheduler;
        private MockTimer _timer;

        /// <summary>
        ///     Gets or sets the test context which provides
        ///     information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        #region Additional test attributes

        //
        // You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{

        //}
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        [TestInitialize]
        public void MyTestInitialize()
        {
            _timer = new MockTimer();
            _scheduler = new TaskScheduler(DefaultInterval, _timer);
        }

        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        #endregion

        [TestMethod]
        public void IsWaiting_ReturnsTrueWhileTimerIsRunning()
        {
            ;
            _scheduler.Start();
            Assert.IsTrue(_scheduler.IsWaiting);
        }

        [TestMethod]
        public void DefaultConstructorWorks()
        {
            var mre = new ManualResetEvent(false);
            _scheduler = new TaskScheduler(100);
            Assert.AreEqual(100, _scheduler.Interval);
            _scheduler.Elapsed += (sender, args) => { mre.Set(); };
            _scheduler.Start();
            Assert.IsFalse(mre.WaitOne(10));
            Assert.IsTrue(mre.WaitOne(200));
        }

        [TestMethod]
        public void IsWaiting_ReturnsFalseWhileTimerIsStopped()
        {
            _scheduler.Stop();
            Assert.IsFalse(_scheduler.IsWaiting);
        }

        [TestMethod]
        public void ReStart_ReturnDefaultIntervalIfTimerRunning()
        {
            _scheduler.Start();
            Assert.IsTrue(_scheduler.ReStart());
        }

        [TestMethod]
        public void TaskScheduler_FiresOffEventWhenTimerElapses()
        {
            var b = false;
            _scheduler.Elapsed += (sender, args) => b = true;
            Assert.IsFalse(b);
            _timer.TriggerElapsed();
            Assert.IsTrue(b);
        }

        [TestMethod]
        public void TaskScheduler_ReturnsReducedTimeOnResetWithReducerWhenTimerIsRunning()
        {
            _timer.Start();
            var i = _scheduler.ReStart(10);
            Assert.AreEqual(DefaultInterval - 10, i);
        }

        [TestMethod]
        public void TaskScheduler_ReturnsReducedTimeOnResetWithReducerWhenTimerIsStopped()
        {
            var i = _scheduler.ReStart(10);
            Assert.AreEqual(DefaultInterval, i);
        }

        [TestMethod]
        public void TaskScheduler_ReturnsReducedBottomsOutAtMinimumInterval()
        {
            _timer.Start();
            var i = _scheduler.ReStart(DefaultInterval);
            Assert.AreEqual(_scheduler.MinimumInterval, i);
        }

        [TestMethod]
        public void TaskScheduler_UpdatesInterval()
        {
            _scheduler.Interval = 99999;
            Assert.AreEqual(99999, _scheduler.Interval);
        }

        [TestMethod]
        public void TaskScheduler_UpdatesIntervalWithBottomOfMinimumInterval()
        {
            _scheduler.Interval = 0;
            Assert.AreEqual(_scheduler.MinimumInterval, _scheduler.Interval);
        }
    }
}