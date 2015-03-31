using System;
using System.Collections.Generic;

namespace SaveScumAgent.Archiver
{
    public class ArchivingEventArgs :
        EventArgs
    {
        public ArchivingEventArgs(string directory, string file, byte percentDone = 0,
            List<string> changedFileList = null)
        {
            ChangedFileList = changedFileList;
            CompressedDirectory = directory;
            ArchiveFile = file;
            PercentDone = percentDone;
        }

        public List<string> ChangedFileList { get; set; }
        public string CompressedDirectory { get; }
        public string ArchiveFile { get; }
        public byte PercentDone { get; }
    }

    public class ArchivingInterruptedEventArgs :
        EventArgs
    {
        public ArchivingInterruptedEventArgs(string file, bool fileDeleted, Exception e = null)
        {
            ArchiveFile = file;
            FileDeleted = fileDeleted;
        }

        public string ArchiveFile { get; }
        public bool FileDeleted { get; }
    }
}