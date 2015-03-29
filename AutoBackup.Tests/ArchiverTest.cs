// <copyright file="ArchiverTest.cs">Copyright ©  2015</copyright>

using System;
using AutoBackup.ArchiveTools;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoBackup.ArchiveTools
{
    [TestClass]
    [PexClass(typeof (Archiver))]
    [PexAllowedExceptionFromTypeUnderTest(typeof (ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof (InvalidOperationException))]
    public partial class ArchiverTest
    {
        [PexMethod]
        public void Archive([PexAssumeUnderTest] Archiver target)
        {
            target.Archive();
            // TODO: add assertions to method ArchiverTest.StartArchiving(Archiver, String, String)
        }
    }
}
