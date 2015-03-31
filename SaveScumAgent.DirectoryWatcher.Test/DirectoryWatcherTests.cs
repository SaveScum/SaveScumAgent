using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SaveScumAgent.TaskScheduler;

namespace SaveScumAgent.DirectoryWatcher.Test
{
    /// <summary>
    /// Summary description for DirectoryWatcherTests
    /// </summary>
    [TestClass]
    public class DirectoryWatcherTests
    {

        private DirectoryWatcher _directoryWatcher;
        private MockTaskScheduler _scheduler;
        private MockFileSystemWatcher _mockedFileSystemWatcher;
        private const string BaseDir = @"C:\temp";
        private int _fileIndex;
        private const string FilenameFormat = "fakefile{0}.fake";
        private readonly Random _random = new Random();

        public DirectoryWatcherTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

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
        [TestInitialize()]
        public void MyTestInitialize()
        {
            _scheduler = new MockTaskScheduler();
            _mockedFileSystemWatcher = new MockFileSystemWatcher();
            _directoryWatcher = new DirectoryWatcher(_mockedFileSystemWatcher, _scheduler);
        }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullWatcherThrowsArgNullException()
        {
            var agent = new DirectoryWatcher(null, _scheduler);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullTimerThrowsArgNullException()
        {
            // ReSharper disable once UnusedVariable
            var agent = new DirectoryWatcher(_mockedFileSystemWatcher, null);

        }

        [TestMethod]
        public void FolderWatcherAgentWaitsForTimerToElapseBeforeFiring()
        {
            var fired = false;
            var agent = new DirectoryWatcher(_mockedFileSystemWatcher, _scheduler);
            agent.DirectoryChangeDetected += (sender, args) =>
            {
                fired = true;
            };

            _mockedFileSystemWatcher.RaiseChangeEvent(new FileSystemEventArgs(GetRandomChangeType(), BaseDir,
                GetFakeFilename()));

            Assert.IsFalse(fired);
            _scheduler.OnElapsed();
            Assert.IsTrue(fired);

        }

        [TestMethod]
        public void FolderWatcherReturnsListOfChangedFiles()
        {
            var fileList = new List<FilesystemChangeRecord>();
            var agent = new DirectoryWatcher(_mockedFileSystemWatcher, _scheduler);
            agent.DirectoryChangeDetected += (sender, args) =>
            {
                CollectionAssert.AreEquivalent(args.ChangedFiles, fileList);
            };

            for (int i = 0; i < 5; i++)
            {
                var f = GetFakeFilename();
                var w = GetRandomChangeType();
                fileList.Add(new FilesystemChangeRecord(f, w));
                _mockedFileSystemWatcher.RaiseChangeEvent(new FileSystemEventArgs(w, BaseDir, f));
            }

            for (int i = 0; i < 5; i++)
            {
                var f = GetFakeFilenameWithRandomSubdir();
                var w = GetRandomChangeType();
                fileList.Add(new FilesystemChangeRecord(f, w));
                _mockedFileSystemWatcher.RaiseChangeEvent(new FileSystemEventArgs(w, BaseDir, f));
            }

            _scheduler.OnElapsed();

        }

        private WatcherChangeTypes GetRandomChangeType()
        {
            var values = Enum.GetValues(typeof(WatcherChangeTypes));
            return (WatcherChangeTypes)values.GetValue(_random.Next(values.Length));
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
