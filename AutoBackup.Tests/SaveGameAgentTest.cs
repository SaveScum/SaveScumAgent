using SevenZip;
// <copyright file="SaveGameAgentTest.cs">Copyright ©  2015</copyright>

using System;
using AutoBackup;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoBackup
{
    [TestClass]
    [PexClass(typeof(SaveAgent))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class SaveGameAgentTest
    {
        [PexMethod]
        public SaveAgent Constructor(
            string watchFolder,
            string archiveFolder,
            int watchEventDelay,
            OutArchiveFormat format,
            CompressionLevel level
        )
        {
            SaveAgent target = new SaveAgent(watchFolder, archiveFolder, watchEventDelay, format, level)
              ;
            return target;
            // TODO: add assertions to method SaveGameAgentTest.Constructor(String, String, Int32, OutArchiveFormat, CompressionLevel)
        }
    }
}
