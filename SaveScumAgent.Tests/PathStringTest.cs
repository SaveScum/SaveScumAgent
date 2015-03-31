using System;
using System.IO;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Generated;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// <copyright file="PathStringTest.cs">Copyright ©  2015</copyright>

namespace SaveScumAgent.Tests
{
    public partial class PathStringTest
    {
        [TestMethod]
        [PexGeneratedBy(typeof (PathStringTest))]
        public void WithTrailingSlash_AppendsTailingSlashToEmptyString()
        {
            string s;
            PathString s0 = new PathString("");
            s = s0.WithTrailingSlash();
            Assert.AreEqual(Path.DirectorySeparatorChar.ToString(), s);
        }
        [TestMethod]
        public void WithTrailingSlash_AppendsTailingSlashToNonEmptyString()
        {
            PathString s0 = new PathString(@"c:\test");
            var s = s0.WithTrailingSlash();

            var expectedString = String.Format("c:\\test{0}", Path.DirectorySeparatorChar);
            Assert.AreEqual(expectedString, s);
        }
    }

    [TestClass]
    [PexClass(typeof(PathString))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class PathStringTest
    {
        [PexMethod]
        public bool IsAbsoluteUrl([PexAssumeUnderTest]PathString target)
        {
            bool result = target.IsAbsoluteUrl();
            return result;
            // TODO: add assertions to method PathStringTest.IsAbsoluteUrl(PathString)
        }
        [PexMethod]
        public string GetRelativePathFromAbsolute([PexAssumeUnderTest]PathString target, string basePath)
        {
            string result = target.GetRelativePathFromAbsolute(basePath);
            return result;
            // TODO: add assertions to method PathStringTest.GetRelativePathFromAbsolute(PathString, String)
        }
        [PexMethod]
        public bool IsFolderSubfolderOf(DirectoryInfo possibleSubDir, DirectoryInfo possibleParentDir)
        {
            bool result = PathString.IsFolderSubfolderOf(possibleSubDir, possibleParentDir);
            return result;
            // TODO: add assertions to method PathStringTest.IsFolderSubfolderOf(DirectoryInfo, DirectoryInfo)
        }
        [PexMethod]
        public bool IsFolderSubfolderOf01([PexAssumeUnderTest]PathString target, string possibleParentDir)
        {
            bool result = target.IsFolderSubfolderOf(possibleParentDir);
            return result;
            // TODO: add assertions to method PathStringTest.IsFolderSubfolderOf01(PathString, String)
        }
    }
}
