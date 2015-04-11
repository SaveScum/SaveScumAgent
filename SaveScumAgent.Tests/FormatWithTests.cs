using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SaveScumAgent.UtilityClasses;

namespace SaveScumAgent.Tests
{
    /// <summary>
    ///     Summary description for FormatWithTests
    /// </summary>
    [TestClass]
    public class FormatWithTests
    {
        private Dictionary<string, string> substitutionDictionary;

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
        //Use TestInitialize to run code before running each test
        [TestInitialize]
        public void MyTestInitialize()
        {
            substitutionDictionary = new Dictionary<string, string>
            {
                {"APPDATA", "c:\\appdata"},
                {"DESKTOP", "c:\\desktop"},
                {"PROGRAMS", "c:\\programs"}
            };
        }

        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        #endregion

        [TestMethod]
        public void FormatWith_ReplacesTagInDictionary()
        {
            var resultString = "{APPDATA}\\junk".FormatWith(substitutionDictionary);
            Assert.AreEqual("c:\\appdata\\junk", resultString);
        }

        [TestMethod]
        public void FormatWith_ReplacesTagInDictionary_ByCaseInsensitiveMatch()
        {
            var resultString = "{appdata}\\junk".FormatWith(substitutionDictionary);
            Assert.AreEqual("c:\\appdata\\junk", resultString);
        }

        [TestMethod]
        public void FormatWith_IgnoresTagsNotInDictionary()
        {
            var resultString = "{NOT_APPDATA}\\junk".FormatWith(substitutionDictionary);
            Assert.AreEqual("{NOT_APPDATA}\\junk", resultString);
        }
    }
}