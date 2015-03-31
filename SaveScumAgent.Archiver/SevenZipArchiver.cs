using SevenZip;

namespace SaveScumAgent.Archiver
{
    internal class SevenZipArchiver : ZipArchiver
    {
        public SevenZipArchiver(
            string directory,
            string archivesDirectory,
            CompressionLevel compressionLevel = CompressionLevel.Normal) :
                base(directory, archivesDirectory, compressionLevel)
        {
        }
    }
}