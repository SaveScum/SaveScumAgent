using SevenZip;

namespace SaveScumAgent.Archiver
{
    public class SevenZipArchiver : ZipArchiver
    {
        public SevenZipArchiver() : base(new SevenZipCompressorWrapper())
        {
        }

        public SevenZipArchiver(ISevenZipCompressor compressor)
        {
            Compressor = compressor;
        }

        protected override string Extension => ".7z";
        protected override OutArchiveFormat ArchiveFormat => OutArchiveFormat.Zip;
    }
}