using System;
using System.Configuration;
using System.IO;
using SevenZip;

namespace SaveScumAgent.Archiver
{
    public class SevenZipDllAssemblyLoader
    {
        private static SevenZipDllAssemblyLoader _instance;

        private SevenZipDllAssemblyLoader()
        {
        }

        public static SevenZipDllAssemblyLoader Instance => _instance ?? (_instance = new SevenZipDllAssemblyLoader());
        public LibraryFeature CurrentLibraryFeatures { get; private set; }
        public void Load() => Load(Location());

        public void Load(string path)
        {
            if (CurrentLibraryFeatures == LibraryFeature.None)
            {
                if (!File.Exists(path))
                    throw new FileNotFoundException("7z.dll/7z64.dll missing", path);

                ConfigurationManager.AppSettings["7zLocation"] = path;

                CurrentLibraryFeatures = SevenZipBase.CurrentLibraryFeatures;

                if (!CurrentLibraryFeatures.HasFlag(LibraryFeature.CompressAll | LibraryFeature.ExtractAll))
                    throw new InvalidOperationException("7z.dll not properly loaded");
            }
        }

        private static string Location()
        {
            var dll = Environment.Is64BitProcess ? "NativeBinaries\\amd64\\7z.dll" : "NativeBinaries\\x86\\7z.dll";
            dll = Path.GetFullPath(dll);
            return dll;
        }
    }
}