using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Environment;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SaveScumAgent.UtilityClasses;

namespace SaveScumAgent.Tests
{
    /// <summary>
    /// Summary description for SpecialFolderHelperTests
    /// </summary>
    [TestClass]
    public class SpecialFolderHelperTests
    {
        private string _desktopPath = GetFolderPath(SpecialFolder.DesktopDirectory);
        private string _windowsPath = GetFolderPath(SpecialFolder.Windows);

        public SpecialFolderHelperTests()
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
            
        }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void FindMatchedSpecialFolders_ReturnsEmptyListForNoMatch()
        {
            var testPath = "c:\\junk_path\\test";
            var result = SpecialFolderHelper.FindMatchedSpecialFolders(testPath);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void FindMatchedSpecialFolders_ReturnsListForMatch()
        {
            var testPath = _desktopPath + "\\test";
            var d = SpecialFolderHelper.PathsDictionary;
            var result = SpecialFolderHelper.FindMatchedSpecialFolders(testPath);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
            Assert.AreEqual("DESKTOPDIRECTORY", result.First().Tag);
        }

        [TestMethod]
        public void FindMatchedSpecialFolders_ReturnsFormattedReplacementStringForMatch()
        {
            var testPath = _desktopPath + "\\test";
            var result = SpecialFolderHelper.FindMatchedSpecialFolders(testPath);
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("{DESKTOPDIRECTORY}\\test", result.First().ReplacedString);
        }

        [TestMethod]
        public void FindMatchedSpecialFolders_ReturnsFormattedReplacementStringForMatchWithRelativePath()
        {
            var testPath = _desktopPath + "\\..\\test";
            var result = SpecialFolderHelper.FindMatchedSpecialFolders(testPath);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("{USERPROFILE}\\test", result.First().ReplacedString);
        }

        [TestMethod]
        public void FindMatchedSpecialFolders_ReturnsFormattedMatchedString()
        {
            var testPath = _desktopPath + "\\test";
            var result = SpecialFolderHelper.FindMatchedSpecialFolders(testPath, "<em>{0}</em>");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
            TestContext.WriteLine(result.First().MatchedString);
            Assert.AreEqual("<em>" + _desktopPath + "</em>\\test", result.First().MatchedString);
        }

        [TestMethod]
        public void FindMatchedSpecialFolders_ReturnsRawMatchedString()
        {
            var testPath = _desktopPath + "\\test";
            var result = SpecialFolderHelper.FindMatchedSpecialFolders(testPath, "<em>{0}</em>");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
            TestContext.WriteLine(result.First().MatchedString);
            Assert.AreEqual("{DESKTOPDIRECTORY}\\test", result.First().RawReplacedString);
        }
    }
}
