using SevenZip;
using AutoBackup.ArchiveTools;
// <copyright file="ArchiverFactory.cs">Copyright ©  2015</copyright>

using System;
using Microsoft.Pex.Framework;

namespace AutoBackup.ArchiveTools
{
    /// <summary>A factory for AutoBackup.ArchiveTools.Archiver instances</summary>
    public static partial class ArchiverFactory
    {
        /// <summary>A factory for AutoBackup.ArchiveTools.Archiver instances</summary>
        [PexFactoryMethod(typeof(Archiver))]
        public static Archiver Create(
            string archiveDir,
            string archiveFile,
            OutArchiveFormat format_i,
            CompressionLevel compressionLevel_i1_
        )
        {
            Archiver archiver = new Archiver(archiveFile, archiveDir, format_i, compressionLevel_i1_);
            return archiver;

            // TODO: Edit factory method of Archiver
            // This method should be able to configure the object in all possible ways.
            // Add as many parameters as needed,
            // and assign their values to each field by using the API.
        }
    }
}
