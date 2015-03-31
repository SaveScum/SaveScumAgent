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
            var s0 = new PathString("");
            s = s0.WithTrailingSlash();
            Assert.AreEqual(Path.DirectorySeparatorChar.ToString(), s);
        }

        [TestMethod]
        public void WithTrailingSlash_AppendsTailingSlashToNonEmptyString()
        {
            var s0 = new PathString(@"c:\test");
            var s = s0.WithTrailingSlash();

            var expectedString = String.Format("c:\\test{0}", Path.DirectorySeparatorChar);
            Assert.AreEqual(expectedString, s);
        }

        [TestMethod]
        [PexGeneratedBy(typeof (PathStringTest))]
        [ExpectedException(typeof (ArgumentException))]
        public void IsFolderSubfolderOf01ThrowsArgumentException_WhenPossibleParentIsRelative()
        {
            var s0 = new PathString("some.junk");
            IsFolderSubfolderOf01(s0, @"..\relative_path");
        }

        [TestMethod]
        [PexGeneratedBy(typeof (PathStringTest))]
        public void IsFolderSubfolderOf_ReturnsTrueOnRelativePath()
        {
            var s0 = new PathString("some.junk");
            var b = IsFolderSubfolderOf01(s0, @"c:\relative_path");
            Assert.IsTrue(b);
        }

        [TestMethod]
        [PexGeneratedBy(typeof (PathStringTest))]
        public void IsFolderSubfolderOf_ReturnsTrueWhenPossibleParentPathIsParent()
        {
            var s0 = new PathString(@"c:\parent_path\some.junk");
            var b = IsFolderSubfolderOf01(s0, @"c:\parent_path");
            Assert.IsTrue(b);
        }

        [TestMethod]
        [PexGeneratedBy(typeof (PathStringTest))]
        public void IsFolderSubfolderOf_ReturnsTrueWhenPossibleParentPathIsSlashedParent()
        {
            var s0 = new PathString(@"c:\parent_path\some.junk");
            var b = IsFolderSubfolderOf01(s0, @"c:\parent_path\");
            Assert.IsTrue(b);
        }

        [TestMethod]
        [PexGeneratedBy(typeof (PathStringTest))]
        public void IsFolderSubfolderOf_ReturnsTrueWhenPossibleParentPathIsInSubfolderOfParent()
        {
            var s0 = new PathString(@"c:\parent_path\sub_parent\some.junk");
            var b = IsFolderSubfolderOf01(s0, @"c:\parent_path");
            Assert.IsTrue(b);
        }

        [TestMethod]
        [PexGeneratedBy(typeof (PathStringTest))]
        public void IsFolderSubfolderOf_ReturnsFalseWhenPossibleParentPathIsInSubfolderOfWrongParent()
        {
            var s0 = new PathString(@"c:\parent_path\sub_parent\some.junk");
            var b = IsFolderSubfolderOf01(s0, @"c:\different_path");
            Assert.IsFalse(b);
        }

        [TestMethod]
        [PexGeneratedBy(typeof (PathStringTest))]
        public void IsFolderSubfolderOf_ReturnsFalseWhenPossibleParentPathIsNotParent()
        {
            var s0 = new PathString(@"c:\parent_path\some.junk");
            var b = IsFolderSubfolderOf01(s0, @"c:\different_path");
            Assert.IsFalse(b);
        }

        [TestMethod]
        [PexGeneratedBy(typeof (PathStringTest))]
        [ExpectedException(typeof (InvalidOperationException))]
        public void GetRelativePathFromAbsoluteThrowsInvalidOperationException940()
        {
            var s0 = new PathString(null);
            GetRelativePathFromAbsolute(s0, null);
        }

        [TestMethod]
        [PexGeneratedBy(typeof (PathStringTest))]
        [ExpectedException(typeof (ArgumentException))]
        public void GetRelativePathFromAbsolute_ThrowsArgumentExceptionForEmptyBasePath()
        {
            var s0 = new PathString(@"c:\junk\stuff.text");
            GetRelativePathFromAbsolute(s0, "");
        }

        [TestMethod]
        [PexGeneratedBy(typeof (PathStringTest))]
        public void GetRelativePathFromAbsolute_ReturnsOnlyFilenameWithParentPath()
        {
            string s;
            var s0 = new PathString(@"c:\junk\stuff.text");
            s = GetRelativePathFromAbsolute(s0, @"c:\junk\");
            Assert.AreEqual("stuff.text", s);
        }

        [TestMethod]
        [PexGeneratedBy(typeof (PathStringTest))]
        public void GetRelativePathFromAbsolute_ReturnsOnlyFilenameWithNoTailingSlashParentPath()
        {
            var s0 = new PathString(@"c:\junk\stuff.text");
            var s = GetRelativePathFromAbsolute(s0, @"c:\junk");
            Assert.AreEqual("stuff.text", s);
        }

        [TestMethod]
        [PexGeneratedBy(typeof (PathStringTest))]
        public void GetRelativePathFromAbsolute_ReturnsFilenameAndSubParentFromSubfolderOfBasePath()
        {
            var s0 = new PathString(@"c:\junk\stuff.text");
            var s = GetRelativePathFromAbsolute(s0, @"c:\");
            Assert.AreEqual(@"junk\stuff.text", s);
        }

        [TestMethod]
        [PexGeneratedBy(typeof (PathStringTest))]
        public void IsAbsoluteUrl_ReturnsFalseForEmptyString()
        {
            var s0 = new PathString("");
            Assert.IsFalse(IsAbsoluteUrl(s0));
        }

        [TestMethod]
        [PexGeneratedBy(typeof (PathStringTest))]
        public void IsAbsoluteUrl_ReturnsFalseForNullString()
        {
            var s0 = new PathString(null);
            Assert.IsFalse(IsAbsoluteUrl(s0));
        }

        [TestMethod]
        [PexGeneratedBy(typeof (PathStringTest))]
        public void IsAbsoluteUrl_ReturnsFalseForRelativePath()
        {
            var s0 = new PathString(@".\stuff.txt");
            Assert.IsFalse(IsAbsoluteUrl(s0));
        }

        [TestMethod]
        [PexGeneratedBy(typeof (PathStringTest))]
        public void IsAbsoluteUrl_ReturnsTrueForAbsolutePath()
        {
            var s0 = new PathString(@"c:\stuff.txt");
            Assert.IsTrue(IsAbsoluteUrl(s0));
        }
    }

    [TestClass]
    [PexClass(typeof (PathString))]
    [PexAllowedExceptionFromTypeUnderTest(typeof (ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof (InvalidOperationException))]
    public partial class PathStringTest
    {
        [PexMethod]
        public bool IsAbsoluteUrl([PexAssumeUnderTest] PathString target)
        {
            var result = target.IsAbsoluteUrl();
            return result;
            // TODO: add assertions to method PathStringTest.IsAbsoluteUrl(PathString)
        }

        [PexMethod]
        public string GetRelativePathFromAbsolute([PexAssumeUnderTest] PathString target, string basePath)
        {
            var result = target.GetRelativePathFromAbsolute(basePath);
            return result;
            // TODO: add assertions to method PathStringTest.GetRelativePathFromAbsolute(PathString, String)
        }

        [PexMethod]
        public bool IsFolderSubfolderOf01([PexAssumeUnderTest] PathString target, string possibleParentDir)
        {
            var result = target.IsFolderSubfolderOf(possibleParentDir);
            return result;
            // TODO: add assertions to method PathStringTest.IsFolderSubfolderOf01(PathString, String)
        }
    }
}