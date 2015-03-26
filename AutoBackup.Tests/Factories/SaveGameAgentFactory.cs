using SevenZip;
using AutoBackup;
// <copyright file="SaveGameAgentFactory.cs">Copyright ©  2015</copyright>

using System;
using System.IO.Compression;
using Microsoft.Pex.Framework;
using CompressionLevel = SevenZip.CompressionLevel;

namespace AutoBackup
{
    /// <summary>A factory for AutoBackup.SaveAgent instances</summary>
    public static partial class SaveGameAgentFactory
    {
        /// <summary>A factory for AutoBackup.SaveAgent instances</summary>
        [PexFactoryMethod(typeof(Utils), "AutoBackup.SaveAgent")]
        public static SaveAgent Create(
            string watchFolder_s,
            string archiveFolder_s1,
            int watchEventDelay_i,
            OutArchiveFormat format_i1_,
            CompressionLevel level_i2_
        )
        {
            SaveAgent saveAgent
               = new SaveAgent(watchFolder_s, archiveFolder_s1,
                                   watchEventDelay_i, format_i1_, level_i2_);
            return saveAgent;

            // TODO: Edit factory method of SaveAgent
            // This method should be able to configure the object in all possible ways.
            // Add as many parameters as needed,
            // and assign their values to each field by using the API.
        }
    }
}
