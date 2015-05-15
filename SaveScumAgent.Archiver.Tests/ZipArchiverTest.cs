using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SaveScumAgent.Archiver.Formats;

namespace SaveScumAgent.Archiver.Tests
{
    /// <summary>
    ///     Summary description for ZipArchiverTest
    /// </summary>
    [TestClass]
    public class ZipArchiverTest
    {
        private const string GameTitle = "A terrible game";
        private const string ArchivesPath = @"c:\archives\";
        private const string GameArchivePath = ArchivesPath + GameTitle + "\\";
        private MockZipFile _compressor;
        private ZipArchiver _subject;

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
        public void Initialize()
        {
            _compressor = new MockZipFile();
            _subject = new ZipArchiver
            {
                ArchivesLocation = GameArchivePath,
                DirectoryToArchive = @"c:\savegames",
                GameTitle = GameTitle
            };
        }

        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        #endregion

        [TestMethod]
        public void TestContructorWorks()
        {
            Assert.IsNotNull(_subject);
        }

        [TestMethod]
        public void DefaultConstructorWorks()
        {
            _subject = new ZipArchiver();
            Assert.IsNotNull(_subject);
        }

        [TestMethod]
        public void ZipArchiver_CreatesZipFilename()
        {
            _subject.StartArchivingAsync(new MockZipFile());
            Assert.IsTrue(_subject.ArchiveIdentifier.EndsWith(".zip"));
            TestContext.WriteLine(_subject.ArchiveIdentifier);
        }

        [TestMethod]
        public void ZipArchiver_RasesCompleteEvent()
        {
            var fired = false;
            _subject.ArchivingDone += (sender, args) => { fired = true; };
            _subject.StartArchivingAsync(_compressor);
            _compressor.OnCompressionFinished();
            Assert.IsTrue(fired);
        }

        [TestMethod]
        public void ZipArchiver_RasesInProgressEvent()
        {
            var fired = false;
            _subject.ArchiveProgress += (sender, args) => { fired = true; };
            _subject.StartArchivingAsync(_compressor);
            _compressor.OnSaveWriteEntry();
            Assert.IsTrue(fired);
        }

        [TestMethod]
        public void ZipArchiver_Integration_RecievesProgressEventFromZip()
        {
            var mre = new ManualResetEvent(false);
            _subject = new ZipArchiver {ArchivesLocation = "C:\\temp", DirectoryToArchive = "C:\\Temporary"};
            _subject.ArchiveProgress += (sender, args) =>
            {
                TestContext.WriteLine(args.ToString());
                mre.Set();
            };
            _subject.StartArchivingAsync();
            Assert.IsTrue(mre.WaitOne(10000));
        }

        [TestMethod]
        public void ZipArchiver_Integration_AbortsGracefully()
        {
            var mre = new ManualResetEventSlim(false);
            _subject = new ZipArchiver {ArchivesLocation = "C:\\temp", DirectoryToArchive = "C:\\Temporary"};
            _subject.ArchiveProgress += (sender, args) => { _subject.Abort(); };
            _subject.ArchivingError += (sender, args) =>
            {
                mre.Set();
                Assert.IsFalse(_subject.IsArchiving);
                Assert.IsTrue(args.Aborted);
            };
            _subject.StartArchivingAsync();
            Assert.IsTrue(mre.Wait(10000));
        }

        [TestMethod]
        public void ZipArchiver_Disposes()
        {
            _subject.StartArchivingAsync(_compressor);
            _subject.Abort();
            _compressor.OnSaveStarted();
            Assert.IsTrue(_compressor.Aborted);
        }

        [TestMethod]
        public void ZipArchiver_CreatesZipInAppropriateFolder()
        {
            var mre = new ManualResetEventSlim(false);
            var b = false;
            _subject.ArchiveProgress += (sender, args) =>
            {
                b = _compressor.ArchiveName.IsFolderSubfolderOf(GameArchivePath);
                mre.Set();
            };
            _subject.StartArchivingAsync(_compressor);
            mre.Wait();
            Assert.IsTrue(b);
        }

        [TestMethod]
        public void ZipArchiver_CanSendAbortSignal()
        {
            _subject.StartArchivingAsync(_compressor);
            _subject.Abort();
            _compressor.OnSaveStarted();
            Assert.IsTrue(_compressor.Aborted);
        }

        [TestMethod]
        [ExpectedException(typeof (InvalidOperationException))]
        public void ZipArchiver_FailsWhenArchiveIsSavedToDirectoryToBeArchived()
        {
            _subject = new ZipArchiver
            {
                ArchivesLocation = @"c:\savegames\archives",
                DirectoryToArchive = @"c:\savegames"
            };
            _subject.StartArchivingAsync(new MockZipFile());
        }
    }
}