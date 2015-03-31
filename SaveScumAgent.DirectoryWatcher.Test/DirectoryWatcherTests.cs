using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SaveScumAgent.DirectoryWatcher.Test
{
    /// <summary>
    ///     Summary description for DirectoryWatcherTests
    /// </summary>
    [TestClass]
    public class DirectoryWatcherTests
    {
        private const string BaseDir = @"C:\temp";
        private const string FilenameFormat = "fakefile{0}.fake";
        private readonly Random _random = new Random();
        private DirectoryWatcher _directoryWatcher;
        private int _fileIndex;
        private MockFileSystemWatcher _mockedFileSystemWatcher;
        private MockTaskScheduler _scheduler;

        /// <summary>
        ///     Gets or sets the test context which provides
        ///     information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        #region Additional test attributes

        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        [TestInitialize]
        public void MyTestInitialize()
        {
            _scheduler = new MockTaskScheduler();
            _mockedFileSystemWatcher = new MockFileSystemWatcher(BaseDir);
            _directoryWatcher = new DirectoryWatcher(_mockedFileSystemWatcher, _scheduler);
        }

        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        #endregion

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void NullWatcherThrowsArgNullException()
        {
            var agent = new DirectoryWatcher(null, _scheduler);
        }

        [TestMethod]
        public void ProductionConstructorWorks()
        {
            var agent = new DirectoryWatcher(TestContext.TestRunDirectory, 100);
            Assert.IsFalse(agent.Enabled);
            Assert.IsNotNull(agent);
            agent.Enabled = true;
            Assert.IsTrue(agent.Enabled);
        }

        [TestMethod]
        public void DirectoryWatcherCanBeToggled()
        {
            _directoryWatcher.Enabled = true;
            Assert.IsTrue(_directoryWatcher.Enabled);

            _directoryWatcher.Enabled = false;
            Assert.IsFalse(_directoryWatcher.Enabled);

            _directoryWatcher.Enabled = true;
            Assert.IsTrue(_directoryWatcher.Enabled);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void NullTimerThrowsArgNullException()
        {
            // ReSharper disable once UnusedVariable
            var agent = new DirectoryWatcher(_mockedFileSystemWatcher, null);
        }

        [TestMethod]
        public void FolderWatcherAgentWaitsForTimerToElapseBeforeFiring()
        {
            var fired = false;
            _directoryWatcher.DirectoryChangeDetected += (sender, args) => { fired = true; };

            _mockedFileSystemWatcher.RaiseChangeEvent(ThrowawayFileSystemEventArgs());

            Assert.IsFalse(fired);
            _scheduler.OnElapsed();
            Assert.IsTrue(fired);
        }

        [TestMethod]
        public void FolderWatcherAgent_OnlyRaisesEventsWhenEnabled()
        {
            var fired = false;
            _directoryWatcher.DirectoryChangeDetected += (sender, args) => { fired = true; };
            _mockedFileSystemWatcher.RaiseChangeEvent(ThrowawayFileSystemEventArgs());
            Assert.IsFalse(_scheduler.IsWaiting);
        }

        /// <summary>
        ///     This is required in case the game makes one final save on exit
        /// </summary>
        [TestMethod]
        public void FolderWatcherAgent_RaisesEventIfRunningWhenDisabled()
        {
            var fired = false;
            _directoryWatcher.Enabled = true;
            _directoryWatcher.DirectoryChangeDetected += (sender, args) => { fired = true; };

            _mockedFileSystemWatcher.RaiseChangeEvent(ThrowawayFileSystemEventArgs());
            _directoryWatcher.Enabled = false;
            _scheduler.OnElapsed();

            Assert.IsTrue(fired);
        }

        [TestMethod]
        public void FolderWatcherReturnsListOfChangedFiles()
        {
            _directoryWatcher.Enabled = true;
            var fileList = new List<FilesystemChangeRecord>();
            _directoryWatcher.DirectoryChangeDetected +=
                (sender, args) => { CollectionAssert.AreEquivalent(args.ChangedFiles, fileList); };

            for (var i = 0; i < 5; i++)
            {
                var f = GetFakeFilename();
                var w = GetRandomChangeType();
                fileList.Add(new FilesystemChangeRecord(f, w));
                _mockedFileSystemWatcher.RaiseChangeEvent(new FileSystemEventArgs(w, BaseDir, f));
            }

            for (var i = 0; i < 5; i++)
            {
                var f = GetFakeFilenameWithRandomSubdir();
                var w = GetRandomChangeType();
                fileList.Add(new FilesystemChangeRecord(f, w));
                _mockedFileSystemWatcher.RaiseChangeEvent(new FileSystemEventArgs(w, BaseDir, f));
            }

            _scheduler.OnElapsed();
        }


        [TestMethod]
        public void DirectoryWatcher_RaisesErrorOnFilesystemWatcherError()
        {
            var fired = false;
            _directoryWatcher.DirectoryWatchError += (sender, args) => fired = true;
            _directoryWatcher.Enabled = true;
            _mockedFileSystemWatcher.RaiseErrorEvent(null);
            Assert.IsTrue(fired);
        }

        [TestMethod]
        public void DirectoryWatcher_ReturnsCorrectFileListAcrossRaisedEvents()
        {
            _directoryWatcher.Enabled = true;
            var raised = 0;
            _directoryWatcher.DirectoryChangeDetected +=
                (sender, args) =>
                {
                    Assert.AreEqual(1, args.ChangedFiles.Count);
                    raised++;
                };
            _mockedFileSystemWatcher.RaiseChangeEvent(ThrowawayFileSystemEventArgs());
            _scheduler.OnElapsed();
            _mockedFileSystemWatcher.RaiseChangeEvent(ThrowawayFileSystemEventArgs());
            _scheduler.OnElapsed();
            Assert.AreEqual(raised, 2);
        }


        [TestMethod]
        public void DirectoryWatcher_StartsScheduledTask()
        {
            _directoryWatcher.Enabled = true;
            Assert.IsFalse(_scheduler.IsWaiting);
            _mockedFileSystemWatcher.RaiseChangeEvent(ThrowawayFileSystemEventArgs());
            Assert.IsTrue(_scheduler.IsWaiting);
        }

        [TestMethod]
        public void MockedScheduledTask_ResetsIsWaiting()
        {
            Assert.IsFalse(_scheduler.IsWaiting);
            _scheduler.Start();
            Assert.IsTrue(_scheduler.IsWaiting);
            _scheduler.OnElapsed();
            Assert.IsFalse(_scheduler.IsWaiting);
        }


        private FileSystemEventArgs ThrowawayFileSystemEventArgs()
        {
            return new FileSystemEventArgs(GetRandomChangeType(), BaseDir, GetFakeFilename());
        }


        private WatcherChangeTypes GetRandomChangeType()
        {
            var values = Enum.GetValues(typeof (WatcherChangeTypes));
            return (WatcherChangeTypes) values.GetValue(_random.Next(values.Length));
        }

        private string GetFakeFilenameWithRandomSubdir()
        {
            return GetFakeFilename(Guid.NewGuid().ToString());
        }

        private string GetFakeFilename(string subdir = "")
        {
            return Path.Combine(subdir, string.Format(FilenameFormat, _fileIndex++));
        }
    }
}