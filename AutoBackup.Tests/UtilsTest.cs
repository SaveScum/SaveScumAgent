using System.IO;
// <copyright file="UtilsTest.cs">Copyright ©  2015</copyright>

using System;
using AutoBackup;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoBackup
{
    [TestClass]
    [PexClass(typeof(Utils))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class UtilsTest
    {
        [PexMethod]
        public bool IsFolderSubfolderOf(DirectoryInfo possibleSubDir, DirectoryInfo possibleParentDir)
        {
            bool result = Utils.IsFolderSubfolderOf(possibleSubDir, possibleParentDir);
            return result;
            // TODO: add assertions to method UtilsTest.IsFolderSubfolderOf(DirectoryInfo, DirectoryInfo)
        }
    }
}
