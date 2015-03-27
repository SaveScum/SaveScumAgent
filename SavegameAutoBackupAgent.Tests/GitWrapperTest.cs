// <copyright file="GitWrapperTest.cs">Copyright ©  2015</copyright>

using System;
using LibGit2Sharp;
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
        public string CloneRepo(string localRepo, string remoteRepo, bool overwrite)
        {
            string result = GitWrapper.CloneRepo(localRepo, remoteRepo, overwrite);
            return result;
            // TODO: add assertions to method GitWrapperTest.CloneRepo(String, String)
        }
        [PexMethod]
        public string GetGitFolder(string localRepoName)
        {
            string result = GitWrapper.ProfilesFolder;
            return result;
            // TODO: add assertions to method GitWrapperTest.GetGitFolder(String)
        }
        [PexMethod(MaxBranches = 20000)]
        public string CloneProfilesRepo()
        {
            string result = GitWrapper.CloneProfilesRepo();
            return result;
            // TODO: add assertions to method GitWrapperTest.CloneProfilesRepo()
        }
    }
}
