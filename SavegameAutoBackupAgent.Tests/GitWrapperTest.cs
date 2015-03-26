// <copyright file="GitWrapperTest.cs">Copyright ©  2015</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SavegameAutoBackupAgent;

namespace SavegameAutoBackupAgent
{
    [TestClass]
    [PexClass(typeof(GitWrapper))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class GitWrapperTest
    {
        [PexMethod]
        public string CloneRepo(string localRepo, string remoteRepo)
        {
            string result = GitWrapper.CloneRepo(localRepo, remoteRepo);
            return result;
            // TODO: add assertions to method GitWrapperTest.CloneRepo(String, String)
        }
        [PexMethod]
        public object CheckRepoStatusAgainstRemote(string localRepo)
        {
            object result = GitWrapper.CheckRepoStatus(localRepo);
            return result;
            // TODO: add assertions to method GitWrapperTest.CheckRepoStatus(String)
        }
        [PexMethod]
        public string GetGitFolder(string localRepoName)
        {
            string result = GitWrapper.GetGitFolder(localRepoName);
            return result;
            // TODO: add assertions to method GitWrapperTest.GetGitFolder(String)
        }
    }
}
