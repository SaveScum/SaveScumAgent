// <copyright file="SaveFolderWatcherTest.cs">Copyright ©  2015</copyright>

using System;
using AutoBackup;
using AutoBackup.FilesystemWatcher;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoBackup
{
    [TestClass]
    [PexClass(typeof(FolderWatcher))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class SaveFolderWatcherTest
    {
        [PexMethod]
        internal FolderWatcher Constructor(
            string folderToWatch,
            int delayInSeconds
        )
        {
            FolderWatcher target = new FolderWatcher(folderToWatch, delayInSeconds);
            return target;
            // TODO: add assertions to method SaveFolderWatcherTest.Constructor(String, String, Int32)
        }
    }
}
