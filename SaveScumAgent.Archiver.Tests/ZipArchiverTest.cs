using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        public void MyTestInitialize()
        {
            _compressor = new MockSevenZipCompressor();
            _subject = new ZipArchiver(_compressor)
            {
                ArchivesDirectory = @"c:\archives",
                Directory = @"c:\savegames"
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
            _subject.StartArchiving();
            Assert.IsTrue(_subject.ArchiveIdentifier.EndsWith(".zip"));
            TestContext.WriteLine(_subject.ArchiveIdentifier);
        }

        [TestMethod]
        [ExpectedException(typeof (InvalidOperationException))]
        public void ZipArchiver_FailsWhenArchiveIsSavedToDirectoryToBeArchived()
        {
            _subject = new ZipArchiver(_compressor)
            {
                ArchivesDirectory = @"c:\savegames\archives",
                Directory = @"c:\savegames"
            };
            _subject.StartArchiving();
        }
    }
}