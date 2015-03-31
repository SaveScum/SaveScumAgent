using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.IO.Fakes;
using System.Security.AccessControl;
using System.Threading.Tasks;
using System.Timers;
using System.Timers.Fakes;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SavegameAutoBackupAgent.FolderWatcherAgent;
using SaveScumTests.Fakes;

namespace SaveScumTests
{
    /// <summary>
    /// Summary description for FolderWatcherAgent
    /// </summary>
    [TestClass]
    public class FolderWatcherAgent
    {

        private Task _stubTimer;
        private EventedStubFileSystemWatcher _stubFileSystemWatcher;
        private const string _baseDir = @"C:\temp";
        private int _fileIndex = 0;
        private const string _filenameFormat = "fakefile{0}.fake";

        public FolderWatcherAgent()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext _testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get { return _testContextInstance; }
            set { _testContextInstance = value; }
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
            
            _stubFileSystemWatcher = new EventedStubFileSystemWatcher(_baseDir);
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
            //var agent = new FolderWatcher(null, _stubTimer);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void NullTimerThrowsArgNullException()
        {
            var agent = new FolderWatcher(_stubFileSystemWatcher, null);

        }

        [TestMethod]
        public void FolderWatcherAgentWaitsForTimerToElapseBeforeFiring()
        {
            //var fired = false;
            //var agent = new FolderWatcher(_stubFileSystemWatcher, _stubTimer);
            //agent.DirectoryChangeDetected += (sender, args) =>
            //{
            //    fired = true;
            //};
            //_stubTimer.OnIntervalElapsed();
            //_stubFileSystemWatcher.RaiseChangeEvent(new FileSystemEventArgs(GetRandomChangeType(), _baseDir,
            //    GetFakeFilename()));




//            Assert.IsFalse(fired);

        }

        private WatcherChangeTypes GetRandomChangeType()
        {
            var values = Enum.GetValues(typeof (WatcherChangeTypes));
            var random = new Random();
            return (WatcherChangeTypes) values.GetValue(random.Next(values.Length));
        }

        private string GetFakeFilename()
        {
            return string.Format(_filenameFormat, _fileIndex++);
        }

    }
}