using Microsoft.VisualStudio.TestTools.UnitTesting;
using SaveScumAgent.Archiver.Formats;

namespace SaveScumAgent.Archiver.Tests
{
    /// <summary>
    ///     Summary description for ArchiverBaseTest
    /// </summary>
    [TestClass]
    public class ArchiverBaseTest
    {
        /// <summary>
        ///     Gets or sets the test context which provides
        ///     information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void FromFormatEnum_ReturnsBaseInstanceFromType()
        {
            var obj = ArchiverBase.FromFormatEnum(ArchiveFormat.Zip);

            Assert.AreEqual(obj.GetType(), typeof (ZipArchiver));
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
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        #endregion
    }
}