using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SaveScumAgent.Archiver.Formats;
using SaveScumAgent.UtilityClasses;
using SevenZip;

namespace SaveScumAgent.Archiver.Tests
{
    /// <summary>
    ///     Summary description for ZipArchiverTest
    /// </summary>
    [TestClass]
    public class ZipArchiverTest
    {
        private MockSevenZipCompressor _compressor;
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
            _compressor = new MockSevenZipCompressor();
            _subject = new ZipArchiver()
            {
                ArchivesLocation = @"c:\archives",
                DirectoryToArchive = @"c:\savegames",
                GameTitle = "A terrible game"
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
            var t = ArchiveFormats.Zip.GetAttributeOfType<ArchiveFormatAttribute>();
            var instance = (IArchiver)Activator.CreateInstance(t.FormatType);
            TestContext.WriteLine(instance.ArchiveIdentifier);

            _subject = new ZipArchiver();
            Assert.IsNotNull(_subject);
        }

        [TestMethod]
        public void ZipArchiver_CreatesZipFilename()
        {
            _subject.StartArchiving();
            Assert.IsTrue(_subject.ArchiveIdentifier.EndsWith(".zip"));
            TestContext.WriteLine(_subject.ArchiveIdentifier);
        }

        [TestMethod]
        public void ZipArchiver_RasesCompleteEvent()
        {
            var fired = false;
            _subject.ArchivingDone += (sender, args) => { fired = true; };
            _subject.StartArchiving();
            _compressor.OnCompressionFinished();
            Assert.IsTrue(fired);
        }

        [TestMethod]
        public void ZipArchiver_RasesInProgressEvent()
        {
            var fired = false;
            _subject.ArchiveProgress += (sender, args) => { fired = true; };
            _subject.StartArchiving();
            _compressor.OnCompressing(new ProgressEventArgs(0, 0));
            Assert.IsTrue(fired);
        }

        [TestMethod]
        public void ZipArchiver_RecievesProgressEventFromSevenZip()
        {
            var fired = false;
            var mre = new ManualResetEvent(false);
            _subject = new ZipArchiver { ArchivesLocation = "C:\\temp", DirectoryToArchive = "C:\\Temporary"};
            _subject.ArchiveProgress += (sender, args) =>
            {
                TestContext.WriteLine(args.ArchiveFile);
                mre.Set();
            };
            _subject.StartArchiving();
            Assert.IsTrue(mre.WaitOne(10000));
        }

        [TestMethod]
        [ExpectedException(typeof (InvalidOperationException))]
        public void ZipArchiver_FailsWhenArchiveIsSavedToDirectoryToBeArchived()
        {
            _subject = new ZipArchiver()
            {
                ArchivesLocation = @"c:\savegames\archives",
                DirectoryToArchive = @"c:\savegames"
            };
            _subject.StartArchiving();
        }
    }
}