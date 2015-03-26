using System.Text;
using Microsoft.Pex.Framework.Generated;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// <copyright file="GitWrapperTest.CloneRepo.g.cs">Copyright �  2015</copyright>
// <auto-generated>
// This file contains automatically generated tests.
// Do not modify this file manually.
// 
// If the contents of this file becomes outdated, you can delete it.
// For example, if it no longer compiles.
// </auto-generated>
using System;
using System.IO;

namespace SavegameAutoBackupAgent
{
    public partial class GitWrapperTest
    {
        [TestMethod]
        [PexGeneratedBy(typeof (GitWrapperTest))]
        public void CloneRepoTest()
        {
            string s;
            s = this.CloneRepo("test", );

            Assert.IsTrue(Directory.Exists(s));
        }
[TestMethod]
[PexGeneratedBy(typeof(GitWrapperTest))]
[ExpectedException(typeof(ArgumentNullException))]
public void CloneRepoThrowsArgumentNullException477()
{
    string s;
    s = this.CloneRepo((string)null, (string)null);
}
    }
}